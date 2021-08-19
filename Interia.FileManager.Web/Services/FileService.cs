using System.IO;
using Interia.Core.Contracts.FileProviders;
using Interia.Core.Contracts.FileProviders.Factory;
using Interia.Dto.Files;
using Interia.FileProviders.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Interia.FileManager.Web.Services
{
	internal class FileService : IFileService
	{
		private readonly IFileProviderFactory _fileProviderFactory;
		private FileStorageEnum _fileStorageEnum;

		public FileService(IFileProviderFactory fileProviderFactory, IOptions<FileUploadOptions> fileUploadOptions)
		{
			_fileProviderFactory = fileProviderFactory;
			_fileStorageEnum = fileUploadOptions.Value.FileStorageType;
		}

		private IFileProvider _fileProvider => _fileProviderFactory.GetInstance(_fileStorageEnum);

		public bool UploadFiles(IFormFile[] files)
		{
			foreach (var file in files)
			{
				UploadFile(file);
			}

			return true;
		}

		public FileViewModel GetFiles()
		{
			return _fileProvider.GetFiles();
		}

		public Stream GetFile(string fileName)
		{
			return _fileProvider.GetFile(fileName);
		}

		public bool UploadFile(IFormFile file)
		{
			var fileName = Path.GetFileName(file.FileName);
			using var stream = file.OpenReadStream();
			_fileProvider.UploadFile(fileName, stream);
			return true;
		}

		public bool DeleteFile(string fileName)
		{
			return _fileProvider.DeleteFile(fileName);
		}
	}
}