using System;
using System.IO;
using System.Threading.Tasks;
using Interia.Core.Contracts.FileProviders;
using Interia.Dto.Files;

namespace Interia.FileProviders.Providers
{
	internal class DatabaseFileProvider : IFileProvider
	{
		public Task<bool> UploadFile(string fileName, Stream stream)
		{
			throw new NotImplementedException();
		}

		public FileViewModel GetFiles()
		{
			throw new NotImplementedException();
		}

		public Stream GetFile(string fileName)
		{
			throw new NotImplementedException();
		}

		public bool DeleteFile(string fileName)
		{
			throw new NotImplementedException();
		}
	}
}