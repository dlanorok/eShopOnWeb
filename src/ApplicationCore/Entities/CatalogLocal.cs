using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace Microsoft.eShopWeb.ApplicationCore.Entities;

public class CatalogLocal : BaseEntity, IAggregateRoot
{
    public string Local { get; private set; }
    public CatalogLocal(string local)
    {
        Local = local;
    }
}
