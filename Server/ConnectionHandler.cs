using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Text;

namespace Server
{
	using HTTP;
	using HTTP.Contracts;
	using Routing.Contracts;
	using Handlers;
	public class ConnectionHandler
	{
		private readonly Socket client;
		private readonly IServerRouteConfig serverRouteConfig;

		public ConnectionHandler(Socket client, IServerRouteConfig config)
		{
			CustomValidator.ThrowIfNull(client, nameof(client));
			CustomValidator.ThrowIfNull(config, nameof(config));

			this.client = client;
			serverRouteConfig = config;
		}

		public async Task ProcessRequestAsync()
		{
			string request = await ReadRequest();
			if(request != null)
			{
				IHttpContext httpContext = new HttpContext(request);
				IHttpResponse response = new HttpHandler(serverRouteConfig).Handle(httpContext);
				ArraySegment<byte> toBytes = new ArraySegment<byte>(Encoding.ASCII.GetBytes(response.ToString()));
				await client.SendAsync(toBytes, SocketFlags.None);

				Console.WriteLine("=========REQUEST=========");
				Console.WriteLine(request);
				Console.WriteLine("=========RESPONSE=========");
				Console.WriteLine(response.ToString());
				Console.WriteLine();
			}
			
			client.Shutdown(SocketShutdown.Both);
		}

		private async Task<string> ReadRequest()
		{
			StringBuilder request = new StringBuilder();
			ArraySegment<byte> datat = new ArraySegment<byte>(new byte[1024]);
			int numBytesRead;

			while ((numBytesRead = await client.ReceiveAsync(datat, SocketFlags.None)) > 0)
			{
				request.Append(Encoding.ASCII.GetString(datat.Array, 0, numBytesRead));
				if (numBytesRead < 1023) break;
			}

			return (request.Length == 0) ? null : request.ToString();
		}
	}
}
