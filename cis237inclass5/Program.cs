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

            //*****************************************************
            //List out all of the cars in the table
            //*****************************************************
            Console.WriteLine("Print the list");

            foreach(Car car in carsTestEntities.Cars)
            {
                Console.WriteLine(car.id + " " + car.make + " " + car.model);
            }

            //*****************************************************
            //Find a specific one by the primary key
            //*****************************************************

            //Pull out a car from the table based on the id which is the primary key
            //If the record doesn't exist in the database, it will return null, so
            //check what you get back and see if it is null. If so, it doesn't exist.
            Car foundCar = carsTestEntities.Cars.Find("V0LCD1814");

            //Print it out
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Print out a found car using the Find Method");
            Console.WriteLine(foundCar.id + " " + foundCar.make + " " + foundCar.model);

            //*****************************************************
            //Find a specific one by any property
            //*****************************************************

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

            //*****************************************************
            //Get out multiple cars
            //*****************************************************

            List<Car> queryCars = carsTestEntities.Cars.Where(
                car => car.cylinders == 8
                ).ToList();

            //The where clause returns a query object that can be used to add
            //more queries to it.  Here we have on where clause, and then
            //after we opened another where clause.  Lastly we put it into a list.
            //The actual query is not executed until the last part is done.
            //We need to call something like toList or First to get it to actually execute
            //the query on the server.
            var queryObjectCars = carsTestEntities.Cars.Where(car => car.cylinders == 8);
            queryObjectCars = queryObjectCars.Where(car => car.horsepower == 400);
            List<Car> queryCars2 = queryObjectCars.ToList();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Find all cars with 8 cylinders");
            foreach (Car car in queryCars)
            {
                Console.WriteLine(car.id + " " + car.make + " " + car.model);
            }

            //*****************************************************
            //Add a new Car to the Database
            //*****************************************************

            //Make an instance of a new car
            Car newCarToAdd = new Car();

            //Assign properties to the parts of the model
            newCarToAdd.id = "88888";
            newCarToAdd.make = "Nissan";
            newCarToAdd.model = "GT-R";
            newCarToAdd.horsepower = 550;
            newCarToAdd.cylinders = 8;
            newCarToAdd.year = "2016";
            newCarToAdd.type = "Car";
            
            try
            {
                //Add the new car to the Cars Collection
                carsTestEntities.Cars.Add(newCarToAdd);

                //Persist the collection to the database.
                //This call will actually do the work of saving the changes to the database.
                carsTestEntities.SaveChanges();
            }
            catch (Exception e)
            {
                //Remove the new car from the Cars Collection since we can't save it
                carsTestEntities.Cars.Remove(newCarToAdd);

                //This catch might get thrown for reasons other than a primary key error.
                //Here I am assuming this is the error.
                Console.WriteLine("Can't add the record. Already have one with that primary key");
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Just added a new car.  Going to fetch and print to verify:");
            carToFind = carsTestEntities.Cars.Find("88888");
            Console.WriteLine(carToFind.id + " " + carToFind.make + " " + carToFind.model);

            //*****************************************************
            //Update a record
            //*****************************************************

            //Get out the car we want to update
            Car carToFindForUpdate = carsTestEntities.Cars.Find("88888");

            //Output the car to find before the update
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("About to do an update on the following car:");
            Console.WriteLine(carToFindForUpdate.id + " " + carToFindForUpdate.make + " " + carToFindForUpdate.model);
            Console.WriteLine("Doing the update now");

            //Update some of the properties of the car we found.  We don't have to update all of the fields if we don't want to
            carToFindForUpdate.make = "Nissssssssssssssan";
            carToFindForUpdate.model = "GT-ARRRRRRRRRRRRRRRRGH";
            carToFindForUpdate.horsepower = 9001;
            carToFindForUpdate.cylinders = 16;

            //Save the changes to the database. Since we got the model from the collection, we just have to save the changes
            carsTestEntities.SaveChanges();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Just updated the car we previously added.  Going to fetch and print to verify:");
            carToFind = carsTestEntities.Cars.Find("88888");
            Console.WriteLine(carToFind.id + " " + carToFind.make + " " + carToFind.model + " " + carToFind.horsepower + " " + carToFind.cylinders);


            //*****************************************************
            //How to Delete a record
            //*****************************************************

            //Get a car out of the database that we would like to delete
            Car carToFindForDelete = carsTestEntities.Cars.Find("88888");

            //Remove the Car from the Cars Collection
            carsTestEntities.Cars.Remove(carToFindForDelete);

            //Save the changes to the database
            carsTestEntities.SaveChanges();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Just deleted a car.  Looking to see if it is still in the Database:");

            //Check to see if the car was deleted.
            //try
            //{
            //    //This statement will execute just fine.  It's when we go to access the property id on null that the exception will be thrown
            //    carToFindForDelete = carsTestEntities.Cars.Find("88888");
            //    Console.WriteLine(carToFindForDelete.id + " " + carToFindForDelete.make + " " + carToFindForDelete.model);
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("The model you are looking for does not exist " + e.ToString() + " " + e.StackTrace);
            //}

            //Another way to check to see if the record has been deleted
            carToFindForDelete = carsTestEntities.Cars.Find("88888");

            if (carToFindForDelete == null)
            {
                Console.WriteLine("The model you are looking for does not exist");
            }
        }
    }
}