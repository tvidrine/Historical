using System;
using System.Threading.Tasks;
using Dapper;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Domain.Policies;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Domain.Policies;
using Apollo.Core.Messages.Responses;
using Apollo.Infrastructure.Factories;
using Apollo.Infrastructure.Models;

namespace Apollo.Infrastructure.Repositories
{
    public class PolicyRepository : BaseRepository<PolicyDto, IPolicy, IPolicy>, IPolicyRepository
    {
        public PolicyRepository(IConnectionFactory connectionFactory, ILogManager loggerManager, PolicyDto dto) 
            : base(connectionFactory, loggerManager, dto)
        {
        }

        public async Task<GetResponse<bool>> IsValidPolicyAgentAsync(int auditNumber, string policyNumber)
        {
            var response = new GetResponse<bool>();

            try
            {
                var sql = GetIsValidAgentStatement();

                var parms = new {AuditNumber = auditNumber, PolicyNumber = policyNumber};

                using (var connection = ConnectionFactory.GetConnection())
                {
                    var result = await connection.ExecuteScalarAsync<int>(sql, parms);

                    response.Content = result > 0;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                response.AddError(e);
            }

            return response;
        }

        public async Task<SaveResponse> SaveAgentAsync(PolicyAgent agent)
        {
            var response = new SaveResponse();

            try
            {
                var sql = GetSaveAgentStatement();

                using (var connection = ConnectionFactory.GetConnection())
                {
                    var x = await connection.ExecuteAsync(sql, agent);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                response.AddError(e);
                LogManager.LogError(e, $@"Error saving agent: {agent.FirstName} {agent.LastName}.");
            }

            return response;
        }

        #region Sql Statements
        private string GetIsValidAgentStatement()
        {
            return $@"
                SELECT COUNT(*)
                FROM Policy.Policy p INNER JOIN Policy.PolicyNumber pn ON P.Id = pn.PolicyId AND pn.IsDeleted = 0
	                INNER JOIN Policy.Audit a ON p.Id = a.PolicyId AND a.IsDeleted = 0
                WHERE pn.PolicyNumber = @policyNumber 
	                AND a.Number = @auditNumber
	                AND p.IsDeleted = 0;";
        }

        private string GetSaveAgentStatement()
        {
            return @"
                MERGE [Policy].[PolicyAgent] AS T
                USING (SELECT
                              V.[Id]
                            , V.[UserId]
                            , P.[PolicyId]
                            , V.[CreatedOn]
                            , V.[CreatedById]
                            , V.[LastModifiedOn]
                            , V.[LastModifiedById]
		                FROM (VALUES ( @id, @userId, @policyNumber, SYSDATETIMEOFFSET(), @createdById, SYSDATETIMEOFFSET(), @lastModifiedById)) AS 
                        V([Id], [UserId], [PolicyNumber], [CreatedOn], [CreatedById], [LastModifiedOn], [LastModifiedById]) 
			                INNER JOIN [Policy].[PolicyNumber] P ON V.[PolicyNumber] = P.[PolicyNumber] AND P.IsDeleted = 0) AS S
                ON T.[UserId] = S.[UserId] AND T.[PolicyId] = S.[PolicyId]
                WHEN NOT MATCHED THEN
                    INSERT
                        ([Id]
                        ,[UserId]
                        ,[PolicyId]
                        ,[CreatedOn]
                        ,[CreatedById]
                        ,[LastModifiedOn]
                        ,[LastModifiedById])
                    VALUES
                        ( S.[Id]
                        , S.[UserId]
                        , S.[PolicyId]
                        , S.[CreatedOn]
                        , S.[CreatedById]
                        , S.[LastModifiedOn]
                        , S.[LastModifiedById])
                OUTPUT inserted.*;";
        }
        #endregion Sql Statements
    }
}