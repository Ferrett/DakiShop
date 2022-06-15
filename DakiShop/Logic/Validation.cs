using DakiShop.Logic;
using System.ComponentModel.DataAnnotations;

namespace RaportBlazorServer.Logic
{
	public static class Validation
	{
		static readonly int MinPasswordLength = 8;
		static readonly int MinLoginLength = 5;

		public static Tuple<bool, string> ValidateRegistration(string login, string email, string password, string passwordConfirm)
		{
			if (!password.Equals(passwordConfirm))
				return Tuple.Create(false, "Пароли не совпадают");

			if (string.IsNullOrWhiteSpace(password))
				return Tuple.Create(false, "Некорректный пароль");

			if (password.Length < MinPasswordLength)
				return Tuple.Create(false, $"Пароль должен быть минимум {MinPasswordLength} символов в длинну");

			if (login.Length < MinLoginLength)
				return Tuple.Create(false, $"Логин должен быть минимум {MinLoginLength} символов в длинну");

			if (!ValidateEmail(email))
				return Tuple.Create(false, "Некорректная почта");

			if (DBService.IsLoginTaken(login))
				return Tuple.Create(false, "Этот логин уже занят");

			if (DBService.IsEmailTaken(email))
				return Tuple.Create(false, "Эта почта уже занята");

			return Tuple.Create(true, "Регистрация прошла успешно");
		}
		public static Tuple<bool, string> ValidateLogIn(string login, string password, bool captchaIsValid)
		{
			if (!DBService.IsUserExists(login))
				return Tuple.Create(false, "Пользователь не найден");

			if (!DBService.UserLogIn(login, password))
				return Tuple.Create(false, "Неправильный пароль");

			if (!captchaIsValid)
				return Tuple.Create(false, "Слышишь ты, уёбище ебаное, пошёл нахуй отсюда");


			return Tuple.Create(true, "Вход прошёл успешно");
		}


		public static bool ValidateEmail(string email)
		{
			return new EmailAddressAttribute().IsValid(email) ? true : false;
		}
	}
}
