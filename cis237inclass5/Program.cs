using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cis237inclass5
{
    class Program
    {
        static void Main(string[] args)
        {
            //Make a new instance of the entities class
            CarsTestEntities carsTestEntities = new CarsTestEntities();

            //*************************************
            //List out all of the cars in the table
            //*************************************
            Console.WriteLine("Print the list");

            foreach(Car car in carsTestEntities.Cars)
            {
                Console.WriteLine(car.id + " " + car.make + " " + car.model);
            }

            //**************************************
            //Find a specific one by the primary key
            //**************************************

            //Pull out a car from the table based on the id which is the primary key
            //If the record doesn't exist in the database, it will return null, so
            //check what you get back and see if it is null. If so, it doesn't exist.
            Car foundCar = carsTestEntities.Cars.Find("V0LCD1814");

            //Print it out
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Print out a found car using the Find Method");
            Console.WriteLine(foundCar.id + " " + foundCar.make + " " + foundCar.model);

            //**************************************
            //Find a specific one by any property
            //**************************************

            //Call the Where method on the table Cars and pass in a lambda expression
            //for the criteria we are lookin for. There is nothing special about the
            //word car in the part that reads: cart => car.id == "V0...". It could be
            //any characters we want it to be. It is just a variable name for the current
            //car we are considering in the expression. This will automagically loop
            //through all the Cars, and run the expression against each of them. When
            //the result is finally true, it will return that car.
            Car carToFind = carsTestEntities.Cars.Where(
                car => car.id == "V0LCD1814"
                ).First();

            Car otherCarToFind = carsTestEntities.Cars.Where(
                car => car.model == "Challenger"
                ).First();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Find 2 specific cars");
            Console.WriteLine(carToFind.id + " " + carToFind.make + " " + carToFind.model);
            Console.WriteLine(otherCarToFind.id + " " + otherCarToFind.make + " " + otherCarToFind.model);

            //****************************************************
            //Get out multiple cars
            //***************************************************

            List<Car> queryCars = carsTestEntities.Cars.Where(
                car => car.cylinders == 8
                ).ToList();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Find all cars with 8 cylinders");
            foreach (Car car in queryCars)
            {
                Console.WriteLine(car.id + " " + car.make + " " + car.model);
            }
            
        }
    }
}
