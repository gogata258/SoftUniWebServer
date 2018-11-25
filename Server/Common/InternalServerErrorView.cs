using System;

namespace Server.Common
{
	using Contracts;
	public class InternalServerErrorView : IView
	{
		private readonly Exception exception;
		public InternalServerErrorView(Exception exception)
		{
			this.exception = exception;
		}
		public string View()
		{
			return $"<h2>{exception.Message}</h2><h4>{exception.StackTrace}</h4>";
		}
	}
}
