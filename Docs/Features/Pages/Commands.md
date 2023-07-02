# Pages Commands
The following describes the commands the application layer supports.

## PageTemplates
- Create a new template ✅
- Delete a template ✅
- Archive a template ❌
- Create a custom template from a page ❌
- Change status from Archived to Public ❌
- Add a ContentBlock to a template ❌
- Remove a ContentBlock from a template ❌
- Update the position of a ContentBlock in a template ❌
- Update the width and height of a ContentBlock in a template ❌
- Update the IsOptional field of a ContentBlock in a template ❌
- Add a supported layout - TODO: Rethink layouts ❌
- Remove a supported layout - TODO: Rethink layouts ❌

## Page
- Create a new page from a template ❌
- Update the name of a page ❌
- Delete a page ❌
- Update the Data of a ContentBlockData element 
  (Probably needs an update action for each type) ❌
- Save a page: Updates should not be saved immediately, a copy of the page 
  should be saved each time a ContentBlockData element is edited. 
  Only when a "Save" button is clicked, should the temporary
  page overwrite the original page. ❌
- Add page to history: Save all published versions of a page in a table ❌