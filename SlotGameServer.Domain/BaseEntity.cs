using System;

namespace SlotGameServer.Domain
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public int CreateUserId { get; set; }
        public int LastModifiedUserId { get; set; }
    }
}
