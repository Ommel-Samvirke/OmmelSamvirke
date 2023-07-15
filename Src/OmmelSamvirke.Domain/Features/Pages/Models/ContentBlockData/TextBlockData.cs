using OmmelSamvirke.Domain.Common;
using OmmelSamvirke.Domain.Common.Validators;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;

/// <summary>
/// This class represents the data associated with a <see cref="TextBlock"/>. 
/// It extends the abstract <see cref="ContentBlockData{T}"/> class.
/// </summary>
public class TextBlockData : ContentBlockData<TextBlock>
{
    /// <summary>
    /// The text content for the text block.
    /// </summary>
    public string Text { get; set; } = null!;

    /// <summary>
    /// Create a new instance of a TextBlockData.
    /// This constructor should be used when the model has not yet been saved to the database.
    /// </summary>
    /// <param name="textBlock"><see cref="ContentBlock"/></param>
    /// <param name="text"><see cref="Text"/></param>
    /// <param name="page"><see cref="ContentBlockData{T}.Page"/></param>
    public TextBlockData(TextBlock textBlock, string text, Page page) : base(textBlock, page)
    {
        Initialize(text);
    }
    
    /// <summary>
    /// Create an instance of a TextBlockData that is loaded from the database.
    /// </summary>
    /// <param name="id"><see cref="BaseModel.Id"/></param>
    /// <param name="dateCreated"><see cref="BaseModel.DateCreated"/></param>
    /// <param name="dateModified"><see cref="BaseModel.DateModified"/></param>
    /// <param name="textBlock"><see cref="ContentBlock"/></param>
    /// <param name="text"><see cref="Text"/></param>
    /// <param name="page"><see cref="ContentBlockData{T}.Page"/></param>
    public TextBlockData(
        int id,
        DateTime dateCreated,
        DateTime dateModified,
        TextBlock textBlock,
        string text, 
        Page page
    ) : base(id, dateCreated, dateModified, textBlock, page)
    {
        Initialize(text);
    }

    private void Initialize(string text)
    {
        StringLengthValidator.Validate(text, 1, 5000);
        Text = text;
    }
    
    /// <summary>
    /// Private constructor for EF Core.
    /// </summary>
    private TextBlockData()
    {
        
    }
}
