namespace BlogApi.Models.DTOs.Report;

public class ReportDto
{
    public string Reporter { get; set; } // şikayeti eden kiş
    public string Comment { get; set; } // yorum
    public string Reason { get; set; } // şikayeti
}