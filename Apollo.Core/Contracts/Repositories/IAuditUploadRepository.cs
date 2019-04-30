// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 11/26/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Contracts.Domain.Audit;
using Apollo.Core.Messages.Responses;

namespace Apollo.Core.Contracts.Repositories
{
    public interface IAuditUploadRepository
    {
        Task<DeleteResponse> DeleteAsync(int id);
        Task<GetResponse<IReadOnlyList<IAuditUpload>>> GetAllAsync();
        Task<GetResponse<IAuditUpload>> GetByIdAsync(int id);
        Task<SaveResponse<IAuditUpload>> SaveAsync(IAuditUpload auditUpload);
        Task<SaveResponse<IReadOnlyList<IAuditUpload>>> SaveAllAsync(IReadOnlyList<IAuditUpload> auditUpload);
    }
}
