using System;
using System.Collections.Generic;
using System.Text;

namespace Server.HTTP.Response
{
	public class BadRequestResponse : HttpResponse
	{
		public BadRequestResponse()
		{
			StatusCode = Enums.HttpStatusCode.BadRequest;
		}
	}
}
