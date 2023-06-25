using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Application.Interfaces.FacadPatternSite;
using Store.Application.Services.Products.Queries.GetAllProducts;
using Store.Application.Services.Products.Queries.GetAllProductsForSite;
using Store.Application.Services.Products.Queries.GetProductDetailForSite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Products.FacadpatternSite
{
    public class ProductFacadSite: IProductFacadSite
    {
        private readonly IDataBaseContext _context;
        public ProductFacadSite(IDataBaseContext context)
        {
            _context = context;
        }
        private IGetAllProductForSiteServices _getAllProductForSiteServices;
        public IGetAllProductForSiteServices GetAllProductForSiteServices
        {
            get
            {
                return _getAllProductForSiteServices = _getAllProductForSiteServices ?? new GetAllProductForSiteServices(_context);
            }
        }

        private IGetProductDetailForSiteServices _getProductDetailForSiteServices;
        public IGetProductDetailForSiteServices GetProductDetailForSiteServices
        {
            get
            {
                return _getProductDetailForSiteServices = _getProductDetailForSiteServices ?? new GetProductDetailForSiteServices(_context);
            }
        }
    }
}
