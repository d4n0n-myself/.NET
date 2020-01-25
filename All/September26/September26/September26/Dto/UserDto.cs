using System.ComponentModel.DataAnnotations;

namespace September26.Dto
{
	public class UserDto
	{
		[Display(Name = "alert")]
		public string FullName { get; set; }
		
		[Display(Name = "retatd")]
		public string Email { get; set; }

		public string OrganisationName { get; set; }
	}
}