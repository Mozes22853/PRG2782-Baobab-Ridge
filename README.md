# Baobab Ridge Wildlife Rehabilitation Records System

## Group members

| Name | Student number |
| --- | --- |
| Marius Tjaart van der walt | 603855 |
| Gift Mathe | 600731 |
| Nedhil Deolal | 605115 |
| Thabiso Mahlatse Mokhosoa | 601695 |

## How to build and run

You need Windows and Visual Studio with the **.NET desktop development** tools and support for **.NET 8**.

1. Clone or download this repository. Use the latest version on the **main** branch.
2. Open Visual Studio. Choose **File > Open > Project/Solution**.
3. Open **PRG2782-Baobab-Ridge.sln** from the project folder.
4. Wait for Visual Studio to finish restoring the files needed to build the program. Internet access may be needed the first time.
5. Choose **Build > Build Solution**.
6. Make sure **BaobabRidge** is selected next to the green Run button. Press **F5** to run the program. If another project is selected, right-click **BaobabRidge** in Solution Explorer and choose **Set as Startup Project**.

## Features

- **Add an animal:** Enter an ID, name, species, age and recovery score. The program checks the details, works out the status and housing unit, and saves the animal.
- **View animals:** See the saved animals in a table when the program opens. The table updates after an animal is added, changed or deleted.
- **Update an animal:** Select a row or search for an Animal ID. Change the details and click **Save Changes**. The ID cannot be changed. **Clear** empties the input boxes.
- **Delete an animal:** Select a row and click **Delete selected**. Check the animal's ID and name in the confirmation. Choose **Yes** to delete it or **No** to keep it.
- **Generate a summary:** Click **Generate Summary** to see the total animals, average age, average recovery score and number of animals in each status. The report is also saved to a text file.

## File formats

Both files are plain text files saved using UTF-8. They are kept in the same folder as the running program.

**animals.txt** stores one animal on each line. Each line has seven values separated by a pipe character (`|`), in this order:

```text
ID|Name|Species|Age|Score|Status|Housing Unit
```

This shows the order only. The file has no heading or blank lines. For example, a saved animal looks like this:

```text
WR-0001|Thabo|African Penguin|4|85|Release-Ready|Pre-Release Camp
```

**summary.txt** stores the latest summary report. It contains a title, the date and time, the total animals, both averages, and counts for all five statuses. The date and time use `yyyy-MM-dd HH:mm`, for example `2026-10-20 09:32`. Averages use two decimal places and a full stop. If there are no animals, all counts are zero and the averages show `N/A`. Generating a new report replaces the previous report.

## Known limitations

- The program runs on Windows only.
- Records are saved on each computer. GitHub does not share changes made to the animal data while the program is running. Avoid changing the same data file from more than one running copy of the program at a time.
- Lines with invalid animal details are skipped when loading. The program tells you how many lines were skipped.
- The summary does not update by itself. Click **Generate Summary** again after changing animal records.
- On smaller screens, you may need to scroll to reach all the controls.
- Changes are saved to the copy of `animals.txt` beside the running program. Deleting the build output folder can delete those saved records.
