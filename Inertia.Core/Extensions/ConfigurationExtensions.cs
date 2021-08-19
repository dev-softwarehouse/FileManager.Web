using Microsoft.Extensions.Configuration;

namespace Interia.Core.Extensions
{
	/// <summary>
	/// Wraps configuration object so that it can be used in static contexts
	/// </summary>
	public class ConfigurationExtensions
	{
		public static IConfiguration Configuration { get; set; }
		public static int MaxAllowedFileSize => Configuration.GetValue<int>("MaxAllowedFileSize");
		public static string AllowedExtensions => Configuration.GetValue<string>("FileUploadSettings:AllowedFileTypes");
	}
}