using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Interia.Core.Contracts.FileProviders;
using Interia.Dto.Files;
using Interia.FileProviders.Settings;

namespace Interia.FileProviders.Providers
{
	internal class DiskFileProvider : IFileProvider
	{
		private readonly FileUploadOptions _fileUploadOptions;

		public DiskFileProvider(FileUploadOptions fileUploadOptions)
		{
			_fileUploadOptions = fileUploadOptions;
		}

		public Task<bool> UploadFile(string fileName, Stream stream)
		{
			var filePath = Path.Combine(_fileUploadOptions.DiskStoragePath, fileName);

			if (File.Exists(filePath)) File.Delete(filePath);

			using var localFile = File.OpenWrite(filePath);
			stream.CopyTo(localFile);
			return Task.FromResult(true);
		}

		public FileViewModel GetFiles()
		{
			var allFiles = Directory.GetFiles(_fileUploadOptions.DiskStoragePath);
			var filteredFiles = _fileUploadOptions.AllowedFileTypes.Any() 
				? allFiles.Where(f => _fileUploadOptions.AllowedFileTypes.Contains(Path.GetExtension(f))).ToArray() 
				: allFiles;
			var fileViewModel = new FileViewModel();
			foreach (var filePath in filteredFiles)
				fileViewModel.Files.Add(new FileDetails {Name = Path.GetFileName(filePath), Path = filePath});

			return fileViewModel;
		}

		public Stream GetFile(string fileName)
		{
			var file = Directory
				.GetFiles(_fileUploadOptions.DiskStoragePath).FirstOrDefault(f =>
					Path.GetFileName(f).Equals(fileName, StringComparison.InvariantCultureIgnoreCase));
			if (string.IsNullOrEmpty(file))
				throw new FileNotFoundException();
			return File.OpenRead(file);
		}

		public bool DeleteFile(string fileName)
		{
			var file = Directory
				.GetFiles(_fileUploadOptions.DiskStoragePath).FirstOrDefault(f =>
					Path.GetFileName(f).Equals(fileName, StringComparison.InvariantCultureIgnoreCase));
			if (!string.IsNullOrEmpty(file) && File.Exists(file))
				File.Delete(file);
			else
				throw new FileNotFoundException();

			return true;
		}
	}
}