using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.FileManager.Queries.ListDirectories;
using Store.Common.ResultDto;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IGetListDirectoryServices _getListDirectory;
        public ProductController(IGetListDirectoryServices getListDirectory)
        {
            _getListDirectory=getListDirectory;
        }
    

        public IActionResult Index()
        {

            return View();
        }
        public async Task<IActionResult> GetDirectoryList(string url="")
        {
            var list=await  _getListDirectory.Execut(url);
            return Json(new ResultDto<List<DirectoryItems>>
            {
                Data=list,
                IsSuccess=true,
            });
        }
    }
}
