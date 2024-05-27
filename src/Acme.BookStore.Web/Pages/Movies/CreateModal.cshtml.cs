using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Acme.BookStore.Movies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Acme.BookStore.Web.Pages.Movies
{
    public class CreateModalModel : BookStorePageModel
    {
        [BindProperty]
        public CreateMovieViewModel Movie { get; set; }

        public List<SelectListItem> Actors { get; set; }

        public List<SelectListItem> Directors { get; set; }

        private readonly IMovieAppService _movieAppService;

        public CreateModalModel(
            IMovieAppService movieAppService)
        {
            _movieAppService = movieAppService;
        }

        public async Task OnGetAsync()
        {
            Movie = new CreateMovieViewModel();

            var actorLookup = await _movieAppService.GetActorLookupAsync();
            Actors = actorLookup.Items
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();
            var directorLookup = await _movieAppService.GetDirectorLookupAsync();
            Directors = directorLookup.Items
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var movie = ObjectMapper.Map<CreateMovieViewModel, CreateUpdateMovieDto>(Movie);
            if (Movie.File != null)
            {
                using (var ms = new MemoryStream())
                {
                    Movie.File.CopyTo(ms);
                    movie.ImageContent = ms.ToArray();
                }
            }

            await _movieAppService.CreateAsync(movie);
            return NoContent();
        }

        public class CreateMovieViewModel
        {
            [Required]
            [StringLength(128)]
            public string Name { get; set; }

            [HiddenInput]
            public List<Guid>? Actors { get; set; }

            [SelectItems(nameof(Directors))]
            [DisplayName("Director")]
            public Guid DirectorId { get; set; }

            [Required]
            public MovieType Type { get; set; }

            [Required]
            public float IMDBRatings { get; set; }

            [Required]
            [Display(Name = "File")]
            public IFormFile File { get; set; }
        }
    }
}
