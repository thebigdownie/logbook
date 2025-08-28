using MongoDB.Bson;

namespace network.R;

public class UserLoginRequest
{
    public required string Username { get; set; }
    public required string Password { get; set; }

}