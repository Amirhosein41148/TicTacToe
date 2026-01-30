using System;

namespace TicTacToeNxN
{
    class Program
    {
        static char[,] board;
        static int size;
        static char human = 'X';
        static char computer = 'O';
        static Random random = new Random();

        static void Main(string[] args)
        {
            Console.WriteLine("=== بازی دوز N×N (انسان در برابر ربات) ===\n");
            Console.Write("اندازه صفحه (N) را وارد کنید: ");
            size = int.Parse(Console.ReadLine());
            InitializeBoard();
            
            Console.WriteLine("\nشما X هستید و ربات O است.");
            Console.WriteLine("برای حرکت، شماره سطر و ستون را وارد کنید.\n");
            
            bool humanTurn = true;
            
            while (true)
            {
                PrintBoard();
                if (humanTurn) HumanMove();
                else ComputerMove();
                
                char winner = CheckWinner();
                if (winner != ' ')
                {
                    PrintBoard();
                    if (winner == human) Console.WriteLine("\n🎉 شما برنده شدید!");
                    else Console.WriteLine("\n🤖 ربات برنده شد!");
                    break;
                }
                
                if (IsBoardFull())
                {
                    PrintBoard();
                    Console.WriteLine("\n🤝 بازی مساوی شد!");
                    break;
                }
                
                humanTurn = !humanTurn;
            }
            
            Console.WriteLine("\nبازی پایان یافت.");
            Console.ReadKey();
        }
        
        static void InitializeBoard()
        {
            board = new char[size, size];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    board[i, j] = ' ';
        }
        
        static void PrintBoard()
        {
            Console.WriteLine();
            Console.Write("   ");
            for (int j = 0; j < size; j++) Console.Write($" {j} ");
            Console.WriteLine();
            
            for (int i = 0; i < size; i++)
            {
                Console.Write($"{i} ");
                for (int j = 0; j < size; j++)
                    Console.Write($"|{board[i, j]}");
                Console.WriteLine("|");
            }
        }
        
        static void HumanMove()
        {
            while (true)
            {
                try
                {
                    Console.Write($"\nحرکت شما (X). سطر (0 تا {size-1}): ");
                    int row = int.Parse(Console.ReadLine());
                    Console.Write($"ستون (0 تا {size-1}): ");
                    int col = int.Parse(Console.ReadLine());
                    
                    if (row < 0 || row >= size || col < 0 || col >= size)
                    {
                        Console.WriteLine("خطا! اعداد باید بین 0 تا {size-1} باشند.");
                        continue;
                    }
                    if (board[row, col] != ' ')
                    {
                        Console.WriteLine("این خانه قبلاً پر شده است.");
                        continue;
                    }
                    board[row, col] = human;
                    break;
                }
                catch { Console.WriteLine("ورودی نامعتبر!"); }
            }
        }
        
        static void ComputerMove()
        {
            Console.WriteLine("\n🤖 در حال فکر کردن ربات...");
            System.Threading.Thread.Sleep(500);
            
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    if (board[i, j] == ' ')
                    {
                        board[i, j] = computer;
                        if (CheckWinner() == computer)
                        {
                            Console.WriteLine($"ربات در خانه [{i},{j}] حرکت کرد.");
                            return;
                        }
                        board[i, j] = ' ';
                    }
            
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    if (board[i, j] == ' ')
                    {
                        board[i, j] = human;
                        if (CheckWinner() == human)
                        {
                            board[i, j] = computer;
                            Console.WriteLine($"ربات در خانه [{i},{j}] حرکت کرد.");
                            return;
                        }
                        board[i, j] = ' ';
                    }
            
            while (true)
            {
                int row = random.Next(0, size);
                int col = random.Next(0, size);
                if (board[row, col] == ' ')
                {
                    board[row, col] = computer;
                    Console.WriteLine($"ربات در خانه [{row},{col}] حرکت کرد.");
                    return;
                }
            }
        }
        
        static char CheckWinner()
        {
            for (int i = 0; i < size; i++)
            {
                char first = board[i, 0];
                if (first == ' ') continue;
                bool win = true;
                for (int j = 1; j < size; j++)
                    if (board[i, j] != first) { win = false; break; }
                if (win) return first;
            }
            
            for (int j = 0; j < size; j++)
            {
                char first = board[0, j];
                if (first == ' ') continue;
                bool win = true;
                for (int i = 1; i < size; i++)
                    if (board[i, j] != first) { win = false; break; }
                if (win) return first;
            }
            
            char firstDiagonal = board[0, 0];
            if (firstDiagonal != ' ')
            {
                bool win = true;
                for (int i = 1; i < size; i++)
                    if (board[i, i] != firstDiagonal) { win = false; break; }
                if (win) return firstDiagonal;
            }
            
            char secondDiagonal = board[0, size - 1];
            if (secondDiagonal != ' ')
            {
                bool win = true;
                for (int i = 1; i < size; i++)
                    if (board[i, size - 1 - i] != secondDiagonal) { win = false; break; }
                if (win) return secondDiagonal;
            }
            
            return ' ';
        }
        
        static bool IsBoardFull()
        {
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    if (board[i, j] == ' ') return false;
            return true;
        }
    }
}
