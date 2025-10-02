using System;
using System.Threading;

class Program
{
    const int Width = 10;
    const int Height = 20;
    static int blockX = Width / 2;
    static int blockY = 0;
    
    static int[,] board = new int[Height, Width];

    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();

            // Draw the board
            // goes down the rows
            for (int y = 0; y < Height; y++)
            {
                // goes down the columns
                for (int x = 0; x < Width; x++)
                {
                    if (x == blockX && y == blockY)
                    {
                        Console.Write("#");
                    }
                    // when the loop reaches the exact spot where the block is, it draw a block symbol #
                    // otherwise it draws an empty space .
                    // save the position of the block in variables y and x
                    else if (board[y, x] == 1)
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }
                // adding a new line after each row
                Console.WriteLine();
            }

            // check for key presses
            if (Console.KeyAvailable)
            {
                // make a variable key of the type ConsoleKey to store the key that was pressed
                ConsoleKey key = Console.ReadKey(true).Key;
                // checks if the key pressed was the left arrow key and if the block is not already at the left edge
                // of the board to make sure you don't move outside the left edge of the board.
                if (key == ConsoleKey.LeftArrow && blockX > 0)
                {
                    // move the block left
                    blockX = blockX - 1;
                }
                // width - 1 because the index starts at 0, it "equals 9" for a width of 10
                else if (key == ConsoleKey.RightArrow && blockX < Width - 1)
                {
                    blockX = blockX + 1;
                }
                else if (key == ConsoleKey.Q)
                {
                    break;
                }
            }

            // block falls down every loop (not only when you press a key)

            // if height = 20, the rows go from 0-19, so when blockY = 20, it means it has reached the bottom

            // check if the block has reached the bottom of the board or if there is another block directly below it
            if (blockY == Height - 1 || board[blockY + 1, blockX] == 1)
            {
                // save the block's position on the board
                board[blockY, blockX] = 1;
                // the block went to the bottom, reset it to the top
                blockY = 0;
                // reset the block to the middle of the board
                blockX = Width / 2;
            }
            else
            {
                blockY = blockY + 1;
            }

            // control the speed of the game loop
            Thread.Sleep(300);
        }
    }
}

