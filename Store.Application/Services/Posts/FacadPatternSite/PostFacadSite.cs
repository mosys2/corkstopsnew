using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Application.Interfaces.FacadPattern;
using Store.Application.Interfaces.FacadPatternSite;
using Store.Application.Services.Posts.Queries;
using Store.Application.Services.Products.Queries.GetAllProductsForSite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Posts.FacadPatternSite
{
    public class PostFacadSite : IPostFacadSite
    {
        private readonly IDataBaseContext _context;
        public PostFacadSite(IDataBaseContext context)
        {
            _context = context;
        }
        private IGetCityForPayServices _getCityForPayServices;
        public IGetCityForPayServices GetCityForPayServices
        {
            get
            {
                return _getCityForPayServices = _getCityForPayServices ?? new GetCityForPayServices(_context);
            }
        }

        private IGetProvinceServices _getProvinceServices;
        public IGetProvinceServices GetProvinceServices
        {
            get
            {
                return _getProvinceServices = _getProvinceServices ?? new GetProvinceServices(_context);
            }
        }

        private IGetCityService _getCityService;
        public IGetCityService GetCityService
        {
            get
            {
                return _getCityService = _getCityService ?? new GetCityService(_context);
            }
        }

    }
}
