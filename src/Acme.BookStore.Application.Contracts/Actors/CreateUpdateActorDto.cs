using System;
using System.ComponentModel.DataAnnotations;

namespace Acme.BookStore.Actors;

public class CreateUpdateActorDto
{
    [Required]
    [StringLength(ActorConsts.MaxNameLength)]
    public string Name { get; set; }

    [Required]
    public DateTime BirthDate { get; set; }

    public string ShortBio { get; set; }
}
