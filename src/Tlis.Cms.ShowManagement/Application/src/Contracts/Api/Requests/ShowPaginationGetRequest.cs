using System;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Tlis.Cms.ShowManagement.Application.Contracts.Api.Responses;

namespace Tlis.Cms.ShowManagement.Application.Contracts.Api.Requests;

public sealed class ShowPaginationGetRequest : IRequest<PaginationResponse<ShowPaginationGetResponse>>
{
    [Required]
    public int Limit { get; set; }

    [Required] [Range(1, int.MaxValue)]
    public int Page { get; set; }
}