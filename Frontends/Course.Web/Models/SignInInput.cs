namespace Course.Web.Models
{
	public class SignInInput
	{
		public string Email { get; set; }

		public string Password { get; set; }

		public bool RememberMe { get; set; }
	}
}
