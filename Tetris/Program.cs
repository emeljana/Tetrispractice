using System;
using System.Threading;

class Program
{
    const int Width = 10;
    const int Height = 20;
    static int blockX = Width / 2;
    static int blockY = 0;
    
    // create the game board as a 2D array
    static int[,] board = new int[Height, Width];

    // Offsets for the ## piece (origin + right block)
    static (int dx, int dy)[] shapeRectangle = new (int, int)[] { (0, 0), (1, 0) };
    static (int dx, int dy)[] shapeSquare = new (int, int)[] { (0, 0), (1, 0), (0, 1), (1, 1) };
    static (int dx, int dy)[][] allShapes = new (int, int)[][]
    {
        shapeRectangle,
        shapeSquare
    };
    // current shape
    static (int dx, int dy)[] shape;
    static void NewShape()
    {
        shape = allShapes[new Random().Next(allShapes.Length)];
        blockX = Width / 2;
        blockY = 0;
    }
    

    static void Main(string[] args)
    {
        NewShape();
        while (true)
        {
            Console.Clear();

            // Draw the board
            // goes down the rows
            for (int y = 0; y < Height; y++)
            {
                // goes over the columns
                for (int x = 0; x < Width; x++)
                {
                    // assume the cell is not part of the piece
                    bool isPieceCell = false;
                    // check if the current cell is part of the falling piece
                    foreach (var (dx, dy) in shape)
                    {
                        if (x == blockX + dx && y == blockY + dy)
                        {
                            isPieceCell = true;
                            break;
                        }
                    }
                    // when the loop reaches the exact spot where the block is, it draw a block symbol #
                    // otherwise it draws an empty space "."
                    // save the position of the block in variables y and x
                    if (isPieceCell)
                    {
                        Console.Write("#");
                    }
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
            bool collision = false;
            foreach (var (dx, dy) in shape)
            {
                int newX = blockX + dx;
                int newY = blockY + dy + 1; // moving down one row
                if (newY >= Height || board[newY, newX] == 1)
                {
                    collision = true;
                    break;
                }
            }
            if (collision)
            {
                // place the piece on the board
                foreach (var (dx, dy) in shape)
                {
                    // calculate the final position of each block in the shape
                    int finalX = blockX + dx;
                    int finalY = blockY + dy;
                    // checks to ensure the block is within the bounds of the board
                    if (finalY >= 0 && finalY < Height && finalX >= 0 && finalX < Width)
                    {
                        // if yes, mark that position on the board as occupied (1)
                        board[finalY, finalX] = 1;
                    }
                }
                // spawn a new piece
                NewShape();
            }
            else
            {
                // move the block down
                blockY = blockY + 1;
            }

            // control the speed of the game loop
            Thread.Sleep(300);
        }
    }
}

