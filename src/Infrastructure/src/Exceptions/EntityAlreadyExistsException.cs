using System;

namespace Tlis.Cms.ShowManagement.Infrastructure.Exceptions;

public class EntityAlreadyExistsException : Exception
{
    public EntityAlreadyExistsException(string? message = null)
        : base(message)
    {
    }
}