using Store.Application.Services.Products.Queries.GetAllProducts;
using Store.Application.Services.Products.Queries.GetAllProductsForSite;
using Store.Application.Services.Products.Queries.GetProductDetailForSite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Interfaces.FacadPatternSite
{
    public interface IProductFacadSite
    {
        IGetAllProductForSiteServices GetAllProductForSiteServices { get; }
        IGetProductDetailForSiteServices GetProductDetailForSiteServices { get; }
    }
}
