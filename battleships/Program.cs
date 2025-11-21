static void Start()
{
    Console.WriteLine("Welcome to Battleships!\n");
    Console.WriteLine("To start game press [Enter], to play with custom rules, type \"custom\" and then press [Enter]");
    string rules = Console.ReadLine();

    if (rules == "custom")
    {
        bool successHeight = false;
        bool successWidth = false;
        bool successShips = false;

        while (successHeight == false && successWidth == false && successShips == false)
        {
            Console.WriteLine("How high do you want the board to be?");
            string boardHeightString = Console.ReadLine();
            Console.WriteLine("How wide do you want the board to be?");
            string boardWidthString = Console.ReadLine();
            Console.WriteLine("How many battleships do you want each player to have?");
            string shipsAmountString = Console.ReadLine();

            successHeight = int.TryParse(boardHeightString, out int boardHeight);
            successWidth = int.TryParse(boardWidthString, out int boardWidth);
            successShips = int.TryParse(shipsAmountString, out int shipsAmount);

            if (successHeight == true && successWidth == true && successShips == true)
            {
                Console.WriteLine("");
                int[,] board1 = new int[boardHeight, boardWidth];
                int[,] board2 = board1;
                PlaceShips(true, board1, board2, boardHeight, boardWidth, shipsAmount, true);
            }
        }
    }
    else
    {
        PlaceShips(true, new int[10, 10], new int[10, 10], 10, 10, 15, true);
    }
}

static void PrintBoard(int height, int width, int[,] board1, int[,] board2)
{
    for (int y = 0; y < height; y++)
    {
        for (int x = 0; x < width; x++)
        {
            if (board1[y, x] == 0)
            {
                Console.Write("~ ");
            }
            else if (board1[y, x] == 1)
            {
                Console.Write("= ");
            }

        }
        Console.WriteLine();
    }
}

static void PlaceShips(bool p1, int[,] board1, int[,] board2, int height, int width, int shipsAmount, bool printBoard)
{
    if (printBoard == true)
    {
        PrintBoard(height, width, board1, board2);
    }
    for (int i = 0; i < shipsAmount; i++)
    {
        Console.Write($"\nWhere do you want to place your battleship nr: {i + 1}\nAfter that you have {shipsAmount - i - 1} left to place\nIf you want to do all this automaticaly just type \"auto\" as y value\ny:");

        string yPlaceString = Console.ReadLine();

        if (yPlaceString == "auto")
        {
            i = shipsAmount;
            for (int a = 0; a < shipsAmount; a++)
            {
                board1[RandomBoardSquareY(height), RandomBoardSquareX(width)] = 1;
            }
        }
        else
        {
            Console.Write("x:");
            string xPlaceString = Console.ReadLine();
            bool successY = int.TryParse(yPlaceString, out int yPlace);
            bool successX = int.TryParse(xPlaceString, out int xPlace);
            if (successY == true && successX == true)
            {
                board1[yPlace - 1, xPlace - 1] = 1;
            }
        }
    }
    PrintBoard(height, width, board1);
}

static int RandomBoardSquareY(int max)
{
    return Random.Shared.Next(max);
}
static int RandomBoardSquareX(int max)
{
    return Random.Shared.Next(max);
}

Start();
Console.ReadLine();