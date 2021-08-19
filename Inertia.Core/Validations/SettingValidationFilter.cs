using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using IValidatableObject = Interia.Core.Contracts.Validations.IValidatableObject;

namespace Interia.Core.Validations
{
	/// <summary>
	/// Validates if required settings are present
	/// </summary>
	public class SettingValidationFilter : IStartupFilter
	{
		private readonly IEnumerable<IValidatableObject> _validatableObjects;

		public SettingValidationFilter(IEnumerable<IValidatableObject> validatableObjects)
		{
			_validatableObjects = validatableObjects;
		}

		public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
		{
			foreach (var validatableObject in _validatableObjects) 
				validatableObject.Validate();

			return next;
		}
	}
}