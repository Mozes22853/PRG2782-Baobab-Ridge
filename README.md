# Baobab Ridge Wildlife Rehabilitation Records System

PRG2782 Project 2026: C# Windows Forms with plain text file storage.

## Build and run

Requires Windows and Visual Studio with the .NET desktop development workload, or a .NET SDK capable of targeting .NET 8. The application targets `net8.0-windows` and uses no third-party storage packages.

1. Clone this repository and use the `main` branch, which includes Member 1's merged work.
2. In Visual Studio, choose **File > Open > Project/Solution** and open `PRG2782-Baobab-Ridge.sln`. This solution contains the app only; the separate `Member1Checks` program is for verification.
3. Allow NuGet restore to finish. The run target should be **BaobabRidge**, not **Current Document (Program.cs)**. Press **F5** to run. If needed, right-click the BaobabRidge project and choose **Set as Startup Project**. Alternatively, run `dotnet run --project BaobabRidge/BaobabRidge.csproj` from the repository folder.
4. Enter an ID such as `WR-0007`, a name, species, age, and recovery score, then select **Add animal**.

The `.exe`, `animals.txt`, and supporting runtime files are in the build output folder, for example `BaobabRidge/bin/Debug/net8.0-windows/`. Runtime changes affect the output copy of the file. Avoid deleting that folder if it contains records you need. The source `animals.txt` uses **Copy if newer** (`PreserveNewest`).

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
| 1 | Tjaart van der Walt (existing Git identity; confirm) | To be supplied | Model, shared classification/validation, Add |
| 2 | To be supplied | To be supplied | View, shared file error review, starter data |
| 3 | To be supplied | To be supplied | Update |
| 4 | To be supplied | To be supplied | Delete, Summary, repository documentation |

## Feature status

- **Member 1 implemented:** animal model, one classification method, shared input validation, Add, duplicate checking, UTF-8 append, field-specific feedback/focus, displayed classification, cleared inputs, and grid refresh.
- **View integration included for Add:** initial loading, exact read-only grid columns, file order, missing-file creation, and one skipped-line count. Member 2 can extend and validate this foundation on their branch.
- **Pending other members:** Update, Delete, Summary Report, and at least eight starter animals covering four statuses. The committed `animals.txt` is intentionally empty at this stage.

## File formats

`animals.txt`: UTF-8, one animal per line, seven pipe-separated fields: ID, Name, Species, Age, Score, Status, Housing Unit. No header. Stored beside the executable.

`summary.txt`: generated beside the executable, overwritten for each report, with timestamp, totals, averages, and all five status counts in the assignment's required layout. Excluded from Git.

## Checks and handover

Run `dotnet run --project Member1Checks/Member1Checks.csproj --configuration Release` on Windows. This package-free console checks program exercises validation, all band endpoints, append preservation, duplicate detection, malformed records, read-only/locked files, and the actual form's Add event. The form opens briefly during UI checks. File fixtures use temporary paths or the checks project's output copy and are restored afterward.

See [Member 1 handover](docs/member-1-handover.md) for requirements, shared-method use, manual demonstration steps, and work still needed for submission. Lecture concepts are recorded there; supplied lecture files are not uploaded to this repository.

## Known limitations and assistance

The entire group project is not complete. The remaining feature branches, member details, lecturer access, genuine commits over three days, and presentation participation are still required. The file loader also rejects records whose status/housing disagree with the recovery score. Add preserves malformed existing lines and reserves their ID if their first field matches the new ID.

This implementation and documentation were produced with OpenAI Codex assistance. The project brief prohibits submitting AI-generated code or text as the group's own work. The group must resolve permitted use with its lecturer and must not misrepresent authorship. Assistance does not establish individual contribution or guarantee marks. Commit messages record this assistance and use actual dates.
