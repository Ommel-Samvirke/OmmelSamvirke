﻿using OmmelSamvirke.Domain.Common;

namespace OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

/// <summary>
/// This class represents a text block that can be placed on a page.
/// It extends the abstract ContentBlock class.
/// </summary>
public class TextBlock : ContentBlock
{
    /// <summary>
    /// Create a new instance of a TextBlock.
    /// This constructor should be used when the model has not yet been saved to the database.
    /// </summary>
    /// <param name="isOptional"><see cref="ContentBlock.IsOptional"/></param>
    /// <param name="desktopConfiguration"><see cref="ContentBlock.DesktopConfiguration"/></param>
    /// <param name="tabletConfiguration"><see cref="ContentBlock.TabletConfiguration"/></param>
    /// <param name="mobileConfiguration"><see cref="ContentBlock.MobileConfiguration"/></param>
    /// <param name="pageTemplate"><see cref="ContentBlock.PageTemplate"/></param>
    public TextBlock(
        bool isOptional,
        ContentBlockLayoutConfiguration desktopConfiguration,
        ContentBlockLayoutConfiguration tabletConfiguration,
        ContentBlockLayoutConfiguration mobileConfiguration,
        PageTemplate pageTemplate
    )
        : base(isOptional, desktopConfiguration, tabletConfiguration, mobileConfiguration, pageTemplate)
    {
    }

    /// <summary>
    /// Create an instance of a TextBlock that is loaded from the database.
    /// </summary>
    /// <param name="id"><see cref="BaseModel.Id"/></param>
    /// <param name="dateCreated"><see cref="BaseModel.DateCreated"/></param>
    /// <param name="dateModified"><see cref="BaseModel.DateModified"/></param>
    /// <param name="isOptional"><see cref="ContentBlock.IsOptional"/></param>
    /// <param name="desktopConfiguration"><see cref="ContentBlock.DesktopConfiguration"/></param>
    /// <param name="tabletConfiguration"><see cref="ContentBlock.TabletConfiguration"/></param>
    /// <param name="mobileConfiguration"><see cref="ContentBlock.MobileConfiguration"/></param>
    /// <param name="pageTemplate"><see cref="ContentBlock.PageTemplate"/></param>
    public TextBlock(
        int id,
        DateTime dateCreated,
        DateTime dateModified,
        bool isOptional,
        ContentBlockLayoutConfiguration desktopConfiguration,
        ContentBlockLayoutConfiguration tabletConfiguration,
        ContentBlockLayoutConfiguration mobileConfiguration,
        PageTemplate pageTemplate
    ) : base(id, dateCreated, dateModified, isOptional, desktopConfiguration, tabletConfiguration, mobileConfiguration, pageTemplate)
    {
    }
    
    /// <summary>
    /// Private constructor for EF Core.
    /// </summary>
    private TextBlock()
    {
        
    }
}
