using System;
using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson;
using MongoDbGenericRepository.Attributes;

namespace MongoAuthenticatorAPI.Models
{
    [CollectionName("users")]
    public class ApplicationUser : MongoIdentityUser<Guid>
    {
        public IEnumerable<ObjectId> Trips { get; set; } = [];
        public IEnumerable<ObjectId> Friends { get; set; } = [];
        public IEnumerable<ObjectId> Following { get; set; } = [];
    }

    [CollectionName("roles")]
    public class ApplicationRole : MongoIdentityRole<Guid>
    {
    }
}


