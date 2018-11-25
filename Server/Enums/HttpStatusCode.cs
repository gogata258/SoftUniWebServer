namespace Server.Enums
{
	public enum HttpStatusCode
	{
		OK = 200,
		MovedPermanently = 301,
		Found = 302,
		MovedTemporarily = 303,
		BadRequest = 400,
		Unathorized = 401,
		NotFound = 404,
		InternalServerError = 500,
	};
}
