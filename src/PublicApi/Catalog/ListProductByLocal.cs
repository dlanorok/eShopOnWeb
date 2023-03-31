using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Specification;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints;
using MinimalApi.Endpoint;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Microsoft.eShopWeb.PublicApi.CatalogLocalEndpoints;

/// <summary>
/// List Catalog Brands
/// </summary>
public class ListProductByLocalEndpoint : IEndpoint<IResult, ListProductByLocalRequest, IRepository<CatalogItem>, IRepository<CatalogLocal>>
{
    private readonly IMapper _mapper;

    public ListProductByLocalEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/local/{localId}/products",
            async (int localId, IRepository<CatalogItem> catalogItemRepository, IRepository<CatalogLocal> catalogLocalRepository) =>
            {
                return await HandleAsync(new ListProductByLocalRequest(localId), catalogItemRepository, catalogLocalRepository);
            })
           .Produces<ListProductByLocalResponse>()
           .WithTags("CatalogLocalEndpoints");
    }

    public async Task<IResult> HandleAsync(ListProductByLocalRequest request, IRepository<CatalogItem> catalogItemRepository, IRepository<CatalogLocal> catalogLocalRepository)
    {
        var local = await catalogLocalRepository.FirstOrDefaultAsync(new CatalogLocalIdFilterSpecification(request.LocalId));
        if (local == null)
        {
            return Results.BadRequest();
        }

        var response = new ListProductByLocalResponse(request.CorrelationId(), local.Local);
        var specification = new CatalogFilterSpecification(null, null, request.LocalId);
        var items = await catalogItemRepository.ListAsync(specification);

        response.CatalogItems.AddRange(items.Select(_mapper.Map<CatalogItemDto>));

        return Results.Ok(response);
    }
}
 
public class ListProductByLocalResponse : BaseResponse
{
    public ListProductByLocalResponse(Guid correlationId, string? local) : base(correlationId)
    {
        Local = local;
    }

    public ListProductByLocalResponse(string? local)
    {
        Local = local;
    }

    public string? Local { get; set; }
    public List<CatalogItemDto> CatalogItems { get; set; } = new List<CatalogItemDto>();
}


public class ListProductByLocalRequest : BaseRequest
{
    public int LocalId { get; init; }

    public ListProductByLocalRequest(int localId)
    {
        LocalId = localId;
    }
}
 