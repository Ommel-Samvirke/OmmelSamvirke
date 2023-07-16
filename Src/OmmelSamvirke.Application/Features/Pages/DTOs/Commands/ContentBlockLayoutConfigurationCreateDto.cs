﻿using OmmelSamvirke.Domain.Common;

namespace OmmelSamvirke.Application.Features.Pages.DTOs.Commands;

public class ContentBlockLayoutConfigurationCreateDto : BaseModel
{
    public int XPosition { get; set; }
    public int YPosition { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }

    public ContentBlockLayoutConfigurationCreateDto(
        int xPosition,
        int yPosition,
        int width,
        int height
    )
    {
        XPosition = xPosition;
        YPosition = yPosition;
        Width = width;
        Height = height;
    }
    
    /// <summary>
    /// Needed for deserialization
    /// </summary>
    public ContentBlockLayoutConfigurationCreateDto()
    {
        
    }
}
