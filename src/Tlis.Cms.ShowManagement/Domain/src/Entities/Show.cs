using System;
using System.Collections.Generic;
using Tlis.Cms.ShowManagement.Domain.Entities.Base;

namespace Tlis.Cms.ShowManagement.Domain.Entities;

public sealed class Show : BaseEntity
{
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public List<Guid> ModeratorIds { get; set; } = [];

    public DateOnly CreatedDate { get; set; }

    public Guid? ProfileImageId { get; set; }
}