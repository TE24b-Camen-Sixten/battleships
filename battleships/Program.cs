static void Start()
{
    Console.WriteLine("Welcome to Battleships!\n");
    
    bool success1 = false;
    bool success2 = false;

    while(success1 == false && success2 == false)
    {
        Console.WriteLine("How high do you want the board to be (10 recomended)");
        string boardHeightString = Console.ReadLine();
        Console.WriteLine("How wide do you want the board to be (10 recomended)");
        string boardWidthString = Console.ReadLine();

        success1 = int.TryParse(boardHeightString, out int boardHeight);
        success2 = int.TryParse(boardWidthString, out int boardWidth);
        if (success1 == true && success2 == true)
        {   
            Console.WriteLine("");
            int[,] board = new int[boardHeight, boardWidth];
            PrintBoard(boardHeight, boardWidth, board);
        }
    }
}

static void PrintBoard(int height, int width, int[,] board)
{
    for (int y = 0; y < height; y++)
    {
        for (int x = 0; x < width; x++)
        {
            if (board[y, x] == 0)
            {
                Console.Write("~ ");
            }
            else if (board[y, x] == 1)
            {
                Console.Write("= ");
            }
                
        }
        Console.WriteLine();
    }

    PlaceShips(board, height, width);
}

static void PlaceShips(int[,] board, int height, int width)
{
    Console.Write("\nWhere do you want to place your battleships\ny:");
    string yPlaceString = Console.ReadLine();
    Console.Write("x:");
    string xPlaceString = Console.ReadLine();

    bool success1 = int.TryParse(yPlaceString, out int yPlace);
    bool success2 = int.TryParse(xPlaceString, out int xPlace);
    if (success1 == true && success2 == true)
    {
        board[yPlace - 1, xPlace - 1] = 1;
    }
}

Start();