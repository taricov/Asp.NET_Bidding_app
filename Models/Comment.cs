using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace BidingApp;

public class Comment
{
    public int Id { get; set; }
    public string Content { get; set;} = "";
    [Required]
    public string? IdentityUserId { get; set; }
    [ForeignKey("IdentityUserId")]
    public IdentityUser? User {get; set;}
[ForeignKey("ListingId")]
    public Listing? Listing {get; set;}
    public string? ListingId { get; set; }

}
