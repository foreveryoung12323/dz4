namespace dz4
{
    public class Car
    {
        public string Model { get; set; }
        public string LicensePlate { get; set; }
        public bool NeedsRepair { get; set; }

        public Car(string model, string licensePlate)
        {
            Model = model;
            LicensePlate = licensePlate;
            NeedsRepair = false;
        }

        public void MarkForRepair()
        {
            NeedsRepair = true;
            Console.WriteLine($"Car {Model} ({LicensePlate}) marked for repair.");
        }

        public void MarkRepaired()
        {
            NeedsRepair = false;
            Console.WriteLine($"Car {Model} ({LicensePlate}) has been repaired.");
        }
    }

    public class Driver
    {
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;

        public Driver(string name)
        {
            Name = name;
        }

        public void RequestRepair(Car car)
        {
            if (!IsActive)
            {
                Console.WriteLine($"Driver {Name} is not active and cannot request repair.");
                return;
            }

            car.MarkForRepair();
            Console.WriteLine($"Driver {Name} requested repair for car {car.Model}.");
        }

        public void MarkRouteCompleted(Route route, Car car)
        {
            if (!IsActive)
            {
                Console.WriteLine($"Driver {Name} is not active and cannot mark the route as completed.");
                return;
            }

            route.IsCompleted = true;
            Console.WriteLine($"Driver {Name} completed route {route.Description} with car {car.Model}.");
        }
    }

    public class Route
    {
        public string Description { get; set; }
        public bool IsCompleted { get; set; } = false;

        public Route(string description)
        {
            Description = description;
        }
    }

    public class Dispatcher
    {
        private List<Driver> Drivers = new();
        private List<Car> Cars = new();
        private List<Route> Routes = new();

        public void AddDriver(Driver driver) => Drivers.Add(driver);

        public void AddCar(Car car) => Cars.Add(car);

        public void AddRoute(Route route) => Routes.Add(route);

        public void AssignRouteToDriver(Route route, Driver driver, Car car)
        {
            if (!Drivers.Contains(driver) || !Cars.Contains(car))
            {
                Console.WriteLine("Driver or car not found in the system.");
                return;
            }

            if (!driver.IsActive)
            {
                Console.WriteLine($"Driver {driver.Name} is not active and cannot be assigned to a route.");
                return;
            }

            if (car.NeedsRepair)
            {
                Console.WriteLine($"Car {car.Model} needs repair and cannot be assigned.");
                return;
            }

            Console.WriteLine($"Route '{route.Description}' assigned to driver {driver.Name} with car {car.Model}.");
        }

        public void SuspendDriver(Driver driver)
        {
            if (Drivers.Contains(driver))
            {
                driver.IsActive = false;
                Console.WriteLine($"Driver {driver.Name} has been suspended.");
            }
            else
            {
                Console.WriteLine("Driver not found in the system.");
            }
        }

        public void ReactivateDriver(Driver driver)
        {
            if (Drivers.Contains(driver))
            {
                driver.IsActive = true;
                Console.WriteLine($"Driver {driver.Name} has been reactivated.");
            }
            else
            {
                Console.WriteLine("Driver not found in the system.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Dispatcher dispatcher = new Dispatcher();
            Driver driver1 = new Driver("John Doe");
            Car car1 = new Car("Ford Transit", "AA1234BB");
            Route route1 = new Route("Route 101: City Center to Airport");

            dispatcher.AddDriver(driver1);
            dispatcher.AddCar(car1);
            dispatcher.AddRoute(route1);

            dispatcher.AssignRouteToDriver(route1, driver1, car1);

            driver1.MarkRouteCompleted(route1, car1);

            driver1.RequestRepair(car1);

            dispatcher.AssignRouteToDriver(route1, driver1, car1);

            dispatcher.SuspendDriver(driver1);
            dispatcher.AssignRouteToDriver(route1, driver1, car1);
            dispatcher.ReactivateDriver(driver1);
            dispatcher.AssignRouteToDriver(route1, driver1, car1);
        }
    }
}