using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Acme.BookStore.Movies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Acme.BookStore.Web.Pages.Movies;

public class EditModalModel : BookStorePageModel
{
    [BindProperty]
    public EditMovieViewModel Movie { get; set; }

    public List<SelectListItem> Authors { get; set; }

    private readonly IMovieAppService _movieAppService;

    public EditModalModel(IMovieAppService movieAppService)
    {
        _movieAppService = movieAppService;
    }

    public async Task OnGetAsync(Guid id)
    {
        var movieDto = await _movieAppService.GetAsync(id);
        Movie = ObjectMapper.Map<MovieDto, EditMovieViewModel>(movieDto);

    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _movieAppService.UpdateAsync(
            Movie.Id,
            ObjectMapper.Map<EditMovieViewModel, CreateUpdateMovieDto>(Movie)
        );

        return NoContent();
    }

    public class EditMovieViewModel
    {
        [HiddenInput]
        public Guid Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [Required]
        public MovieType Type { get; set; } 

        [Required]
        public float IMDBRatings { get; set; } 
      [Required]
        public string Director { get; set; }
    }
}
