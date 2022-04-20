using System.Text;

namespace CommandLine2048;

public static class Game {
    private const int EndValue = 2048;
    
    private static void Main()
    {
        var board = new Board(4);
        var boardChanged = true;
        
        while (true)
        {
            if (boardChanged)
            {
                board.AddRandomTile();
            }
            else if (!board.HasEmptySpace() && !board.FutureMovesPossible())
            {
                Console.WriteLine("You Lose.");
                break;
            }

            Console.Clear();
            PrintBoard(board);
            
            if (board.MaxValue() == EndValue)
            {
                Console.WriteLine("You Win!");
                break;
            }
            
            var input = Console.ReadKey();
            var next = new Board(board);
            
            switch (input.Key)
            {
                case ConsoleKey.W or ConsoleKey.UpArrow:
                    next = board.MergeUp();
                    break;
                case ConsoleKey.A or ConsoleKey.LeftArrow:
                    next = board.MergeLeft();
                    break;
                case ConsoleKey.S or ConsoleKey.DownArrow:
                    next = board.MergeDown();
                    break;
                case ConsoleKey.D or ConsoleKey.RightArrow:
                    next = board.MergeRight();
                    break;
                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    break;
                default:
                    boardChanged = false;
                    continue;
            }

            boardChanged = !next.Equals(board);
            board = next;
        }

        Console.WriteLine("press any key to exit...");
        Console.ReadKey();
    }

    private static ConsoleColor GetColor(int tile)
    {
        return tile switch
        {
            0 => ConsoleColor.Black,
            2 => ConsoleColor.DarkCyan,
            4 => ConsoleColor.Cyan,
            8 => ConsoleColor.Magenta,
            16 => ConsoleColor.DarkRed,
            32 => ConsoleColor.Red,
            64 => ConsoleColor.DarkYellow,
            2048 => ConsoleColor.Green,
            _ => ConsoleColor.Yellow
        };
    }
    
    /// <summary>
    /// Prints the board to the console.
    /// </summary>
    private static void PrintBoard(Board board)
    {
        string PadToFourCharWidth(int x)
        {
            var sb = new StringBuilder();
            var xStr = x.ToString();
            sb.Append(xStr);
            for (var i = 0; i < 4 - xStr.Length; i++)
                sb.Append(' ');
            return sb.ToString();
        }
        
        // print top cap
        var sb = new StringBuilder();
        sb.Append("┌──");
        for (var col = 0; col < board.Size - 1; col++)
            sb.Append("──┬──");
        sb.Append("──┐");
        Console.WriteLine(sb.ToString());

        // print rows
        for (var row = 0; row < board.Size; row++)
        {
            for (var col = 0; col < board.Size; col++)
            {
                var tile = board.Tiles[row, col];
                Console.Write("│");
                Console.BackgroundColor = GetColor(tile);
                Console.Write(PadToFourCharWidth(board.Tiles[row, col]));
                Console.ResetColor();
            }
            Console.Write("│\n");
            if (row != board.Size - 1)
            {
                sb.Clear();
                sb.Append("├──");
                for (var col = 0; col < board.Size - 1; col++)
                    sb.Append("──┼──");
                sb.Append("──┤");
                Console.WriteLine(sb.ToString());
            }
        }

        // print bottom cap
        sb.Clear();
        sb.Append("└──");
        for (var col = 0; col < board.Size - 1; col++)
            sb.Append("──┴──");
        sb.Append("──┘");
        Console.WriteLine(sb.ToString());
    }
}
