# Pages Commands
The following describes the commands the application layer supports. 
All updates use the strategy optimistic locking.

## PageTemplates
- Create a new template ✅
- Delete a template ✅
  - Before deleting a template, check if any pages implement it ✅
  - Perform the deletion action ✅
- Archive a template ✅
- Change status from Archived to Public ✅
- Create a custom template from a page ✅
- Add a ContentBlock to a template ✅
- Remove a ContentBlock from a template ✅
- Update a ContentBlock in a template ✅
- Update PageTemplate by overwriting the original template with a temporary template ✅
- Save a version of a PageTemplate ✅

## Page
- Create a new page from a template ✅
- Update the name of a page ✅
- Delete a page ✅
- Save a page ✅
- Add page to history: Save all published versions of a page in a table ✅
- Create a new ContentBlockData element for each ContentBlock in a page when the page is created ✅

## ContentBlockData
- Update the Data of a ContentBlockData element 
  - HeadlineBlockData ❌
  - ImageBlockData ❌
  - PdfBlockData ❌
  - SlideshowBlockData ❌
  - TextBlockData ❌
  - VideoBlockData ❌

## For later
- Delete ContentBlocks associated with a template, when the template is deleted ❌
- Delete ContentBlockData elements associated with a page, when the page is deleted ❌
- Check if Admin has an un-submitted temporary template ❌
- Share a page on Facebook ❌
- Maybe add a timeline/wall component ❌ 