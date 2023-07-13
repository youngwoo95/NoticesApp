using Microsoft.AspNetCore.Components;
using NoticeApp.Models;
using NoticeApp.Pages.Notices.Components;


namespace NoticeApp.Pages.Notices
{
    public partial class Manage
    {
        [Parameter]
        public int ParentId { get; set; } = 0;

        [Inject]
        public INoticeRepositoryAsync NoticeRepositoryAsyncReference { get; set; }

        [Inject]
        public NavigationManager NavigationManagerReference { get; set; }

        public EditorForm EditorFormReference { get; set; }
        public DeleteDialog DeleteFormReference { get; set; }

        protected List<Notice> models;

        protected Notice model = new Notice();

        /// <summary>
        /// 공지사항으로 올리기 폼을 띄울건지 여부
        /// </summary>
        public bool IsInlineDialogShow { get; set; } = false;

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
            if (this.searchQuery != "")
            {
                await DisplayData();
            }
            else
            {
                await SearchData();
            }
        }

        private async Task DisplayData()
        {
            if(ParentId == 0)
            {
                var resultsSet = await NoticeRepositoryAsyncReference.GetAllAsync(pager.PageIndex, pager.PageSize);
                pager.RecordCount = resultsSet.TotalRecords;
                models = resultsSet.Records.ToList();
            }
            else
            {
                var resultsSet = await NoticeRepositoryAsyncReference.GetAllByParentIdAsync(pager.PageIndex, pager.PageSize, ParentId);
                pager.RecordCount = resultsSet.TotalRecords;
                models = resultsSet.Records.ToList();
            }
            StateHasChanged();
        }

        private async Task SearchData()
        {
            if(ParentId == 0)
            {
                var resultsSet = await NoticeRepositoryAsyncReference.SearchAllAsync(pager.PageIndex, pager.PageSize, this.searchQuery);
                pager.RecordCount = resultsSet.TotalRecords;
                models = resultsSet.Records.ToList();
            }
            else
            {
                var resultsSet = await NoticeRepositoryAsyncReference.SearchAllByParentIdAsync(pager.PageIndex, pager.PageSize, this.searchQuery, ParentId);
                pager.RecordCount = resultsSet.TotalRecords;
                models = resultsSet.Records.ToList();
            }
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
            
            if (this.searchQuery == "")
            {
                await DisplayData();
            }
            else
            {
                await SearchData();
            }

            StateHasChanged();
        }

        private string searchQuery;
        protected async void Search(string query)
        {
            this.searchQuery = query;
            await SearchData();
            StateHasChanged();
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
        
        protected void ToggleBy(Notice model)
        {
            this.model = model;
            IsInlineDialogShow = true;
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

        protected void ToggleClose()
        {
            IsInlineDialogShow = false;
            this.model = new Notice();
        }

        protected async void ToggleClick()
        {
            this.model.IsPinned = (this.model.IsPinned == true) ? false : true;

            await NoticeRepositoryAsyncReference.EditAsync(this.model);
            IsInlineDialogShow = false;

            this.model = new Notice();
            await DisplayData();
        }


    }
}
