﻿namespace Ecommerce.Catalog.Infrastructure.Persistence.Outbox;

internal sealed class OutboxMessage
{
    internal Guid Id { get; set; }

    internal string Type { get; set; } = string.Empty;

    internal string Content { get; set; } = string.Empty;

    internal DateTime OccurredOnUtc { get; set; }

    internal DateTime? ProcessedOnUtc { get; set; }

    internal string? Error { get; set; }
}
