namespace BookClubAPI.Models;

public class UserModel
{
    public int IdUser { get; set; }
    public string Name { get; set; }
    public int Idade { get; set; }
    public string Credencial { get; set; } = Guid.NewGuid().ToString().Substring(0, 4);
    public int TotalBooks { get; set; }
    public int TotalPoints { get; set; }
}