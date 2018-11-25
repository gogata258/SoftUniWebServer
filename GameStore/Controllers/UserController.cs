using System;
using System.Text.RegularExpressions;

namespace GameStore_App.Controllers
{
	using Server.HTTP;
	using Server.HTTP.Contracts;
	using Server.HTTP.Response;
	using Services;
	using Services.Contracts;
	using ViewModel;
	using ViewModel.Account;

	public class UserController : BaseController
	{
		private readonly string LoginView = @"/Account/Login";
		private readonly string RegisterView = @"/Account/Register";
		private readonly string RegisterErrorView = @"/Account/RegisterError";
		IUserService service;
		private bool isRedirecting = false;
		public UserController(IHttpRequest request) : base(request)
		{
			service = new UserService();
		}
		internal IHttpResponse RegisterPost(IHttpRequest request)
		{
			ErrorCheckData(request, "fullname");
			ErrorCheckData(request, "email");
			ErrorCheckData(request, "password");
			ErrorCheckData(request, "passwordRepeat");

			if (!IsRegistrationInputValid(request) || (request.FormData["password"] != request.FormData["passwordRepeat"]))
				return FileViewResponse(RegisterErrorView);

			RegisterUserViewModel user = new RegisterUserViewModel(request.FormData["email"], request.FormData["password"], request.FormData["passwordRepeat"], request.FormData["fullname"]);

			if (service.Register(user).Result == true) return LoginPost(request, new LoginUserViewModel(user));
			else return FileViewResponse(RegisterErrorView);
		}
		internal IHttpResponse LogoutGet(IHttpRequest context)
		{
			context.Session.Clear();
			return RedirectResponse("/");
		}
		internal IHttpResponse LoginGet(IHttpRequest request)
		{
			SetDefaultViewData(CheckUserSession(request));
			return FileViewResponse(LoginView);
		}
		internal IHttpResponse RegisterGet(IHttpRequest request)
		{
			return FileViewResponse(RegisterView);
		}

		internal IHttpResponse LoginPost(IHttpRequest request, bool v)
		{
			isRedirecting = true;
			return LoginPost(request);
		}

		internal IHttpResponse LoginPost(IHttpRequest request)
		{
			ErrorCheckData(request, "email");
			ErrorCheckData(request, "password");

			LoginUserViewModel user = new LoginUserViewModel(request.FormData["email"], request.FormData["password"]);
			return LoginPost(request, user);
		}
		private IHttpResponse LoginPost(IHttpRequest request, LoginUserViewModel user)
		{
			if (service.Login(user))
			{
				SaveUserInSession(request, service.GetUserId(user.Email));
				if(isRedirecting) return new ShoppingController(request).CheckoutPost(request);
				return new RedirectResponse("/");
			}
			else return RedirectResponse("/login");
		}
		#region Helper Methods
		private void SaveUserInSession(IHttpRequest request, int Id)
		{
			request.Session.Add(SessionStore.SessionLoginId, Id);
		}
		private void CleatSessionFromUser(IHttpRequest request)
		{
			request.Session.Remove(SessionStore.SessionLoginId);
		}
		bool CheckUserSession(IHttpRequest request)
		{
			return request.Session.Contains(SessionStore.SessionLoginId);
		}
		int GetUserIdFromSession(IHttpRequest request)
		{
			return (int)request.Session.Get(SessionStore.SessionLoginId);
		}
		#region Registration Input Validationprivate 
		bool IsRegistrationInputValid(IHttpRequest request)
		{
			bool isValid = true;
			if (!IsMailValid(request.FormData["email"])) isValid = false;
			if (!IsFullNameValid(request.FormData["fullname"])) isValid = false;
			if (!IsPasswordValid(request.FormData["password"])) isValid = false;
			if (!IsPasswordValid(request.FormData["passwordRepeat"])) isValid = false;

			return isValid;
		}
		private bool IsPasswordValid(string password)
		{
			string pattern = @"(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,}";
			Regex regex = new Regex(pattern);
			return regex.IsMatch(password);
		}
		bool IsFullNameValid(string name)
		{
			string pattern = @"^[a-zA-Z -.]+$";
			Regex regex = new Regex(pattern);
			return regex.IsMatch(name);
		}
		bool IsMailValid(string emailaddress)
		{
			try
			{
				System.Net.Mail.MailAddress m = new System.Net.Mail.MailAddress(emailaddress);
				return true;
			}
			catch (FormatException)
			{
				return false;
			}
		}
		#endregion
		#endregion
	}
}
