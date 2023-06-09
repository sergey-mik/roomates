﻿using Roommates.Models;
using Roommates.Repositories;
using System;
using System.Collections.Generic;

namespace Roommates
{
    class Program
    {
        //  This is the address of the database.
        //  We define it here as a constant since it will never change.
        private const string CONNECTION_STRING = @"server=localhost\SQLExpress;database=Roommates;integrated security=true;TrustServerCertificate=true;";

        static void Main(string[] args)
        {

            RoomRepository roomRepo = new RoomRepository(CONNECTION_STRING);
            ChoreRepository choreRepo = new ChoreRepository(CONNECTION_STRING);
            RoommateRepository rmRepo = new RoommateRepository(CONNECTION_STRING);

            bool runProgram = true;
            while (runProgram)
            {
                string selection = GetMenuSelection();

                switch (selection)
                {
                    case ("Show all rooms"):
                        List<Room> rooms = roomRepo.GetAll();
                        foreach (Room r in rooms)
                        {
                            Console.WriteLine($"{r.Name} has an Id of {r.Id} and a max occupancy of {r.MaxOccupancy}");
                        }
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Search for room"):
                        Console.Write("Room Id: ");
                        int id = int.Parse(Console.ReadLine());

                        Room room = roomRepo.GetById(id);

                        Console.WriteLine($"{room.Id} - {room.Name} Max Occupancy({room.MaxOccupancy})");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Add a room"):
                        Console.Write("Room name: ");
                        string name = Console.ReadLine();

                        Console.Write("Max occupancy: ");
                        int max = int.Parse(Console.ReadLine());

                        Room roomToAdd = new Room()
                        {
                            Name = name,
                            MaxOccupancy = max
                        };

                        roomRepo.Insert(roomToAdd);

                        Console.WriteLine($"{roomToAdd.Name} has been added and assigned an Id of {roomToAdd.Id}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Show all chores"):
                        List<Chore> chores = choreRepo.GetAll();
                        foreach (Chore c in chores)
                        {
                            Console.WriteLine($"{c.Name} has an Id of {c.Id}");
                        }
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Search for chore"):
                        Console.Write("Chore Id: ");
                        int cid = int.Parse(Console.ReadLine());

                        Chore chore = choreRepo.GetById(cid);

                        Console.WriteLine($"{chore.Id} - {chore.Name}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Add a chore"):
                        Console.Write("Chore name: ");
                        string cname = Console.ReadLine();


                        Chore choreToAdd = new Chore()
                        {
                            Name = cname
                        };

                        choreRepo.Insert(choreToAdd);

                        Console.WriteLine($"{choreToAdd.Name} has been added and assigned an Id of {choreToAdd.Id}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Get unassigned chores"):
                        List<Chore> uchores = choreRepo.GetUnassignedChores();
                        foreach (Chore c in uchores)
                        {
                            Console.WriteLine($"{c.Name} has an Id of {c.Id}, and nobody is doing it! Seriously do we live in a barn?");
                        }
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Assign a chore"):
                        List<Chore> achores = choreRepo.GetAll();
                        List<Roommate> roommates = rmRepo.GetAll();
                        foreach (Chore c in achores)
                        {
                            Console.WriteLine($"{c.Name} has an Id of {c.Id}");
                        }
                        Console.Write("Type the ID of the chore you would like to assign: ");
                        int choreAssignId = Int32.Parse(Console.ReadLine());
                        foreach (Roommate r in roommates)
                        {
                            Console.WriteLine($"{r.FirstName} {r.LastName} has an Id of {r.Id}");
                        }
                        Console.Write("Type the ID of the roommate you would like to assign this chore to: ");
                        int rmAssignId = Int32.Parse(Console.ReadLine());
                        int newRoommateChoreId = choreRepo.AssignChore(rmAssignId, choreAssignId);
                        Console.WriteLine($"Roommate {rmAssignId} has been asigned to chore {choreAssignId} successfully with a RoommateChore Id of {newRoommateChoreId}. Press any key to continue.");
                        Console.ReadKey();
                        break;

                    case ("Search for roommate"):
                        Console.Write("Roommate Id: ");
                        int rmid = int.Parse(Console.ReadLine());

                        Roommate roommate = rmRepo.GetById(rmid);

                        Console.WriteLine($"{roommate.Id} - {roommate.FirstName} {roommate.LastName} owes portion: {roommate.RentPortion}%, and lives in {roommate.Room.Name}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Exit"):
                        runProgram = false;
                        break;
                }
            }

        }

        static string GetMenuSelection()
        {
            Console.Clear();

            List<string> options = new List<string>()
            {
                "Show all rooms",
                "Search for room",
                "Add a room",
                "Show all chores",
                "Search for chore",
                "Add a chore",
                "Get unassigned chores",
                "Assign a chore",
                "Search for roommate",
                "Exit"
            };

            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]}");
            }

            while (true)
            {
                try
                {
                    Console.WriteLine();
                    Console.Write("Select an option > ");

                    string input = Console.ReadLine();
                    int index = int.Parse(input) - 1;
                    return options[index];
                }
                catch (Exception)
                {

                    continue;
                }
            }
        }
    }
}