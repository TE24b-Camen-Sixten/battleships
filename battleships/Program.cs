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
            Console.Write("~ ");
        }
        Console.WriteLine();
    }

    PlaceShips(board, height, width);
}

static void PlaceShips(int[,] board, int height, int width)
{
    Console.Write("\nVar vill du placera dina skepp?\ny:");
    string yPlaceString = Console.ReadLine();
    Console.Write("x:");
    string xPlaceString = Console.ReadLine();
}

Start();