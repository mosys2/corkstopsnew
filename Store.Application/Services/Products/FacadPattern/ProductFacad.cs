using Store.Application.Interfaces.Contexts;
using Store.Application.Interfaces.FacadPattern;
using Store.Application.Services.Products.Commands.AddNewProduct;
using Store.Application.Services.Products.Commands.AddNewTagServices;
using Store.Application.Services.Products.Commands.EditProduct;
using Store.Application.Services.Products.Commands.RemoveProduct;
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

namespace Store.Application.Services.Products.FacadPattern
{
    public class ProductFacad : IProductFacad
    {
        private readonly IDataBaseContext _context;
        public ProductFacad(IDataBaseContext context)
        {
            _context = context;
        }

        private AddNewProductServices _addNewProductServices;
        public AddNewProductServices AddNewProductServices
        {
            get { return _addNewProductServices=_addNewProductServices ?? new AddNewProductServices(_context); }
        }

        private AddNewTagServices _addNewTagServices;
        public AddNewTagServices AddNewTagServices
        {
            get { return _addNewTagServices=_addNewTagServices ?? new AddNewTagServices(_context); }
        }

        private IGetAllCategoriesService _allCategoriesService;
        public IGetAllCategoriesService GetAllCategoriesServices
        {
            get
            {
                return _allCategoriesService = _allCategoriesService ?? new GetAllCategoriesService(_context);
            }
        }

        private IGetAllTagsServices _getAllTagsServices;
        public IGetAllTagsServices GetAllTagsServices
        {
            get
            {
                return _getAllTagsServices = _getAllTagsServices ?? new GetAllTagsServices(_context);
            }
        }

        private IGetAllBrandsServices _getAllBrandsServices;
        public IGetAllBrandsServices GetAllBrandsServices
        {
            get
            {
                return _getAllBrandsServices = _getAllBrandsServices ?? new GetAllBrandsServices(_context);
            }
        }

        private IGetAllProductServices _getAllProductServices;
        public IGetAllProductServices GetAllProductServices
        {
            get
            {
                return _getAllProductServices = _getAllProductServices ?? new GetAllProductServices(_context);
            }
        }

        private IGetProductDetailServices _getProductDetailServices;
        public IGetProductDetailServices GetProductDetailServices
        {
            get
            {
                return _getProductDetailServices = _getProductDetailServices ?? new GetProductDetailServices(_context);
            }
        }

        private IEditProductServices _editProductServices;
        public IEditProductServices EditProductServices
        {
            get
            {
                return _editProductServices = _editProductServices ?? new EditProductServices(_context);
            }
        }

        private IRemoveProductServices _removeProductServices;
        public IRemoveProductServices RemoveProductServices
        {
            get
            {
                return _removeProductServices = _removeProductServices ?? new RemoveProductServices(_context);
            }
        }

    }
}
