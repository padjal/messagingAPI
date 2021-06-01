using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Peer_Review_11.Models
{
    public class User
    {
        [Required] public string UserName { get; set; }

        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [Required] public string Email {get;set;}
    }
}