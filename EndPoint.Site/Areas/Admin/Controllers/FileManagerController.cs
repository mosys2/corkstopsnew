using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.FileManager.Queries.ListDirectories;
using Store.Common.ResultDto;
using System.Security.Policy;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FileManagerController : Controller
    {
        private readonly IGetListDirectoryServices _getListDirectory;
        public FileManagerController(IGetListDirectoryServices getListDirectory)
        {
            _getListDirectory=getListDirectory;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetDirectory(InputDirectoryModel model)
        {
            var list = await _getListDirectory.Execut(model.Directory);
            return Json(new ResultDto<List<DirectoryItems>>
            {
                Data=list,
                IsSuccess=true
            });
        }
        [HttpPost]
        public async Task<IActionResult> CreateDirectory(InputCreateDirectoryModel model)
        {
            var list = await _getListDirectory.Execut(model.Directory);
            return Json(new ResultDto<List<DirectoryItems>>
            {
                Data=list,
                IsSuccess=true
            });
        }
    }
    public class InputDirectoryModel
    {
        public string? Directory { get; set; }
    }
    public class InputCreateDirectoryModel
    {
        public string? Directory { get; set; }
        public string Name { get; set; }
    }
}
