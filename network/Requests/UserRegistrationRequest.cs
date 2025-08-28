using MongoDB.Bson;

namespace network.R;

public class UserRegistrationRequest
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }

}