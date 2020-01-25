using System;
using System.ComponentModel.DataAnnotations;

namespace September26.Dto
{
	public class User
	{
		public string FullName => $"{FirstName} {LastName}";

		[DataType(DataType.Currency)]
		public int Number { get; set; }
		
		public bool Alert { get; set; }

		public TypeCode Code { get; set; }
		
		public Organisation Organisation { get; set; }
		
		public string FirstName { get; set; }
		
		public string LastName { get; set; }
		
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
		
		public DateTime BirthDate { get; set; }
		
		[DataType(DataType.PhoneNumber)]
		public string PhoneNumber { get; set; }

		[Display(Name = "ALERT", Description = "retard")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		
		[Display(Description = "Hello")]
		[DataType(DataType.Time)]
		public DateTime RegisterTime { get; set; }
	}

	public class Organisation
	{
		public string Name { get; set; }

		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
	}
}