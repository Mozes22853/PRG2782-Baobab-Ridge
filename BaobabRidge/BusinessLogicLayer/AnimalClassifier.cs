namespace BaobabRidge.BusinessLogicLayer
{
    public static class AnimalClassifier
    {
        // Calculates both derived fields. Add and the future Update feature must call this method.
        public static void Classify(Animal animal)
        {
            if (animal.RecoveryScore < 0 || animal.RecoveryScore > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(animal), "Recovery Score must be between 0 and 100.");
            }

            if (animal.RecoveryScore <= 19)
            {
                animal.Status = "Critical";
                animal.HousingUnit = "Intensive Care Unit";
            }
            else if (animal.RecoveryScore <= 39)
            {
                animal.Status = "Serious";
                animal.HousingUnit = "High-Dependency Ward";
            }
            else if (animal.RecoveryScore <= 59)
            {
                animal.Status = "Stable";
                animal.HousingUnit = "Recovery Ward";
            }
            else if (animal.RecoveryScore <= 79)
            {
                animal.Status = "Recovering";
                animal.HousingUnit = "Outdoor Enclosure";
            }
            else
            {
                animal.Status = "Release-Ready";
                animal.HousingUnit = "Pre-Release Camp";
            }
        }
    }
}
