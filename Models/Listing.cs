using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace BidingApp;

public class Listing
{
    public int Id {get; set;}
    public string Title {get; set;} = "";
    public string Description {get; set;} = "";
    public decimal Price {get; set;}
    public string ImgPath {get; set;} = "";
    public bool IsSold {get; set;} 
    [Required]
    public string? IdentityUserId {get; set;}
    public List<Bid>? Bids {get; set;}
    public List<Comment>? Comments {get; set;} 
    [ForeignKey("IndentityUserId")]
    public IdentityUser? User {get; set;}
    
}
