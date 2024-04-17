using System;
using System.Linq;

namespace TrainReservationSys.User
{
    public class User
    {
        static TrainReservationDBEntities1 db = new TrainReservationDBEntities1();
        static User_details ud = new User_details();

        public static void ShowAvailableTrains()
        {
            var trains = db.Trains.ToList();

            Console.WriteLine("----------------------------------------------------------------------------------------------------------");
            Console.WriteLine("| Train Number  |      Train Name         |  Source    |  Destination | IsActive |");
            Console.WriteLine("----------------------------------------------------------------------------------------------------------");

            foreach (var train in trains)
            {
                Console.WriteLine("----------------------------------------------------------------------------------------------------------");

                Console.WriteLine($"| {train.TrainNumber,-13} | {train.TrainName,-23} | {train.Source,-10} | {train.Destination,-12} | {train.IsActive,-8} |");
                Console.WriteLine("----------------------------------------------------------------------------------------------------------");
               
            }



            
        }

        public static void BookTicket(string username, int trainNumber)
        {
            Console.WriteLine("\n----------------------------------------------");
            Console.WriteLine("             Ticket Booking Portal            ");
            Console.WriteLine("----------------------------------------------");

            // Check if the train exists and is active
            var train = db.Trains.FirstOrDefault(t => t.TrainNumber == trainNumber);

            if (train == null)
            {
                Console.WriteLine("Invalid train number. Cannot book tickets.");
                return;
            }
            else if (!(train?.IsActive ?? false))
            {
                Console.WriteLine("Train is not active. Cannot book tickets.");
                return;
            }

            ShowFares(trainNumber);
            ShowTrainClasses(trainNumber);

            BookedTicket bt = new BookedTicket();

            Random r = new Random();
            int bid = r.Next(11111, 99999);
            bt.BookingId = bid;

            bt.TrainNumber = trainNumber;

            bt.UserName = username;

            Console.WriteLine("Number of seats ");
            int seat = int.Parse(Console.ReadLine());
            bt.numberofberths = seat;
            Console.Write("For First class Enter 'first_ac'\nSecond Class 'second_ac'\nSleeper Class 'sleeper'\nYour Choice:");
            String input = Console.ReadLine().ToUpper();
            bt.@class = input;
            int totalFare = 0;
            if (input == "FIRST_AC")
                totalFare = seat * (int)db.fares.Where(f => f.TrainNumber == trainNumber).Select(f => f.first_ac).FirstOrDefault();
            else if (input == "SECOND_AC")
                totalFare = seat * (int)db.fares.Where(f => f.TrainNumber == trainNumber).Select(f => f.second_ac).FirstOrDefault();
            else if (input == "SLEEPER")
                totalFare = seat * (int)db.fares.Where(f => f.TrainNumber == trainNumber).Select(f => f.sleeper).FirstOrDefault();

            bt.TotalAmt = totalFare;
            bt.BookingDate = DateTime.Now;
            db.BookedTickets.Add(bt);
            db.SaveChanges();
            db.MgTbl_tclasses(trainNumber, input, seat);

            // Display booked ticket details
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine("          Ticket Booked Successfully            ");
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("\nHere's Your Ticket Details");
            Console.WriteLine($"Booking ID: {bid}");
            Console.WriteLine($"Train Number: {bt.TrainNumber}");
            Console.WriteLine($"User Name: {bt.UserName}");
            Console.WriteLine($"Number of Seats: {bt.numberofberths}");
            Console.WriteLine($"Class: {bt.@class}");
            Console.WriteLine($"Total Fare: {bt.TotalAmt}");
            Console.WriteLine($"Booking Date: {bt.BookingDate}");
        }



        public static void CancelTicket(int bookingId, int ticketsToCancel)
        {
            Console.WriteLine("\n------------------------------------------------");
            Console.WriteLine("            Cancel Ticket Portal           ");
            Console.WriteLine("------------------------------------------------");

            var ticketToDelete = db.BookedTickets.FirstOrDefault(t => t.BookingId == bookingId);

            if (ticketToDelete != null)
            {
                // Check if the number of tickets to cancel is valid
                if (ticketsToCancel <= 0 || ticketsToCancel > ticketToDelete.numberofberths)
                {
                    Console.WriteLine("Invalid number of tickets to cancel.");
                    return;
                }

                int tno = (int)db.BookedTickets.Where(b => b.BookingId == bookingId).Select(ct => ct.TrainNumber).FirstOrDefault();
                string cls = (string)db.BookedTickets.Where(b => b.BookingId == bookingId).Select(ct => ct.@class).FirstOrDefault();
                int refundAmount = CalculateRefund(ticketToDelete.TotalAmt ?? 0, ticketsToCancel);

               

              
                ticketToDelete.numberofberths -= ticketsToCancel;

                
                if (ticketToDelete.numberofberths <= 0)
                {
                    db.BookedTickets.Remove(ticketToDelete);
                    Console.WriteLine("All tickets canceled successfully.");
                }
                else
                {
                    
                    Console.WriteLine($"{ticketsToCancel} ticket(s) canceled successfully. {ticketToDelete.numberofberths} ticket(s) remaining.");
                }

              
                Console.WriteLine($"Refund Amount: {refundAmount}");
              
                ticketToDelete.RefundedAmount = refundAmount;

                db.SaveChanges();

             
                db.MgTbl_tclassesCan(tno, cls, ticketsToCancel);
            }
            else
            {
                Console.WriteLine("Invalid booking ID. No ticket found.");
            }

            Console.ReadLine();
        }

        // Method to calculate the refund amount
        public static int CalculateRefund(int totalAmount, int ticketsToCancel)
        {
            //refunding 80% of the total amount per canceled ticket
            double refundPercentage = 0.8;
            double refundAmountPerTicket = totalAmount * refundPercentage / ticketsToCancel;
            return (int)Math.Round(refundAmountPerTicket);
        }


        public static void ShowTickets(string userName)
        {
            var userTickets = db.BookedTickets.Where(t => t.UserName == userName).ToList();

            if (userTickets.Any())
            {
                Console.WriteLine("\n--------------------------------------------------");
                Console.WriteLine($"Tickets booked by User ID {userName}:");
                foreach (var ticket in userTickets)
                {
                    Console.WriteLine("--------------------------------------------------");
                    Console.WriteLine($"Booking ID: {ticket.BookingId}");
                    Console.WriteLine($"Train Number: {ticket.TrainNumber}");
                    Console.WriteLine($"Number of Seats: {ticket.numberofberths}");
                    Console.WriteLine($"Class: {ticket.@class}");
                    Console.WriteLine($"Total Fare: {ticket.TotalAmt}");
                    Console.WriteLine($"Booking Date: {ticket.BookingDate}");
                }
                Console.WriteLine("--------------------------------------------------");
            }
            else
            {
                Console.WriteLine("No tickets found for this user.");
            }
        }

        public static void ShowFares(int trainNumber)
        {
            var fare = db.fares.FirstOrDefault(f => f.TrainNumber == trainNumber);

            if (fare != null)
            {
                Console.WriteLine("\n----------------------------------------------------------------------");
                Console.WriteLine($"Fares for Train Number {trainNumber}:");
                Console.WriteLine($"First Class (AC): {fare.first_ac}");
                Console.WriteLine($"Second Class (AC): {fare.second_ac}");
                Console.WriteLine($"Sleeper Class: {fare.sleeper}");
                Console.WriteLine("----------------------------------------------------------------------\n");
            }
            else
            {
                Console.WriteLine("Fares not available for this train.");
            }
        }
        public static void ShowTrainClasses(int trainNumber)
        {
            var classes = db.train_classes.FirstOrDefault(tc => tc.TrainNumber == trainNumber);

            if (classes != null)
            {
                Console.WriteLine("\n----------------------------------------------------------------------");
                Console.WriteLine($"Classes available for Train Number {trainNumber}:");
                Console.WriteLine($"First Class (AC): {classes.first_ac}");
                Console.WriteLine($"Second Class (AC): {classes.second_ac}");
                Console.WriteLine($"Sleeper Class: {classes.sleeper}");
                Console.WriteLine("----------------------------------------------------------------------\n");
            }
            else
            {
                Console.WriteLine("Classes not available for this train.");
            }
        }

        public static bool ExistingUserLogin(int userId, string userName)
        {
            // Check if the user exists in the database
            var existingUser = db.User_details.FirstOrDefault(u => u.User_id == userId && u.User_Name == userName);

            return existingUser != null;
        }

        public static void NewUserRegistration(int userId, string userName, int age)
        {
            // Check if the user already exists
            var existingUser = db.User_details.FirstOrDefault(u => u.User_id == userId);

            if (existingUser != null)
            {
                Console.WriteLine("User with the provided ID already exists. Please choose a different ID.");
                return;
            }

            // Create a new user entry in the database
            User_details newUser = new User_details
            {
                User_id = userId,
                User_Name = userName,
                Age = age
            };

            db.User_details.Add(newUser);
            db.SaveChanges();

            Console.WriteLine("User registered successfully!");
        }
        public static bool ValidateUser(string username)
        {
            var existingUser = db.User_details.FirstOrDefault(u => u.User_Name == username);
            return existingUser != null;
        }
    }
}


