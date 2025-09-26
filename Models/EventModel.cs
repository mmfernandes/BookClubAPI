namespace BookClubAPI.Models;

 public class EventModel
{
    public int IdEvent { get; set; }
    public int BooksBought { get; set; }
    public string? Credential { get; set; }
    public string? RequestedUserName { get; set; }
    public string Description { get; set; }
}