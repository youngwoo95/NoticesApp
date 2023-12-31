﻿using Microsoft.AspNetCore.Components;
using NoticeApp.Models;

namespace NoticeApp.Pages.Notices
{
    public partial class Index
    {
        [Inject]
        public INoticeRepositoryAsync NoticeRepositoryAsyncReference { get; set; }

        [Inject]
        public NavigationManager NavigationManagerReference { get; set; }

        protected List<Notice> models;

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
            var resultsSet = await NoticeRepositoryAsyncReference.GetAllAsync(pager.PageIndex, pager.PageSize);
            pager.RecordCount = resultsSet.TotalRecords;
            models = resultsSet.Records.ToList();
        }

        private async Task SearchData()
        {
            var resultsSet = await NoticeRepositoryAsyncReference.SearchAllAsync(pager.PageIndex, pager.PageSize, this.searchQuery);
            pager.RecordCount = resultsSet.TotalRecords;
            models = resultsSet.Records.ToList();
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

            // Refresh
            StateHasChanged();
        }

        private string searchQuery;
        protected async void Search(string query)
        {
            this.searchQuery = query;
            await SearchData();
            StateHasChanged();
        }

    }
}
