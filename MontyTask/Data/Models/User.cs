﻿using System.ComponentModel.DataAnnotations;

namespace MontyTask.Data.Models;
public class User
{
    public int Id { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(255)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

}