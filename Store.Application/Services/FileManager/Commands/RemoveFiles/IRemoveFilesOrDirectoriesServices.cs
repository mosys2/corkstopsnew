using FluentFTP;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Store.Common.Constant;
using Store.Common.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Store.Application.Services.FileManager.Commands.RemoveFiles
{
    public interface IRemoveFilesOrDirectoriesServices
    {
        Task<ResultDto> Execut(List<string> names, string directoryPath);
    }
    public class RemoveFilesOrDirectoriesServices : IRemoveFilesOrDirectoriesServices
    {
        private readonly IHostingEnvironment _environment;
        private readonly IConfiguration _configuration;

        public RemoveFilesOrDirectoriesServices(IHostingEnvironment environment, IConfiguration configuration)
        {
            _environment = environment;
            _configuration = configuration;
        }
        public async Task<ResultDto> Execut(List<string> names, string directoryPath)
        {
            // اتصال به سرور FTP
            try
            {
                using (var client = new FtpClient())
                {
                    string ftpServer = _configuration.GetSection("FtpServer").Value;
                    string username = _configuration.GetSection("FtpUsername").Value;
                    string password = _configuration.GetSection("FtpPassword").Value;
                    string ftpRoot = _configuration.GetSection("FtpRoot").Value;
                    string url = ftpRoot+directoryPath;

                    client.Host=ftpServer;
                    client.Credentials = new NetworkCredential(username, password);

                    foreach(var item in names)
                    {
                        client.DeleteDirectory(url+item);
                        client.DeleteFile(url+item);
                    }
                    client.Disconnect();
                    return new ResultDto
                    {
                        IsSuccess=true,
                        Message=Messages.Message.RemovedSuccess
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResultDto
                {
                    IsSuccess=false,
                    Message=ex.Message
                };
            }
        }
    }

}
