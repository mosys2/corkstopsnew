using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.FileManager.Commands.CreateDirectory;
using Store.Application.Services.FileManager.Commands.RemoveFiles;
using Store.Application.Services.FileManager.Commands.UploadFiles;
using Store.Application.Services.FileManager.Queries.ListDirectories;
using Store.Common.ResultDto;
using System.Security.Policy;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FileManagerController : Controller
    {
        private readonly IGetListDirectoryServices _getListDirectory;
        private readonly ICreateDirectoryServices _createDirectory;
        private readonly IUploadFilesServices _uploadFiles;
        private readonly IRemoveFilesOrDirectoriesServices _removeFiles;
        public FileManagerController(IGetListDirectoryServices getListDirectory,
            ICreateDirectoryServices createDirectory,
            IUploadFilesServices uploadFiles,
            IRemoveFilesOrDirectoriesServices removeFiles)
        {
            _getListDirectory=getListDirectory;
            _createDirectory=createDirectory;
            _uploadFiles=uploadFiles;
            _removeFiles=removeFiles;
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
            var result = await _createDirectory.Execut(model.Directory,model.Name);
            return Json(new ResultDto
            {
                IsSuccess=result.IsSuccess,
                 Message=result.Message
            });
        }

        [HttpPost]
        public async Task<IActionResult> UploadFiles(IEnumerable<IFormFile> Files, string Directory)
        {
          var result=await _uploadFiles.Execut(Files, Directory);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFiles(InputRemoveModel model)
        {
            var result=await _removeFiles.Execute(model.ArryRemoveItem, model.Directory);
            return Json(result);
        }
    }

    public class InputDirectoryModel
    {
        public string? Directory { get; set; }
    }
    public class InputCreateDirectoryModel
    {
        public string? Directory { get; set; }
        public string? Name { get; set; }
    }
    public class InputRemoveModel
    {
        public List<string>? ArryRemoveItem { get; set; }
        public string? Directory { get; set; }
    }
}
