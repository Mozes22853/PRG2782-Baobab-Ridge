using System.Globalization;
using System.Text;
using BaobabRidge.BusinessLogicLayer;

namespace BaobabRidge.DataLayer
{
    public class FileHandler
    {
        private readonly string filePath;

        // Uses the executable folder in the application; tests may supply a temporary path.
        public FileHandler(string? path = null)
        {
            filePath = path ?? Path.Combine(AppContext.BaseDirectory, "animals.txt");
        }

        // Loads a fresh list for the Add grid and skips malformed records without crashing.
        public bool TryLoadAnimals(out List<Animal> animals, out int skippedLines, out string message)
        {
            animals = new List<Animal>();
            skippedLines = 0;
            message = string.Empty;
            try
            {
                if (!File.Exists(filePath))
                {
                    using (FileStream emptyFile = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write))
                    {
                        // Dispose the new empty file before reading it.
                    }
                }

                using (StreamReader reader = new StreamReader(filePath, Encoding.UTF8))
                {
                    string? line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (TryParseAnimal(line, out Animal? animal))
                        {
                            animals.Add(animal!);
                        }
                        else
                        {
                            skippedLines++;
                        }
                    }
                }
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                message = "Cannot access animals.txt. Check the file and folder permissions.";
            }
            catch (DirectoryNotFoundException)
            {
                message = "The folder for animals.txt could not be found. Restore the application folder and try again.";
            }
            catch (IOException)
            {
                message = "Cannot read animals.txt. Close any program locking the file and try again.";
            }
            catch (Exception ex) when (ex is NotSupportedException || ex is ArgumentException
                || ex is System.Security.SecurityException)
            {
                // Unusual path or security-policy problems: still show a friendly message, never a crash.
                message = "animals.txt could not be opened because its location is not valid or not allowed.";
            }
            animals.Clear();
            return false;
        }

        // Checks current file IDs under a lock, then appends without changing existing bytes.
        public bool TryAppendAnimal(Animal animal, out bool duplicateId, out string message)
        {
            duplicateId = false;
            message = string.Empty;
            try
            {
                // Hold one exclusive handle across duplicate checking and writing.
                using (FileStream stream = new FileStream(filePath, FileMode.OpenOrCreate,
                    FileAccess.ReadWrite, FileShare.None))
                {
                    long originalLength = stream.Length;
                    bool hasText = false;
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8, true, 1024, true))
                    {
                        string? line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            hasText = true;
                            string existingId = line.Split('|')[0].Trim();
                            // Reserve an ID even if the rest of its stored line is malformed.
                            if (string.Equals(existingId, animal.Id, StringComparison.OrdinalIgnoreCase))
                            {
                                duplicateId = true;
                                message = "Animal ID already exists. Enter a unique Animal ID.";
                                return false;
                            }
                        }
                    }

                    bool needsNewLine = false;
                    if (hasText && stream.Length > 0)
                    {
                        stream.Seek(-1, SeekOrigin.End);
                        int lastByte = stream.ReadByte();
                        needsNewLine = lastByte != '\n' && lastByte != '\r';
                    }
                    stream.Seek(0, SeekOrigin.End);

                    try
                    {
                        // An explicit UTF-8 encoding prevents inserting a BOM midway through a file.
                        using (StreamWriter writer = new StreamWriter(stream, new UTF8Encoding(false), 1024, true))
                        {
                            if (needsNewLine)
                            {
                                writer.WriteLine();
                            }
                            writer.WriteLine(ToFileLine(animal));
                        }
                    }
                    catch (IOException)
                    {
                        // If an append partially fails, remove only the attempted appended bytes.
                        try
                        {
                            stream.SetLength(originalLength);
                            stream.Flush();
                        }
                        catch (IOException)
                        {
                            message = "Writing animals.txt failed and could not be rolled back. Check the file before trying again.";
                            return false;
                        }
                        throw;
                    }
                }
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                message = "Cannot save to animals.txt. Remove the read-only setting or ask for write permission, then try again.";
            }
            catch (DirectoryNotFoundException)
            {
                message = "The folder for animals.txt could not be found. Restore the application folder and try again.";
            }
            catch (IOException)
            {
                message = "Cannot save to animals.txt. Close any program locking the file and check that the disk has free space.";
            }
            return false;
        }
        // Updates one valid matching record; a temporary file protects the original if writing fails.
        public bool TryUpdateAnimal(Animal animal, out string message)
        {
            message = string.Empty;
            string? temporaryPath = null;
            try
            {
                string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);
                int targetIndex = -1;

                for (int i = 0; i < lines.Length; i++)
                {
                    // Use the same parser as View so a skipped malformed line cannot be edited by mistake.
                    if (TryParseAnimal(lines[i], out Animal? storedAnimal)
                        && string.Equals(storedAnimal!.Id, animal.Id, StringComparison.OrdinalIgnoreCase))
                    {
                        if (targetIndex >= 0)
                        {
                            message = "More than one valid record has this Animal ID. Correct the duplicate IDs before updating.";
                            return false;
                        }
                        targetIndex = i;
                    }
                }

                if (targetIndex < 0)
                {
                    message = "Animal ID not found in file.";
                    return false;
                }

                lines[targetIndex] = ToFileLine(animal);
                // Keep the temporary file beside the original so replacement stays on the same drive.
                temporaryPath = filePath + "." + Guid.NewGuid().ToString("N") + ".tmp";
                using (FileStream stream = new FileStream(temporaryPath, FileMode.CreateNew, FileAccess.Write))
                {
                    using (StreamWriter writer = new StreamWriter(stream, new UTF8Encoding(false), 1024, true))
                    {
                        foreach (string line in lines)
                        {
                            writer.WriteLine(line);
                        }
                    }
                    stream.Flush(true);
                }
                // Replace only after the complete new file has been written successfully.
                File.Replace(temporaryPath, filePath, null);
                temporaryPath = null;
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                message = "Cannot modify animals.txt. Check permissions.";
            }
            catch (DirectoryNotFoundException)
            {
                message = "The folder for animals.txt could not be found.";
            }
            catch (IOException)
            {
                message = "Cannot update animals.txt. Close any program locking the file.";
            }
            catch (Exception ex) when (ex is NotSupportedException || ex is ArgumentException
                || ex is System.Security.SecurityException)
            {
                message = "animals.txt could not be updated because its location is not valid or not allowed.";
            }
            finally
            {
                if (temporaryPath != null)
                {
                    try
                    {
                        File.Delete(temporaryPath);
                    }
                    catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException
                        || ex is System.Security.SecurityException)
                    {
                        // Cleanup failure must not hide the original file-operation result.
                    }
                }
            }
            return false;
        }

        // Removes precisely one selected valid record, preserving every other line's original bytes.
        public bool TryDeleteAnimal(Animal selectedAnimal, out string message)
        {
            message = string.Empty;
            string? temporaryPath = null;
            try
            {
                byte[] original = File.ReadAllBytes(filePath);
                int deleteStart = -1;
                int deleteEnd = -1;
                int matchingRecords = 0;

                // Find record boundaries in raw UTF-8 so unrelated lines retain their exact content
                // and original CR, LF or CRLF endings even after one animal is removed.
                for (int start = 0; start < original.Length; )
                {
                    int lineEnd = start;
                    while (lineEnd < original.Length && original[lineEnd] != (byte)'\r'
                        && original[lineEnd] != (byte)'\n')
                    {
                        lineEnd++;
                    }

                    int nextStart = lineEnd;
                    if (nextStart < original.Length && original[nextStart] == (byte)'\r')
                    {
                        nextStart++;
                    }
                    if (nextStart < original.Length && original[nextStart] == (byte)'\n')
                    {
                        nextStart++;
                    }

                    string line = Encoding.UTF8.GetString(original, start, lineEnd - start);
                    if (start == 0 && line.StartsWith("\uFEFF", StringComparison.Ordinal))
                    {
                        line = line.Substring(1);
                    }
                    if (TryParseAnimal(line, out Animal? storedAnimal)
                        && string.Equals(storedAnimal!.Id, selectedAnimal.Id, StringComparison.OrdinalIgnoreCase))
                    {
                        matchingRecords++;
                        // Refuse to delete a stale row that has changed since it was displayed.
                        if (ToFileLine(storedAnimal) != ToFileLine(selectedAnimal))
                        {
                            message = "This animal has changed on disk. Reload the records before deleting.";
                            return false;
                        }
                        deleteStart = start;
                        deleteEnd = nextStart;
                    }
                    start = nextStart;
                }

                if (matchingRecords == 0)
                {
                    message = "Animal ID not found in animals.txt. Reload the records before deleting.";
                    return false;
                }
                if (matchingRecords > 1)
                {
                    message = "More than one valid animal has this ID. Correct duplicate IDs before deleting.";
                    return false;
                }

                byte[] updated = new byte[original.Length - (deleteEnd - deleteStart)];
                Buffer.BlockCopy(original, 0, updated, 0, deleteStart);
                Buffer.BlockCopy(original, deleteEnd, updated, deleteStart, original.Length - deleteEnd);
                temporaryPath = filePath + "." + Guid.NewGuid().ToString("N") + ".tmp";
                using (FileStream stream = new FileStream(temporaryPath, FileMode.CreateNew, FileAccess.Write))
                {
                    stream.Write(updated, 0, updated.Length);
                    stream.Flush(true);
                }
                // Replace only after the complete new file is ready; errors leave the original intact.
                File.Replace(temporaryPath, filePath, null);
                temporaryPath = null;
                return true;
            }
            catch (FileNotFoundException)
            {
                message = "animals.txt was not found. Reload the records before deleting.";
            }
            catch (DirectoryNotFoundException)
            {
                message = "The folder for animals.txt could not be found.";
            }
            catch (UnauthorizedAccessException)
            {
                message = "Cannot delete this animal. Check permissions or the read-only setting on animals.txt.";
            }
            catch (IOException)
            {
                message = "Cannot delete this animal. Close any program locking animals.txt and try again.";
            }
            catch (Exception ex) when (ex is NotSupportedException || ex is ArgumentException
                || ex is System.Security.SecurityException)
            {
                message = "Cannot delete this animal because the file location is invalid or restricted.";
            }
            finally
            {
                if (temporaryPath != null)
                {
                    try { File.Delete(temporaryPath); }
                    catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException
                        || ex is System.Security.SecurityException)
                    {
                        // The original failure is more important than temporary-file cleanup.
                    }
                }
            }
            return false;
        }

        // Overwrites summary.txt beside animals.txt only after a complete UTF-8 report is ready.
        public bool TryWriteSummary(string report, out string message)
        {
            message = string.Empty;
            string? temporaryPath = null;
            try
            {
                string summaryPath = Path.Combine(Path.GetDirectoryName(Path.GetFullPath(filePath))!, "summary.txt");
                temporaryPath = summaryPath + "." + Guid.NewGuid().ToString("N") + ".tmp";
                using (FileStream stream = new FileStream(temporaryPath, FileMode.CreateNew, FileAccess.Write))
                {
                    using (StreamWriter writer = new StreamWriter(stream, new UTF8Encoding(false), 1024, true))
                    {
                        writer.Write(report);
                    }
                    stream.Flush(true);
                }
                if (File.Exists(summaryPath))
                {
                    File.Replace(temporaryPath, summaryPath, null);
                }
                else
                {
                    File.Move(temporaryPath, summaryPath);
                }
                temporaryPath = null;
                return true;
            }
            catch (DirectoryNotFoundException)
            {
                message = "The folder for summary.txt could not be found. Restore the application folder and try again.";
            }
            catch (UnauthorizedAccessException)
            {
                message = "Cannot save summary.txt. Remove the read-only setting or check file and folder permissions.";
            }
            catch (IOException)
            {
                message = "Cannot save summary.txt. Close any program locking it and check that the disk has free space.";
            }
            catch (Exception ex) when (ex is NotSupportedException || ex is ArgumentException
                || ex is System.Security.SecurityException)
            {
                message = "summary.txt could not be saved because its location is invalid or restricted.";
            }
            finally
            {
                if (temporaryPath != null)
                {
                    try { File.Delete(temporaryPath); }
                    catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException
                        || ex is System.Security.SecurityException)
                    {
                        // Cleanup must not hide the friendly error for the failed report write.
                    }
                }
            }
            return false;
        }

        // Serializes exactly the seven required fields, with no header or extra spaces.
        public static string ToFileLine(Animal animal)
        {
            return string.Join("|", animal.Id, animal.Name, animal.Species,
                animal.Age.ToString(CultureInfo.InvariantCulture),
                animal.RecoveryScore.ToString(CultureInfo.InvariantCulture),
                animal.Status, animal.HousingUnit);
        }

        // Converts one complete record and checks that its derived fields match its score.
        private static bool TryParseAnimal(string line, out Animal? animal)
        {
            animal = null;
            string[] fields = line.Split('|');
            if (fields.Length != 7)
            {
                return false;
            }
            if (!AnimalValidator.TryCreateAnimal(fields[0], fields[1], fields[2], fields[3], fields[4],
                out animal, out _, out _))
            {
                return false;
            }
            AnimalClassifier.Classify(animal!);
            return fields[5] == animal!.Status && fields[6] == animal.HousingUnit;
        }
    }
}
