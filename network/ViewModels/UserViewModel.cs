using MongoDB.Bson;

namespace network.Models;

public class UserViewModel
{
    public required string UserId { get; set; }
    public required string Username { get; set; }
    public IEnumerable<ObjectId> Trips { get; set; } = [];
    public IEnumerable<ObjectId> Friends { get; set; } = [];
    public IEnumerable<ObjectId> Following { get; set; } = [];
    public string AccessToken { get; set; } = string.Empty;

}
