using System;
using System.Threading;
using System.Diagnostics;

namespace Snake_Game
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch fps = new Stopwatch();
                       
            //snake bools
            bool gameOver = false;
            bool touchWalls = false;
            bool isEaten = false;
            bool touchItSelf = false;
            bool quit = false;

            int ms = 0;

            do
            { 
            
                int v = 180;
                int h = 100;
            string dir = "";

            int[] x = new int[100];
            int[] y = new int[100];

            //apple cord
            int aX = 0;
            int aY = 0;
            int appleCounter = 0;

            Console.Title = "CS Snake";

                   
                //snake head                
                x[0] = 25;
                y[0] = 10;
                               

                Console.SetCursorPosition(54, 2);
                Console.WriteLine(appleCounter); 

                SnakeBorder();

                Snake(x, y, appleCounter);
                        
                Apple(out aX, out aY, x, y, appleCounter);
            
                ConsoleKey input = Console.ReadKey().Key;                                   
                             
                //game loop
                do
                {
                    fps.Start();
                    switch (input)
                    {
                        case ConsoleKey.UpArrow:
                            if (dir != "down")
                            {
                                Console.SetCursorPosition(x[appleCounter + 1], y[appleCounter + 1]);
                                Console.Write(' ');
                                y[0]--;
                                dir = "up";
                            }
                            else
                            {
                                Console.SetCursorPosition(x[appleCounter + 1], y[appleCounter + 1]);
                                Console.Write(' ');
                                y[0]++;
                            }
                            break;
                        case ConsoleKey.DownArrow:
                            if (dir != "up")
                            {
                                Console.SetCursorPosition(x[appleCounter + 1], y[appleCounter + 1]);
                                Console.Write(' ');
                                y[0]++;
                                dir = "down";
                            }
                            else
                            {
                                Console.SetCursorPosition(x[appleCounter + 1], y[appleCounter + 1]);
                                Console.Write(' ');
                                y[0]--;
                            }
                            break;
                        case ConsoleKey.RightArrow:
                            if (dir != "left")
                            {
                                Console.SetCursorPosition(x[appleCounter + 1], y[appleCounter + 1]);
                                Console.Write(' ');
                                x[0]++;
                                dir = "right";
                            }
                            else
                            {
                                Console.SetCursorPosition(x[appleCounter + 1], y[appleCounter + 1]);
                                Console.Write(' ');
                                x[0]--;
                            }
                            break;
                        case ConsoleKey.LeftArrow:
                            if (dir != "right")
                            {
                                Console.SetCursorPosition(x[appleCounter + 1], y[appleCounter + 1]);
                                Console.Write(' ');
                                x[0]--;
                                dir = "left";
                            }
                            else
                            {
                                Console.SetCursorPosition(x[appleCounter + 1], y[appleCounter + 1]);
                                Console.Write(' ');
                                x[0]++;
                            }
                            break;
                        default:
                            break;
                    }

                    Console.CursorVisible = false;

                    //snake border
                    SnakeBorder();

                    //snake
                    Snake(x, y, appleCounter);

                    //apple
                    isEaten = EatApple(aX, aY, x[0], y[0], ref appleCounter);

                    if (isEaten)
                        Apple(out aX, out aY, x, y, appleCounter);

                    //wall collision
                    touchWalls = IsitTouching(x[0], y[0]);

                    //itself collision
                    touchItSelf = IsitTouchingItSelf(x, y, appleCounter);

                    if (touchWalls)
                        gameOver = true;

                    if (touchItSelf)
                        gameOver = true;

                    ms = dir == "up" || dir == "down" ? 180 : 100;
                    Thread.Sleep(ms);

                    if (Console.KeyAvailable)
                        input = Console.ReadKey().Key;

                    Console.SetCursorPosition(54, 2);
                    Console.WriteLine(appleCounter);

                    fps.Stop();

                    TimeSpan ts = fps.Elapsed;

                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                        ts.Hours, ts.Minutes, ts.Seconds,
                        ts.Milliseconds / 10);

                    Console.SetCursorPosition(54, 4);
                    Console.Write(elapsedTime);

                } while (!(gameOver));
                

                Console.SetCursorPosition(21, 10);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("GAME OVER!");
                               

                //quiting or retrying
                Console.SetCursorPosition(25, 11);
                string ans = Console.ReadLine();
                Console.Clear();
                if (ans == "x")
                {
                    quit = true;
                }
                else if (ans == "r")
                {
                    quit = false;
                    gameOver = false;
                    ms -= 10;
                }

            } while (!(quit));  
            

        }
               
        public static bool IsitTouchingItSelf(int[] x, int[] y, int appleCounter)
        {
            for (int i = 2; i < appleCounter + 1; i++)
            {
                if (x[0] == x[i] && y[0] == y[i])
                    return true;                
            }

            return false;
        }

        public static bool EatApple(int aX, int aY, int sX, int sY, ref int appleCounter)
        {
            if (sX == aX && sY == aY)
            {
                appleCounter++;
                return true;                
            }

            return false;
            
        }

        public static void Apple(out int x, out int y, int[] sX, int[] sY, int appleCounter)
        {
            Random rand = new Random();
            x = 0;
            y = 0;

            x = rand.Next(2, 50);
            y = rand.Next(2, 20);

            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write((char)1);

            for (int i = 0; i < appleCounter + 1; i++)
            {
                if (sX[i] == x && sY[i] == y)
                {
                    x = rand.Next(2, 50);
                    y = rand.Next(2, 20);

                    Console.SetCursorPosition(x, y);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write((char)1);
                }                
            }            
        }

        public static bool IsitTouching(int x, int y)
        {
            if (x < 2 || x > 50)
                return true;
            else if (y < 2 || y > 20)
                return true;

            return false;
        }

        public static void Snake(int[] x, int[] y, int appleCounter)
        {
            Console.SetCursorPosition(x[0], y[0]);
            Console.ForegroundColor = ConsoleColor.Yellow;            
            Console.Write("o");

            for (int i = 1; i < appleCounter + 1; i++)
            {
                Console.SetCursorPosition(x[i], y[i]);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write('o');                
            }

            for (int i = appleCounter + 1; i > 0; i--)
            {
                x[i] = x[i - 1];                
                y[i] = y[i - 1];                
            }            
        }
        
        public static void SnakeBorder()
        {
            for (int i = 0; i < 21; i++)
            {
                Console.SetCursorPosition(1, i+1);
                Console.Write('#');
                Console.ForegroundColor = ConsoleColor.White;
            }

            for (int i = 0; i < 21; i++)
            {
                Console.SetCursorPosition(51, i+1);
                Console.Write('#');
                Console.ForegroundColor = ConsoleColor.White;
            }

            for (int i = 0; i < 51; i++)
            {
                Console.SetCursorPosition(i+1, 1);
                Console.Write('#');
                Console.ForegroundColor = ConsoleColor.White;
            }

            for (int i = 0; i < 51; i++)
            {
                Console.SetCursorPosition(i+1, 21);
                Console.Write('#');
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
