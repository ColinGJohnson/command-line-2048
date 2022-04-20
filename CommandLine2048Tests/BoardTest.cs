using CommandLine2048;
using Xunit;

namespace CommandLine2048Tests;

public class BoardTest
{
    [Fact]
    public void TestMergeRow()
    {
        Assert.Equal(new []{0, 0, 0, 0}, Board.MergeRow(new []{0, 0, 0, 0}));
        Assert.Equal(new []{2, 0, 0, 0}, Board.MergeRow(new []{0, 0, 0, 2}));
        Assert.Equal(new []{4, 0, 0, 0}, Board.MergeRow(new []{0, 0, 2, 2}));
        Assert.Equal(new []{4, 4, 0, 0}, Board.MergeRow(new []{2, 2, 2, 2}));
        Assert.Equal(new []{4, 4, 0, 0}, Board.MergeRow(new []{4, 2, 2, 0}));
        Assert.Equal(new []{4, 4, 0, 0}, Board.MergeRow(new []{4, 0, 2, 2}));
        Assert.Equal(new []{4, 16, 2, 0}, Board.MergeRow(new []{2, 2, 16, 2}));
    }
}
