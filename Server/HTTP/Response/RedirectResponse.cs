namespace Server.HTTP.Response
{
	using Enums;
	public class RedirectResponse : HttpResponse
	{
		public RedirectResponse(string redirectUrl)
		{
			CustomValidator.ThrowIfNullOrEmpty(redirectUrl, nameof(redirectUrl));
			StatusCode = HttpStatusCode.Found;
			AddHeader(HttpHeader.Location, redirectUrl);
		}
	}
}
