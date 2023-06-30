using OmmelSamvirke.Domain.Common.Validators;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;

public class TextBlockData : ContentBlockData<TextBlock>
{
    public string Text { get; private set; } = null!;

    public TextBlockData(TextBlock textBlock, string text, int pageId) : base(textBlock, pageId)
    {
        Initialize(text);
    }
    
    public TextBlockData(
        int id,
        DateTime dateCreated,
        DateTime dateModified,
        TextBlock textBlock,
        string text, 
        int pageId
    ) : base(id, dateCreated, dateModified, textBlock, pageId)
    {
        Initialize(text);
    }

    private void Initialize(string text)
    {
        StringLengthValidator.Validate(text, 1, 5000);
        Text = text;
    }
}
