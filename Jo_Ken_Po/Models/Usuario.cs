public class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public int Score { get; set; }

    public User(string username, string password)
    {
        Username = username;
        Password = password;
        Score = 0;
    }

    public void IncreaseScore()
    {
        Score++;
    }
}
