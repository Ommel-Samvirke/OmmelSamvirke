using OmmelSamvirke.Domain.Features.Pages.Enums;

namespace OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

/// <summary>
/// This class represents a PDF block that can be placed on a page. 
/// It extends the abstract ContentBlock class.
/// </summary>
public class PdfBlock : ContentBlock
{
    public string PdfUrl { get; set; } = string.Empty;
    public ContentBlockType ContentBlockType { get; set; } = ContentBlockType.PdfBlock;
}