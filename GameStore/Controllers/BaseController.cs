using System;

namespace GameStore_App.Controllers
{
	using Components;
	using Server.Abstractions;
	using Server.HTTP;
	using Server.HTTP.Contracts;
	using Services;
	using Services.Contracts;
	using ViewModel;
	public class BaseController : ControllerBase
	{
		protected override string ApplicationDirectory => Environment.CurrentDirectory;
		private readonly IUserService service;
		public BaseController(IHttpRequest request)
		{
			service = new UserService();
			ApplyAuthentication(request);
			if (!request.Session.Contains(Cart.CartSessionKey)) request.Session.Add(Cart.CartSessionKey, new Cart());
		}
		public Authentication Authentication { get; private set; }
		private void ApplyAuthentication(IHttpRequest request)
		{
			string anonymousDisplay = "flex";
			string authDisplay = "none";
			string adminDisplay = "none";

			int? authenticatedId;
			if (!request.Session.Contains(SessionStore.SessionLoginId)) authenticatedId = null;
			else authenticatedId = request.Session.Get<int>(SessionStore.SessionLoginId);

			if (authenticatedId != null)
			{
				anonymousDisplay = "none";
				authDisplay = "flex";
				var isAdmin = service.CheckAdminStatus(authenticatedId.Value);
				if (isAdmin) adminDisplay = "inline-block";
				Authentication = new Authentication(true, isAdmin);
			}
			else Authentication = new Authentication(false, false);

			ViewData["anonDisplay"] = anonymousDisplay;
			ViewData["authDisplay"] = authDisplay;
			ViewData["adminDisplay"] = adminDisplay;
		}
		public bool CheckAdmin(IHttpRequest request)
		{
			if (request.Session.Contains(SessionStore.SessionLoginId))
				if (service.CheckAdminStatus(request.Session.Get<int>(SessionStore.SessionLoginId)))
					return true;
			return false;
		}
	}
}
