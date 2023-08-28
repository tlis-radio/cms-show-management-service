using System;
using Riok.Mapperly.Abstractions;
using Tlis.Cms.ShowManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.ShowManagement.Domain.Entities;

namespace Tlis.Cms.ShowManagement.Application.Mappers;

[Mapper]
public partial class ShowMapper
{
    public Show ToEntity(ShowCreateRequest request)
    {
        var entity = MapToEntity(request);

        entity.CreatedDate = DateOnly.FromDateTime(DateTime.UtcNow);

        return entity;
    }

    [MapperIgnoreTarget(nameof(Show.Id))]
    [MapperIgnoreTarget(nameof(Show.CreatedDate))]
    private partial Show MapToEntity(ShowCreateRequest request);
}