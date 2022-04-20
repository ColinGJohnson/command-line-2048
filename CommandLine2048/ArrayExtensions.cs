namespace CommandLine2048;

/// <summary>
/// Contains extension methods for multidimensional arrays.
/// (See: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/extension-methods)
/// </summary>
public static class ArrayExtensions
{
    /// <summary>
    /// Extension method for rotating 2D arrays.
    /// </summary>
    /// <param name="array">The 2D array to rotate.</param>
    /// <returns>The result of rotating the input array 90 degrees clockwise.</returns>
    public static T[,] RotateClockwise<T>(this T[,] array)
    {
        var size = array.GetLength(0);
        var rotated = new T[size, size];
        
        // matrix transpose
        for (var row = 0; row < size; row++)
            for (var col = 0; col < size; col++)
                rotated[row, col] = array[col, row];   
        
        // reverse each row
        for (var row = 0; row < size; row++)
            for (var col = 0; col < size / 2; col++)
                (rotated[row, col], rotated[row, size - col - 1]) = (rotated[row, size - col - 1], rotated[row, col]);
        
        return rotated;
    }
    
    /// <summary>
    /// Extension method for shifting all the nonzero elements in an array to the left size of that array.
    /// E.g. [0, 1, 0, 2] becomes [1, 2, 0, 0]
    /// </summary>
    /// <param name="array">The array to shift.</param>
    /// <returns>The array with nonzero elements shifted to the left.</returns>
    public static int[] ShiftNonzeroLeft(this int[] array)
    {
        return array.Where(x => x != 0).Concat(Enumerable.Repeat(0, array.Count(x => x == 0))).ToArray();
    }
}
