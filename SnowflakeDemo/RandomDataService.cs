namespace SnowflakeDemo;

public class RandomDataService
{
    public class Person
    {
        public string UniqueId { get; set; } = 
             System.Guid.NewGuid().ToString();
        public string LastName { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string Address { get; set; } = "";
        public string City { get; set; } = "";
        public string StateOrProvince { get; set; } = "";
    }

    public List<Person> GetSamplePeople(int howMany = 100000)
    {
        var lastNames = GetLastNames();
        var firstNames = GetFirstNames();
        var addresses = GetStreetNames();
        var cities = GetCities();
        var states = GetStatesAndProvinces();

        var retval = new List<Person>();
        for (int i = 0; i < howMany; i++)
        {
            var person = new Person();
            person.LastName = lastNames[Between0and9()];
            person.FirstName = firstNames[Between0and9()];
            person.City = cities[Between0and9()];
            person.StateOrProvince = states[Between0and9()];
            person.Address =
               $"{RandomAddressNumber()} " +
               $"{addresses[Between0and9()]}";


            retval.Add(person);
        }
        return retval;
    }
    public int RandomAddressNumber()
    {
        var random = Random.Shared.Next(1000, 99999);
        return random;
    }
    public int Between0and9()
    {

        var random = Random.Shared.Next(0, 9);
        return random;
    }

    public List<string> GetLastNames()
    {
        var retval = new List<string>();
        retval.Add("Lucas");
        retval.Add("Smith");
        retval.Add("Spielberg");
        retval.Add("Gygax");
        retval.Add("Garland");
        retval.Add("Wolff");
        retval.Add("West");
        retval.Add("Kardashian");
        retval.Add("Van Halen");
        retval.Add("Grohl");
        return retval;
    }

    public List<string> GetFirstNames()
    {
        var retval = new List<string>();
        retval.Add("Mary");
        retval.Add("Leslie");
        retval.Add("Jane");
        retval.Add("Jessica");
        retval.Add("John");
        retval.Add("Paul");
        retval.Add("George");
        retval.Add("Ringo");
        retval.Add("Eddie");
        retval.Add("Alex");
        return retval;
    }

    public List<string> GetStreetNames()
    {
        var retval = new List<string>();
        retval.Add("Orange");
        retval.Add("Main");
        retval.Add("Maple");
        retval.Add("Oak");
        retval.Add("Poplar");
        retval.Add("Chestnut");
        retval.Add("Elm");
        retval.Add("Redwood");
        retval.Add("Lincoln Blvd");
        retval.Add("Sepulveda Blvd");
        return retval;
    }

    public List<string> GetCities()
    {
        var retval = new List<string>();
        retval.Add("Seattle");
        retval.Add("Austin");
        retval.Add("Regina");
        retval.Add("Calgary");
        retval.Add("Winnipeg");
        retval.Add("Portland");
        retval.Add("Los Angeles");
        retval.Add("Encino");
        retval.Add("Montreal");
        retval.Add("Ottawa");
        return retval;
    }

    public List<string> GetStatesAndProvinces()
    {
        var retval = new List<string>();
        retval.Add("AB");
        retval.Add("SK");
        retval.Add("CA");
        retval.Add("OR");
        retval.Add("WA");
        retval.Add("TX");
        retval.Add("CO");
        retval.Add("NY");
        retval.Add("MN");
        retval.Add("KY");
        return retval;
    }


}