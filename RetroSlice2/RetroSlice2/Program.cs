//  Jonathan Rossouw 601761
//  Akonaho Singo 602433
//  Kamohelo Mabena 602556

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RetroSlice
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu();
        }

        enum MenuOptions
        {
            CaptureDetails = 1,
            CheckQualification,
            ShowStats,
            CalculateAveragePizzasPerFirstVisit, // New menu options added
            GetAgeRange,
            CheckLongTermLoyalty,
            Exit
        }
        public static void displayMenu()
        {
            // Displays the menu items in a user-friendly format
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nRetroSlice Menu:");
            Console.WriteLine("1. Capture Details");
            Console.WriteLine("2. Check Game Token Credit Qualification");
            Console.WriteLine("3. Show Current Arcade & Bowling Stats");
            Console.WriteLine("4. Calculate Average Pizzas Per First Visit"); // New menu option displayed
            Console.WriteLine("5. Show the youngest and oldest applicant");
            Console.WriteLine("6. Check Long term loyalty");
            Console.WriteLine("7. Exit");
            Console.WriteLine("-----------------------------------------------------");
            Console.Write("Enter your choice: ");
        }
        static void DisplayRetroLogo()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            string[] logoArt = {
        @" $$$$$$$\             $$\                                $$$$$$\  $$\ $$\                     ",
        @"$$  __$$\            $$ |                              $$  __$$\ $$ |\__|                    ",
        @"$$ |  $$ | $$$$$$\ $$$$$$\    $$$$$$\   $$$$$$\        $$ /  \__|$$ |$$\  $$$$$$$\  $$$$$$\  ",
        @"$$$$$$$  |$$  __$$\\_$$  _|  $$  __$$\ $$  __$$\       \$$$$$$\  $$ |$$ |$$  _____|$$  __$$\ ",
        @"$$  __$$< $$$$$$$$ | $$ |    $$ |  \__|$$ /  $$ |       \____$$\ $$ |$$ |$$ /      $$$$$$$$ |",
        @"$$ |  $$ |$$   ____| $$ |$$\ $$ |      $$ |  $$ |      $$\   $$ |$$ |$$ |$$ |      $$   ____|",
        @"$$ |  $$ |$$   ____| $$ |$$\ $$ |      $$ |  $$ |      $$\   $$ |$$ |$$ |$$ |      $$   ____|",
        @"$$ |  $$ |\$$$$$$$\  \$$$$  |$$ |      \$$$$$$  |      \$$$$$$  |$$ |$$ |\$$$$$$$\ \$$$$$$$\ ",
        @"\__|  \__| \_______|  \____/ \__|       \______/        \______/ \__|\__| \_______| \_______|"
    };

            foreach (string line in logoArt)
            {
                Console.WriteLine(line);
            }

            Console.ResetColor();
            System.Threading.Thread.Sleep(5000);
        }

        static void Menu()
        {
            string[][] applicants = new string[0][]; //Declaring 2D array to store the user values
            int qualifiedCount = 0; //A variable to count how many applicants qualify for credit
            int deniedCount = 0;
            bool hasData = false; // a variable to check if there is data in the 2D array, initialized as False, since there is no data when the program runs for the first time

            DisplayRetroLogo(); // Display the retro logo
            

            while (true)
            {             
                
                displayMenu();

                if (Enum.TryParse(Console.ReadLine(), out MenuOptions option))
                {
                    Console.WriteLine("========================================================"); // Line separation

                    switch (option)
                    {
                        case MenuOptions.CaptureDetails:
                            applicants = CaptureApplicantDetails(applicants);
                            hasData = true;
                            break;
                        case MenuOptions.CheckQualification:
                            Console.Clear();
                            if (!hasData)
                            {
                                Console.WriteLine("No data available. Please enter applicant details first.");
                            }
                            else
                            {
                                AnimateLoadingEffect(3); // Show a loading animation for 3 seconds
                                (qualifiedCount, deniedCount) = CheckQualification(applicants);
                            }
                            break;
                        case MenuOptions.ShowStats:
                            Console.Clear();
                            if (!hasData)
                            {
                                Console.WriteLine("No data available. Please enter applicant details first.");
                            }
                            else
                            {
                                AnimateLoadingEffect(3); // Show a loading animation for 3 seconds
                                ShowStats(applicants, qualifiedCount, deniedCount);                               
                            }
                            break;
                        case MenuOptions.CalculateAveragePizzasPerFirstVisit:
                            Console.Clear();
                            if (!hasData)
                            {
                                Console.WriteLine("No data available. Please enter applicant details first.");
                            }
                            else
                            {
                                AnimateLoadingEffect(5); // Show a loading animation for 5 seconds
                                Console.Clear();
                                double averagePizzasPerFirstVisit = CalculateAveragePizzasPerFirstVisit(applicants);
                                Console.WriteLine($"Average pizzas consumed per first visit: {averagePizzasPerFirstVisit:F2}");
                            }
                            break;
                        case MenuOptions.CheckLongTermLoyalty:
                            Console.Clear();
                            if (!hasData)
                            {
                                Console.WriteLine("No data available. Please enter applicant details first.");
                            }
                            else
                            {
                                AnimateLoadingEffect(4); // Show a loading animation for 4 seconds
                                Console.Clear();
                                CheckLongTermLoyalty(applicants);
                            }
                            break;
                        case MenuOptions.GetAgeRange:
                            Console.Clear();
                            if (!hasData)
                            {
                                Console.WriteLine("No data available. Please enter applicant details first.");
                            }
                            else
                            {
                                AnimateLoadingEffect(3); // Show a loading animation for 3 seconds
                                Console.Clear();
                                GetAgeRange(applicants);
                            }
                            break;
                        case MenuOptions.Exit:
                            Console.Clear();
                            AnimateLoadingEffect(2);
                            Console.WriteLine("Exiting RetroSlice...");
                            return;
                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please try again.");
                }
            }
        }

        static string[][] CaptureApplicantDetails(string[][] applicants)
        {
            List<string[]> applicantList = new List<string[]>(applicants);

            while (true)
            {
                Console.Clear();//Captures all the applicants details
                Console.Write("\nPlease enter applicant's name: ");
                string name = Console.ReadLine();

                Console.Write("Please enter applicant's age: ");
                int age = int.Parse(Console.ReadLine());

                Console.Write("Please enter applicant's high score rank: ");
                int highScoreRank = int.Parse(Console.ReadLine());

                Console.Write("Please enter applicant's starting date as a loyal customer (MM/DD/YYYY): ");
                DateTime startDate = DateTime.ParseExact(Console.ReadLine(), "MM/dd/yyyy", null);

                Console.Write("Please enter the number of pizzas consumed since first visit: ");
                int pizzasConsumed = int.Parse(Console.ReadLine());

                Console.Write("Please enter applicant's bowling high score: ");
                int bowlingHighScore = int.Parse(Console.ReadLine());

                Console.Write("Please enter applicant's current employment status (true/false): ");
                bool isEmployed = bool.Parse(Console.ReadLine());

                Console.Write("Please enter applicant's slush-puppy flavor preference: ");
                string slushPuppyFlavor = Console.ReadLine();

                Console.Write("Please enter the number of slush-puppies consumed since first visit: ");
                int slushPuppiesConsumed = int.Parse(Console.ReadLine());

                applicantList.Add(new string[]//adds all the data to the list
                {
                    name, age.ToString(), highScoreRank.ToString(), startDate.ToString("MM/dd/yyyy"),
                    pizzasConsumed.ToString(), bowlingHighScore.ToString(), isEmployed.ToString(),
                    slushPuppyFlavor, slushPuppiesConsumed.ToString()
                });

                Console.Write("Do you want to capture another applicant's details? (Y/N): ");
                string choice = Console.ReadLine().ToUpper();

                if (choice == "N")
                {
                    break;
                }
            }
            return applicantList.ToArray();//converts the list to an array
        }

        static (int, int) CheckQualification(string[][] applicants)
        {
            int qualifiedCount = 0;
            int deniedCount = 0;
            Console.Clear();
            foreach (string[] applicant in applicants)
            {
                string name = applicant[0];//assigns all the data to variables
                int age = int.Parse(applicant[1]);
                int highScoreRank = int.Parse(applicant[2]);
                DateTime startDate = DateTime.ParseExact(applicant[3], "MM/dd/yyyy", null);
                int pizzasConsumed = int.Parse(applicant[4]);
                int bowlingHighScore = int.Parse(applicant[5]);
                bool isEmployed = bool.Parse(applicant[6]);
                string slushPuppyFlavor = applicant[7];
                int slushPuppiesConsumed = int.Parse(applicant[8]);

                bool isParentEmployed = age < 18 && isEmployed;//checks qualifying criteria for the applicant
                bool isLoyalCustomer = (DateTime.Now - startDate).TotalDays >= 730;
                bool hasHighScore = highScoreRank > 2000 || bowlingHighScore > 1500 || (highScoreRank + bowlingHighScore) / 2 > 1200;
                bool hasSufficientPizzaConsumption = pizzasConsumed / ((DateTime.Now - startDate).TotalDays / 30) >= 3;
                bool hasInsufficientSlushPuppyConsumption = slushPuppiesConsumed / ((DateTime.Now - startDate).TotalDays / 30) < 4;
                bool isGooeyGulpGalore = slushPuppyFlavor.ToLower() == "gooey gulp galore";

                if (isEmployed || isParentEmployed)//checks if all criteria is met simultaneously
                {
                    if (isLoyalCustomer && hasHighScore && hasSufficientPizzaConsumption && !hasInsufficientSlushPuppyConsumption && !isGooeyGulpGalore)
                    {
                        Console.WriteLine($"{name} has qualified for game token credit.");
                        qualifiedCount++;
                    }
                    else
                    {
                        Console.WriteLine($"{name} has been denied game token credit.");
                        deniedCount++;
                    }
                }
                else
                {
                    Console.WriteLine($"{name} is not employed and is ineligible for game token credit.");
                    deniedCount++;
                }
            }
            return (qualifiedCount, deniedCount);
        }

        static void ShowStats(string[][] applicants, int qualifiedCount, int deniedCount)
        {
            Console.Clear();
            Console.WriteLine("\nCurrent Arcade & Bowling Stats:");
            Console.WriteLine($"Number of applicants granted game token credit: {qualifiedCount}");
            Console.WriteLine($"Number of applicants denied game token credit: {deniedCount}");

            Console.WriteLine("\nApplicant Details:");
            foreach (string[] applicant in applicants)//displays the applicants and the stats
            {
                string name = applicant[0];
                int bowlingHighScore = int.Parse(applicant[5]);
                int highScoreRank = int.Parse(applicant[2]);

                Console.WriteLine($"Name: {name}, Bowling High Score: {bowlingHighScore}, Arcade High Score Rank: {highScoreRank}");
            }
        }

        static double CalculateAveragePizzasPerFirstVisit(string[][] applicants)
        {
            double totalPizzasConsumed = 0;
            int totalApplicants = 0;

            foreach (string[] applicant in applicants)
            {
                int pizzasConsumed = int.Parse(applicant[4]);
                DateTime startDate = DateTime.ParseExact(applicant[3], "MM/dd/yyyy", null);

                // Calculate the number of days since the applicant's first visit
                double daysSinceFirstVisit = (DateTime.Now - startDate).TotalDays;

                // Calculate the average pizzas consumed per day since the first visit
                double averagePizzasPerDay = pizzasConsumed / daysSinceFirstVisit;

                // Add the average pizzas consumed per day to the total
                totalPizzasConsumed += averagePizzasPerDay;
                totalApplicants++;
            }

            // Calculate the overall average pizzas consumed per first visit
            double averagePizzasPerFirstVisit = totalPizzasConsumed / totalApplicants;

            return averagePizzasPerFirstVisit;
        }
        static void GetAgeRange(string[][] applicants)
        {
            int lowestAge = int.MaxValue;
            int highestAge = int.MinValue;

            foreach (string[] applicant in applicants)//sorts the ages to determine the youngest and oldest
            {
                int age = int.Parse(applicant[1]);

                if (age < lowestAge)
                    lowestAge = age;

                if (age > highestAge)
                    highestAge = age;
            }

            Console.WriteLine($"Youngest Applicant: {lowestAge}");
            Console.WriteLine($"Oldest Applicant: {highestAge}");
        }
        static void CheckLongTermLoyalty(string[][] applicants)
        {
            Console.WriteLine("\nLong-Term Loyalty Award Qualifications:");

            foreach (string[] applicant in applicants)
            {
                string name = applicant[0];
                DateTime startDate = DateTime.ParseExact(applicant[3], "MM/dd/yyyy", null);
                TimeSpan customerDuration = DateTime.Now - startDate;

                if (customerDuration.TotalDays >= 3650) // 10 years
                {
                    Console.WriteLine($"{name} qualifies for an unlimited number of credits!");
                }
                else
                {
                    Console.WriteLine($"{name} does not qualify for the long-term loyalty award.");
                }
            }
        }
        static void AnimateLoadingEffect(int waitTimeSeconds)
        {
            Console.Clear();
            Console.WriteLine("Please wait while we process your request...");
            Console.WriteLine(new string(' ', Console.WindowWidth));
            Console.WriteLine(new string(' ', Console.WindowWidth));

            int animationDuration = waitTimeSeconds * 1000; // Convert seconds to milliseconds
            int animationInterval = 200; // Interval between animation frames in milliseconds

            int currentFrame = 0;
            var animationTimer = new System.Threading.Timer(_ =>
            {
                Console.Write(new string('\u2588', currentFrame % Console.WindowWidth));
                Console.SetCursorPosition(0, Console.CursorTop);
                currentFrame++;
            }, null, 0, animationInterval);

            System.Threading.Thread.Sleep(animationDuration);
            animationTimer.Dispose();
            Console.WriteLine();
        }

    }
}