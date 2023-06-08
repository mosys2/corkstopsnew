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
                                Icon="/AdminTemplate/images/filemanager-icon/folder.png",
                                Name=item.Name,
                                Path=$"/{url}/{item.Name}"
                            });
                        }
                        else
                        {
                            string postfix = item.FullName.Split(".").Last().ToString().ToLower();
                            if (postfix=="png" || postfix=="jpg" || postfix=="jpeg" || postfix=="gif")
                            {
                                directoryItems.Add(new DirectoryItems
                                {
                                    Icon=$"/{url}/{item.Name}",
                                    Name=item.Name,
                                    Path=$"/{url}/{item.Name}"
                                });
                            }
                            else if (postfix=="mp3" || postfix=="wma" || postfix=="aac" || postfix=="ac3" || postfix=="wav")
                            {
                                directoryItems.Add(new DirectoryItems
                                {
                                    Icon="/AdminTemplate/images/filemanager-icon/audio-icon.png",
                                    Name=item.Name,
                                    Path=$"/{url}/{item.Name}"
                                });
                            }
                            else if (postfix=="avi" || postfix=="mp4" || postfix=="mkv" || postfix=="wma" || postfix=="mpeg" || postfix=="mov")
                            {
                                directoryItems.Add(new DirectoryItems
                                {
                                    Icon="/AdminTemplate/images/filemanager-icon/video.png",
                                    Name=item.Name,
                                    Path=$"/{url}/{item.Name}"
                                });
                            }
                            else
                            {
                                directoryItems.Add(new DirectoryItems
                                {
                                    Icon="/AdminTemplate/images/filemanager-icon/file.png",
                                    Name=item.Name,
                                    Path=$"/{url}/{item.Name}"
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
        public string? Path { get; set; }
        public string? Icon { get; set; }
    }
}
