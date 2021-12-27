using System;

namespace JobWebApi.AppModels.Models
{
    public abstract class BaseEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string CreatedAt { get; set; } = new DateTime().ToString();
        public string UpdatedAt { get; set; } = new DateTime().ToString();
    }
}
