//Author.rzor 숨김 파일

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Authors;
using BookStore.Permissions;
using Blazorise;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Castle.Components.DictionaryAdapter.Xml;
using System.Xml.Serialization;

namespace BookStore.Blazor.Pages
{
    public partial class Authors
    {
        IReadOnlyList<AuthorDto> AuthorList { get; set; }

        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; }
        private string CurrentSorting { get; set; }
        private int TotalCount { get; set; }

        private bool CanCreateAuthor { get; set; }
        private bool CanEditAuthor { get; set; }
        private bool CanDeleteAuthor { get; set; }
        
        private CreateAuthorDto NewAuthor { get; set; }

        private Guid EditingAuthorId { get; set; }
        private UpdateAuthorDto EditingAuthor { get; set; }

        private Modal CreateAuthorModal { get; set; }
        private Modal EditAuthorModal { get; set; }

        private Validations CreateValidationsRef;

        private Validations EditValidationsRef;

        public Authors()
        {
            NewAuthor = new CreateAuthorDto();
            EditingAuthor=new UpdateAuthorDto();
        }

        protected override async Task OnInitializedAsync()
        {
            await SetPermissionsAsync();
            await GetAuthorAsync();
        }

        private async Task SetPermissionsAsync()
        {
            CanCreateAuthor = await AuthorizationService.IsGrantedAsync(BookStorePermissions.Authors.Create);
            CanEditAuthor = await AuthorizationService.IsGrantedAsync(BookStorePermissions.Authors.Edit);
            CanDeleteAuthor = await AuthorizationService.IsGrantedAsync(BookStorePermissions.Authors.Delete);
        }

        private async Task GetAuthorAsync()
        {
            var result = await AuthorAppService.GetListAsync(
                                                                new GetAuthorListDto
                                                                {
                                                                    MaxResultCount = PageSize,
                                                                    SkipCount = CurrentPage * PageSize,
                                                                    Sorting = CurrentSorting
                                                                });

            AuthorList = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        //데이터 읽기
        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<AuthorDto> e)
        {
            //CurrentSorting = e.Columns
            //   .Where(c => c.SortDirection != SortDirection.Default)
            //   .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
            //   .JoinAsString(",");

            //await GetAuthorAsync();

            //await InvokeAsync(StateHasChanged);

            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.Default)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page - 1;

            await GetAuthorAsync();

            StateHasChanged();

        }


        //페이지 상단 row의 NewAuthor 버튼 클릭 시 생성 모달창
        private void OpenCreateAuthorModal()
        {
            CreateValidationsRef.ClearAll();
            NewAuthor = new CreateAuthorDto();
            CreateAuthorModal.Show();
        }

        //생성 모달창 상단 닫기 버튼
        private void CloseCreateAuthorModal()
        {
            CreateAuthorModal.Hide();
        }


        //grid 수정버튼 클릭 시 show edit modal
        private void OpenEditAuthorModal(AuthorDto author)
        {
            EditValidationsRef.ClearAll();
            EditingAuthorId = author.Id;
            EditingAuthor=ObjectMapper.Map<AuthorDto, UpdateAuthorDto>(author);
            EditAuthorModal.Show();
        }

        //edit modal 상단 닫기 버튼
        private void CloseEditAuthorModal()
        {
            EditAuthorModal.Hide();
        }

        //grid 삭제 버튼
        private async Task DeleteAuthorAsync(AuthorDto author)
        {
            var confirmMessage = L["AuthorDeletionConfirmationMessage", author.Name];

            if (!await Message.Confirm(confirmMessage))
            {
                return;
            }

            await AuthorAppService.DeleteAsync(author.Id);
            await GetAuthorAsync();
        }

        //생성 modal 창 하단 생성 버튼 클릭 이벤트
        private async Task CreateAuthorAsync()
        {
            if (await CreateValidationsRef.ValidateAll())
            {
                await AuthorAppService.CreateAsync(NewAuthor);
                await GetAuthorAsync();
                CreateAuthorModal.Hide();
            }
        }

        //modal 하단 edit button click event
        private async Task UpdateAuthorAsync()
        {
            if (await EditValidationsRef.ValidateAll())
            {
                await AuthorAppService.UpdateAsync(EditingAuthorId, EditingAuthor);
                await GetAuthorAsync();
                EditAuthorModal.Hide();
            }
        }



    }
}