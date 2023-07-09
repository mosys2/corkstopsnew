using Store.Application.Services.Posts.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Interfaces.FacadPatternSite
{
    public interface IPostFacadSite
    {
        IGetProvinceServices GetProvinceServices { get; }
        IGetCityForPayServices GetCityForPayServices { get; }
        IGetCityService GetCityService { get; } 
    }
}
