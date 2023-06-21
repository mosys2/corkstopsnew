using Store.Application.Services.Products.Commands.AddNewProduct;
using Store.Application.Services.Products.Commands.AddNewTagServices;
using Store.Application.Services.Products.Queries.GetAllBrands;
using Store.Application.Services.Products.Queries.GetAllCategories;
using Store.Application.Services.Products.Queries.GetAllProducts;
using Store.Application.Services.Products.Queries.GetAllTags;
using Store.Application.Services.Products.Queries.GetProductDetail;
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
        AddNewTagServices AddNewTagServices { get; }
        IGetAllCategoriesService GetAllCategoriesServices { get; }
        IGetAllTagsServices GetAllTagsServices { get; }
        IGetAllBrandsServices GetAllBrandsServices { get; }
        IGetAllProductServices GetAllProductServices { get; }
        IGetProductDetailServices GetProductDetailServices { get; }

    }
}
