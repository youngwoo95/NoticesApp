using Dul.Domain.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;

namespace NoticeApp.Models
{
    /// <summary>
    /// [6] Repository Class
    /// </summary>
    public class NoticeRepositoryAsync : INoticeRepositoryAsync
    {
        public Task<Notice> AddAsync(Notice model)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> EditAsync(Notice model)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Notice>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<PagingResult<Notice>> GetAllAsync(int pageIndex, int pageSize)
        {
            throw new System.NotImplementedException();
        }

        public Task<PagingResult<Notice>> GetAllByParentIdAsync(int pageIndex, int pageSize, int parentId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Notice> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
