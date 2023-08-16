using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Acme.BookStore.Movies;
using Microsoft.AspNetCore.Mvc;

namespace Acme.BookStore.Web.Pages.Movies;

public class CreateModalModel : BookStorePageModel
{
    [BindProperty]
    public CreateMovieViewModel Movie { get; set; }


    private readonly IMovieAppService _movieAppService;

    public CreateModalModel(
        IMovieAppService movieAppService)
    {
        _movieAppService = movieAppService;
    }

    public async Task OnGetAsync()
    {
        Movie = new CreateMovieViewModel();
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
