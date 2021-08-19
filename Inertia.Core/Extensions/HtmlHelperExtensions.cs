using System.Linq;
using Interia.Core.Helper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;

namespace Interia.Core.Extensions
{
	/// <summary>
	/// Provides additional helpers to be used from razor pages
	/// </summary>
	public static class HtmlHelperExtensions
	{
		/// <summary>
		/// Read and convert allowed file types as file filter
		/// </summary>
		/// <param name="helper"></param>
		/// <returns></returns>
		public static string GetAcceptTypes(this IHtmlHelper helper)
		{
			var options = ConfigurationExtensions.Configuration.GetValue<string>("FileUploadSettings:AllowedFileTypes");

			if (string.IsNullOrEmpty(options)) return "*.*";
			return options
				.Replace("*","")
				.Split(',')
				.Select(opt => FileHelper.MimeTypes[opt.Trim()])
				.Aggregate((a, b) => $"{a},{b}");
		}
	}
}
