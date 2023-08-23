using Acme.BookStore.Movies;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Acme.BookStore.ViewModels
{
    public class MovieCreateOrUpdateViewModel
    {
        public CreateUpdateMovieDto Movie { get; set; }
        public List<SelectListItem> ActorLookupItems { get; set; }
    }
}
