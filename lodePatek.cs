using System;

class BattleshipGame
{
    static char[,] playerBoard;
    static char[,] computerBoard;
    static bool[,] playerHits;
    static bool[,] computerHits;
    static int playerShips;
    static int computerShips;
    static Random rand = new Random();

    static void Main()
    {
        Console.WriteLine("Zadejte velikost herního pole (např. 10 pro pole 10x10):");
        int size = int.Parse(Console.ReadLine());

        InitializeBoards(size);
        PlacePlayerShips(size);
        PlaceComputerShips(size);

        bool gameOver = false;

        while (!gameOver)
        {
            // Zobrazení stavu herních polí
            DisplayBoards(size);

            // Hráčův tah - střílí na počítačovo pole
            PlayerTurn(size);

            // Zkontroluj, zda hráč vyhrál
            if (computerShips == 0)
            {
                gameOver = true;
                Console.WriteLine("Gratulujeme! Vyhráli jste.");
                break;
            }

            // Počítačův tah - střílí na hráčovo pole
            ComputerTurn(size);

            // Zkontroluj, zda počítač vyhrál
            if (playerShips == 0)
            {
                gameOver = true;
                Console.WriteLine("Počítač vyhrál.");
                break;
            }
        }
    }

    static void InitializeBoards(int size)
    {
        playerBoard = new char[size, size];
        computerBoard = new char[size, size];
        playerHits = new bool[size, size];
        computerHits = new bool[size, size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                playerBoard[i, j] = '~'; // Voda
                computerBoard[i, j] = '~'; // Voda
                playerHits[i, j] = false;
                computerHits[i, j] = false;
            }
        }

        playerShips = 5; // Počet hráčových lodí
        computerShips = 5; // Počet počítačových lodí
    }

    static void DisplayBoards(int size)
    {
        Console.Clear();

        // Zobrazení hráčova pole
        Console.WriteLine("Hráčovo pole:");
        Console.Write("   ");
        for (int i = 0; i < size; i++)
        {
            Console.Write($"{(char)('A' + i)} ");
        }
        Console.WriteLine();
        for (int i = 0; i < size; i++)
        {
            Console.Write($"{i + 1,2} ");
            for (int j = 0; j < size; j++)
            {
                if (playerHits[i, j])
                {
                    if (playerBoard[i, j] == 'O') Console.Write("X "); // Zásah do lodi
                    else Console.Write("- "); // Zásah do vody
                }
                else if (playerBoard[i, j] == 'O')
                {
                    Console.Write("O "); // Loď
                }
                else
                {
                    Console.Write("~ "); // Voda
                }
            }
            Console.WriteLine();
        }

        // Zobrazení počítačova pole (ale ne lodě)
        Console.WriteLine("\nPočítačovo pole:");
        Console.Write("   ");
        for (int i = 0; i < size; i++)
        {
            Console.Write($"{(char)('A' + i)} ");
        }
        Console.WriteLine();
        for (int i = 0; i < size; i++)
        {
            Console.Write($"{i + 1,2} ");
            for (int j = 0; j < size; j++)
            {
                if (computerHits[i, j])
                {
                    if (computerBoard[i, j] == 'O') Console.Write("X "); // Zásah do lodi
                    else Console.Write("- "); // Zásah do vody
                }
                else
                {
                    Console.Write("~ "); // Voda
                }
            }
            Console.WriteLine();
        }
    }

    static void PlacePlayerShips(int size)
    {
        Console.WriteLine("Rozmístěte své lodě na své pole (A-J, 1-10).");

        // Funkce pro umístění lodí
        PlaceShip("Letadlová loď (5)", 5, size);
        PlaceShip("Bitevní loď (4)", 4, size);
        PlaceShip("Křižník (3)", 3, size);
        PlaceShip("Ponorka (3)", 3, size);
        PlaceShip("Torpédoborec (2)", 2, size);
    }

    static void PlaceShip(string shipName, int shipSize, int size)
    {
        bool placed = false;
        while (!placed)
        {
            Console.WriteLine($"Zadejte souřadnice pro {shipName} (např. A1 pro začátek a zadejte směr - horizontálně nebo vertikálně):");
            string input = Console.ReadLine().ToUpper();

            if (input.Length < 3)
            {
                Console.WriteLine("Neplatný formát. Zkuste to znovu.");
                continue;
            }

            char columnChar = input[0];
            int row = int.Parse(input.Substring(1, 1)) - 1; // Předpokládáme, že druhý znak je číslo řádku
            int col = columnChar - 'A';

            if (row < 0 || col < 0 || row >= size || col >= size)
            {
                Console.WriteLine("Neplatná pozice, zkuste to znovu.");
                continue;
            }

            char direction = input[2]; // Směr (H pro horizontální, V pro vertikální)

            if (IsValidShipPlacement(row, col, shipSize, direction, playerBoard, size))
            {
                // Pokud je umístění validní, umístíme loď
                for (int i = 0; i < shipSize; i++)
                {
                    if (direction == 'H')
                        playerBoard[row, col + i] = 'O'; // Značení lodě horizontálně
                    else
                        playerBoard[row + i, col] = 'O'; // Značení lodě vertikálně
                }
                placed = true;
            }
            else
            {
                Console.WriteLine("Neplatná pozice nebo směr, zkuste to znovu.");
            }
        }
    }

    static bool IsValidShipPlacement(int row, int col, int shipSize, char direction, char[,] board, int size)
    {
        // Zkontroluj, jestli loď nevystupuje za okraj pole
        if (direction == 'H' && col + shipSize > size)
            return false;
        if (direction == 'V' && row + shipSize > size)
            return false;

        // Zkontroluj, jestli se loď dotýká jiných lodí na sousedních polích
        for (int i = -1; i <= shipSize; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                int newRow = (direction == 'H') ? row + j : row + i;
                int newCol = (direction == 'V') ? col + j : col + i;

                // Ověř, zda je pozice platná
                if (newRow >= 0 && newCol >= 0 && newRow < size && newCol < size)
                {
                    // Pokud je na tomto poli loď, nebo se loď dotýká, vrátí false
                    if (board[newRow, newCol] == 'O')
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }

    static void PlaceComputerShips(int size)
    {
        // Počítač umístí své lodě náhodně
        for (int i = 0; i < 5; i++)
        {
            int shipSize = 5 - i; // Lodě mají velikosti 5, 4, 3, 3, 2
            bool placed = false;

            while (!placed)
            {
                int row = rand.Next(0, size);
                int col = rand.Next(0, size);
                char direction = rand.Next(0, 2) == 0 ? 'H' : 'V';

                if (IsValidShipPlacement(row, col, shipSize, direction, computerBoard, size))
                {
                    for (int j = 0; j < shipSize; j++)
                    {
                        if (direction == 'H')
                            computerBoard[row, col + j] = 'O'; // Značení lodě horizontálně
                        else
                            computerBoard[row + j, col] = 'O'; // Značení lodě vertikálně
                    }
                    placed = true;
                }
            }
        }
    }

    static void PlayerTurn(int size)
    {
        Console.WriteLine("Váš tah! Zadejte souřadnice pro střelu:");
        string input = Console.ReadLine().ToUpper();

        int row = int.Parse(input.Substring(1)) - 1;
        int col = input[0] - 'A';

        if (computerHits[row, col])
        {
            Console.WriteLine("Toto místo už jste střelili. Zkuste to jinde.");
            return;
        }

        computerHits[row, col] = true;

        if (computerBoard[row, col] == 'O')
        {
            Console.WriteLine("Zasáhli jste loď počítače!");
            computerShips--;
        }
        else
        {
            Console.WriteLine("Minuli jste.");
        }
    }

    static void ComputerTurn(int size)
    {
        int row, col;

        // Počítač hledá náhodné místo na střelbu
        do
        {
            row = rand.Next(0, size);
            col = rand.Next(0, size);
        } while (playerHits[row, col]);

        playerHits[row, col] = true;

        Console.WriteLine($"Počítač střelil na {(char)('A' + col)}{row + 1}.");

        if (playerBoard[row, col] == 'O')
        {
            Console.WriteLine("Počítač zasáhl vaši loď!");
            playerShips--;
        }
        else
        {
            Console.WriteLine("Počítač minul.");
        }
    }
}
