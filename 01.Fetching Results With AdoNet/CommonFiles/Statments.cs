using System.Data.SqlClient;

namespace CommonFiles
{
    public static class Statments
    {
        //EX 9
        public const string ProcedureName = "usp_GetOlder";
        public const string UpdatedMinion = "SELECT Name, Age FROM Minions WHERE Id = {0}"; 

        //EX 8 
        public const string UpdateMinions = "UPDATE Minions SET Name = upper(LEFT(Name, 1)) + SUBSTRING(Name, 2, LEN(Name)), Age += 1 WHERE Id in ({0})";
        public const string GetUpdatedValues = "SELECT Name, Age FROM Minions WHERE Id in ({0})"; 

        //EX 7
        public const string GetMinionsNames = "SELECT Name FROM Minions";

        //EX 6
        public const string TurnOffConstraint = "ALTER TABLE {0} NOCHECK CONSTRAINT ALL";
        public const string TurnOnConstraint = "ALTER TABLE {0} CHECK CONSTRAINT ALL";
        public const string VillainNameQ = "SELECT Name FROM Villains WHERE Id = {0}";
        public const string MinionsCountQuery = "SELECT COUNT(*) FROM MinionsVillains WHERE VillainId = {0}";
        public const string DeleteFromTable = "DELETE FROM {0} WHERE {1} = {2}";

        //EX 5
        public const string townsInCountry = "SELECT t.Name FROM Towns as t JOIN Countries AS c ON c.Id = t.CountryCode WHERE c.Name = '{0}'";
        public const string townsToUpper = "UPDATE Towns SET Name = UPPER(Name) WHERE CountryCode = (SELECT c.Id FROM Countries AS c WHERE c.Name = '{0}')";

        //EX 4
        public const string getTown = "SELECT Id FROM Towns WHERE Name = '{0}'";
       public const string getVillain = "SELECT Id FROM Villains WHERE Name = '{0}'";
       public const string getMinion = "SELECT Id FROM Minions WHERE Name = '{0}'";
       public const string insertTown = "INSERT INTO Towns (Name) VALUES ('{0}')";
       public const string insertVillain = "INSERT INTO Villains (Name, EvilnessFactorId) VALUES ('{0}', 4)";
       public const string insertMinion = "INSERT INTO Minions (Name, Age, TownId) VALUES ('{0}', {1}, {2})";
       public const string insertMinionAndVillain = "INSERT INTO MinionsVillains (MinionId, VillainId) VALUES ({0}, {1})";

        //EX 3
        public const string VillainQuerry = "select * from Villains where id = {0}";
        public const string GetMinionsQuerry = "SELECT ROW_NUMBER() OVER (ORDER BY m.Name) as RowNum, m.Name, m.Age FROM MinionsVillains AS mv JOIN Minions As m ON mv.MinionId = m.Id WHERE mv.VillainId = {0}";
        public const string NoMinions = "(no minions)";
        public const string NoVillain = "No villain with ID {0} exists in the database.";
        public const string VillainName = "Villain: {0}";

        //EX 2
        public static string[] CreateStatments()
        {
            var createCountries = "CREATE TABLE Countries (Id INT PRIMARY KEY IDENTITY,Name VARCHAR(50))";
            var createTowns = "CREATE TABLE Towns(Id INT PRIMARY KEY IDENTITY,Name VARCHAR(50), CountryCode INT FOREIGN KEY REFERENCES Countries(Id))";
            var createMinions = "CREATE TABLE Minions(Id INT PRIMARY KEY IDENTITY,Name VARCHAR(30), Age INT, TownId INT FOREIGN KEY REFERENCES Towns(Id))";
            var createEvilnes = "CREATE TABLE EvilnessFactors(Id INT PRIMARY KEY IDENTITY, Name VARCHAR(50))";
            var createVillains = "CREATE TABLE Villains (Id INT PRIMARY KEY IDENTITY, Name VARCHAR(50), EvilnessFactorId INT FOREIGN KEY REFERENCES EvilnessFactors(Id))";
            var createMinionsAndVillains = "CREATE TABLE MinionsVillains (MinionId INT FOREIGN KEY REFERENCES Minions(Id),VillainId INT FOREIGN KEY REFERENCES Villains(Id),CONSTRAINT PK_MinionsVillains PRIMARY KEY (MinionId, VillainId))";
            var insertIntoCountries = "INSERT INTO Countries ([Name]) VALUES ('Bulgaria'),('England'),('Cyprus'),('Germany'),('Norway')";
            var insertIntoTowns = "INSERT INTO Towns ([Name], CountryCode) VALUES ('Plovdiv', 1),('Varna', 1),('Burgas', 1),('Sofia', 1),('London', 2),('Southampton', 2),('Bath', 2),('Liverpool', 2),('Berlin', 3),('Frankfurt', 3),('Oslo', 4)";
            var insertIntoMinions = "INSERT INTO Minions (Name,Age, TownId) VALUES('Bob', 42, 3),('Kevin', 1, 1),('Bob ', 32, 6),('Simon', 45, 3),('Cathleen', 11, 2),('Carry ', 50, 10),('Becky', 125, 5),('Mars', 21, 1),('Misho', 5, 10),('Zoe', 125, 5),('Json', 21, 1)";
            var insertIntoEvilness = "INSERT INTO EvilnessFactors (Name) VALUES ('Super good'),('Good'),('Bad'), ('Evil'),('Super evil')";
            var insertIntoVillains = "INSERT INTO Villains (Name, EvilnessFactorId) VALUES ('Gru',2),('Victor',1),('Jilly',3),('Miro',4),('Rosen',5),('Dimityr',1),('Dobromir',2)";
            var insertIntoMinionsAndVillains = "INSERT INTO MinionsVillains VALUES(1, 2), (3, 1), (1, 3), (3, 3), (4, 1), (2, 2), (1, 1), (3, 4), (1, 4), (1, 5), (5, 1)";

            var querries = new string[]
            {
                createCountries,
                createTowns,
                createMinions,
                createEvilnes,
                createVillains,
                createMinionsAndVillains,
                insertIntoCountries,
                insertIntoTowns,
                insertIntoMinions,
                insertIntoEvilness,
                insertIntoVillains,
                insertIntoMinionsAndVillains
            };

            return querries;
        }

        public static string ExerciseTwo()
        {
            return "SELECT v.Name, COUNT(mv.VillainId) AS MinionsCount FROM Villains AS v JOIN MinionsVillains AS mv ON v.Id = mv.VillainId GROUP BY v.Id, v.Name HAVING COUNT(mv.VillainId) > 3 ORDER BY COUNT(mv.VillainId)";
        }
    }
}
