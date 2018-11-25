using System.Text;

namespace Server.HTTP.Response
{
	using Server.Enums;
	using Server.Exceptions;
	using Server.Contracts;

	public class ViewResponse : HttpResponse
	{
		private readonly IView view;
		public ViewResponse(HttpStatusCode responseCode, IView view)
		{
			ValidateStatusCode(responseCode);
			StatusCode = responseCode;
			this.view = view;
			Headers.Add(HttpHeader.ContentType, "text/html");
		}

		private void ValidateStatusCode(HttpStatusCode codeEnum)
		{
			int code = (int)codeEnum;
			if (code >= 300 && code < 400)
				throw new InvalidResponseException("Status code invalid. Status code should not be between 300 and 399");
		}

		public override string ToString()
		{
			StringBuilder response = new StringBuilder();
			response.Append($"{base.ToString()} {System.Environment.NewLine} {view.View()}");
			return response.ToString();
		}
	}
}
