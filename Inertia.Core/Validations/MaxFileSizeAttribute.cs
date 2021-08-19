using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using ConfigurationExtensions = Interia.Core.Extensions.ConfigurationExtensions;

namespace Interia.Core.Validations
{
	/// <summary>
	/// Validates uploaded file for max size. This is used to avoid DOS attacks
	/// </summary>
	public class MaxFileSizeAttribute : ValidationAttribute
	{
		private readonly int _maxFileSize;
		public MaxFileSizeAttribute()
		{
			_maxFileSize = ConfigurationExtensions.MaxAllowedFileSize;
		}

		protected override ValidationResult IsValid(
			object value, ValidationContext validationContext)
		{
			if (value is IFormFile file)
			{
				if (file.Length > _maxFileSize)
				{
					return new ValidationResult(GetErrorMessage());
				}
			}

			return ValidationResult.Success;
		}

		public string GetErrorMessage()
		{
			return $"Maximum allowed file size is { _maxFileSize} bytes.";
		}
	}
}