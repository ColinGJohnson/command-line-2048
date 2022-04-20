using CommandLine2048;
using Xunit;

namespace CommandLine2048Tests;

public class ArrayExtensionsTest
{
    [Fact]
    public void Test3X3RotateClockwise()
    {
        var input = new int[,]
        {
            {1, 2, 3},
            {4, 5, 6},
            {7, 8, 9},
        };

        var expected = new int[,]
        {
            {7, 4, 1},
            {8, 5, 2},
            {9, 6, 3},
        };
        
        Assert.Equal(expected, input.RotateClockwise());
    }
    
    [Fact]
    public void Test4X4RotateClockwise()
    {
        var input = new int[,]
        {
            { 1,  2,  3,  4},
            { 5,  6,  7,  8},
            { 9, 10, 11, 12},
            {13, 14, 15, 16}
        };

        var expected = new int[,]
        {
            { 13,  9, 5, 1},
            { 14, 10, 6, 2},
            { 15, 11, 7, 3},
            { 16, 12, 8, 4}
        };
        
        Assert.Equal(expected, input.RotateClockwise());
    }
}
