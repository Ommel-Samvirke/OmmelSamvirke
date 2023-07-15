using OmmelSamvirke.Domain.Common;
using OmmelSamvirke.Domain.Common.Validators;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;
using OmmelSamvirke.Domain.ValueObjects;

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
    public Url PdfUrl { get; set; } = null!;

    /// <summary>
    /// Create a new instance of a PdfBlockData.
    /// This constructor should be used when the model has not yet been saved to the database.
    /// </summary>
    /// <param name="pdfBlock"><see cref="ContentBlock"/></param>
    /// <param name="pdfUrl"><see cref="PdfUrl"/></param>
    /// <param name="page"><see cref="ContentBlockData{T}.Page"/></param>
    public PdfBlockData(PdfBlock pdfBlock, Url pdfUrl, Page page) : base(pdfBlock, page)
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
    /// <param name="page"><see cref="ContentBlockData{T}.Page"/></param>
    public PdfBlockData(
        int id,
        DateTime dateCreated,
        DateTime dateModified,
        PdfBlock pdfBlock,
        Url pdfUrl,
        Page page
    ) : base(id, dateCreated, dateModified, pdfBlock, page)
    {
        Initialize(pdfUrl);
    }

    private void Initialize(Url pdfUrl)
    {
        StringLengthValidator.Validate(pdfUrl.Address, 1, 2000);
        PdfUrl = pdfUrl;
    }
    
    /// <summary>
    /// Private constructor for EF Core.
    /// </summary>
    private PdfBlockData()
    {
        
    }
}
