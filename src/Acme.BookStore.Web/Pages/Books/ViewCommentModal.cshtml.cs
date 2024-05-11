using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Acme.BookStore.BookComments;
using Acme.BookStore.Books;
using AutoMapper.Internal.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Volo.Abp.AspNetCore.Mvc.UI.Theming;

namespace Acme.BookStore.Web.Pages.Books;

public class ViewCommentModalModel : BookStorePageModel
{
    [BindProperty]
    public BookCommentViewModel Book { get; set; }

    public List<SelectListItem> Authors { get; set; }

    private readonly IBookCommentAppService _bookCommentAppService
        ;

    public ViewCommentModalModel(IBookCommentAppService bookCommentAppService)
    {
        _bookCommentAppService = bookCommentAppService;
    }

    public async Task OnGetAsync(Guid bookId)
    {
        var allComments = await _bookCommentAppService.GetListAsync(new PagedAndSortedResultRequestDto());
        var comments= allComments.Items.Where(b => b.BookId == bookId).ToList();

    }
    public async Task<IActionResult> OnPostAsync()
    {
       
        return NoContent();
    }

    public class CommentViewModel
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string Comment { get; set; }  
        
        public DateTime Date { get; set; }

    }
}
