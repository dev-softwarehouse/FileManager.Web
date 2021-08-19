using System.Collections.Generic;
using System.IO;

namespace Interia.Core.Helper
{
	public class FileHelper
	{
		/// <summary>
		/// Wraps known mime types
		/// </summary>
		public static Dictionary<string, string> MimeTypes =>
			new()
			{
				{".txt", "text/plain"},
				{".doc", "application/vnd.ms-word"},
				{".pdf", "application/pdf"},
				{".xls", "application/vnd.ms-excel"},
				{".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
				{".png", "image/png"},
				{".docx", "application/vnd.ms-word"},
				{".jpg", "image/jpeg"},
				{".jpeg", "image/jpeg"},
				{".gif", "image/gif"},
				{".csv", "text/csv"},
				{".json", "application/json"},
				{".cs", "text/plain"},
				{".trdp", "text/plain"},
				{".jsx", "application/x-javascript"}
			};

		/// <summary>
		/// Get Mimetype based on file name
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public static string GetContentType(string path)
		{
			return MimeTypes[Path.GetExtension(path).ToLowerInvariant()];
		}
	}
}