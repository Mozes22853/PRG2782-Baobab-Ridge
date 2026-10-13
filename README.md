# Baobab Ridge Wildlife Rehabilitation Records System

PRG2782 Project 2026: C# Windows Forms with plain text file storage.

## Initial setup

This first commit contains only the blank Windows Forms project, a Visual Studio/.NET ignore file, and this README. Feature work follows on separate branches.

Open `BaobabRidge/BaobabRidge.csproj` in Visual Studio with the .NET desktop development workload and .NET 8 SDK or newer. Restore, build, and run the project. From a Windows terminal, use `dotnet run --project BaobabRidge/BaobabRidge.csproj`.

## Team

Member names and student numbers will be supplied by the group before submission.

## Planned features and files

Add, View, Update, Delete, and Summary Report.

`animals.txt`: UTF-8, one animal per line, seven pipe-separated fields: ID, Name, Species, Age, Score, Status, Housing Unit. No header. Stored beside the executable.

`summary.txt`: generated beside the executable, overwritten for each report, with timestamp, totals, averages, and all five status counts in the assignment's required layout. Excluded from Git.

## Current limitations and assistance

This is an empty scaffold; application features are not implemented yet. This repository is being developed with OpenAI Codex assistance. The project brief prohibits submitting AI-generated code or text as the group's own work. The group must resolve permitted use with its lecturer and must not misrepresent authorship. Assistance does not establish individual contribution or guarantee marks.
