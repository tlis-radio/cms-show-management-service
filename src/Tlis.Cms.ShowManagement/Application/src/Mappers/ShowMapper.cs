using System;
using System.Collections.Generic;
using System.Linq;
using Riok.Mapperly.Abstractions;
using Tlis.Cms.ShowManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.ShowManagement.Application.Contracts.Api.Responses;
using Tlis.Cms.ShowManagement.Domain.Entities;
using Tlis.Cms.ShowManagement.Infrastructure.HttpServices.Dtos;

namespace Tlis.Cms.ShowManagement.Application.Mappers;

[Mapper]
public partial class ShowMapper
{
    public partial ShowPaginationGetResponse ToPaginationDto(Show entity);

    public ShowDetailsGetResponse ToDto(Show entity, List<UserDto> Users, ImageDto? image)
    {
        var response = new ShowDetailsGetResponse
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            CreatedDate = entity.CreatedDate,
            Moderators = entity.ModeratorIds.Select(id =>
            {
                var user = Users.FirstOrDefault(u => u.Id == id)
                    ?? throw new InvalidOperationException($"User with id {id} not found");

                return new ShowDetailsGetResponseModerators
                {
                    Id = user.Id,
                    Nickname = user.Nickname
                };
            }).ToList()
        };

        if (image != null)
        {
            response.ProfileImage = new ShowDetailsGetResponseImage
            {
                Id = image.Id,
                Url = image.Url
            };
        }

        return response;
    }

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