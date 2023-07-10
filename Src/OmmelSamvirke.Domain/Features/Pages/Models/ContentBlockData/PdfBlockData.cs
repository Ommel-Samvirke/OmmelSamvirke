using OmmelSamvirke.Domain.Common;
using OmmelSamvirke.Domain.Common.Validators;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;

/// <summary>
/// This class represents the data associated with a <see cref="PdfBlock"/>. 
/// It extends the abstract <see cref="ContentBlockData{T}"/> class.
/// </summary>
public class PdfBlockData : ContentBlockData<PdfBlock>
{
    /// <summary>
    /// The URL of the PDF file for the block. Must be 1-2000 characters long.
    /// </summary>
    public string PdfUrl { get; private set; } = null!;

    /// <summary>
    /// Create a new instance of a PdfBlockData.
    /// This constructor should be used when the model has not yet been saved to the database.
    /// </summary>
    /// <param name="pdfBlock"><see cref="ContentBlock"/></param>
    /// <param name="pdfUrl"><see cref="PdfUrl"/></param>
    /// <param name="pageId"><see cref="ContentBlockData{T}.PageId"/></param>
    public PdfBlockData(PdfBlock pdfBlock, string pdfUrl, int pageId) : base(pdfBlock, pageId)
    {
        Initialize(pdfUrl);
    }
    
    /// <summary>
    /// Create an instance of a PdfBlockData that is loaded from the database.
    /// </summary>
    /// <param name="id"><see cref="BaseModel.Id"/></param>
    /// <param name="dateCreated"><see cref="BaseModel.DateCreated"/></param>
    /// <param name="dateModified"><see cref="BaseModel.DateModified"/></param>
    /// <param name="pdfBlock"><see cref="ContentBlock"/></param>
    /// <param name="pdfUrl"><see cref="PdfUrl"/></param>
    /// <param name="pageId"><see cref="ContentBlockData{T}.PageId"/></param>
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
