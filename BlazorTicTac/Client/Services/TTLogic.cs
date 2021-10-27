using System;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorTicTac.Client.Services
{
    public class TTLogic
    {
        public TTLogic(TTBoard board)
        {
           this.Board = board;
        }
        public TTBoard Board { get; set; }

        public bool Winner { get; set; }
        public bool InvalidMove { get; set; }
        public int Count { get; set; } = 0;
        public int LastCol { get; set; }
        public int LastRow { get; set; }
        public bool LastMove { get; set; }

        TTBoard board = new TTBoard();

        public void Mark(int x, int y, int playerID)  // validate and place your mark on board
        {
            if (Board.Winner || !(Board.GameField[x, y]).Equals("empty")) // if is winner or field is already in use - do nothing
            {
                return;
            }
            UndoLastMove();  // Undo previous move if player chose another field on board
            Board.InsertValue(x, y, playerID);
            ObtainMarkValues(x, y);
        }

        public void ConfirmMove() // confirm button
        {
            if (!LastMove)
            {
                InvalidMove = true;
                return;
            }
            LastMove = false;
            HasWon(Board.PlayerID);
            if (Board.Winner)
            {
                return;
            }
            Board.PlayerID = (++Board.PlayerID) % 2; // swap players
        }

        public void UndoLastMove() // Undo previous move if player chose another field on board
        {
            if (LastMove) 
            {
                Board.InsertValue(LastRow, LastCol, 2);
            }
        }

        public void ObtainMarkValues(int x, int y) 
        {
            LastMove = true;
            InvalidMove = false;
            LastRow = x;   //save last selected cell
            LastCol = y;
        }

        public void HasWon(int playerId) // check for winner
        {
            if (WinHorizontal(playerId, Board.GameField) || WinVertical(playerId) || WinDiag(playerId, Board.GameField))
            {
                Board.Winner = true;
            }
        }

        public bool WinHorizontal(int playerId, string[,] gameField)
        {
            for (int i = 0; i < 3; i++) // horizontal check
            {
                for (int j = 0; j < 3; j++)
                {
                    if (gameField[i, j] == ((PlayerPiece)playerId).ToString())
                    {
                        Count++;
                    }
                    if (Count == 3)
                    {
                        Count = 0;
                        return true;
                    }
                }
                Count = 0;
            }
            return false;
        }

        public bool WinVertical(int playerId)
        {
            for (int i = 0; i < 3; i++) // vertical check
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Board.GameField[j, i] == ((PlayerPiece)playerId).ToString())
                    {
                        Count++;
                    }
                    if (Count == 3)
                    {
                        Count = 0;
                        return true;
                    }
                }
                Count = 0;
            }
            return false;
        }

        public bool WinDiag(int playerId, string[,] gameField)
        {
            for (int i = 0; i < 3; i++) //diag one
            {
                if (gameField[i, i] == ((PlayerPiece)playerId).ToString())
                {
                    Count++;
                }
                if (Count == 3)
                {
                    return true;
                }
            }
            int h = 2;
            Count = 0;

            for (int i = 0; i < 3; i++) // diag two
            {
                if (gameField[i, h] == ((PlayerPiece)playerId).ToString())
                {
                    Count++;
                }

                if (Count == 3)
                {
                    return true;
                }
                h--;
            }
            Count = 0;
            return false;
        }

        ////---- More advanced AI moves ----//
        //public bool BlockMoveAI() // Check if blocking is possible
        //{
        //    if (BlockHorizontalAI() || BlockVerticalAI() || BlockDiagAI())
        //    {
        //        return true;
        //    }
        //    return false;

        //} //[AI]

        //public bool BlockHorizontalAI()
        //{
        //    Count = 0;
        //    for (int i = 0; i < 3; i++) // horizontal check
        //    {

        //        for (int j = 0; j < 3; j++)
        //        {
        //            if
        //                    (board.GameField[i, j] == 1)
        //            {
        //                Count++;
        //            }
        //            if (Count == 2)
        //            {
        //                Count = 0;
        //                return MegaBrainHorizontalAI(i);
        //            }
        //        }
        //        Count = 0;
        //    }
        //    return false;
        //}

        //public bool BlockVerticalAI()
        //{
        //    for (int i = 0; i < 3; i++) // vertical check
        //    {
        //        for (int j = 0; j < 3; j++)
        //        {
        //            if (board.GameField[j, i] == 1)
        //            {
        //                Count++;
        //            }
        //            if (Count == 2)
        //            {
        //                Count = 0;
        //                return MegaBrainVerticalAI(i);
        //            }
        //        }
        //        Count = 0;
        //    }
        //    return false;
        //}
        //public bool BlockDiagAI()
        //{
        //    for (int i = 0; i < 3; i++) //diag one
        //    {
        //        if (board.GameField[i, i] == 1)
        //        {
        //            Count++;
        //        }
        //        if (Count == 2)
        //        {
        //            Count = 0;
        //            return MegaBrainDiagOneAI();
        //        }
        //    }
        //    int h = 2;
        //    Count = 0;

        //    for (int i = 0; i < 3; i++) // diag two
        //    {

        //        if (board.GameField[i, h] == 1)
        //        {
        //            Count++;
        //        }
        //        if (Count == 2)
        //        {
        //            Count = 0;
        //            return MegaBrainDiagTwoAI();
        //        }

        //        h--;
        //    }
        //    Count = 0;
        //    return false;
        //}
        //public bool MegaBrainHorizontalAI(int blockThis)
        //{
        //    for (int i = 0; i < 3; i++)
        //    {
        //        if (board.GameField[blockThis, i] == 0)
        //        {
        //            board.GameField[blockThis, i] = 2;
        //            return true;
        //        }
        //    }
        //    return false;
        //}
        //public bool MegaBrainVerticalAI(int blockThis)
        //{
        //    for (int i = 0; i < 3; i++)
        //    {
        //        if (board.GameField[i, blockThis] == 0)
        //        {
        //            board.GameField[i, blockThis] = 2;
        //            return true;
        //        }
        //    }
        //    return false;
        //}
        //public bool MegaBrainDiagOneAI()
        //{
        //    for (int i = 0; i < 3; i++)
        //    {
        //        if (board.GameField[i, i] == 0)
        //        {
        //            board.GameField[i, i] = 2;
        //            return true;
        //        }
        //    }
        //    return false;
        //}
        //public bool MegaBrainDiagTwoAI()
        //{
        //    int h = 2;
        //    for (int i = 0; i < 3; i++)
        //    {
        //        if (board.GameField[i, h] == 0)
        //        {
        //            board.GameField[i, h] = 2;
        //            return true;
        //        }
        //        h--;
        //    }
        //    return false;
        //}

    }
}
