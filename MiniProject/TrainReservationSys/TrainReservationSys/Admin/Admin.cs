using System;
using System.Linq;

namespace TrainReservationSys.Admin
{
    public class Admin
    {
        static TrainReservationDBEntities1 db = new TrainReservationDBEntities1();

        public static void showTrains()
        {
            var trains = db.Trains.ToList();

            Console.WriteLine("----------------------------------------------------------------------------------------------------------");
            Console.WriteLine("| Train Number  |      Train Name         |  Source    |  Destination | IsActive |");
            Console.WriteLine("----------------------------------------------------------------------------------------------------------");

            foreach (var train in trains)
            {
                Console.WriteLine($"| {train.TrainNumber,-13} | {train.TrainName,-23} | {train.Source,-10} | {train.Destination,-12} | {train.IsActive,-8} |");
                Console.WriteLine("----------------------------------------------------------------------------------------------------------");
            }
        }

        public static void updateTrain()
        {
            try
            {
                Console.WriteLine("Enter Train Number to update:");
                int trainNumber = int.Parse(Console.ReadLine());

                var train = db.Trains.FirstOrDefault(t => t.TrainNumber == trainNumber);
                if (train != null)
                {
                    if (train.IsActive == null || train.IsActive == false)
                    {
                        Console.WriteLine("Train is inactive. Do you want to activate it? (yes/no)");
                        string activateChoice = Console.ReadLine().ToLower();
                        if (activateChoice == "yes")
                        {
                            ActivateTrain(trainNumber);
                        }
                        else
                        {
                            Console.WriteLine("Train not activated. Returning to the admin menu.");
                            return;
                        }
                    }

                    Console.WriteLine("Enter New Source:");
                    string newSource = Console.ReadLine();

                    Console.WriteLine("Enter New Destination:");
                    string newDestination = Console.ReadLine();

                    Console.WriteLine("Enter New Active Status (True/False):");
                    bool newActiveStatus;
                    if (!bool.TryParse(Console.ReadLine(), out newActiveStatus))
                    {
                        Console.WriteLine("Invalid input for active status. Please enter 'True' or 'False'.");
                        return;
                    }

                    train.Source = newSource;
                    train.Destination = newDestination;
                    train.IsActive = newActiveStatus;

                    db.SaveChanges();
                    Console.WriteLine("Train details updated successfully.");
                }
                else
                {
                    Console.WriteLine("Train not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public static void deleteTrain()
        {
            showTrains();
            try
            {
                Console.WriteLine("Enter Train Number to delete:");
                int trainNumber = int.Parse(Console.ReadLine());

                var train = db.Trains.FirstOrDefault(t => t.TrainNumber == trainNumber);
                if (train != null)
                {
                    if (train.IsActive == null || train.IsActive == false)
                    {
                        Console.WriteLine("Train is inactive. Do you want to activate it before deletion? (yes/no)");
                        string activateChoice = Console.ReadLine().ToLower();
                        if (activateChoice == "yes")
                        {
                            ActivateTrain(trainNumber);
                        }
                        else
                        {
                            Console.WriteLine("Train not activated. Returning to the admin menu.");
                            return;
                        }
                    }

                    train.IsActive = false; // Soft-delete by setting IsActive to false
                    db.SaveChanges();
                    Console.WriteLine("Train deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Train not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public static void AddTrain()
        {
            Console.WriteLine("Adding Train....");
            Console.WriteLine("Enter Train Number");
            int tn = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Train Name");
            string tname = Console.ReadLine();
            Console.WriteLine("Enter Source");
            string sr = Console.ReadLine();
            Console.WriteLine("Enter Destination");
            string dest = Console.ReadLine();

            db.AddTrain(tn, tname, sr, dest, true);
        }

        public static bool ValidateAdmin(int adminId, string passcode)
        {
            Admin_Details ad = new Admin_Details();
            var admin = db.Admin_Details.FirstOrDefault(a => a.Admin_id.Equals(adminId) && a.passcode.Equals(passcode));
            if (admin != null)
            {
                return true; // Admin credentials are correct
            }
            else
            {
                Console.WriteLine("Invalid admin credentials. Please try again.");
                return false; // Admin credentials are incorrect
            }
        }
        public static void UpdateFaresAndSeats(int trainNumber)
        {
            try
            {
                var train = db.Trains.FirstOrDefault(t => t.TrainNumber == trainNumber);
                if (train != null)
                {
                    if (train.IsActive == null || train.IsActive == false)
                    {
                        Console.WriteLine("Train is inactive. Do you want to activate it? (yes/no)");
                        string activateChoice = Console.ReadLine().ToLower();
                        if (activateChoice == "yes")
                        {
                            ActivateTrain(trainNumber);
                        }
                        else
                        {
                            Console.WriteLine("Train not activated. Returning to the admin menu.");
                            return;
                        }
                    }

                    Console.WriteLine($"Updating fares and seats for Train Number: {trainNumber}");

                    // Retrieve existing fare and seat information
                    var fare = db.fares.FirstOrDefault(f => f.TrainNumber == trainNumber);
                    var classes = db.train_classes.FirstOrDefault(s => s.TrainNumber == trainNumber);

                    // If fare and seat information doesn't exist, create new entries
                    if (fare == null)
                    {
                        fare = new fare { TrainNumber = trainNumber };
                        db.fares.Add(fare);
                    }
                    if (classes == null)
                    {
                        classes = new train_classes { TrainNumber = trainNumber };
                        db.train_classes.Add(classes);
                    }

                    // Prompt user for new fare and seat information
                    Console.WriteLine("Enter new fare for First Class (AC):");
                    fare.first_ac = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter new fare for Second Class (AC):");
                    fare.second_ac = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter new fare for Sleeper Class:");
                    fare.sleeper = int.Parse(Console.ReadLine());

                    Console.WriteLine("Enter new number of seats for First Class (AC):");
                    classes.first_ac = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter new number of seats for Second Class (AC):");
                    classes.second_ac = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter new number of seats for Sleeper Class:");
                    classes.sleeper = int.Parse(Console.ReadLine());

                    // Save changes to the database
                    db.SaveChanges();
                    Console.WriteLine("Fares and seats updated successfully.");
                }
                else
                {
                    Console.WriteLine("Train not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }



        public static void ShowFaresAndSeats(int trainNumber)
        {
            var fare = db.fares.FirstOrDefault(f => f.TrainNumber == trainNumber);
            var classes = db.train_classes.FirstOrDefault(s => s.TrainNumber == trainNumber);

            if (fare != null && classes != null)
            {
                Console.WriteLine("\nCurrent Fares and Seats:");
                Console.WriteLine($"First Class (AC) - Fare: {fare.first_ac}, Seats: {classes.first_ac}");
                Console.WriteLine($"Second Class (AC) - Fare: {fare.second_ac}, Seats: {classes.second_ac}");
                Console.WriteLine($"Sleeper Class - Fare: {fare.sleeper}, Seats: {classes.sleeper}");
            }
            else
            {
                Console.WriteLine("Fares and Seats not available for this train.");
            }
        }

        public static void ActivateTrain(int trainNumber)
        {
            try
            {
                var train = db.Trains.FirstOrDefault(t => t.TrainNumber == trainNumber);
                if (train != null)
                {
                    train.IsActive = true; // Set the IsActive property to true to activate the train
                    db.SaveChanges();
                    Console.WriteLine("Train activated successfully.");
                }
                else
                {
                    Console.WriteLine("Train not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
