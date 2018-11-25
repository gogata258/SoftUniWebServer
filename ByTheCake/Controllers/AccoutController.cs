namespace ByTheCake_App.Controllers
{
	using Server.Abstractions;
	using Server.HTTP;
	using Server.HTTP.Contracts;
	using Server.HTTP.Response;
	using Services;
	using Services.Contracts;
	using ViewModel;
	using ViewModel.Account;

	public class AccoutController : ControllerBase
	{
		private const string RegisterView = @"Account\Register";
		private const string LoginView = @"Account\Login";
		private const string ProfileView = @"Account\Profile";
		readonly IUserService service;
		public AccoutController()
		{
			service = new UserService();
		}
		protected override string ApplicationDirectory => System.Environment.CurrentDirectory;
		internal IHttpResponse LoginGet(IHttpRequest request)
		{
			SetDefaultViewData(CheckUserSession(request));
			return FileViewResponse(LoginView);
		}
		internal IHttpResponse LoginPost(IHttpRequest request, LoginUserViewModel user)
		{
			IUserService userService = new UserService();
			if (userService.Login(user))
				SaveUserInSession(request, userService.GetUserId(user.Username));
			return new RedirectResponse("/");
		}
		internal IHttpResponse LoginPost(IHttpRequest request)
		{
			LoginUserViewModel user = new LoginUserViewModel(request.FormData["username"], request.FormData["password"]);
			return LoginPost(request, user);
		}
		internal IHttpResponse LogoutGet(IHttpRequest request)
		{
			request.Session.Remove(SessionStore.SessionLoginId);
			return new RedirectResponse("/login");
		}
		internal IHttpResponse RegisterGet(IHttpRequest request)
		{
			SetDefaultViewData(CheckUserSession(request));
			return FileViewResponse(RegisterView);
		}
		internal IHttpResponse RegisterPost(IHttpRequest request)
		{
			ErrorCheckData(request, "fullname");
			ErrorCheckData(request, "username");
			ErrorCheckData(request, "password");
			ErrorCheckData(request, "passwordRepeat");

			if (request.FormData["password"] != request.FormData["passwordRepeat"]) return new RedirectResponse("/register");

			RegisterUserViewModel user = new RegisterUserViewModel(request.FormData["username"], request.FormData["password"], request.FormData["passwordRepeat"], request.FormData["fullname"]);
			service.Register(user);
			return LoginPost(request, new LoginUserViewModel(user));
		}

		internal IHttpResponse ProfileGet(IHttpRequest request)
		{
			AccountDetailsViewModel profile = service.GetUserProfile(GetUserIdFromSession(request));

			ViewData["username"] = profile.Username;
			ViewData["registrationDate"] = profile.RegistrationDate.ToShortDateString();
			ViewData["totalOrders"] = profile.Count.ToString();

			return FileViewResponse(ProfileView);
		}

		void SaveUserInSession(IHttpRequest request, int Id)
		{
			request.Session.Add(SessionStore.SessionLoginId, Id);
			request.Session.Add(Cart.CartSessionKey, new Cart());
		}
		bool CheckUserSession(IHttpRequest request)
		{
			return request.Session.Contains(SessionStore.SessionLoginId);
		}
		int GetUserIdFromSession(IHttpRequest request)
		{
			return (int)request.Session.Get(SessionStore.SessionLoginId);
		}
		void DeleteUserSession(IHttpRequest request)
		{
			request.Session.Remove(SessionStore.SessionLoginId);
			request.Session.Remove(Cart.CartSessionKey);
		}
	}
}
