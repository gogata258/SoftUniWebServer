using System.Collections.Concurrent;

namespace Server.HTTP
{
	public class SessionStore
	{
		public const string SessionCookieKey = "MY_SID";
		public const string SessionLoginId = "USER_ID";
		private static readonly ConcurrentDictionary<string, HttpSession> sessions = new ConcurrentDictionary<string, HttpSession>();
		public static HttpSession Get(string id) => sessions.GetOrAdd(id, x => new HttpSession(id));
	}
}
