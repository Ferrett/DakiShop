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

		public static Tuple<bool, string> ValidateNewManufacturer(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
				return Tuple.Create(false, "Некорректное название");

			if (DBService.IsManufacturerExists(name))
				return Tuple.Create(false, "Производитель с таким названием уже есть");

			return Tuple.Create(true, "Производитель добавлен успешно");
		}

		public static Tuple<bool, string> ValidateNewDakimakura(string dakiNameImagePath, string dakiName, string dakiPrice, string dakiSize, string dakiFiller,bool reqNewName)
		{
			if (string.IsNullOrWhiteSpace(dakiNameImagePath))
				return Tuple.Create(false, "Некорректное изображение");

			if (string.IsNullOrWhiteSpace(dakiName))
				return Tuple.Create(false, "Некорректное имя");

			if (string.IsNullOrWhiteSpace(dakiSize))
				return Tuple.Create(false, "Некорректный размер");

			if (string.IsNullOrWhiteSpace(dakiFiller))
				return Tuple.Create(false, "Некорректный наполнитель");

			if (!int.TryParse(dakiPrice,out int i) ||int.Parse(dakiPrice)<=0)
				return Tuple.Create(false, "Некорректная цена");

			if (reqNewName)
			{
				if (DBService.IsDakimakuraExists(dakiName))
					return Tuple.Create(false, "Дакимакура с таким названием уже есть");
			}

			if (reqNewName)
				return Tuple.Create(true, "Дакимакура добавлена успешно");

			return Tuple.Create(true, "Дакимакура изменена успешно");
		}

		public static Tuple<bool, string> ValidateDeleteManufacturer(int id)
		{
			if (DBService.IsManufacturerUsed(id))
				return Tuple.Create(false, "Удаление невозможно. Производитель используется");

			return Tuple.Create(true, "Производитель успешно удалён");
		}

		public static Tuple<bool, string> ValidateDeleteCategory(int id)
		{
			if (DBService.IsCategoryUsed(id))
				return Tuple.Create(false, "Удаление невозможно. Категория используется");

			return Tuple.Create(true, "Категория успешно удалена");
		}

		public static Tuple<bool, string> ValidateDeleteDakimakura(int id)
		{
			if (!DBService.IsDakimakuraExists(id))
				return Tuple.Create(false, "Удаление невозможно. Категория используется");

			return Tuple.Create(true, "Дакимакура успешно удалена");
		}
	}
}
