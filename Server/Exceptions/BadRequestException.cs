using System;
using System.Runtime.Serialization;

namespace Server.Exceptions
{
	[Serializable]
	internal class BadRequestException : Exception
	{
		private const string INVALID_REQUEST_EXCEPTION_MESSAGE = "Request is invalid";
		public static object ThrowFromInvalidRequest() => throw new BadRequestException(INVALID_REQUEST_EXCEPTION_MESSAGE);

		public BadRequestException()
		{
		}

		public BadRequestException(string message) : base(message)
		{
		}

		public BadRequestException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected BadRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}