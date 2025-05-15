﻿using Microsoft.Extensions.Primitives;

namespace VerticalTemplate.Api.Common.Extensions;

public static class HttpContextExtensions
{
    public static StringValues GetCorrelationId(this HttpContext context, bool allowEmpty = false)
    {
        if (context.Request.Headers.TryGetValue(
            HeaderConstants.CorrelationId, out StringValues requestCorrelationId))
        {
            return requestCorrelationId;
        }

        if (context.Response.Headers.TryGetValue(
            HeaderConstants.CorrelationId, out StringValues responseCorrelationId))
        {
            return responseCorrelationId;
        }

        return allowEmpty
            ? StringValues.Empty
            : new StringValues(Guid.NewGuid().ToString());
    }
}