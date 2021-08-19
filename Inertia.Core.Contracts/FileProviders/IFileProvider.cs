using System.IO;
using System.Threading.Tasks;
using Interia.Dto.Files;

namespace Interia.Core.Contracts.FileProviders
{
	public interface IFileProvider
	{
		Task<bool> UploadFile(string fileName, Stream stream);
		FileViewModel GetFiles();
		Stream GetFile(string fileName);
		bool DeleteFile(string fileName);
	}
}