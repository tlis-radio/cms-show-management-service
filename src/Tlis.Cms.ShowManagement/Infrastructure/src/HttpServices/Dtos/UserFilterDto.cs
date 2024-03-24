using System.Collections.Generic;

namespace Tlis.Cms.ShowManagement.Infrastructure.HttpServices.Dtos;

public sealed class UserFilterDto
{
    public List<UserDto> Results { get; set; } = [];
}