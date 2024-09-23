using System.Security.Cryptography.X509Certificates;

namespace BattleShip
{
    internal class Program
    {

        static int[,] GenerateShip(int numberOfShips,int gridX,int gridY)           //  A method that returns a 
        {                                                                          //    filled grid of ships 
            Random random = new Random();                                         //      with x amount of ships
            int randomNumberx = random.Next(0, 5);                               //
            int randomNumbery = random.Next(0, 5);                              //    
            int shipsGenerated = 0;
            int[,] filledGrid=new int[gridX,gridY];

            while (shipsGenerated <= numberOfShips)
            {
                randomNumberx = random.Next(0, 5);
                randomNumbery = random.Next(0, 5);
                if (filledGrid[randomNumberx, randomNumbery] != 1)
                {
                    filledGrid[randomNumberx, randomNumbery] = 1;
                    shipsGenerated++;
                }
            }
            return filledGrid;
        }

        static void drawGrid(int[,]grid)
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)         // Method to print a grid to the screen
                {                                                   // Does not return anything
                    Console.Write(grid[i,j]);                       //  accepts int[,]
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }

        static void drawGrid(string[,] grid)
        {
            for (int i = 0; i < grid.GetLength(0); i++)         //  Method to print a grid 
            {                                                   //   of strings instead
                for (int j = 0; j < grid.GetLength(1); j++)     //    of integer
                {                                               //
                    Console.Write(grid[i, j]);                  //    
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }

        static int[] promptUser()
        {
            int[] coords = new int[2];

            Console.WriteLine("Input x coordinate");           //   Method to ask the user for
            coords[0] = Convert.ToInt32(Console.ReadLine());   //   to input an X and Y
            Console.WriteLine("Input y coordinate");           //   coordinate
            coords[1] = Convert.ToInt32(Console.ReadLine());   //   returns int[]
            
            return coords;
        }

        static Tuple<string,int[,]> Fire(int[] usersCoords,int[,]shipLocation, int[,]grid)                                //
        {                                                                                                   //
            string outcome="";                                                                              //    Method to check
            if (usersCoords[0] > grid.GetLength(0) || usersCoords[1] > grid.GetLength(1))                                                   //    for hits or misses
            {                                                                                               //    
                Console.WriteLine("out of bounds - Press Enter to continue");                               //
                outcome = "outofbounds";                                                                    //                
            }
            if (grid[usersCoords[0], usersCoords[1]] == 2 || grid[usersCoords[0], usersCoords[1]] == 3)
            {
                Console.Clear();
                Console.WriteLine("Already Entered that position!!");
                outcome = "outofbounds";
            }
            else if (shipLocation[usersCoords[0], usersCoords[1]] == 1)
            {
                Console.WriteLine("Hit!   Press Enter to continue ");
                grid[usersCoords[0], usersCoords[1]] = 3;
                outcome = "Hit";
                Console.WriteLine("");
                Console.ReadLine();
                Console.Clear();
            }
            else if (shipLocation[usersCoords[0], usersCoords[1]] == 0)                                        // Needs to add near miss
            {
                Console.WriteLine("Miss! Try again Press Enter to continue;");
                grid[usersCoords[0], usersCoords[1]] = 2;
                outcome = "Miss";
                Console.ReadLine();
                Console.Clear();
            }
            return Tuple.Create(outcome,grid);
        }

        // convert int matrix to string matrix
        static string[,] convertToString(int[,] grid)
        {
            string[,] convertedArray = new string[grid.GetLength(0),grid.GetLength(1)];


            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for(int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] == 0)
                    {
                        convertedArray[i,j]="~";
                    }
                    if(grid[i, j] == 1)
                    {
                        convertedArray[i,j]="0";
                    }
                    if (grid[i,j] == 2)
                    {
                        convertedArray[i,j]= "X";
                    }
                    else
                    {
                        convertedArray[i,j]="!";
                    }

                }

            }
            return convertedArray;


        }














        static void Main(string[] args)
        {
            const int gameboardX=10;
            const int gameboardY=10;
            int numberOfShips=10;
            int hits = 0;
            int misses = 0;

            int[] usersCoords = new int[2];

            int[,] displayGrid = new int[gameboardX, gameboardY];
            int[,] shipLocation = new int[gameboardX, gameboardY];

            string outcome="";

            shipLocation = GenerateShip(numberOfShips,gameboardX,gameboardY);
            convertToString(displayGrid);

            while (hits < 5 && misses<5)
            {
                drawGrid(displayGrid);
                usersCoords = promptUser();
                Fire(usersCoords, shipLocation, displayGrid);
                //displayGrid=Fire(usersCoords);
                Tuple.Equals(displayGrid, shipLocation);

                if (outcome == "Hit"&& hits != 5)
                {
                    hits++;
                    if (hits == 5)
                    {
                        Console.WriteLine("You Win!");
                        drawGrid(displayGrid);
                        Console.WriteLine();
                        drawGrid(shipLocation);
                    }
                }
                if (outcome == "Miss"&&misses!=5)
                {
                    misses++;
                    if (misses == 5)
                    {
                        Console.WriteLine("You Lose");
                        drawGrid(displayGrid);
                        Console.WriteLine();
                        drawGrid(shipLocation);
                    }
                }
            }
            }
        }
    }
