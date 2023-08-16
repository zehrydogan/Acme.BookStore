/*using System.Threading.Tasks;
using Acme.BookStore.Movies;
using Microsoft.AspNetCore.Mvc;

namespace Acme.BookStore.Web.Pages.Movies
{
    public class CreateModalModel : BookStorePageModel
    {
        [BindProperty]
        public CreateUpdateMovieDto Movie { get; set; }

        private readonly IMovieAppService _movieAppService;

        public CreateModalModel(IMovieAppService movieAppService)
        {
            _movieAppService = movieAppService;
        }

        public void OnGet()
        {
            Movie = new CreateUpdateMovieDto();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _movieAppService.CreateAsync(Movie);
            return NoContent();
        }
    }
}*/

////using System;
////using System.Collections.Generic;
////using System.ComponentModel;
////using System.ComponentModel.DataAnnotations;
////using System.Linq;
////using System.Threading.Tasks;
////using Acme.BookStore.Movies;
////using AutoMapper.Internal.Mappers;
////using Microsoft.AspNetCore.Mvc;
////using Microsoft.AspNetCore.Mvc.Rendering;
////using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

////namespace Acme.BookStore.Web.Pages.Movies;

////public class CreateModalModel : BookStorePageModel
////{
////    [BindProperty]
////    public CreateMovieViewModel Movie { get; set; }

////    public List<SelectListItem> Authors { get; set; }

////    private readonly IMovieAppService _movieAppService;

////    public CreateModalModel(
////        IMovieAppService movieAppService)
////    {
////        _movieAppService = movieAppService;
////    }

//public async Task OnGetAsync()
//{
//    Movie = new CreateMovieViewModel();

//    var authorLookup = await _movieAppService.GetAuthorLookupAsync();
//    Authors = authorLookup.Items
//        .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
//        .ToList();
//}

////    public async Task<IActionResult> OnPostAsync()
////    {
////        await _movieAppService.CreateAsync(
////            ObjectMapper.Map<CreateMovieViewModel, CreateUpdateMovieDto>(Movie)
////            );
////        return NoContent();
////    }

////    public class CreateMovieViewModel
////    {
////        [SelectItems(nameof(Authors))]
////        [DisplayName("Author")]
////        public Guid AuthorId { get; set; }

////        [Required]
////        [StringLength(128)]
////        public string Name { get; set; }

////        [Required]
////        public MovieType Type { get; set; } 


////        public DateTime PublishDate { get; set; } = DateTime.Now;

////        [Required]
////        public float Price { get; set; }
////    }
////}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Acme.BookStore.Movies;
using AutoMapper.Internal.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Acme.BookStore.Web.Pages.Movies;

public class CreateModalModel : BookStorePageModel
{
    [BindProperty]
    public CreateMovieViewModel Movie { get; set; }

    public List<SelectListItem> Authors { get; set; }

    private readonly IMovieAppService _movieAppService;

    public CreateModalModel(
        IMovieAppService movieAppService)
    {
        _movieAppService = movieAppService;
    }

    public async Task OnGetAsync()
    {
        Movie = new CreateMovieViewModel();

        var authorLookup = await _movieAppService.GetAuthorLookupAsync();
        Authors = authorLookup.Items
            .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
            .ToList();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _movieAppService.CreateAsync(
            ObjectMapper.Map<CreateMovieViewModel, CreateUpdateMovieDto>(Movie)
            );
        return NoContent();
    }

    public class CreateMovieViewModel
    {
        [SelectItems(nameof(Authors))]
        [DisplayName("Author")]
        public Guid AuthorId { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [Required]
        public MovieType Type { get; set; } 

        [Required]
        public float IMDBRatings { get; set; }

        [Required]
        public float Price { get; set; }
    }
}
