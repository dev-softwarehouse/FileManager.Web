using Interia.Dto.Files;

namespace Interia.Core.Contracts.FileProviders.Factory
{
	public interface IFileProviderFactory
	{
		IFileProvider GetInstance(FileStorageEnum fileStorageEnum);
	}
}