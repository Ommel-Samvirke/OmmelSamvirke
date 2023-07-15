namespace OmmelSamvirke.Application.Features.Pages.DTOs.Queries.ContentBlockData;

public class PdfBlockDataQueryDto : ContentBlockDataQueryDto
{
    public string PdfUrl { get; set; }
    public PdfBlockDataQueryDto(
        int id,
        DateTime createdDate,
        DateTime modifiedDate,
        ContentBlockQueryDto contentBlock,
        string pdfUrl,
        PageQueryDto page
    ) : base(id, createdDate, modifiedDate, contentBlock, page)
    {
        PdfUrl = pdfUrl;
    }

    /// <summary>
    /// Needed for deserialization
    /// </summary>
    public PdfBlockDataQueryDto()
    {
        
    }
}