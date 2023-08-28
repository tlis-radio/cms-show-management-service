using System;

namespace Tlis.Cms.ShowManagement.Infrastructure.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string? message = null) : base(message)
    {
    }
}