using System.IO;
using Interia.Dto.Files;
using Microsoft.AspNetCore.Http;

namespace Interia.FileManager.Web.Services
{
	public interface IFileService
	{
		bool UploadFiles(IFormFile[] files);
		FileViewModel GetFiles();
		Stream GetFile(string fileName);
		bool UploadFile(IFormFile file);
		bool DeleteFile(string filename);
	}
}