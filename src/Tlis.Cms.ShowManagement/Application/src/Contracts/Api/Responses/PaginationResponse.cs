using System.Collections.Generic;

namespace Tlis.Cms.ShowManagement.Application.Contracts.Api.Responses;

public class PaginationResponse<T>
{
    public int Total { get; set; }

    public int Limit { get; set; }

    public int Page { get; set; }

    public int TotalPages { get; set; }

    public IReadOnlyCollection<T> Results { get; set; } = [];
}