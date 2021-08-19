using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Interia.Core.Extensions;
using Microsoft.AspNetCore.Http;

namespace Interia.Core.Validations
{
	/// <summary>
	/// Validates that only allowed extensions are uploaded. This is used to avoid DOS/XSRF attacks
	/// </summary>
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public class AllowedExtensionsAttribute : ValidationAttribute
	{
		public string Extensions { get; set; } = ConfigurationExtensions.AllowedExtensions;

		protected override ValidationResult IsValid(
			object value, ValidationContext validationContext)
		{
			var file = value as IFormFile;
			var isValid = true;

			var allowedExtensions = this.Extensions
				.Replace("*","")
				.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

			if (file != null)
			{
				var fileName = file.FileName.Trim();

				isValid = allowedExtensions.Any(y => Path.GetExtension(fileName).Equals(y.Trim(),StringComparison.InvariantCultureIgnoreCase));
			}

			return isValid ? ValidationResult.Success : new ValidationResult(GetErrorMessage());
		}

		public string GetErrorMessage()
		{
			return "Danger! This type of file is not allowed on server.";
		}
	}
}