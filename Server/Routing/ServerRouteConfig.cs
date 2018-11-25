using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Server.Routing
{
	using Contracts;
	using Server.Enums;
	public class ServerRouteConfig : IServerRouteConfig
	{
		public IDictionary<HttpRequestMethod, IDictionary<string, IRoutingContext>> Routes { get; }
		public ICollection<string> AnonymousPaths { get; }

		public ServerRouteConfig(IAppRouteConfig config)
		{
			CustomValidator.ThrowIfNull(config, nameof(config));

			Routes = new Dictionary<HttpRequestMethod, IDictionary<string, IRoutingContext>>();
			AnonymousPaths = new List<string>(config.AnonymousPaths);

			InitializeServerConfig(config);
		}

		private void InitializeServerConfig(IAppRouteConfig config)
		{
			foreach (var pair in config.Routes)
			{
				Routes.Add(pair.Key, new Dictionary<string, IRoutingContext>());
				foreach (var handler in pair.Value)
				{
					List<string> args = new List<string>();
					string parsedRegex = ParseRoute(handler.Key, args);
					IRoutingContext routingContext = new RoutingContext(handler.Value, args);
					Routes[pair.Key].Add(parsedRegex, routingContext);
				}
			}
		}

		private string ParseRoute(string key, List<string> list)
		{
			StringBuilder parsedRegex = new StringBuilder();
			parsedRegex.Append("^");

			if (key == "/")
			{
				parsedRegex.Append($"{key}$");
				return parsedRegex.ToString();
			}
			string[] tokens = key.Split('/');
			ParseTokens(list, tokens, parsedRegex);

			return parsedRegex.ToString();
		}
		private void ParseTokens(List<string> list, string[] tokens, StringBuilder parsedRegex)
		{
			for (int idx = 0; idx < tokens.Length; idx++)
			{
				string end = idx == tokens.Length - 1 ? "$" : "/";
				if(!tokens[idx].StartsWith('{') && !tokens[idx].EndsWith('}'))
				{
					parsedRegex.Append($"{tokens[idx]}{end}");
					continue;
				}

				Regex rex = new Regex("<\\w+>");
				Match match = rex.Match(tokens[idx]);

				if (!match.Success) continue;

				string paramName = match.Groups[0].Value.Substring(1, match.Groups[0].Length - 2);
				list.Add(paramName);
				parsedRegex.Append($"{tokens[idx].Substring(1, tokens[idx].Length - 2)}{end}");
			}
		}
	}
}