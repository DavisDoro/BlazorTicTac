using System;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorTicTac.Client.Services
{
    public class TTLogic
    {
        public TTLogic()
        {
            this.Board = board;
            this.Bot = bot;
        }
        public TTBoard Board { get; set; }
        public AILogic Bot { get; set; }

        public bool Winner { get; set; }
        public bool InvalidMove { get; set; }
        public int Count { get; set; } = 0;

        TTBoard board = new TTBoard();
        AILogic bot = new AILogic();

        public void HasWon(int playerId) // check for winner
        {
            if (WinHorizontal(playerId) || WinVertical(playerId) || WinDiag(playerId))
            {
                Board.Winner = true;
            }

        }
        public bool WinHorizontal(int playerId)
        {
            Count = 0;
            for (int i = 0; i < 3; i++) // horizontal check
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board.GameField[i, j] == ((PlayerPiece)playerId).ToString())
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
                    if (board.GameField[j, i] == ((PlayerPiece)playerId).ToString())
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
        public bool WinDiag(int playerId)
        {
            for (int i = 0; i < 3; i++) //diag one
            {
                if (board.GameField[i, i] == ((PlayerPiece)playerId).ToString())
                {
                    Count++;
                }
                if (Count == 3)
                {
                    Count = 0;
                    return true;
                }
            }
            int h = 2;
            Count = 0;

            for (int i = 0; i < 3; i++) // diag two
            {

                if (board.GameField[i, h] == ((PlayerPiece)playerId).ToString())
                {
                    Count++;
                }

                if (Count == 3)
                {
                    Count = 0;
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
