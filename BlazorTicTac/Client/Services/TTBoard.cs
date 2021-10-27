using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorTicTac.Client.Services
{
    public enum PlayerPiece
    {
        X,
        O,
        Blank
    }
    public class TTBoard
    {
        public int PlayerID { get; set; } = 0;
        public string[,] GameField { get; set; } = new string[3, 3] {
            { "", "", "" },
            { "", "", "" },
            { "", "", "" } };
        public bool Winner { get; set; }
        public int LastRow { get; set; }
        public int LastCol { get; set; }
        public int AIMoveCount { get; set; }
        public bool LastMove { get; set; }
        public bool InvalidMove { get; set; }
        public int AIrow { get; set; }
        public int AIcol { get; set; }

        private readonly Random random = new Random();

        public void Move()
        {
            GameField[0, 0] = ((PlayerPiece)PlayerID).ToString();

        }
        public void Reset() // reset game board
        {
            GameField = new string[3, 3] {
            { "", "", "" },
            { "", "", "" },
            { "", "", "" }
            };
            Winner = false;
            PlayerID = (++PlayerID) % 2;
        }
        public void ResetAI() // reset game board
        {
            GameField = new string[3, 3] {
            { "", "", "" },
            { "", "", "" },
            { "", "", "" }
            };
            Winner = false;
            PlayerID = 0;
            AIMoveCount = 0;
        }
        public void InsertValue(int x, int y, int playerID)
        {
            GameField[x, y] = ((PlayerPiece)playerID).ToString();
            Console.WriteLine($"Inserted on {x}, {y} player {playerID}");
        }
        public void Mark(int row, int col, int playerId)  // validate and place your mark on board
        {
            if (Winner)
            {
                return;
            }
            if (GameField[row, col] == "O" || GameField[row, col] == "X")
            {
                return;
            }
            if (LastMove) // reset previous field on board if player chose multiple
            {
                GameField[LastRow, LastCol] = "";
            }

            GameField[row, col] = ((PlayerPiece)PlayerID).ToString();

            LastMove = true;
            InvalidMove = false;

            LastRow = row;   //obtain last selected cell
            LastCol = col;
        }
        public void ConfirmMove() // confirm button
        {
            if (!LastMove)
            {
                InvalidMove = true;
                return;
            }
            LastMove = false;
            if (Winner)
            {
                return;
            }
            PlayerID = (++PlayerID) % 2;
        }
        //public void MoveAI()
        //{
        //    if (AIMoveCount == 4 || Winner)
        //    {
        //        return;
        //    }
        //    Thread.Sleep(500);

        //    if (BlockMoveAI())
        //    {

        //    }
        //    else
        //    {
        //        do
        //        {
        //            AIrow = random.Next(3);
        //            AIcol = random.Next(3);

        //        } while (GameField[AIrow, AIcol] != "");
        //    }

        //    GameField[AIrow, AIcol] = "1";
        //    PlayerID = 1;
        //    AIMoveCount++;
        //    ConfirmMove();
        //} //[AI]
    }
   


}
