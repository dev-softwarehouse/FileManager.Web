using Interia.Core.Validations;
using Microsoft.AspNetCore.Http;

namespace Interia.FileManager.Web.Models
{
	public class FileInputModel
	{
		[AllowedExtensions, ValidateMimeType, MaxFileSize]
		public IFormFile File { get; set; }
	}
}