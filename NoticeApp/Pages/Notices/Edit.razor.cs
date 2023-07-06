using Microsoft.AspNetCore.Components;
using NoticeApp.Models;

namespace NoticeApp.Pages.Notices
{
    public partial class Edit
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        public INoticeRepositoryAsync NoticeRepositoryAsyncReference { get; set; }

        [Inject]
        public NavigationManager NavigationManagerReference { get; set; }

        protected Notice model = new Notice();

        public string ParentId { get; set; }

        protected int[] parentIds = { 1, 2, 3 };

        protected string content = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            model = await NoticeRepositoryAsyncReference.GetByIdAsync(Id);

            content = Dul.HtmlUtility.EncodeWithTabAndSpace(model.Content);
            ParentId = model.ParentId.ToString();
        }

        protected async void FormSubmit()
        {
            int.TryParse(ParentId, out var parentId);
            model.ParentId = parentId;

            // 추가
            await NoticeRepositoryAsyncReference.EditAsync(model);

            // 페이지 이동
            NavigationManagerReference.NavigateTo("/Notices");
        }

    }
}
