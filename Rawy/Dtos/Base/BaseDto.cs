using System;

namespace Rawy.Dtos.Base
{
    public abstract class BaseDto
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
} 