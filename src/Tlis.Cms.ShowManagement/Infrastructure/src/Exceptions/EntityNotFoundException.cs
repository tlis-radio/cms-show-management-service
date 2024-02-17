using System;

namespace Tlis.Cms.ShowManagement.Infrastructure.Exceptions;

public class EntityNotFoundException(string? message = null) : Exception(message);