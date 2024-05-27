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
            if (Movie.File != null)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", Movie.File.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await Movie.File.CopyToAsync(stream);
                }
            }

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
