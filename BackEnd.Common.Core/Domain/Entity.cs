using System;

namespace BackEnd.Common.Core.Domain
{
    public class Entity : IEntity
    {
        public Entity()
        {
            
            Id = Guid.NewGuid();
            CreationUtcTime = DateTime.UtcNow;
            CreationLocalTime = DateTime.Now;
        }

        public Guid Id { get; protected set; }
        public long SequentialId { get;  set; }
        public DateTime CreationUtcTime { get; protected set; }
        public DateTime CreationLocalTime { get; protected set; }
    }
}
