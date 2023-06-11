using FluentFTP;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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

namespace Store.Application.Services.FileManager.Commands.UploadFiles
{
    public interface IUploadFilesServices
    {
        Task<ResultDto> Execut(IEnumerable<IFormFile>? files, string? directoryPath);
    }
    public class UploadFilesServices : IUploadFilesServices
    {
        private readonly IHostingEnvironment _environment;
        private readonly IConfiguration _configuration;

        public UploadFilesServices(IHostingEnvironment environment, IConfiguration configuration)
        {
            _environment = environment;
            _configuration = configuration;
        }
        public async Task<ResultDto> Execut(IEnumerable<IFormFile>? files, string? directoryPath)
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
                    foreach (var file in files)
                    {
                        string remotFilePath = url+"/"+file.FileName;
                        using (Stream stream = file.OpenReadStream())
                        {
                            client.UploadStream(stream, remotFilePath);
                        }
                    }
                    client.Disconnect();
                    return new ResultDto
                    {
                        IsSuccess = true,
                        Message=Messages.Message.UploadSuccess
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
