using System;
using System.Threading.Tasks;

namespace NoticeApp.Models
{
    /// <summary>
    /// [4] Repository Interface
    /// </summary>
    public interface INoticeRepositoryAsync : ICrudNoticeRepositoryAsync<Notice>
    {
        Task<Tuple<int, int>> GetStatus(int parentId);
    }
}
