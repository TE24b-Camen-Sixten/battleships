static void PrintBoard()
{
    int boardHeight = 10;
    int boardWidth = 10;

    int[,] board = new int[boardHeight, boardWidth];

    for (int y = 0; y < boardHeight; y++)
    {
        for(int x = 0; x < boardWidth; x++)
        {
            Console.Write(board[y, x]);
        }
        Console.WriteLine();
    }
}
PrintBoard();
Console.ReadLine();