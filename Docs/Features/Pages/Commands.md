# Pages Commands
The following describes the commands the application layer supports.

## PageTemplates
- Create a new template ✅
- Delete a template ✅
- Archive a template ✅
- Change status from Archived to Public ✅
- Create a custom template from a page ✅
- Add a ContentBlock to a template ✅ (Optimistic locking)
- Remove a ContentBlock from a template ✅ (Optimistic locking)
- Update a ContentBlock in a template ✅ (Optimistic locking)
- Update PageTemplate by overwriting the original template with a temporary template ❌ (Optimistic locking implementation)
- Check if Admin has an un-submitted temporary template ❌

## Page
- Create a new page from a template ❌
- Update the name of a page ❌
- Delete a page ❌
- Update the Data of a ContentBlockData element 
  (Probably needs an update action for each type) ❌
- Save a page ❌
- Save temporary page: Updates should not be saved immediately, 
  a temporary copy of the page should be saved each time a ContentBlockData element 
  is edited. Only when a "Save" button is clicked, should the temporary
  page overwrite the original page. ❌
- Add page to history: Save all published versions of a page in a table ❌
