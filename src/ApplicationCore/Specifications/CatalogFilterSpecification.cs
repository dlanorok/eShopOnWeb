using Ardalis.Specification;
using Microsoft.eShopWeb.ApplicationCore.Entities;

namespace Microsoft.eShopWeb.ApplicationCore.Specifications;

public class CatalogFilterSpecification : Specification<CatalogItem>
{
    public CatalogFilterSpecification(int? brandId, int? typeId, int? localId)
    {
        Query.Where(i => (!brandId.HasValue || i.CatalogBrandId == brandId) &&
            (!typeId.HasValue || i.CatalogTypeId == typeId) &&
            (!localId.HasValue || i.CatalogLocalId == localId));
    }
}

public class CatalogLocalIdFilterSpecification : Specification<CatalogLocal>
{
    public CatalogLocalIdFilterSpecification(int localId)
    {
        Query.Where(i => i.Id == localId);
    }
}