using MongoDB.Bson.Serialization.Attributes;
using System;

namespace SmileBoy.Client.Entity
{
    public class EntityBase<TKey> : IEntity<TKey>
    {
        [BsonId]
        public TKey Id { get; set; }

        public DateTime CreatedBy { get; set; }

        public DateTime UpdateBy { get; set; }
    }
}
