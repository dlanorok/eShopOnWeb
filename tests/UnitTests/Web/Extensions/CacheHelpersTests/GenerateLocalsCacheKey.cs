using Microsoft.eShopWeb.Web.Extensions;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.Web.Extensions.CacheHelpersTests;

public class GenerateLocalsCacheKey
{
    [Fact]
    public void ReturnsLocalsCacheKey()
    {
        var result = CacheHelpers.GenerateLocalsCacheKey();

        Assert.Equal("local", result);
    }
}
