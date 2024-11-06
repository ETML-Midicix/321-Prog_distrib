namespace Serialize
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Step 1, 2 and 3
            Character perso = new Character();
            perso.FirstName = "Melissa";
            perso.LastName = "D'Anjou";
            perso.PlayedBy = new Actor();
            perso.PlayedBy.FirstName = "Jennifer";
            perso.PlayedBy.LastName = "Aniston";
            Actor Jen = new Actor
            {
                FirstName = "Jennifer",
                LastName = "Aniston",
                BirthDate = new DateTime(1969, 2, 11),
                Country = "USA",
                IsAlive = true
            };
            Character Melissa = new Character
            {
                FirstName = "Melissa",
                LastName = "D'Anjou",
                Description = "null",
                PlayedBy = Jen
            };
            //Le personnage de Melissa D'Anjou est joué par Jennifer Aniston
            Console.WriteLine($"Le personnage de {perso.FirstName} {perso.LastName} est joué par {perso.PlayedBy.FirstName} {perso.PlayedBy.LastName}");

            //Step 4 and 5
            Console.WriteLine(perso.ToJson());

            perso.ToJsonFile($"C:\\Users\\pa64wln\\Downloads\\{perso.FirstName}-{perso.LastName}.json");

            Character thomasCharacter = new Character();
            Console.WriteLine(thomasCharacter.ToCharacter("C:\\Users\\pa64wln\\Downloads\\thomas.json"));
            //Console.WriteLine(thomasCharacter.FirstName);
        }
    }
}
