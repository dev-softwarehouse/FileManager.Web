using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FileSignatures;
using Interia.Core.Extensions;
using Microsoft.AspNetCore.Http;

namespace Interia.Core.Validations
{
	/// <summary>
	/// Validates mime types based on file contents. Still a work in progress . Mitigates XSRF/CSS
	/// </summary>
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public class ValidateMimeTypeAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(
			object value, ValidationContext validationContext)
		{
			var inspector = (IFileFormatInspector)validationContext
				.GetService(typeof(IFileFormatInspector));

			var file = value as IFormFile;
			var isValid = true;
			using var stream = file.OpenReadStream();
			var extension = inspector.DetermineFileFormat(stream)?.Extension;

			var allowedExtensions = ConfigurationExtensions.AllowedExtensions
				.Replace("*", "")
				.Split(',').Select(x=> x.Trim().Replace(".","")).ToList();
			if (extension != null && !allowedExtensions.Contains(extension))
				isValid = false;

			return isValid ? ValidationResult.Success : new ValidationResult(GetErrorMessage());
		}

		public string GetErrorMessage()
		{
			return "Danger! This type of file is not allowed on server.";
		}
	}
}
