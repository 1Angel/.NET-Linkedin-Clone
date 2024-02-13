namespace LinkedinClone.Dtos.User
{
    public class AuthResponseDto
    {
        public string? Message { get; set; }
        public int? Status {  get; set; }
        public string? JwtToken { get; set; }
    }
}
