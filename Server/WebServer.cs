using System;
using System.Net.Sockets;
using System.Net;

namespace Server
{
	using System.Threading.Tasks;
	using Server.Routing;
	using Server.Routing.Contracts;
	using Server.Contracts;
	public class WebServer : IRunnable
	{
		private readonly int port;
		private readonly IServerRouteConfig serverRouteConfig;
		private readonly TcpListener tcpListener;
		private bool isRunning;

		public WebServer(int port, IAppRouteConfig config)
		{
			this.port = port;
			tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
			serverRouteConfig = new ServerRouteConfig(config);
		}

		public void Run()
		{
			tcpListener.Start();
			isRunning = true;
			Console.WriteLine($"Server started. Listening to TCP client @127.0.0.1:{port}");
			Task task = Task.Run(ListenLoop);
			task.Wait();
		}

		private async Task ListenLoop()
		{
			while(isRunning)
			{
				Socket client = await tcpListener.AcceptSocketAsync();
				ConnectionHandler connectionHandler = new ConnectionHandler(client, serverRouteConfig);
				Task connection = connectionHandler.ProcessRequestAsync();
				connection.Wait();
			}
		}
	}
}
