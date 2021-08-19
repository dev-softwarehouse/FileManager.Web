using System;
using Interia.Core.Contracts.FileProviders;
using Interia.Core.Contracts.FileProviders.Factory;
using Interia.Dto.Files;
using Interia.FileProviders.Providers;
using Interia.FileProviders.Settings;

namespace Interia.FileProviders.Factory
{
	public class FileProviderFactory : IFileProviderFactory
	{
		private readonly FileUploadOptions _fileUploadOptions;

		public FileProviderFactory(FileUploadOptions fileUploadOptions)
		{
			_fileUploadOptions = fileUploadOptions;
		}

		public IFileProvider GetInstance(FileStorageEnum fileStorageEnum)
		{
			return fileStorageEnum switch
			{
				FileStorageEnum.Disk => new DiskFileProvider(_fileUploadOptions),
				FileStorageEnum.Database => new DatabaseFileProvider(),
				_ => throw new ArgumentOutOfRangeException(nameof(fileStorageEnum), fileStorageEnum, null)
			};
		}
	}
}