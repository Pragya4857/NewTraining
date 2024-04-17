﻿using System;
using System.Threading;

namespace TrainReservationSys
{
    class Program
    {
        static void Main(string[] args)
        {
            AnimateWelcomeMessage();

            while (true)
            {
                Console.WriteLine("Are you an admin, a user, or do you want to exit?");
                Console.WriteLine("1. Admin");
                Console.WriteLine("2. User");
                Console.WriteLine("3. Exit");
                Console.Write("Enter your choice: ");

                if (!int.TryParse(Console.ReadLine(), out int userType))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (userType)
                {
                    case 1:
                        Console.WriteLine("╔════════════════════════════╗");
                        Console.WriteLine("║       Welcome, Admin!      ║");
                        Console.WriteLine("╚════════════════════════════╝");
                        if (Validate_Admin())
                        {
                            AdminMenu();
                        }
                        else
                        {
                            Console.WriteLine("Invalid admin credentials \n--------Try Again------");
                        }
                        break;
                    case 2:
                        Console.WriteLine("╔════════════════════════════╗");
                        Console.WriteLine("║         Welcome, User!     ║");
                        Console.WriteLine("╚════════════════════════════╝");
                        UserMenu();
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter 1, 2, or 3.");
                        break;
                }
            }
        }

        static void AnimateWelcomeMessage()
        {
            Console.WriteLine("Welcome to Train Reservation System!");

            Console.WriteLine("Loading...");

            string[] animationFrames = { ".", "..", "...", "....", "....." };

            foreach (string frame in animationFrames)
            {
                Console.Write($"\r{frame}");
                Thread.Sleep(500);
            }

            Console.Clear();
            Console.WriteLine("Welcome to Train Reservation System!\n");
            Console.WriteLine("Here You Go");
        }

        static bool Validate_Admin()
        {
            Console.Write("Enter Admin-ID: ");
            int adminId = int.Parse(Console.ReadLine());

            Console.Write("Enter Admin Password: ");
            string passcode = Console.ReadLine();
            return Admin.Admin.ValidateAdmin(adminId, passcode);
        }

        static void AdminMenu()
        {
            
            bool adminMenuActive = true;
            while (adminMenuActive)
            {
                Console.WriteLine("\nAvailable Trains:");
                Admin.Admin.showTrains();
                Console.WriteLine("\nNow, you can delete/update/add a train.");
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Update Train");
                Console.WriteLine("2. Delete Train");
                Console.WriteLine("3. Add Train");
                Console.WriteLine("4. Update Fares and Seats");
                Console.WriteLine("5. No, back to main menu");
                Console.Write("Enter your choice: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        Admin.Admin.updateTrain();
                        break;
                    case 2:
                        Admin.Admin.deleteTrain();
                        break;
                    case 3:
                        Admin.Admin.AddTrain();
                        break;
                    case 4:
                        Console.Write("Enter the train number: ");
                        if (!int.TryParse(Console.ReadLine(), out int trainNumber))
                        {
                            Console.WriteLine("Invalid input. Please enter a number.");
                            continue;
                        }
                        Admin.Admin.UpdateFaresAndSeats(trainNumber);
                        break;
                    case 5:
                        adminMenuActive = false; 
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter 1, 2, 3, 4, or 5.");
                        break;
                }
            }
        }

        static void UserMenu()
        {
            bool userMenuActive = true;
            while (userMenuActive)
            {
                Console.WriteLine("\nAvailable Trains:");
                User.User.ShowAvailableTrains();

                Console.WriteLine("\nDo you want to book, cancel, or view tickets?");
                Console.WriteLine("1. Book a Ticket");
                Console.WriteLine("2. Cancel a Ticket");
                Console.WriteLine("3. View Tickets");
                Console.WriteLine("4. No, just browsing");
                Console.Write("Enter your choice: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        Console.Write("Enter your username: ");
                        string userName = Console.ReadLine();

                        if (!User.User.ValidateUser(userName))
                        {
                            Console.WriteLine("User does not exist. Would you like to register? (yes/no)");
                            string registerChoice = Console.ReadLine().ToLower();

                            if (registerChoice == "yes")
                            {
                                Console.Write("Enter your user ID: ");
                                int userId = int.Parse(Console.ReadLine());

                                Console.Write("Enter your age: ");
                                int age = int.Parse(Console.ReadLine());

                                User.User.NewUserRegistration(userId, userName, age);
                            }
                            else
                            {
                                Console.WriteLine("Returning to the main menu.");
                                continue;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Welcome back, " + userName + "!");
                        }

                        Console.Write("Enter the train number: ");
                        if (!int.TryParse(Console.ReadLine(), out int trainNumber))
                        {
                            Console.WriteLine("Invalid input. Please enter a number.");
                            continue;
                        }
                        User.User.BookTicket(userName, trainNumber);

                        break;
                    case 2:
                        // Prompt the user to enter the booking ID
                        Console.Write("Enter the Booking ID of the ticket to cancel: ");
                        if (!int.TryParse(Console.ReadLine(), out int bookingId))
                        {
                            Console.WriteLine("Invalid input for booking ID. Please enter a valid integer.");
                            return; 
                        }

                        
                        Console.Write("Enter the number of tickets to cancel: ");
                        if (!int.TryParse(Console.ReadLine(), out int ticketsToCancel))
                        {
                            Console.WriteLine("Invalid input for tickets to cancel. Please enter a valid integer.");
                            return; 
                        }

                        // Call the CancelTicket method
                        User.User.CancelTicket(bookingId, ticketsToCancel);
                        break;

                    case 3:
                        Console.Write("Enter your username: ");
                        string viewUserName = Console.ReadLine();
                        User.User.ShowTickets(viewUserName);
                        break;
                    case 4:
                        userMenuActive = false; // Exit the loop
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter 1, 2, 3, or 4.");
                        break;
                }
            }
        }
    }
}