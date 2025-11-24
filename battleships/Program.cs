static void Start()
{
    Console.WriteLine("Welcome to Battleships!\n");
    Console.WriteLine("To start game press [Enter], to play with custom rules, type \"custom\" and then press [Enter]");
    string rules = Console.ReadLine();

    if (rules == "custom") //Kollar om användaren skriver custom, om hen gör det ger programmet användaren möjlighet att välja höjd, bredd och antal skepp att spela med
    {
        bool successHeight = false;
        bool successWidth = false;
        bool successShips = false;

        while (!successHeight && !successWidth && !successShips) //Ser till att användaren skriver gilltiga tal som sin höjd, bredd och antal skepp. Efter det låter den dig placera skeppen
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

            if (successHeight && successWidth && successShips)
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

static void PrintBoard(bool useBoard1, int height, int width, int[,] board1, int[,] board2) //Skriver ut brädet för antingen spelare 1 eller 2 om "värdet" bakom rutan = 0 gör den vatten (~) annars om det är 1 gör den en båt (=)
{
    for (int y = 0; y < height; y++)
    {
        for (int x = 0; x < width; x++)
        {
            if (useBoard1)
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
            else
            {
                if (board2[y, x] == 0)
                {
                    Console.Write("~ ");
                }
                else if (board2[y, x] == 1)
                {
                    Console.Write("= ");
                }
            }
        }
    }
}

static void PlaceShips(bool p1, int[,] board1, int[,] board2, int height, int width, int shipsAmount, bool printBoard) //Låter användaren placera sinna skepp, kan även skriva ut brädet genom att kalla på den metoden
{
    if (printBoard)// Om brädet ska skrivas, skrivs brädet ut genom att kalla på en metod soms kriver ut brädet
    {
        PrintBoard(p1, height, width, board1, board2);
    }
    for (int i = 0; i < shipsAmount; i++)// Låter användaren placera ett skepp i taget på valfri ruta, om användaren skriver auto så placeras skeppen på random koordinater
    {
        Console.Write($"\nWhere do you want to place your battleship nr: {i + 1}\nAfter that you have {shipsAmount - i - 1} left to place\nIf you want to do all this automaticaly just type \"auto\" as y value\ny:");

        string yPlaceString = Console.ReadLine();

        if (yPlaceString == "auto")
        {
            i = shipsAmount;
            for (int a = 0; a < shipsAmount; a++)
            {
                if (p1)
                {
                    board1[RandomBoardSquareY(height), RandomBoardSquareX(width)] = 1;
                }
                else
                {
                    board2[RandomBoardSquareY(height), RandomBoardSquareX(width)] = 1;
                }
            }
        }
        else
        {
            Console.Write("x:");
            string xPlaceString = Console.ReadLine();
            bool successY = int.TryParse(yPlaceString, out int yPlace);
            bool successX = int.TryParse(xPlaceString, out int xPlace);
            if (successY && successX)
            {
                if (p1)
                {
                    board1[yPlace - 1, xPlace - 1] = 1;
                }
                else
                {
                    board2[yPlace - 1, xPlace - 1] = 1;
                }
            }
        }
    }
    PrintBoard(p1, height, width, board1, board2);
}

static void Attack(bool p1, int[,] board1, int[,] board2)
{
    bool successY = false;
    bool successX = false;
    while (successY && successX)
    {
        Console.Write("Choose which square to attack\ny:");
        string yPlaceString = Console.ReadLine();
        Console.Write("x:");
        string xPlaceString = Console.ReadLine();
        successY = int.TryParse(yPlaceString, out int yPlace);
        successX = int.TryParse(xPlaceString, out int xPlace);
        if (successY && successX)
        {
            if (p1)
            {
                if (board2[yPlace - 1, xPlace - 1] == 1)
                {
                    Console.WriteLine("HIT!");
                }
                else
                {
                    Console.WriteLine("Miss");
                }
            }
            else
            {
                if (board1[yPlace - 1, xPlace - 1] == 1)
                {
                    Console.WriteLine("HIT!");
                }
                else
                {
                    Console.WriteLine("Miss");
                }
            }
        }
    }
}

static bool IsAlive(bool p1, int[,] board1, int[,] board2, int height, int width)
{
    for (int y = 0; y < height; y++)
    {
        for (int x = 0; x < width; x++)
        {
            if (p1)
            {
                if (board1[y, x] == 1)
                {
                    return true;
                }
            }
            else
            {
                if (board2[y, x] == 1)
                {
                    return true;
                }
            }
        }
    }
    return false;
}

static int RandomBoardSquareY(int max)//Skapar ett slumpat värde som inte är mer än höjden på brädet
{
    return Random.Shared.Next(max);
}

static int RandomBoardSquareX(int max)// skapar ett slupat värde som inte är mer än bredden på brädet
{
    return Random.Shared.Next(max);
}

Start();