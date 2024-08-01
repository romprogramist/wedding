namespace RSiteTemplate.Models.Dtos;

public class TelegramDto
{
    public IEnumerable<string> ChatIds { get; set; } = new List<string>();
    public string Message { get; set; } = string.Empty;
}