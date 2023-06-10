using FluentFTP;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Store.Common.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using Store.Common.Constant.Enum;

namespace Store.Application.Services.FileManager.Queries.ListDirectories
{
    public interface IGetListDirectoryServices
    {
        Task<List<DirectoryItems>> Execut(string directoryPath);
    }
    public class GetListDirectoryServices : IGetListDirectoryServices
    {
        private readonly IHostingEnvironment _environment;
        private readonly IConfiguration _configuration;

        public GetListDirectoryServices(IHostingEnvironment environment, IConfiguration configuration)
        {
            _environment = environment;
            _configuration = configuration;
        }
        public async Task<List<DirectoryItems>> Execut(string directoryPath)
        {
            // اتصال به سرور FTP
            using (var client = new FtpClient())
            {
                string ftpServer = _configuration.GetSection("FtpServer").Value;
                string username = _configuration.GetSection("FtpUsername").Value;
                string password = _configuration.GetSection("FtpPassword").Value;
                string ftpRoot = _configuration.GetSection("FtpRoot").Value;
                string BaseUrl = _configuration.GetSection("BaseUrl").Value;
                string url = ftpRoot+directoryPath;

                client.Host=ftpServer;
                client.Credentials = new NetworkCredential(username, password);
                List<DirectoryItems> directoryItems = new List<DirectoryItems>();
                try
                {
                    foreach (FtpListItem item in client.GetListing(url))
                    {
                        if (item.Type==FtpObjectType.Directory)
                        {
                            directoryItems.Add(new DirectoryItems
                            {
                                fileType=FileType.Directory,
                                Name=item.Name,
                                Path=$"/{url}/{item.Name}",
                                Directory=$"/{item.Name}",
                                BaseUrl=BaseUrl
                                
                            });
                        }
                        else
                        {
                            string postfix = item.FullName.Split(".").Last().ToString().ToLower();
                            if (postfix=="png" || postfix=="jpg" || postfix=="jpeg" || postfix=="gif")
                            {
                                directoryItems.Add(new DirectoryItems
                                {
                                    fileType=FileType.Image,
                                    Size=item.Size / 1024 + "KB", 
                                    Name=item.Name,
                                    Path=$"/{url}/{item.Name}",
                                    BaseUrl=BaseUrl
                                });
                            }
                            else
                            {
                                directoryItems.Add(new DirectoryItems
                                {
                                    fileType=FileType.Other,
                                    Postfix=postfix,
                                    Size=item.Size /1024 +"KB",
                                    Name=item.Name,
                                    Path=$"/{url}/{item.Name}",
                                    BaseUrl=BaseUrl
                                });
                            }
                        }

                    }
                    client.Disconnect();
                    return directoryItems;
                }
                catch (WebException ex)
                {
                    Console.WriteLine($"خطا در دریافت لیست فایل‌ها: {ex.Message}");
                    return directoryItems;
                }
            }
        }
    }
    public class DirectoryItems
    {
        public string? Name { get; set; }
        public string? Directory { get; set; }
        public string? Path { get; set; }
        public string? Size { get; set; }
        public string? Postfix { get; set; }
        public string? BaseUrl { get; set; }
        public FileType fileType { get; set; }
    }
}
