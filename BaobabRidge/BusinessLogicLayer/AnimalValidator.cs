using System.Globalization;

namespace BaobabRidge.BusinessLogicLayer
{
    public static class AnimalValidator
    {
        // Validates the shared input rules. Uniqueness is checked separately during Add only.
        public static bool TryCreateAnimal(string id, string name, string species,
            string ageText, string scoreText, out Animal? animal,
            out string invalidField, out string message)
        {
            animal = null;
            invalidField = string.Empty;
            message = string.Empty;
            id = id.Trim().ToUpperInvariant();
            name = name.Trim();
            species = species.Trim();
            ageText = ageText.Trim();
            scoreText = scoreText.Trim();

            if (!IsValidId(id))
            {
                invalidField = "Animal ID";
                message = "Animal ID must be WR- followed by exactly four digits, for example WR-0007.";
                return false;
            }
            if (!IsValidText(name))
            {
                invalidField = "Name";
                message = "Name must contain 1 to 30 characters and cannot contain | or a line break.";
                return false;
            }
            if (!IsValidText(species))
            {
                invalidField = "Species";
                message = "Species must contain 1 to 30 characters and cannot contain | or a line break.";
                return false;
            }
            if (!int.TryParse(ageText, NumberStyles.None, CultureInfo.InvariantCulture, out int age)
                || age < 0 || age > 100)
            {
                invalidField = "Age";
                message = "Age must be a whole number between 0 and 100.";
                return false;
            }
            if (!int.TryParse(scoreText, NumberStyles.None, CultureInfo.InvariantCulture, out int score)
                || score < 0 || score > 100)
            {
                invalidField = "Recovery Score";
                message = "Recovery Score must be a whole number between 0 and 100.";
                return false;
            }

            animal = new Animal(id, name, species, age, score);
            return true;
        }

        // Checks exactly four ASCII digits after WR- without accepting extra characters.
        public static bool IsValidId(string id)
        {
            if (id.Length != 7 || !id.StartsWith("WR-", StringComparison.Ordinal))
            {
                return false;
            }
            for (int index = 3; index < id.Length; index++)
            {
                if (id[index] < '0' || id[index] > '9')
                {
                    return false;
                }
            }
            return true;
        }

        // Keeps each required text value within one pipe-delimited record.
        private static bool IsValidText(string value)
        {
            return value.Length >= 1 && value.Length <= 30
                && !value.Contains('|') && !value.Contains('\r') && !value.Contains('\n');
        }
    }
}
