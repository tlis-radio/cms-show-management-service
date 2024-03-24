using System;

namespace Tlis.Cms.ShowManagement.Application.Contracts.Api.Responses;

public class ShowDetailsGetResponseModerators
{
    public Guid Id { get; set; }

    public string Nickname { get; set; } = null!;
}