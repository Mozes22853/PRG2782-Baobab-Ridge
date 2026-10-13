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
                    using (FileStream emptyFile = File.Create(filePath))
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
