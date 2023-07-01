# PageTemplate
- Has an id
- Has a name
- Has a list of supported layouts (Desktop, tablet, mobile)
- Contains a collection of blocks
- Cannot be deleted if a page implements the template, instead it is archived
- Can be in the following states:
  - Public: Can be used to create a page
  - Custom: A custom template is created if a page dynamically alters the structure of a template
  - Archived: The template has been deleted, but there are still active pages that implement the template

# ContentBlock
- Has an id
- Abstract class representing a block of content
- Can be optional. A page might not implement an optional block
- Contains X & Y positions of top left corner
- Has a width and an optional height. Both must be defined in grid values
- Can be implemented by many ContentBlockData entities

## ContentBlock interfaces
- IContentBlockFontCustomizable
- IContentBlockColorCustomizable
- IContentBlockTextAlignmentCustomizable
- IContentBlockEmbeddable

## ContentBlock Implementations
- HeadlineBlock
- TextBlock
- ImageBlock
- PDFBlock
- VideoBlock
- SlideshowBlock

# Page
- Has an id
- Has a name, which could be inferred from first title block.
However, it should always be customizable.
- Must be sharable on Facebook
  - Automatic generation of title
  - Automatic generation of summary
  - Automatic generation of image
  - All automatically generated fields must be customizable by user

# ContentBlockData
- Has an id
- Contains the data of a ContentBlock
- A ContentBlockData entity implements 1 ContentBlock via an id reference
- For each ContentBlock class there should be a ContentBlockData class

# General
It should be possible to embed other blocks in a text block. 
This could be done by dynamically creating a custom template on the backend.
This custom template should not be available as a "Public" template.
Here a challenge would be how to render the page editor in the same way when
editing the page as when creating a new page.
