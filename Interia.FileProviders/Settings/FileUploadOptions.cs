using System.Collections.Generic;
using System.Configuration;
using Interia.Core.Contracts.Validations;
using Interia.Dto.Files;
using Interia.Resources.Messages;

namespace Interia.FileProviders.Settings
{
	public class FileUploadOptions : IValidatableObject
	{
		public const string FileUploadSettings = "FileUploadSettings";

		public FileUploadOptions()
		{
			AllowedFileTypes = new List<string>();
		}
		public string ConnectionString { get; set; }
		public FileStorageEnum FileStorageType { get; set; }
		public string DiskStoragePath { get; set; }
		public List<string> AllowedFileTypes { get; set; }

		public void Validate()
		{
			switch (FileStorageType)
			{
				case FileStorageEnum.Disk when string.IsNullOrEmpty(DiskStoragePath):
					throw new ConfigurationErrorsException(Messages.DiskStoragePathNotSet);
				case FileStorageEnum.Database when string.IsNullOrEmpty(ConnectionString):
					throw new ConfigurationErrorsException(Messages.ConnectionStringNotSet);
			}
		}
	}
}