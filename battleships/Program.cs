using System.Xml;

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
            Console.WriteLine("How high do you want the board to be? (messured in squares, do not type a unit!)");
            string boardHeightString = Console.ReadLine();
            Console.WriteLine("How wide do you want the board to be? (messured in squares, do not type a unit!)");
            string boardWidthString = Console.ReadLine();
            Console.WriteLine("How many battleships do you want each player to have?");
            string shipsAmountString = Console.ReadLine();

            successHeight = int.TryParse(boardHeightString, out int boardHeight);
            successWidth = int.TryParse(boardWidthString, out int boardWidth);
            successShips = int.TryParse(shipsAmountString, out int shipsAmount);

            if (shipsAmount > boardHeight * boardWidth)
            {
                Console.WriteLine("There are more ships than squares, redo!");
                successShips = false;
            }

            if (successHeight && successWidth && successShips)
            {
                Console.WriteLine("");
                int[,] board1 = new int[boardHeight, boardWidth];
                int[,] board2 = new int[boardHeight, boardWidth];
                PlaceShips(true, board1, board2, boardHeight, boardWidth, shipsAmount);
            }
        }
    }
    else
    {
        PlaceShips(true, new int[10, 10], new int[10, 10], 10, 10, 15);
    }
}

static void PrintBoard(bool useBoard1, int height, int width, int[,] board1, int[,] board2) //Skriver ut brädet för antingen spelare 1 eller 2 om "värdet" bakom rutan = 0 gör den vatten (~) annars om det är 1 gör den en båt (=)
{
    if (useBoard1)
    {
        Console.WriteLine("Hi player 1, this is your board:");
    }
    else
    {
        Console.WriteLine("Hi player 2, this is your board:");
    }

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
        Console.WriteLine(); //går ner en rad efter varje rad med x är klara
    }
}

static void PlaceShips(bool p1, int[,] board1, int[,] board2, int height, int width, int shipsAmount) //Låter användaren placera sinna skepp, kan även skriva ut brädet genom att kalla på den metoden
{
    PrintBoard(p1, height, width, board1, board2);

    if (p1)
    {
        Console.WriteLine("\nNow player 1 will place their ships, player 2 needs to look away");
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
                    bool noOverlap = false;
                    while (!noOverlap)
                    {
                        int randomCordY = RandomBoardSquareY(height);
                        int randomCordX = RandomBoardSquareX(width);
                        if (board1[randomCordY, randomCordX] != 1)
                        {
                            board1[randomCordY, randomCordX] = 1;
                            noOverlap = true;
                        }
                    }
                }
                else
                {
                    bool noOverlap = false;
                    while (!noOverlap)
                    {
                        int randomCordY = RandomBoardSquareY(height);
                        int randomCordX = RandomBoardSquareX(width);
                        if (board2[randomCordY, randomCordX] != 1)
                        {
                            board2[randomCordY, randomCordX] = 1;
                            noOverlap = true;
                        }
                    }
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
                Console.Clear();
                PrintBoard(p1, height, width, board1, board2);
            }
        }
    }
    if (p1)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Now it is player 2s turn to place their ships, player 1 should look away!\nWhen only player 2 is looking, type: \"p2\" and press [Enter]");
            if (Console.ReadLine() == "p2")
            {
                Console.Clear();
                PlaceShips(!p1, board1, board2, height, width, shipsAmount);
                break;
            }
        }
    }
    else
    {
        Attack(!p1, board1, board2, height, width);
    }
}

static void Attack(bool p1, int[,] board1, int[,] board2, int height, int width)
{
    bool successY = false;
    bool successX = false;
    if (p1)
    {
        while (successY == false && successX == false)
        {
            Console.Clear();
            Console.WriteLine("Now it is player 1s turn to attack, player 2 should look away!\nWhen only player 1 is looking, type: \"p1\" and press [Enter]");
            if (Console.ReadLine() == "p1")
            {
                successY = true;
                successX = true;
            }
        }
    }
    else
    {
        while (successY == false && successX == false)
        {
            Console.Clear();
            Console.WriteLine("Now it is player 2s turn to attack, player 1 should look away!\nWhen only player 2 is looking, type: \"p2\" and press [Enter]");
            if (Console.ReadLine() == "p2")
            {
                successY = true;
                successX = true;
            }
        }
    }

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
                    Console.WriteLine("HIT!\nPress [Enter] to procced");
                    Console.ReadLine();
                    if (!IsAlive(!p1, board1, board2, height, width))
                    {
                        GameOver(p1);
                    }
                }
                else
                {
                    Console.WriteLine("Miss\nPress [Enter] to procced");
                    Console.ReadLine();
                }
                Attack(!p1, board1, board2, height, width);
            }
            else
            {
                if (board1[yPlace - 1, xPlace - 1] == 1)
                {
                    Console.WriteLine("HIT!\nPress [Enter] to procced");
                    Console.ReadLine();
                    if (!IsAlive(!p1, board1, board2, height, width))
                    {
                        GameOver(p1);
                    }
                }
                else
                {
                    Console.WriteLine("Miss\nPress [Enter] to procced");
                    Console.ReadLine();
                }
                Attack(!p1, board1, board2, height, width);
            }
        }
    }
}

static void GameOver(bool winnerIsPlayer1)
{
    if (winnerIsPlayer1)
    {
        Console.WriteLine("We have a winner... Player 1!\nDo you want to play again? If yes type \"yes\"");
        if (Console.ReadLine() == "yes")
        {
            Start();
        }
    }
    else
    {
        Console.WriteLine("We have a winner... Player 2!\nDo you want to play again? If yes type \"yes\"");
        if (Console.ReadLine() == "yes")
        {
            Start();
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
// Console.ReadLine();