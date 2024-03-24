using System;

namespace Tlis.Cms.ShowManagement.Infrastructure.HttpServices.Dtos;

public sealed class UserDto
{
    public Guid Id { get; set; }

    public string Nickname { get; set; } = null!;
}