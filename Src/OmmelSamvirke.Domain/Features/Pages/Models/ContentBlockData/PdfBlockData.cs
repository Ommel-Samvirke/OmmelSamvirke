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
    public string PdfUrl { get; set; } = string.Empty;
}
