
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;

namespace Airline.Models
{
	internal class ВходMetadata
	{
		[StringLength(50)]
		[Display(Name = "Логин")]
		[RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
		[DataType(DataType.EmailAddress)]
		public string login;

		[StringLength(50)]
		[Display(Name = "Пароль")]
		[DataType(DataType.Password)]
		public string password;


	}

	internal class ЗаявкаMetadata
	{
		[DataType(DataType.Currency)]
		public decimal Сумма;


		[Display(Name = "Количество мест")]
		[Range(1, 10, ErrorMessage = "Больше 10 мест за раз бронировать нельзя")]
		public int Количество_мест;
	}

	internal class ПолетMetadata
	{

		[DataType(DataType.DateTime)]
		public System.DateTime Дата;
	}
}