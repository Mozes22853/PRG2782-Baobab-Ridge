# Member 1 handover

## Scope and requirements

The user-assigned Member 1 scope is the Animal model, the shared classification logic, and Feature 1 (Add). The brief gives Add 12 marks, with shared code quality, Git, and presentation criteria assessed elsewhere. This implementation is not a claim that the whole group project or the assessment requirements are complete.

| Requirement | Implementation |
| --- | --- |
| Animal class with seven properties | `BusinessLogicLayer/Animal.cs`; ID has no setter |
| One method for both derived fields | `AnimalClassifier.Classify(Animal)` |
| Five labelled inputs and Add button | `PresentationLayer/MainForm.Designer.cs` |
| Trim inputs and uppercase ID | `AnimalValidator.TryCreateAnimal` |
| WR- and exactly four digits, unique | Validator checks format; FileHandler checks the current file case-insensitively |
| Name/species required, 1–30, no pipe | Shared validator; also rejects embedded line breaks to protect file records |
| Age/score whole numbers, 0–100 | `int.TryParse`, invariant culture, and range checks |
| Error explains field and focuses it | Persistent feedback label and `FocusInvalidField` |
| No save on invalid input | Early return before calling the file handler |
| Append one UTF-8 record, preserve existing lines | `FileHandler.TryAppendAnimal` |
| Show calculated classification | Persistent last-saved status/housing label |
| Clear fields and refresh after success | `ClearInputs` and `RefreshAnimals` |
| Friendly file errors | Handled missing folders, permissions/read-only files, and IO failures |
| Comment above every method | Application methods, constructors, and checks are documented |

## Exact classification

| Score | Status | Housing Unit |
| --- | --- | --- |
| 0–19 | Critical | Intensive Care Unit |
| 20–39 | Serious | High-Dependency Ward |
| 40–59 | Stable | Recovery Ward |
| 60–79 | Recovering | Outdoor Enclosure |
| 80–100 | Release-Ready | Pre-Release Camp |

## Structure and class examples

The structure follows the supplied Day 1 Multilayered Architecture slides (folder separation), Day 2 reading/writing and collections slides (System.IO, using blocks, lists, loops), and Day 3 FileHandler slides (models, constructors, and separate handler methods). Day 8's text-file book exercise is the closest example of saving and refreshing a DataGridView. These are conceptual references; the application code is an AI-assisted implementation for this project rather than the group's completed class source code.

The code uses simple classes and explicit control flow. The file handler uses one FileStream with `FileShare.None` to keep duplicate checking and writing together. `leaveOpen: true` lets the reader and writer share that same handle. UTF-8 without a new BOM preserves the existing encoding. If the final record lacks a line terminator, the append first adds one. An IO failure during writing attempts to truncate only the new bytes. Disk failure can still prevent rollback, in which case a distinct message asks the user to inspect the file.

Unlike demonstration paths, the application's path is calculated from `AppContext.BaseDirectory`. No database connection, ORM, or third-party storage package is used.

## Handover to other members

### Member 2

Member 2's View work is integrated: `RefreshAnimals` and `TryLoadAnimals` load a fresh list, retain file order, enforce the seven grid headers, show skipped-line notices, and handle file errors. The source `animals.txt` now contains 12 valid records across all five statuses with Copy if newer. Keep this configuration when integrating later features.

### Member 3

Call `AnimalValidator.TryCreateAnimal` with the original locked ID and edited input values. If it succeeds, call `AnimalClassifier.Classify` on the returned animal every time, even if the score is unchanged. Then perform your targeted rewrite. Do not use `TryAppendAnimal` for Update, because it rejects existing IDs and appends records.

Member 3's Update work is integrated with the simple editable form. `LoadAnimalForEditing` is shared by exact-ID search and row selection; `SetEditMode` locks the ID, switches Add/Save availability and restores empty Add inputs after Save or Clear. The separate search box remains usable during editing. `TryUpdateAnimal` uses the View parser to find one valid record, refuses ambiguous valid IDs, writes a complete temporary file and replaces the original after the write succeeds. Other line contents are retained; line endings are written using Windows defaults. This protects against incomplete writes but does not provide concurrent editing or stale-record conflict detection across separate application instances.

The checks now exercise real Search, Save Changes, Clear and grid-click events, failed-save input retention, and the 79-to-80 boundary. They also cover a malformed line sharing a valid record's ID, duplicate valid IDs, missing targets, permissions and locks.

### Member 4

Delete is integrated through its feature pull request. Its button is an ordinary control in `MainForm.Designer.cs`, and its event handler is in `MainForm.Member4.cs`; open that helper as code. Do not set control positions again in the form constructor after WinForms has auto-scaled them. The grid, Search controls and Delete button must scale together. Keep `AutoScroll` enabled for small windows. The helper has no separate `.resx`; only `MainForm.resx` supplies the form's resources.

Summary is implemented on `feature/summary-report` for integration through a pull request. `AnimalSummary` calculates totals and counts; `BuildReport` formats the required text. The Member 4 helper reads current disk data, updates `txtSummary`, and calls `TryWriteSummary`. A complete temporary file is created before replacing an older report. Read/write failures give friendly feedback and preserve the previous saved report. Empty data uses N/A averages and all zero counts. Keep the button and report box in `MainForm.Designer.cs`. Group details and submission evidence still need confirmation.

## Validation performed

The first scaffold built successfully before it was committed. The member implementation built with zero warnings and zero errors. The package-free checks program passed 123 assertions on 13 October 2026, covering:

- All ten band endpoints, rejected classifier scores, and recalculation from 79 to 80.
- Trim/uppercase rules, required fields, Unicode-digit rejection for ID, length boundaries, pipe/newline rejection, integer parsing, overflow, and range limits.
- Existing file bytes preserved for UTF-8 with/without BOM and CR, LF, CRLF, or no final newline.
- Empty, missing, and BOM-only files; case-insensitive duplicate checks, including IDs in malformed lines.
- Malformed line counting, file order, repeated reloads, file locks, read-only flags, missing folders, and culture-independent serialization.
- Real form Add handlers: invalid-field feedback/focus, successful save/grid refresh, input clearing, classification display, duplicates, and retained inputs on write failure.

A WinForms bitmap preview was visually inspected. Visual Studio's interactive designer and multiple Windows display-scaling settings were not tested. A process crash or disk failure during a write was not simulated.

## Editing the form in Visual Studio

Open `PRG2782-Baobab-Ridge.sln`, right-click `PresentationLayer/MainForm.cs`, and select **View Designer** (Shift+F7). The controls are placed directly on the form with ordinary `Location` and `Size` properties. There are no layout panels or docked controls. Select a control to drag it, resize it with its handles, or edit these properties in the Properties window. If the designer has **Lock Controls** enabled, turn it off before moving controls.

Drag new buttons from the Toolbox into the unused area below the grid. Use the Properties window to set each control's `(Name)` and `Text`; use the Events tab to connect an existing click handler. Preserve `txtAnimalId`, `txtName`, `txtSpecies`, `txtAge`, `txtRecoveryScore`, `btnAdd`, `dgvAnimals`, and the feedback label names so the existing code can continue using them. Grid column order and read-only settings must remain intact.

Members working on an earlier branch must merge the updated `main` into their branch. If a designer-file conflict occurs, retain this simple layout and reapply their own additional controls and event hookups, preserving their feature code. Do not replace another member's form event handlers with an older copy.

## Suggested Member 1 demonstration

Prepare a disposable copy of the data file for practice so real records are not lost. Explain these steps in your own words rather than reading a script:

1. Show the Animal class and explain why its ID cannot be changed.
2. Explain the five bands and why Add and Update must call the same classification method.
3. Add `wr-0007`, `Naledi`, `Serval`, `3`, `79`. Show uppercase ID, Recovering, Outdoor Enclosure, the new grid row, and cleared fields.
4. Attempt the same ID again, then demonstrate a score such as `79.5` or `101`. Show the error, focused field, and unchanged file.
5. Add another ID with score `80` and explain why it is Release-Ready. Member 3 should separately demonstrate changing an existing animal from 79 to 80.
6. Be ready to explain trimming, duplicate checking, appending versus overwriting, and handling file errors.

Every member needs at least three meaningful presentation minutes. The whole presentation remains 20 minutes plus five minutes for questions.

## Submission requirements still outstanding

- All five application features are implemented; verify the final GitHub main and rehearse the complete demonstration.
- Member names/student numbers and lecturer access to the private repository.
- At least 15 meaningful commits across the group, each member's own contribution, and activity on at least three different days. Today's commits count as one day only.
- At least one merged pull request per member and one feature branch for each of the five features.
- Final fresh-clone build/demo of all features, matching GitHub main and the submitted ZIP.
- Submit by 23:59 on 20 October 2026. Presentations are 21–22 October 2026 according to the brief.
- Resolve the brief's AI-authorship restriction before using this AI-assisted implementation in assessed work. Do not submit it as solely student-authored work.

Assignment source: PRG2782 Project 2026.pdf, sections 3–8 and 10–12. Lecture examples are references, not extra assignment instructions.
