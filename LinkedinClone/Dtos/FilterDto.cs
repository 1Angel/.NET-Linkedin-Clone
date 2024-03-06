namespace LinkedinClone.Dtos
{
    public class FilterDto
    {
        public string? title { get; set; }
        public int PageSize = 12;
        public int PageNumber { get; set; } = 1;
    }
}
