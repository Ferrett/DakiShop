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
			if (DBService.IsLoginTaken(login))
				return Tuple.Create(false, "Этот логин уже занят");

			if (DBService.IsEmailTaken(email))
				return Tuple.Create(false, "Эта почта уже занята");

			if (string.IsNullOrWhiteSpace(password))
				return Tuple.Create(false, "Некорректный пароль");

			if (string.IsNullOrWhiteSpace(login))
				return Tuple.Create(false, "Некорректный логин");

			if (!ValidateEmail(email))
				return Tuple.Create(false, "Некорректная почта");

			if (password.Length < MinPasswordLength)
				return Tuple.Create(false, $"Пароль должен быть минимум {MinPasswordLength} символов в длинну");

			if (login.Length < MinLoginLength)
				return Tuple.Create(false, $"Логин должен быть минимум {MinLoginLength} символов в длинну");

			if (!password.Equals(passwordConfirm))
				return Tuple.Create(false, "Пароли не совпадают");


			return Tuple.Create(true, "Регистрация прошла успешно");
		}
		public static Tuple<bool, string> ValidateLogIn(string login, string password)
		{
			if (string.IsNullOrWhiteSpace(password))
				return Tuple.Create(false, "Некорректный пароль");

			if (string.IsNullOrWhiteSpace(login))
				return Tuple.Create(false, "Некорректный логин");

			if (password.Length < MinPasswordLength)
				return Tuple.Create(false, $"Пароль должен быть минимум {MinPasswordLength} символов в длинну");

			if (login.Length < MinLoginLength)
				return Tuple.Create(false, $"Логин должен быть минимум {MinLoginLength} символов в длинну");

			if (!DBService.IsUserExists(login))
				return Tuple.Create(false, "Пользователь не найден");

			if (!DBService.UserLogIn(login, password))
				return Tuple.Create(false, "Неправильный пароль");

			return Tuple.Create(true, "Вход прошёл успешно");
		}

		public static bool ValidateEmail(string email)
		{
			return new EmailAddressAttribute().IsValid(email) ? true : false;
		}

		public static Tuple<bool, string> ValidateNewCategory(string name)
        {
			if (string.IsNullOrWhiteSpace(name))
				return Tuple.Create(false, "Некорректное название");

			if (DBService.IsCategoryExists(name))
				return Tuple.Create(false, "Категория с таким названием уже есть");

			return Tuple.Create(true, "Категория добавлена успешно");
		}
	}
}
