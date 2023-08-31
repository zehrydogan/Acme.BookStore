using System;
using System.ComponentModel.DataAnnotations;

namespace Acme.BookStore.Directors;

public class CreateUpdateDirectorDto
{
    [Required]
    [StringLength(DirectorConsts.MaxNameLength)]
    public string Name { get; set; }

    [Required]
    public GenderType Gender { get; set; }

    [Required]
    public DateTime BirthDate { get; set; }

}
