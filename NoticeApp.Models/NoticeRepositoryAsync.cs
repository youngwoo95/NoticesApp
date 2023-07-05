using Dul.Domain.Common;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;

namespace NoticeApp.Models
{
    /// <summary>
    /// [6] Repository Class : Dapper ADO.NET
    /// </summary>
    public class NoticeRepositoryAsync : INoticeRepositoryAsync
    {
        private readonly NoticeAppDbContext _context;
        private readonly ILogger _logger;

        public NoticeRepositoryAsync(NoticeAppDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            this._logger = loggerFactory.CreateLogger(nameof(NoticeRepositoryAsync));
        }

        // 입력
        public async Task<Notice> AddAsync(Notice model)
        {
            _context.Notices.Add(model);

            try 
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError($"에러 발생({nameof(AddAsync)}): {ex.Message}");
            }
            
            return model;
        }

        // 출력
        public Task<List<Notice>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        // 상세
        public Task<Notice> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        // 수정
        public Task<bool> EditAsync(Notice model)
        {
            throw new System.NotImplementedException();
        }

        // 삭제
        public Task<bool> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        // 페이징
        public Task<PagingResult<Notice>> GetAllAsync(int pageIndex, int pageSize)
        {
            throw new System.NotImplementedException();
        }

        // 부모
        public Task<PagingResult<Notice>> GetAllByParentIdAsync(int pageIndex, int pageSize, int parentId)
        {
            throw new System.NotImplementedException();
        }

      
    }
}
