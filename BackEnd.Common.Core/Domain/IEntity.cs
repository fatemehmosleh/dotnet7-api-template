using System;

namespace BackEnd.Common.Core.Domain
{
    public interface IEntity
    {
        Guid Id { get; }
        long SequentialId { get; }
        DateTime CreationUtcTime { get; }
    }
}