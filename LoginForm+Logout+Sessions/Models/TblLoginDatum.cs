//using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace LoginForm_Logout_Sessions.Models;

public partial class TblLoginDatum
{
    public int Id { get; set; }


  
    public string? UserName { get; set; } 

    [Required]
    [EmailAddress]
    public string? Email { get; set; } 
    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; } 
}
