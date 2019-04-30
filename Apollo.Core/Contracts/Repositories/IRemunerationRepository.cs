// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 12/21/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Contracts.Domain.Payroll;
using Apollo.Core.Messages.Requests;
using Apollo.Core.Messages.Responses;

namespace Apollo.Core.Contracts.Repositories
{
    public interface IRemunerationRepository
    {
        Task<DeleteResponse> DeleteAsync(int id);
        Task<GetResponse<IRemunerationPayroll>> GetAsync(RemunerationRequest request);
        Task<GetResponse<IReadOnlyList<IRemunerationPayroll>>> GetAllAsync(RemunerationRequest request);
        Task<GetResponse<IRemunerationPayroll>> GetByIdAsync(int id);
        Task<SaveResponse<IRemunerationPayroll>> SaveAsync(IRemunerationPayroll remuneration);
        Task<SaveResponse<IReadOnlyList<IRemunerationPayroll>>> SaveAllAsync(IReadOnlyList<IRemunerationPayroll> remuneration);
        
    }
}
