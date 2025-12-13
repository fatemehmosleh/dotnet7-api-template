using System;

namespace BackEnd.Common.Core.Domain
{
    public interface IEntityConfiguration
    {
        Type EntityType { get; }
    }
}