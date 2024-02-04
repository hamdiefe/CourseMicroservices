using System.ComponentModel.DataAnnotations;

namespace Course.Web.Models
{
	public class SignInInput
	{
		[Required]
		public string Email { get; set; }

        [Required]
        public string Password { get; set; }

		public bool RememberMe { get; set; }
	}
}
