using System.ComponentModel.DataAnnotations;

namespace Peer_Review_11.Models
{
    public class Message
    {
        [Required] public string Subject { get; set; }
        [Required] public string Body { get; set; }
        [EmailAddress]
        [Required] public string SenderId { get; set; }
        [EmailAddress]
        [Required] public string ReceiverId { get; set; }
    }
}