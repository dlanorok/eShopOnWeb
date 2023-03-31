using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using MinimalApi.Endpoint;

namespace Microsoft.eShopWeb.PublicApi.CatalogLocalEndpoints;

/// <summary>
/// List Catalog Types
/// </summary>
public class CatalogLocalListEndpoint : IEndpoint<IResult, IRepository<CatalogLocal>>
{
    private readonly IMapper _mapper;

    public CatalogLocalListEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/catalog-locals",
            async (IRepository<CatalogLocal> catalogLocalRepository) =>
            {
                return await HandleAsync(catalogLocalRepository);
            })
            .Produces<ListCatalogLocalsResponse>()
            .WithTags("CatalogTypeEndpoints");
    }

    public async Task<IResult> HandleAsync(IRepository<CatalogLocal> catalogLocalRepository)
    {
        var response = new ListCatalogLocalsResponse();

        var items = await catalogLocalRepository.ListAsync();

        response.CatalogLocals.AddRange(items.Select(_mapper.Map<CatalogLocalDto>));

        return Results.Ok(response);
    }
}
