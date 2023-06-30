using OmmelSamvirke.Domain.Common.Validators;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;

public class PdfBlockData : ContentBlockData<PdfBlock>
{
    public string PdfUrl { get; private set; } = null!;

    public PdfBlockData(PdfBlock pdfBlock, string pdfUrl, int pageId) : base(pdfBlock, pageId)
    {
        Initialize(pdfUrl);
    }
    
    public PdfBlockData(
        int id,
        DateTime dateCreated,
        DateTime dateModified,
        PdfBlock pdfBlock,
        string pdfUrl,
        int pageId
    ) : base(id, dateCreated, dateModified, pdfBlock, pageId)
    {
        Initialize(pdfUrl);
    }

    private void Initialize(string pdfUrl)
    {
        StringLengthValidator.Validate(pdfUrl, 1, 2000);
        PdfUrl = pdfUrl;
    }
}
