namespace CommandLine2048;

public class Board : IEquatable<Board>
{
    public int[,] Tiles { get; }
    public int Size => Tiles.GetLength(0);
    private readonly Random _random = new();

    /// <summary>
    /// Constructs an empty 2048 board of the specified size.
    /// </summary>
    /// <param name="size">The size of the board to build.</param>
    /// <exception cref="ArgumentOutOfRangeException">If the size is too small (less than 3).</exception>
    public Board(int size)
    {
        if (size < 3)
             throw new ArgumentOutOfRangeException(nameof(size), "Board must be larger than three tiles wide.");
        Tiles = new int[size, size];
    }

    public Board(Board board) : this(board.Tiles) { }
    
    private Board(int[,] tiles)
    {
        if (tiles.GetLength(0) != tiles.GetLength(1))
            throw new ArgumentException("Board must be square.", nameof(tiles));
        Tiles = tiles;
    }

    /// <summary>
    /// Gets the value of the largest tile on this board.
    /// </summary>
    /// <returns>The value of the largest tile.</returns>
    public int MaxValue()
    {
        return (from int tile in Tiles select tile).Max();
    }
    
    /// <summary>
    /// Checks if there is any empty space on this board.
    /// </summary>
    /// <returns>The value of the smallest tile.</returns>
    public bool HasEmptySpace()
    {
        return (from int tile in Tiles select tile).Min() == 0;
    }


    /// <summary>
    /// Checks if one of the four movement directions will result in a modified board from the current state.
    /// </summary>
    /// <returns>True if there are still ways to move the board.</returns>
    public bool FutureMovesPossible()
    {
        return !(Equals(MergeUp())
                 && Equals(MergeDown())
                 && Equals(MergeLeft())
                 && Equals(MergeRight()));
    }
    
    public void AddRandomTile()
    {
        // can't place a tile if there is no room
        if (!HasEmptySpace()) return;
        
        // find all candidate locations for the random spawn
        var emptyTiles = new List<(int, int)>();
        for (var row = 0; row < Size; row++)
            for (var col = 0; col < Size; col++)
                if (Tiles[row, col] == 0) emptyTiles.Add((row, col));

        // place a 4 instead of a 2 with a 10% probability
        var (x, z) = emptyTiles[_random.Next(0, emptyTiles.Count)];
        Tiles[x, z] = (_random.NextDouble() < 0.1) ? 4 : 2;
    }

    public static int[] MergeRow(int[] row)
    {
        var progress = true;
        var start = 0;

        while (progress)
        {
            // shift nonzero array members to the left
            row = row.ShiftNonzeroLeft();
            var lastNonzero = Math.Max(Array.FindIndex(row, x => x == 0) - 1, row.Length - 1);
            
            // try to merge nonzero elements which have not already been merged
            progress = false;
            for (var i = start; i < lastNonzero; i++)
            {
                if (row[i] != row[i + 1]) continue;
                row[i] += row[i + 1];
                row[i + 1] = 0;
                progress = true;
                start = i + 1;
                break;
            }
        }

        return row.ShiftNonzeroLeft();
    }

    public Board MergeLeft()
    {
        var mergedTiles = new int[Size, Size];
        
        for (var row = 0; row < Size; row++)
        {
            var toMerge = new int[Size];
            for (var col = 0; col < Size; col++)
                toMerge[col] = Tiles[row, col];
            
            var merged = MergeRow(toMerge);
            for (var col = 0; col < Size; col++)
                mergedTiles[row, col] = merged[col];
        }

        return new Board(mergedTiles);
    }

    public Board MergeUp()
    {
        return RotateClockwise(3).MergeLeft().RotateClockwise(1);
    }
    
    public Board MergeRight()
    {
        return RotateClockwise(2).MergeLeft().RotateClockwise(2);
    }

    public Board MergeDown()
    {
        return RotateClockwise(1).MergeLeft().RotateClockwise(3);
    }
    
    private Board RotateClockwise(int n)
    {
        return n <= 0 ? this : new Board(Tiles.RotateClockwise()).RotateClockwise(n - 1);
    }

    public bool Equals(Board? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Size == other.Size && Tiles.Cast<int>().SequenceEqual(other.Tiles.Cast<int>());
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((Board) obj);
    }

    public override int GetHashCode()
    {
        return Tiles.GetHashCode();
    }
}
