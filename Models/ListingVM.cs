using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace BidingApp;

public class ListingVM
{
    public int Id {get; set;}
    public string Title {get; set;} = "";
    public string Description {get; set;} = "";
    public decimal Price {get; set;}
    public IFormFile? Image {get; set;}
    public bool IsSold {get; set;} = false; 
    [Required]
    public string? IdentityUserId {get; set;}
    // public List<Bid>? Bids {get; set;}
    // public List<Comment>? Comments {get; set;} 
    [ForeignKey("IdentityUserId")]
    public IdentityUser? User {get; set;}
    
}

