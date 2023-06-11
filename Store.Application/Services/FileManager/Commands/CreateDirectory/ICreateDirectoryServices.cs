using FluentFTP;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Store.Application.Services.FileManager.Queries.ListDirectories;
using Store.Common.Constant;
using Store.Common.Constant.Enum;
using Store.Common.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.FileManager.Commands.CreateDirectory
{
    public interface ICreateDirectoryServices
    {
        Task<ResultDto> Execut(string? directoryPath, string? Name);
    }
    public class CreateDirectoryServices : ICreateDirectoryServices
    {
        private readonly IHostingEnvironment _environment;
        private readonly IConfiguration _configuration;

        public CreateDirectoryServices(IHostingEnvironment environment, IConfiguration configuration)
        {
            _environment = environment;
            _configuration = configuration;
        }

        public async Task<ResultDto> Execut(string? directoryPath, string? Name)
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
                    string url = ftpRoot+directoryPath+"/"+Name;

                    client.Host=ftpServer;
                    client.Credentials = new NetworkCredential(username, password);
                    if (!client.DirectoryExists(url))
                    {
                        client.CreateDirectory(url);
                        client.Disconnect();
                        return new ResultDto
                        {
                            IsSuccess = true,
                            Message=Messages.Message.CreatedDirectorySuccess
                        };

                    }
                    else
                    {
                        client.Disconnect();
                        return new ResultDto
                        {
                            IsSuccess = false,
                            Message=Messages.ErrorsMessage.AlreadyExist
                        };
                    }
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
