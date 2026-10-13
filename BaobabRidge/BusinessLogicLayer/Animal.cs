namespace BaobabRidge.BusinessLogicLayer
{
    public class Animal
    {
        public string Id { get; }
        public string Name { get; set; }
        public string Species { get; set; }
        public int Age { get; set; }
        public int RecoveryScore { get; set; }
        public string Status { get; set; }
        public string HousingUnit { get; set; }

        // Creates an animal; its identifier cannot be changed after construction.
        public Animal(string id, string name, string species, int age, int recoveryScore)
        {
            Id = id;
            Name = name;
            Species = species;
            Age = age;
            RecoveryScore = recoveryScore;
            Status = string.Empty;
            HousingUnit = string.Empty;
        }
    }
}
