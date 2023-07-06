using Microsoft.AspNetCore.Components;
using NoticeApp.Models;

namespace NoticeApp.Pages.Notices
{
    public partial class Create
    {
        [Inject]
        public INoticeRepositoryAsync NoticeRepositoryAsyncReference { get; set; }

        [Inject]
        public NavigationManager NavigationManagerReference { get; set; }

        protected Notice model = new Notice();
        public string ParentId { get; set; }

        protected int[] parentIds = { 1, 2, 3 };

        protected async void FormSubmit()
        {
            int.TryParse(ParentId, out var parentId);
            model.ParentId = parentId;

            // 추가
            await NoticeRepositoryAsyncReference.AddAsync(model);

            // 페이지 이동
            NavigationManagerReference.NavigateTo("/Notices");
        }

    }
}
