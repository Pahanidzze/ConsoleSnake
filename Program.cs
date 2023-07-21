using System;

namespace Snake
{
    class Program
    {
        static int Px = 30;
        static int Py = 15;
        static int Fx = 20;
        static int Fy = 20;
        static bool food = true;
        static bool lose = false;
        static int score = 0;
        static int[] TALEx = new int[784];
        static int[] TALEy = new int[784];

        static int direction = 0; //1 = UP, 2 = LEFT, 3 = DOWN, 4 = RIGHT

        static void Move()
        {
            if (Console.KeyAvailable == true)
            {
                ConsoleKeyInfo key = Console.ReadKey(intercept:true);

                if (key.Key == ConsoleKey.W && direction != 3) direction = 1;
                if (key.Key == ConsoleKey.A && direction != 4) direction = 2;
                if (key.Key == ConsoleKey.S && direction != 1) direction = 3;
                if (key.Key == ConsoleKey.D && direction != 2) direction = 4;
            }


            for (int i = score; i > 0; i--)
            {
                TALEx[i] = TALEx[i - 1];
                TALEy[i] = TALEy[i - 1];
            }
            TALEx[0] = Px;
            TALEy[0] = Py;

            Console.SetCursorPosition(TALEx[score], TALEy[score]);
            Console.Write("  ");

            if (direction == 1) Py -= 1;
            if (direction == 2) Px -= 2;
            if (direction == 3) Py += 1;
            if (direction == 4) Px += 2;

            if (Px <= 1 || Px >= 59 || Py <= 0 || Py >= 29)
            {
                lose = true;
            }
            else
            {
                for (int i = 0; i < score; i++)
                {
                    if (Px == TALEx[i] && Py == TALEy[i])
                    {
                        lose = true;
                    }
                }
            }
        }

        static void Food()
        {
            var rnd = new Random();

            if (Px == Fx && Py == Fy)
            {
                food = false;
                Fx = 1;
                Fy = 1;
                while (food == false)
                {
                    if (Fx % 2 != 0)
                    {
                        Fx = rnd.Next(2, 58);
                        Fy = rnd.Next(1, 29);
                    }
                    else
                    {
                        food = true;
                    }

                }
                score++;
            }
        }

        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            for (int i = 0; i < 61; i += 2) //Up Wall
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("#");
            }
            for (int i = 0; i < 61; i += 2) //Down Wall
            {
                Console.SetCursorPosition(i, 29);
                Console.Write("#");
            }
            for (int i = 1; i < 29; i++) //Left Wall
            {
                Console.SetCursorPosition(0, i);
                Console.Write("#");
            }
            for (int i = 1; i < 29; i++) //Right Wall
            {
                Console.SetCursorPosition(60, i);
                Console.Write("#");
            }
            Console.SetCursorPosition(65, 1);
            Console.Write("Очки:");
            while (true)
            {
                //Расчёт
                if (lose == false)
                {
                    Move();
                    Food();
                }
                else
                {
                    if (Console.KeyAvailable == true) //рестарт
                    {
                        ConsoleKeyInfo key = Console.ReadKey(intercept: true);
                        Console.SetCursorPosition(Px + 1, Py);
                        Console.Write(" ");
                        if (key.Key == ConsoleKey.R)
                        {
                            lose = false;
                            Px = 30;
                            Py = 15;
                            direction = 0;
                            score = 0;

                            Console.Clear();
                            for (int i = 0; i < 61; i += 2) //Up Wall
                            {
                                Console.SetCursorPosition(i, 0);
                                Console.Write("#");
                            }
                            for (int i = 0; i < 61; i += 2) //Down Wall
                            {
                                Console.SetCursorPosition(i, 29);
                                Console.Write("#");
                            }
                            for (int i = 1; i < 29; i++) //Left Wall
                            {
                                Console.SetCursorPosition(0, i);
                                Console.Write("#");
                            }
                            for (int i = 1; i < 29; i++) //Right Wall
                            {
                                Console.SetCursorPosition(60, i);
                                Console.Write("#");
                            }
                            Console.SetCursorPosition(65, 1);
                            Console.Write("Очки:");
                        }
                        
                    }
                }

                //Очистка
                
                Console.SetCursorPosition(72, 1);
                Console.Write("   ");

                //Отрисовка

                Console.SetCursorPosition(72, 1);
                Console.Write(score);

                Console.SetCursorPosition(Fx, Fy);
                Console.Write("*");

                if (lose == true)
                {
                    Console.SetCursorPosition(66, 5);

                    Console.Write("Нажмите \"R\", чтобы начать сначала!");
                }

                Console.SetCursorPosition(Px, Py);
                Console.Write("@");
                //for (int i = 0; i <= score; i++)
                //{
                //    Console.SetCursorPosition(TALEx[i], TALEy[i]);
                //    Console.Write("@");
                //}

                //Ожидание
                System.Threading.Thread.Sleep(50);
            }
        }
    }
}
