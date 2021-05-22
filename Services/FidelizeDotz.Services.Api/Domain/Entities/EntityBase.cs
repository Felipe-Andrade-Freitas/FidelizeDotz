using System;

namespace FidelizeDotz.Services.Api.Domain.Entities
{
    public abstract class EntityBase
    {
        protected EntityBase()
        {
        }

        protected EntityBase(DateTime createdAt, DateTime updatedAt, bool isDeleted)
        {
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            IsDeleted = isDeleted;
        }

        public virtual DateTime CreatedAt { get; protected set; }

        public virtual DateTime UpdatedAt { get; protected set; }

        public virtual bool IsDeleted { get; protected set; }
    }
}