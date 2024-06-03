using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace semissssloan.ViewModels
{

public partial class ClientInfoViewModel
{
    public int Id { get; set; }
    [Required]
    public int UserType { get; set; }
    public string UserTypeName { get; set; } = "";
    [Required]
    public string FirstName { get; set; } = null!;
    [Required]
    public string MiddleName { get; set; } = null!;
    [Required]
    public string LastName { get; set; } = null!;
    [Required]
    public string Address { get; set; } = null!;
    [Required]
    public int ZipCode { get; set; }
    [Required]
    public DateTime Birthdate { get; set; }
    [Required]
    public int Age { get; set; }
    [Required]
    public string NameofFather { get; set; } = null!;
    [Required]
    public string NameofMother { get; set; } = null!;
    [Required]
    public string CivilStatus { get; set; } = null!;
    [Required]
    public string Religion { get; set; } = null!;
    [Required]
    public string Occupation { get; set; } = null!;
}
}