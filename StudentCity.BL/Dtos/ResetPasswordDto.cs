namespace StudentCity.BL.Dtos
{
    public class ResetPasswordDto
    {
        public string Code { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

}