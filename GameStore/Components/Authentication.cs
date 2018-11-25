namespace GameStore_App.Components
{
	public class Authentication
	{
		public Authentication(bool isAuthenticated, bool isAdmin)
		{
			IsAuthenticated = isAuthenticated;
			IsAdmin = isAdmin;
		}
		public bool IsAuthenticated { get; private set; }
		public bool IsAdmin { get; private set; }
	}
}
