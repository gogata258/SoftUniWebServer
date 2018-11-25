using System;

namespace Server.HTTP.Response
{
	using Common;
	using Enums;

	public class InternalServerErrorResonse : ViewResponse
	{
		public InternalServerErrorResonse(Exception e) : base(HttpStatusCode.InternalServerError, new InternalServerErrorView(e))
		{
		}
	}
}
