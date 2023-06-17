using Store.Application.Services.Products.Commands.AddNewProduct;
using Store.Application.Services.Products.Queries.GetAllCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Interfaces.FacadPattern
{
    public interface IProductFacad
    {
        AddNewProductServices AddNewProductServices { get; }
        IGetAllCategoriesService GetAllCategoriesServices { get; }
    }
}
