namespace BookClubAPI.Models;

public class UserModel
{
    public string Name { get; set; }
    public bool HasWon { get; set; } = false;
    public string Credencial { get; set; } = Guid.NewGuid().ToString().Substring(0, 4);
    public int TotalBooks { get; set; }
    public int TotalPoints { get; set; }
}