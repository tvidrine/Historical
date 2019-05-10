// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 3/19/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Apollo.Core.Contracts;
using Apollo.Core.Messages.Responses;
using Apollo.Infrastructure.Factories;
using Apollo.Infrastructure.Models;

namespace Apollo.Infrastructure.Repositories
{
    public class BaseRepository<TDto, TModel, TInfo> : AbstractBaseRepository where TDto : DtoBase<TModel, TInfo>
    {
        protected readonly DtoBase<TModel, TInfo> Dto;


        public BaseRepository(IConnectionFactory connectionFactory, ILogManager loggerManager, TDto dto) :
            base(connectionFactory, loggerManager)
        {
            Dto = dto;
        }

        public virtual async Task<DeleteResponse> DeleteAsync(int id)
        {
            var response = new DeleteResponse();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var sql = GetDeleteStatement();
                    var trans = connection.BeginTransaction();
                    var results = await connection.ExecuteAsync(sql, new { id }, trans);

                    trans.Commit();
                    response.Message = $@"{results} {typeof(TModel).Name} records were deleted.";
                }
            }
            catch (Exception e)
            {
                var message = $@"Error deleting {typeof(TModel).Name}";
                LogManager.LogError(e, message);
                response.AddError(e);
                Console.WriteLine(e);
            }

            return response;
        }

        public virtual async Task<GetResponse<IReadOnlyList<TInfo>>> GetInfoListAsync()
        {
            var response = new GetResponse<IReadOnlyList<TInfo>>();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var sql = GetSelectInfoStatement();

                    var results = await connection.QueryAsync<TDto>(sql);

                    response.Content = results
                        .Select(dto => dto.ToInfo())
                        .AsList();
                }
            }
            catch (Exception e)
            {
                var messaage = $@"Unable to retrieve {typeof(TModel).Name} records.";
                response.AddError(e);
                LogManager.LogError(e, messaage);
                Console.WriteLine(e);
            }

            return response;
        }

        public virtual async Task<GetResponse<IReadOnlyList<TModel>>> GetAllAsync()
        {
            var response = new GetResponse<IReadOnlyList<TModel>>();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var sql = GetSummarySelectStatement();

                    var results = await connection.QueryAsync<TDto>(sql);

                    response.Content = results
                        .Select(dto => dto.ToModel())
                        .AsList();
                }
            }
            catch (Exception e)
            {
                var messaage = $@"Unable to retrieve {typeof(TModel).Name} records.";
                response.AddError(e);
                LogManager.LogError(e, messaage);
                Console.WriteLine(e);
            }

            return response;
        }

        public virtual async Task<GetResponse<IReadOnlyList<TModel>>> GetAllAsync(int parentId)
        {
            var response = new GetResponse<IReadOnlyList<TModel>>();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var sql = GetSelectByParentIdStatement();

                    var results = await connection.QueryAsync<TDto>(sql, new {ParentId = parentId});

                    response.Content = results
                        .Select(dto => dto.ToModel())
                        .AsList();
                }
            }
            catch (Exception e)
            {
                var messaage = $@"Unable to retrieve {typeof(TModel).Name} records.";
                response.AddError(e);
                LogManager.LogError(e, messaage);
                Console.WriteLine(e);
            }

            return response;
        }

        public virtual async Task<GetResponse<TModel>> GetByIdAsync(int id)
        {
            var response = new GetResponse<TModel>();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var sql = $@"{GetSelectStatement()}
								 AND Id = @id;";

                    var result = await connection.QuerySingleAsync<TDto>(sql, new { Id = id });

                    response.Content = result
                        .ToModel();
                }
            }
            catch (Exception e)
            {
                var message = $@"Unable to retrieve {typeof(TModel).Name} records.";
                response.AddError(e);
                LogManager.LogError(e, message);
                Console.WriteLine(e);
            }

            return response;
        }

        public virtual async Task<SaveResponse<IReadOnlyList<TModel>>> SaveAllAsync(IReadOnlyList<TModel> items)
        {
            var response = new SaveResponse<IReadOnlyList<TModel>>();

            try
            {
                var savedItems = new List<TModel>();
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var sql = GetMergeStatement();
                    var trans = connection.BeginTransaction();

                    foreach (var item in items)
                    {
                        var singleResponse = await SaveAsync(item);
                        if(singleResponse.IsSuccessful)
                            savedItems.Add(singleResponse.Content);
                        else
                        {
                            response.AddErrors(singleResponse.Errors);
                        }
                    }
                    
                    trans.Commit();
                    response.Content = savedItems;
                    response.Message = $@"{savedItems.Count} {typeof(TModel).Name} records were inserted/updated.";
                }
            }
            catch (Exception e)
            {
                var message = $@"Error saving {typeof(TModel).Name} information.";
                LogManager.LogError(e, message);
                response.AddError(e);
                Console.WriteLine(e);
            }

            return response;
        }

        public virtual async Task<SaveResponse<TModel>> SaveAsync(TModel item)
        {
            var response = new SaveResponse<TModel>();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var sql = GetMergeStatement();
                    var results = await connection.QuerySingleAsync<TDto>(sql, Dto.FromModel(item));

                    response.Content = results.ToModel();
                    response.Message = $@"{results} {typeof(TModel).Name} record was inserted/updated.";
                }
            }
            catch (Exception e)
            {
                var message = $@"Error saving {typeof(TModel).Name} information.";
                LogManager.LogError(e, message);
                response.AddError(e);
                Console.WriteLine(e);
            }

            return response;
        }

        protected virtual string GetDeleteStatement()
        {
            throw new NotImplementedException("GetDeleteStatement is not implemented.");
        }

        protected virtual string GetMergeStatement()
        {
            throw new NotImplementedException("GetMergeStatement is not implemented.");
        }

        protected virtual string GetSelectByParentIdStatement()
        {
            throw new NotImplementedException("GetSelectSqlStatement is not implemented.");
        }

        protected virtual string GetSelectInfoStatement()
        {
            throw new NotImplementedException();
        }
        protected virtual string GetSelectStatement()
        {
            throw new NotImplementedException("GetSelectSqlStatement is not implemented.");
        }

        protected virtual string GetSummarySelectStatement()
        {
            throw new NotImplementedException("GetSummarySelectStatement is not implemented.");
        }
    }
}