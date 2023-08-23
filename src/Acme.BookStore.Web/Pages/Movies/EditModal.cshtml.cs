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
public class EditModalModel : BookStorePageModel
{
    [BindProperty]
    public EditMovieViewModel Movie { get; set; }

    public List<SelectListItem> Actors { get; set; }

    private readonly IMovieAppService _movieAppService;

    public EditModalModel(IMovieAppService movieAppService)
    {
        _movieAppService = movieAppService;
    }

    public async Task OnGetAsync(Guid id)
    {
        var movieDto = await _movieAppService.GetAsync(id);
        Movie = ObjectMapper.Map<MovieDto, EditMovieViewModel>(movieDto);

        var actorLookup = await _movieAppService.GetActorLookupAsync();
        Actors = actorLookup.Items
            .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
            .ToList();
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

        //[SelectItems(nameof(Actors))]
        //[DisplayName("Actor")]
        //public Guid ActorId { get; set; }



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


