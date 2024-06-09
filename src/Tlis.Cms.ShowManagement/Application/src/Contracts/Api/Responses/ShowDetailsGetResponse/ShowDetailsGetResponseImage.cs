using System;
using System.ComponentModel.DataAnnotations;

namespace Tlis.Cms.ShowManagement.Application.Contracts.Api.Responses;

public class ShowDetailsGetResponseImage
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public required string Url { get; set; }
}