using Microsoft.AspNetCore.Components;
using NoticeApp.Models;
using NoticeApp.Pages.Notices.Components;

namespace NoticeApp.Pages.Notices
{
    public partial class Manage
    {
        [Inject]
        public INoticeRepositoryAsync NoticeRepositoryAsyncReference { get; set; }

        [Inject]
        public NavigationManager NavigationManagerReference { get; set; }

        public EditorForm EditorFormReference { get; set; }
        public DeleteDialog DeleteFormReference { get; set; }

        protected List<Notice> models;

        protected Notice model = new Notice();

        /// <summary>
        /// 페이징 처리
        /// </summary>
        protected DulPager.DulPagerBase pager = new DulPager.DulPagerBase()
        {
            PageNumber = 1,
            PageIndex = 0,
            PageSize = 2,
            PagerButtonCount = 5
        };

        protected override async Task OnInitializedAsync()
        {
            // MatBlazor - Prgressbar
            //await Task.Delay(3000);
            await DisplayData();
        }

        private async Task DisplayData()
        {
            var resultsSet = await NoticeRepositoryAsyncReference.GetAllAsync(pager.PageIndex, pager.PageSize);
            pager.RecordCount = resultsSet.TotalRecords;
            models = resultsSet.Records.ToList();

            StateHasChanged();
        }

        /// <summary>
        /// 해당 ID를 가지고 페이지 이동
        /// </summary>
        /// <param name="id"></param>
        protected void NameClick(int id)
        {
            NavigationManagerReference.NavigateTo($"/Notices/Details/{id}");
        }

        protected async void PageIndexChanged(int pageIndex)
        {
            pager.PageIndex = pageIndex;
            pager.PageNumber = pageIndex + 1;

            await DisplayData();
        }

        public string EditorFormTitle { get; set; } = "CREATE";
        protected void ShowEditorForm()
        {
            EditorFormTitle = "CREATE";
            
            this.model = new Notice();
            EditorFormReference.Show();
        }

        protected void EditBy(Notice model)
        {
            EditorFormTitle = "EDIT";

            this.model = model;
            EditorFormReference.Show();
        }

        protected void DeleteBy(Notice model)
        {
            this.model = model;
            
            // DeleteForm 모달창 띄우기
            DeleteFormReference.Show();
        }

        protected async void CreateOrEdit()
        {
            EditorFormReference.Hide();
            
            await DisplayData();
        }

        // 삭제 기능
        protected async void DeleteClick()
        {
            // 삭제 기능 실행
            await NoticeRepositoryAsyncReference.DeleteAsync(this.model.Id);

            // 폼닫기
            DeleteFormReference.Hide();

            // 모델 초기화
            this.model = new Notice();
            // 새로고침 - 데이터 다시 로드
            await DisplayData();
        }


    }
}
