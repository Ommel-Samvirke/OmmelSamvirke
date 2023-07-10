using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Domain.Features.Pages.Interfaces.Services;

public interface IPdfBlockDataService
{
    Task<PdfBlockData> CreatePdfBlockData(PdfBlock pdfBlock, string pdfUrl, int pageId);
    Task<PdfBlockData> UpdatePdfBlockData(int id, PdfBlock pdfBlock, string pdfUrl, int pageId);
    Task<bool> DeletePdfBlockData(int id);
}
