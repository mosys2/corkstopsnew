using Store.Application.Interfaces.Contexts;
using Store.Application.Interfaces.FacadPattern;
using Store.Application.Services.Products.Commands.AddNewProduct;
using Store.Application.Services.Products.Queries.GetAllCategories;
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

        private IGetAllCategoriesService _allCategoriesService;
        public IGetAllCategoriesService GetAllCategoriesServices
        {
            get
            {
                return _allCategoriesService = _allCategoriesService ?? new GetAllCategoriesService(_context);
            }
        }

    }
}
