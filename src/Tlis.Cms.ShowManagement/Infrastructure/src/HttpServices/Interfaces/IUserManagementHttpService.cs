using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tlis.Cms.ShowManagement.Infrastructure.HttpServices.Dtos;

namespace Tlis.Cms.ShowManagement.Infrastructure.HttpServices.Interfaces;

public interface IUserManagementHttpService
{
    Task<UserFilterDto> FilterUsersAsync(IEnumerable<Guid> userIds);
}