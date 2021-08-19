using System;
using Interia.Dto.Files;
using Interia.FileProviders.Factory;
using Interia.FileProviders.Providers;
using Interia.FileProviders.Settings;
using Xunit;

namespace Interia.FileProviders.Tests
{
	public class FileProviderFactoryTests
	{
		[Theory]
		[InlineData(FileStorageEnum.Disk, typeof(DiskFileProvider))]
		[InlineData(FileStorageEnum.Database, typeof(DatabaseFileProvider))]
		public void FileProviderFactoryShouldReturnCorrectFileProviderWhenFileUploadOptionsIsPassed(FileStorageEnum fileStorage , Type providerType)
		{
			var fileProviderFactory = new FileProviderFactory(new FileUploadOptions());
			var fileProvider = fileProviderFactory.GetInstance(fileStorage);
			Assert.IsType(providerType, fileProvider);
		}

		[Fact]
		public void FileProviderFactoryShouldThrowExceptionWhenInvalidFileUploadOptionsIsPassed()
		{
			var fileProviderFactory = new FileProviderFactory(new FileUploadOptions());
			Assert.Throws<ArgumentOutOfRangeException>(() => fileProviderFactory.GetInstance(FileStorageEnum.Database | FileStorageEnum.Disk));
		}
	}
}
