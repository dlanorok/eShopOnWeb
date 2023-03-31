using System;
using System.Collections.Generic;

namespace Microsoft.eShopWeb.PublicApi.CatalogLocalEndpoints;

public class ListCatalogLocalsResponse : BaseResponse
{
    public ListCatalogLocalsResponse(Guid correlationId) : base(correlationId)
    {
    }

    public ListCatalogLocalsResponse()
    {
    }

    public List<CatalogLocalDto> CatalogLocals { get; set; } = new List<CatalogLocalDto>();
}
