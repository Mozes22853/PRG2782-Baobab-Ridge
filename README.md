# Baobab Ridge Wildlife Rehabilitation Records System

PRG2782 Project 2026: a C# Windows Forms application for recording wildlife rehabilitation animals, calculating their recovery status and housing unit, and maintaining their records in plain text files.

## Build and run

Requires Windows and Visual Studio with the .NET desktop development workload, or a .NET SDK capable of targeting .NET 8. The application targets `net8.0-windows` and uses no third-party storage packages.

1. Clone this repository and use the `main` branch for the integrated team work.
2. In Visual Studio, choose **File > Open > Project/Solution** and open `PRG2782-Baobab-Ridge.sln`. This solution contains the app only; the separate `Member1Checks` program is for verification.
3. Allow NuGet restore to finish, then choose **Build > Build Solution**. The run target should be **BaobabRidge**. Press **F5** to run. If needed, right-click the BaobabRidge project and choose **Set as Startup Project**.
4. Enter an unused ID such as `WR-0013`, a name, species, age, and recovery score, then select **Add animal**. The starter data already uses IDs WR-0001 through WR-0012.
5. To update, click a grid row or enter an exact ID in **Search by ID** and click **Search**. Edit the details and select **Save Changes**. **Clear** returns to Add mode. The ID stays read-only while editing.
6. Select a row and click **Delete selected**. Check the ID and name in the confirmation before choosing Yes; No keeps the file unchanged.
7. Click **Generate Summary** to display statistics from the current file and overwrite `summary.txt` beside the executable. The read-only report box supports scrolling and copying. Regenerate after changing records to obtain a new report.

The `.exe`, `animals.txt`, and supporting runtime files are in the build output folder, for example `BaobabRidge/bin/Debug/net8.0-windows/`. Runtime changes affect the output copy of the file. Avoid deleting that folder if it contains records you need. The source `animals.txt` uses **Copy if newer** (`PreserveNewest`).

Alternatively, open a terminal in the cloned repository folder and run:

```powershell
dotnet restore PRG2782-Baobab-Ridge.sln
dotnet build PRG2782-Baobab-Ridge.sln --configuration Release --no-restore
dotnet run --project BaobabRidge/BaobabRidge.csproj --configuration Release --no-build
```

### If the first build reports NETSDK1004

Choose **No** when Visual Studio asks whether to continue debugging a failed build. `NETSDK1004` means the generated `obj/project.assets.json` restore file is missing. These machine-generated files are intentionally not committed to Git. From a terminal in the repository folder, run:

```powershell
dotnet restore PRG2782-Baobab-Ridge.sln
dotnet build PRG2782-Baobab-Ridge.sln --no-restore
```

Then open the solution and press F5. The first restore needs access to NuGet when the required .NET targeting packs are not installed or cached. If restore fails, resolve its first error before building again. See [Microsoft's NETSDK1004 guidance](https://learn.microsoft.com/en-us/dotnet/core/tools/sdk-errors/netsdk1004). Do not run either `Program.cs` as a standalone document. To run the checks, use the explicit project command below, which also restores that project's dependencies.

The first commit contains only the blank Windows Forms project, `.gitignore`, and README. Member 1 work is developed on `feature/add-animal`.

## Team

| Member | Name | Student number | Responsibility |
| --- | --- | --- | --- |
| 1 | Marius Tjaart van der walt | 603855 | Model, shared classification/validation, Add |
| 2 | Gift Mathe | 600731 | View, shared file error handling, starter data |
| 3 | Nedhil Deolal | 605115 | Update |
| 4 | Details to be added later | Pending | Delete, Summary, repository documentation |

## Feature status

- **Member 1 implemented:** animal model, one classification method, shared input validation, Add, duplicate checking, UTF-8 append, field-specific feedback/focus, displayed classification, cleared inputs, and grid refresh.
- **Member 2 integrated:** startup loading, exact read-only grid columns in file order, missing-file creation, friendly file errors, skipped-line notification, and 12 starter animals across all five statuses.
- **Member 3 integrated:** exact case-insensitive ID search, row selection, shared validation and classification on Save Changes, immutable ID, Add/Edit button states, Clear, and automatic grid refresh. Update matches only valid file records and refuses ambiguous valid IDs. It writes a temporary file beside the original and replaces the original only after writing succeeds.
- **Member 4 Delete integrated:** row selection checks, Yes/No confirmation showing ID and name, one-record deletion, refresh and clearing of the deleted record's edit form. Unrelated file bytes are preserved, and stale or ambiguous selections are rejected.
- **Summary integrated:** Generate Summary reads current valid file records, displays the report in a read-only form control, and overwrites `summary.txt` safely. It lists every status, uses invariant two-decimal averages, shows N/A averages for empty data, reports skipped lines, and distinguishes a displayed report from a failed save.

## File formats

`animals.txt`: UTF-8, one animal per line, seven pipe-separated fields in this exact order: ID, Name, Species, Age, Score, Status, Housing Unit. It has no header or blank lines and is stored beside the executable. IDs use `WR-` followed by four digits. Age and score are integers from 0 to 100. Status and housing are calculated from the score. For example:

```text
WR-0001|Thabo|African Penguin|4|85|Release-Ready|Pre-Release Camp
```

`summary.txt`: a plain text report beside the executable, overwritten on each generation. It uses the brief's exact layout, a `yyyy-MM-dd HH:mm` timestamp, the total animal count, average age and recovery score to two decimal places, and counts for all five statuses including zero counts. Empty data shows `N/A` averages. Generated reports are excluded from Git.

## Checks and handover

Run `dotnet run --project Member1Checks/Member1Checks.csproj --configuration Release` on Windows. This package-free checks program exercises validation, classification endpoints, append preservation, duplicate detection, malformed records, read-only/locked files, and the form's Add, Search, row-selection, Save Changes and Clear events. It also checks the malformed-ID update regression, ambiguous IDs, button states and visible control positions. Additional checks cover byte-preserving Delete, stale/ambiguous selections, exact Summary text, culture-independent rounding, empty/missing data, current disk data versus a stale grid, overwritten reports, and locked/read-only report failures. The form opens briefly during UI checks. File fixtures use temporary paths or the checks project's output copy and are restored afterward.

Run the layout checks in separate processes at representative font sizes with `dotnet run --project Member1Checks/Member1Checks.csproj --configuration Release -- --layout-font 9` (repeat with `11.25`, `13.5` and `18`). These exercise WinForms font auto-scaling and smaller-window scrolling. All controls, including Delete, are positioned in `MainForm.Designer.cs`; the Delete handler helper opens as code and has no separate resource file.

See [Member 1 handover](docs/member-1-handover.md) for requirements, shared-method use, manual demonstration steps, and work still needed for submission. Lecture concepts are recorded there; supplied lecture files are not uploaded to this repository.

## Known limitations

- The file loader skips malformed records, including records whose status or housing disagrees with their recovery score. Add preserves existing malformed lines and reserves an ID if their first field matches the new ID.
- Update refuses multiple valid records with the same ID rather than choosing one arbitrarily. Its temporary-file replacement protects the original during a failed write, but concurrent editing and stale-record conflict detection across separate application instances are not supported.
- Update retains other line contents but writes Windows line endings and UTF-8 without a BOM. Runtime records are local to each running copy; GitHub does not synchronise runtime data between group members.
- The form uses ordinary designer controls and WinForms font auto-scaling. Controls do not automatically rearrange when the window shrinks; scrollbars provide access to the complete form on smaller displays. Font-scaling tests do not replace testing on the team's actual monitors and DPI settings.

## Git and submission requirements

- The repository was initialised before application code; the first commit contains the blank Windows Forms project, Visual Studio/.NET `.gitignore`, and README.
- Develop each feature (Add, View, Update, Delete and Summary) on its own feature branch and merge through a GitHub pull request. Every member must own a feature and have at least one merged pull request. Keep `main` stable and buildable.
- The group needs at least 15 commits in total. Each member must make at least three meaningful commits under their own name and email, across at least three different days. Integration assistance and merge commits do not establish another member's personal contribution. Contribution-day requirements remain to be completed where outstanding.
- Begin commit messages with a descriptive verb such as **Add**, **Fix** or **Implement**. Push at the end of each work session and after finishing a feature. The demonstrated and submitted code must match GitHub `main`.
- Keep `bin/`, `obj/`, `.vs/`, generated reports and temporary files out of Git. Preserve the repository history; do not delete/recreate the repository or force-push to rewrite history.
- Add Member 4's name and student number when supplied. Before submission, confirm lecturer access: the repository must be public, or the lecturer's GitHub account must be added as a collaborator if it remains private. Lecturer access has not been confirmed here.

## Assistance disclosure

Parts of the application and documentation, including the initial Member 1 implementation and subsequent integration fixes and the completed Summary feature, were produced with OpenAI Codex assistance. The project brief prohibits submitting AI-generated code or text as the group's own work. The group must resolve permitted use with its lecturer and must not misrepresent authorship. Assistance does not establish individual contribution or guarantee marks. Assisted commits record this assistance and use actual dates; the members' original commits are preserved.
