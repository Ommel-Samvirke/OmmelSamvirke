namespace OmmelSamvirke.Application.Features.Pages.DTOs.ContentBlockData;

public class PdfBlockDataDto : ContentBlockDataDto
{
    public string PdfUrl { get; set; }
    public PdfBlockDataDto(int id, ContentBlockDto contentBlock, string pdfUrl, PageDto page) : base(id, contentBlock, page)
    {
        PdfUrl = pdfUrl;
    }
}