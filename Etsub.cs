
using System;
using System.Collections.Generic;

namespace EmergencyResponseSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            List<EmergencyUnit> units = new List<EmergencyUnit>()
            {
                new Police("Police Unit 1", 60),
                new Firefighter("Firefighter Unit 1", 50),
                new Ambulance("Ambulance Unit 1", 70),

            };

            string[] incidentTypes = { "Crime", "Fire", "Medical" };
            string[] locations = { "City Hall", "Market", "Hospital", "Park", "Mall" };

            int score = 0;
            Random random = new Random();

            for (int turn = 1; turn <= 5; turn++)
            {
                Console.WriteLine($"\n--- Turn {turn} ---");

                string incidentType = incidentTypes[random.Next(incidentTypes.Length)];
                string location = locations[random.Next(locations.Length)];
                Incident incident = new Incident(incidentType, location);

                Console.WriteLine($"Incident: {incident.Type} at {incident.Location}");

                List<EmergencyUnit> availableUnits = new List<EmergencyUnit>();
                for (int i = 0; i < units.Count; i++)
                {
                    if (units[i].CanHandle(incident.Type))
                    {
                        availableUnits.Add(units[i]);
                        Console.WriteLine($"{availableUnits.Count}. {units[i].Name}");
                    }
                }

                if (availableUnits.Count > 0)
                {
                    Console.Write("Choose a unit (1 - " + availableUnits.Count + "): ");
                    string input = Console.ReadLine();
                    int choice;

                    if (int.TryParse(input, out choice) && choice > 0 && choice <= availableUnits.Count)
                    {
                        availableUnits[choice - 1].RespondToIncident(incident);
                        score += 10;
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice.");
                        score -= 5;
                    }
                }
                else
                {
                    Console.WriteLine("No unit available for this type.");
                    score -= 5;
                }

                Console.WriteLine("Current Score: " + score);
            }

            Console.WriteLine("\nFinal Score: " + score);
        }
    }
}


namespace EmergencyResponseSimulation
{
    abstract class EmergencyUnit
    {
        public string Name;
        public int Speed;

        public EmergencyUnit(string name, int speed)
        {
            Name = name;
            Speed = speed;
        }

        public abstract bool CanHandle(string type);
        public abstract void RespondToIncident(Incident incident);
    }
}


namespace EmergencyResponseSimulation
{
    class Police : EmergencyUnit
    {
        public Police(string name, int speed) : base(name, speed) { }

        public override bool CanHandle(string type)
        {
            return type == "Crime";
        }

        public override void RespondToIncident(Incident incident)
        {
            Console.WriteLine($"{Name} is handling crime at {incident.Location}.");
        }
    }
}


namespace EmergencyResponseSimulation
{
    class Firefighter : EmergencyUnit
    {
        public Firefighter(string name, int speed) : base(name, speed) { }

        public override bool CanHandle(string type)
        {
            return type == "Fire";
        }

        public override void RespondToIncident(Incident incident)
        {
            Console.WriteLine($"{Name} is putting out fire at {incident.Location}.");
        }
    }
}

namespace EmergencyResponseSimulation
{
    class Ambulance : EmergencyUnit
    {
        public Ambulance(string name, int speed) : base(name, speed) { }

        public override bool CanHandle(string type)
        {
            return type == "Medical";
        }

      
public override void RespondToIncident(Incident incident)
        {
            Console.WriteLine($"{Name} is treating people at {incident.Location}.");
        }
    }
}

namespace EmergencyResponseSimulation
{
    class Incident
    {
        public string Type;
        public string Location;

        public Incident(string type, string location)
        {
            Type = type;
            Location = location;
        }
    }
}