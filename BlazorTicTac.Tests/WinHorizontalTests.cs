using BlazorTicTac.Client;
using BlazorTicTac.Client.Services;
using System;
using Xunit;


namespace BlazorTicTac.Tests
{
    /// <summary>
    /// Ja viena 2D array rindina ir 3 PlayID simboli, atgriezt true. Citadak atgriezt false;
    /// </summary>
    public class TTLogicTests
    {
        TTBoard board = new TTBoard();

        string[,] bboard = new string[,]{
            { "empty", "empty", "empty" },
            { "empty", "empty", "empty" },
            { "empty", "empty", "empty" } };

        [Theory]
        [InlineData(true, 0, new string[3] { "0", "0", "0" }, new string[3] {"X", "X", "X"}, new string[3]{ "0", "0", "0" } )]
        [InlineData(true, 0, new string[3] { "X", "X", "X" }, new string[3] { "O", "X", "X" }, new string[3] { "0", "0", "0" })]
        [InlineData(true, 0, new string[3] { "0", "0", "0" }, new string[3] { "X", "O", "X" }, new string[3] { "X", "X", "X" })]
        [InlineData(true, 1, new string[3] { "O", "O", "O" }, new string[3] { "X", "O", "X" }, new string[3] { "X", "O", "X" })]
        [InlineData(false, 0, new string[3] { "0", "0", "0" }, new string[3] { "X", "O", "X" }, new string[3] { "O", "X", "X" })]
        [InlineData(false, 1, new string[3] { "0", "X", "0" }, new string[3] { "X", "O", "X" }, new string[3] { "X", "X", "X" })]
        public void WinHorizontal_WhenContain3PlayerIDInOneRow_ThenReturnTrue23(bool expected, int number, string[] arrayOne, string[] arrayTwo, string[] arrayThree)
        {
            //Arrange
            var winClass = new TTLogic(board);

            //Act
            string[,] bboard = new string[3, 3];
            for (int i = 0; i < 3; i++)
            {
                bboard[0, i] = arrayOne[i];
            }
            for (int i = 0; i < 3; i++)
            {
                bboard[1, i] = arrayTwo[i];
            }
            for (int i = 0; i < 3; i++)
            {
                bboard[2, i] = arrayThree[i];
            }

            //Assert
            Assert.Equal(expected, winClass.WinHorizontal(number, bboard));
        }


        [Theory]
        [InlineData(true, 1, new string[3] { "O", "0", "0" }, new string[3] { "X", "O", "X" }, new string[3] { "0", "0", "O" })]
        [InlineData(true, 0, new string[3] { "X", "O", "X" }, new string[3] { "O", "X", "X" }, new string[3] { "0", "0", "X" })]
        [InlineData(false, 0, new string[3] { "X", "0", "O" }, new string[3] { "X", "O", "X" }, new string[3] { "O", "X", "X" })]
        [InlineData(true, 1, new string[3] { "O", "O", "X" }, new string[3] { "X", "O", "X" }, new string[3] { "X", "O", "O" })]
        [InlineData(false, 0, new string[3] { "0", "0", "0" }, new string[3] { "X", "O", "X" }, new string[3] { "O", "X", "X" })]
        [InlineData(false, 1, new string[3] { "0", "X", "0" }, new string[3] { "X", "O", "X" }, new string[3] { "X", "O", "X" })]
        public void WinDiagonal_WhenContain3PlayerIDInOneRow_ThenReturnTrue23(bool expected, int number, string[] arrayOne, string[] arrayTwo, string[] arrayThree)
        {
            //Arrange
            var winClass = new TTLogic(board);

            //Act
            string[,] bboard = new string[3, 3];
            for (int i = 0; i < 3; i++)
            {
                bboard[0, i] = arrayOne[i];
            }
            for (int i = 0; i < 3; i++)
            {
                bboard[1, i] = arrayTwo[i];
            }
            for (int i = 0; i < 3; i++)
            {
                bboard[2, i] = arrayThree[i];
            }

            //Assert
            Assert.Equal(expected, winClass.WinDiag(number, bboard));
        }
    }
}
