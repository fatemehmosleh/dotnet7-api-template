using System;

namespace BackEnd.Common.Core.Domain
{
    public class LogEntity
    {
        public LogEntity()
        {
            CreationUtcTime = DateTime.UtcNow;
            CreationLocalTime = DateTime.Now;
        }

        public long SequentialId { get;  set; }
        public DateTime CreationUtcTime { get;  set; }
        public DateTime CreationLocalTime { get;  set; }
        public DateTime? LogTime { get;  set; }
    }
}