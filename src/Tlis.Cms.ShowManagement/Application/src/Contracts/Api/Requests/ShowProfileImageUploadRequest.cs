using System;
using MediatR;
using Microsoft.AspNetCore.Http;
using Tlis.Cms.ShowManagement.Application.Contracts.Api.Responses;

namespace Tlis.Cms.ShowManagement.Application.Contracts.Api.Requests;

public sealed class ShowProfileImageUploadRequest : IRequest<BaseCreateResponse>
{
    public Guid Id { get; set; }

    public IFormFile Image { get; set; } = null!;
}