using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace Server.Abstractions
{
	using Enums;
	using HTTP.Contracts;
	using HTTP.Response;

	public abstract class ControllerBase
	{
		public const string DefaultPath = @"{0}\Resources\Html\{1}.html";
		public const string ContentPlaceholder = "{{{content}}}";
		protected IDictionary<string, string> ViewData { get; private set; }
		protected abstract string ApplicationDirectory { get; }
		protected virtual void SetDefaultViewData(bool isAuthenticated)
		{
			if (isAuthenticated) ViewData["authDisplay"] = "flex";
			else ViewData["authDisplay"] = "none";
		}
		protected void ErrorCheckData(IHttpRequest request, string paramName)
		{
			if (!request.FormData.ContainsKey(paramName))
				ShowError($"Product information is not valid. {paramName}");
		}
		protected ControllerBase()
		{
			ViewData = new Dictionary<string, string>
			{
				["anonDisplay"] = "none",
				["authDisplay"] = "flex",
				["showError"] = "none"
			};
		}
		protected IHttpResponse FileViewResponse(string fileName)
		{
			var result = ProcessFileHtml(fileName);

			if (ViewData.Any()) foreach (var value in ViewData.Reverse())
					result = result.Replace($"{{{{{{{value.Key}}}}}}}", value.Value);
			return new ViewResponse(HttpStatusCode.OK, new FileView(result));
		}

		protected IHttpResponse RedirectResponse(string route) => new RedirectResponse(route);

		protected void ShowError(string errorMessage)
		{
			ViewData["showError"] = "block";
			ViewData["error"] = errorMessage;
		}

		protected bool ValidateModel(object model)
		{
			var results = new List<ValidationResult>();

			if (Validator.TryValidateObject(model, new ValidationContext(model), results, true) == false)
				foreach (ValidationResult result in results)
					if (result != ValidationResult.Success)
					{
						ShowError(result.ErrorMessage);
						return false;
					}
			return true;
		}

		private string ProcessFileHtml(string fileName)
		{
			string layoutHtml = File.ReadAllText(string.Format(DefaultPath, ApplicationDirectory, "Layout"));
			string fileHtml = File.ReadAllText(string.Format(DefaultPath, ApplicationDirectory, fileName));
			return layoutHtml.Replace(ContentPlaceholder, fileHtml);
		}
	}
}
