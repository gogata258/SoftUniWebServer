namespace Server.HTTP.Contracts
{
	public interface IHttpContext
	{
		IHttpRequest Request { get; }
	}
}
