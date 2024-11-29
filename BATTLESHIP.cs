using System;

class Battleship
{
    static char[,] playerBoard = new char[10, 10]; // Pole pro hráče
    static char[,] computerBoard = new char[10, 10]; // Pole pro počítače
    static char[,] computerHiddenBoard = new char[10, 10]; // Skryté pole pro počítače (pro zobrazení hráči)

    static void Main()
    {
        // Inicializace prázdných polí
        InitializeBoards();

        // Umístění lodí hráče (hráč si je zadává ručně)
        PlaceShips(playerBoard);

        // Počítač si umístí lodě náhodně (stejné lodě jako hráč)
        PlaceShipsRandomly(computerBoard);

        Console.WriteLine("Hra začíná!");

        // Hlavní herní smyčka
        while (true)
        {
            // Zobrazíme aktuální stav obou polí
            Console.Clear();
            Console.WriteLine("Vaše pole:");
            PrintBoard(playerBoard, false); // Zobrazíme pole hráče
            Console.WriteLine();
            Console.WriteLine("Počítačovo skryté pole:");
            PrintBoard(computerHiddenBoard, true); // Zobrazíme skrytý stav pole počítače

            // Hráčův tah
            PlayerTurn();

            // Zkontrolujeme, zda někdo vyhrál
            if (CheckWinner(computerBoard))
            {
                Console.WriteLine("Gratulujeme, vyhráli jste!");
                break;
            }

            // Počítačův tah
            ComputerTurn();

            // Zkontrolujeme, zda počítač vyhrál
            if (CheckWinner(playerBoard))
            {
                Console.WriteLine("Počítač vyhrál!");
                break;
            }
        }
    }

    // Inicializace prázdných polí (naplníme je znakem ' ' - prázdno)
    static void InitializeBoards()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                playerBoard[i, j] = ' '; // Hráčovo pole
                computerBoard[i, j] = ' '; // Počítačovo pole
                computerHiddenBoard[i, j] = ' '; // Skrytý pohled počítače
            }
        }
    }

    // Funkce pro umístění lodí hráče (hráč si je zadává ručně)
    static void PlaceShips(char[,] board)
    {
        Console.WriteLine("Zadejte umístění vašich lodí.");
        // Tady si hráč zadává jednotlivé lodě
        PlaceShip(board, "Letadlová loď (5 polí)", 5);
        PlaceShip(board, "Bojová loď (4 polí)", 4);
        PlaceShip(board, "Křižník (3 polí)", 3);
        PlaceShip(board, "Ponorka (3 polí)", 3);
        PlaceShip(board, "Torpédoborec (2 polí)", 2);
    }

    // Funkce pro umístění jedné lodě
    static void PlaceShip(char[,] board, string shipName, int size)
    {
        bool placed = false;
        while (!placed)
        {
            Console.WriteLine($"Zadejte souřadnice pro {shipName}: ");
            Console.Write("Zadejte počáteční souřadnice (např. A1): ");
            string startPos = Console.ReadLine().ToUpper();
            Console.Write("Zadejte směr (horizontálně - H nebo vertikálně - V): ");
            string direction = Console.ReadLine().ToUpper();

            int startRow, startCol;

            // Ověření, že zadané souřadnice jsou platné
            if (!ParseCoordinates(startPos, out startRow, out startCol))
            {
                Console.WriteLine("Neplatné souřadnice.");
                continue;
            }

            // Pokusíme se umístit loď
            placed = TryPlaceShip(board, startRow, startCol, size, direction);
            if (placed)
            {
                Console.WriteLine($"{shipName} byla úspěšně umístěna.");
            }
            else
            {
                Console.WriteLine($"Nelze umístit {shipName}. Zkuste to znovu.");
            }
        }
    }

    // Funkce pro analýzu souřadnic
    static bool ParseCoordinates(string input, out int row, out int col)
    {
        if (input.Length < 2 || input.Length > 3)
        {
            row = col = -1;
            return false;
        }

        char colChar = input[0];
        string rowString = input.Substring(1);
        row = int.Parse(rowString) - 1;
        col = colChar - 'A';

        return (row >= 0 && row < 10 && col >= 0 && col < 10);
    }

    // Funkce pro umístění lodě na herním poli
    static bool TryPlaceShip(char[,] board, int startRow, int startCol, int size, string direction)
    {
        // Kontrola, zda se loď vejde na zadané pozice
        if (direction == "H")
        {
            if (startCol + size > 10) return false;
            for (int i = 0; i < size; i++)
            {
                if (board[startRow, startCol + i] != ' ') return false;
            }

            // Umístění lodě
            for (int i = 0; i < size; i++)
            {
                board[startRow, startCol + i] = 'S';
            }
        }
        else if (direction == "V")
        {
            if (startRow + size > 10) return false;
            for (int i = 0; i < size; i++)
            {
                if (board[startRow + i, startCol] != ' ') return false;
            }

            // Umístění lodě
            for (int i = 0; i < size; i++)
            {
                board[startRow + i, startCol] = 'S';
            }
        }
        else
        {
            return false; // Neplatný směr
        }

        return true;
    }

    // Funkce pro náhodné umístění lodí počítače (stejné lodě jako hráč)
    static void PlaceShipsRandomly(char[,] board)
    {
        Random rand = new Random();
        // Počítač si musí umístit stejné lodě jako hráč, ale náhodně
        PlaceShipRandom(board, 5, "Letadlová loď");
        PlaceShipRandom(board, 4, "Bojová loď");
        PlaceShipRandom(board, 3, "Křižník");
        PlaceShipRandom(board, 3, "Ponorka");
        PlaceShipRandom(board, 2, "Torpédoborec");
    }

    // Funkce pro náhodné umístění lodí počítače
    static void PlaceShipRandom(char[,] board, int size, string shipName)
    {
        Random rand = new Random();
        bool placed = false;
        while (!placed)
        {
            int direction = rand.Next(2); // 0 = Horizontální, 1 = Vertikální
            int row = rand.Next(10);
            int col = rand.Next(10);

            // Pokusíme se umístit loď
            string dir = direction == 0 ? "H" : "V";
            placed = TryPlaceShip(board, row, col, size, dir);
        }
    }

    // Funkce pro zobrazení herního pole
    static void PrintBoard(char[,] board, bool hideShips)
    {
        Console.WriteLine("  A B C D E F G H I J");
        for (int row = 0; row < 10; row++)
        {
            Console.Write((row + 1) + " ");
            for (int col = 0; col < 10; col++)
            {
                char cell = board[row, col];
                if (hideShips && cell == 'S')
                {
                    cell = ' '; // Skryjeme lodě pro zobrazení hráči
                }
                Console.Write(cell + " ");
            }
            Console.WriteLine();
        }
    }

    // Funkce pro hráčův tah
    static void PlayerTurn()
    {
        Console.Write("Zadejte souřadnice pro váš výstřel (např. A1): ");
        string input = Console.ReadLine().ToUpper();
        int row, col;
        if (!ParseCoordinates(input, out row, out col))
        {
            Console.WriteLine("Neplatné souřadnice.");
            return;
        }

        // Kontrola zásahu
        if (computerBoard[row, col] == 'S')
        {
            computerHiddenBoard[row, col] = 'X'; // Zásah
            Console.WriteLine("Zasáhl jste loď!");
        }
        else
        {
            computerHiddenBoard[row, col] = 'O'; // Míření vedle
            Console.WriteLine("Minul jste.");
        }
    }

    // Funkce pro počítačův tah
    static void ComputerTurn()
    {
        Random rand = new Random();
        int row = rand.Next(10);
        int col = rand.Next(10);

        while (playerBoard[row, col] == 'X' || playerBoard[row, col] == 'O') // Pokud už byl střelba na toto pole
        {
            row = rand.Next(10);
            col = rand.Next(10);
        }

        // Kontrola, zda počítač zasáhl loď
        if (playerBoard[row, col] == 'S')
        {
            playerBoard[row, col] = 'X'; // Zásah
            Console.WriteLine($"Počítač zasáhl vaši loď na pozici {((char)('A' + col))}{row + 1}!");
        }
        else
        {
            playerBoard[row, col] = 'O'; // Míření vedle
            Console.WriteLine($"Počítač minul na pozici {((char)('A' + col))}{row + 1}.");
        }
    }

    // Funkce pro kontrolu vítěze
    static bool CheckWinner(char[,] board)
    {
        foreach (char cell in board)
        {
            if (cell == 'S') return false; // Pokud je na poli ještě nějaká loď, hra pokračuje
        }
        return true; // Pokud nejsou žádné lodě, hráč/počítač vyhrál
    }
}
