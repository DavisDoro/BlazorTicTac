using System;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorTicTac.Client.Services
{
    public class TTLogic
    {
        public TTLogic()
        {
            PlayerId = 1;
        }
        private readonly Random random = new Random();
        public int PlayerId { get; set; }
        public bool Winner { get; set; }
        public bool InvalidMove { get; set; }
        public int[,] GameField { get; set; } = new int[3, 3] {
            { 0, 0, 0 },
            { 0, 0, 0, },
            { 0, 0, 0, } };
        public bool LastMove { get; set; }
        public int LastRow { get; set; }
        public int LastCol { get; set; }
        public int Count { get; set; } = 0;
        public int AIrow { get; set; }
        public int AIcol { get; set; }
        public int AIMoveCount { get; set; } = 0;

        public void Mark(int row, int col, int playerId)  // validate and place your mark on board
        {
            if (Winner)
            {
                return;
            }
            if (GameField[row, col] == 1 || GameField[row, col] == 2)
            {
                return;
            }
            if (LastMove) // reset previous field on board if player chose multiple
            {
                GameField[LastRow, LastCol] = 0;
            }

            GameField[row, col] = playerId;

            LastMove = true;
            InvalidMove = false;

            LastRow = row;   //obtain last selected cell
            LastCol = col;
        }
        public int RandomNumber()
        {
            return random.Next(3);
        }
        public void MoveAI()
        {
            if (AIMoveCount == 4)
            {
                return;
            }
            if (HasWon(PlayerId))
            {
                Winner = true;
                return;
            }
            if (Winner)
            {
                return;
            }
            Thread.Sleep(500);

            if (BlockMoveAI())
            {

            }
            else
            {
                do
                {
                    AIrow = RandomNumber();
                    AIcol = RandomNumber();

                } while (GameField[AIrow, AIcol] != 0);
            }


            GameField[AIrow, AIcol] = 2;
            PlayerId = 2;
            AIMoveCount++;
            ConfirmMove();
        }
        public void ConfirmMove() // confirm button
        {
            if (!LastMove)
            {
                InvalidMove = true;
                return;
            }
            LastMove = false;
            if (HasWon(PlayerId))
            {
                Winner = true;
                return;
            }
            if (PlayerId == 1)
            {
                PlayerId = 2;
            }
            else
            {
                PlayerId = 1;
            }
        }


        public void Reset() // reset game board
        {
            GameField = new int[3, 3] {
            { 0, 0, 0 },
            { 0, 0, 0, },
            { 0, 0, 0, }
            };
            Winner = false;
            if (PlayerId == 1)
            {
                PlayerId = 2;
            }
            else
            {
                PlayerId = 1;
            }
        }
        public void ResetAI() // reset game board
        {
            GameField = new int[3, 3] {
            { 0, 0, 0 },
            { 0, 0, 0, },
            { 0, 0, 0, }
            };
            Winner = false;
            PlayerId = 1;
            AIMoveCount = 0;
        }
        public bool HasWon(int playerId) // check for winner
        {
            if (WinHorizontal(playerId) || WinVertical(playerId) || WinDiag(playerId))
            {
                return true;
            }
            return false;
        }
        public bool WinHorizontal(int playerId)
        {
            Count = 0;
            for (int i = 0; i < 3; i++) // horizontal check
            {

                for (int j = 0; j < 3; j++)
                {
                    if (GameField[i, j] == playerId)
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
                    if (GameField[j, i] == playerId)
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
                if (GameField[i, i] == playerId)
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

                if (GameField[i, h] == playerId)
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



        //---- More advanced AI moves ----//
        public bool BlockMoveAI() // check for winner
        {
            if (BlockHorizontalAI() || BlockVerticalAI() || BlockDiagAI())
            {
                return true;
            }
            return false;

        }

        public bool BlockHorizontalAI()
        {
            Count = 0;
            for (int i = 0; i < 3; i++) // horizontal check
            {

                for (int j = 0; j < 3; j++)
                {
                    if
                            (GameField[i, j] == 1)
                    {
                        Count++;
                    }
                    if (Count == 2)
                    {
                        Count = 0;
                        return MegaBrainHorizontalAI(i);

                    }
                }
                Count = 0;
            }
            return false;
        }

        public bool BlockVerticalAI()
        {
            for (int i = 0; i < 3; i++) // vertical check
            {
                for (int j = 0; j < 3; j++)
                {
                    if (GameField[j, i] == 1)
                    {
                        Count++;
                    }
                    if (Count == 2)
                    {
                        Count = 0;
                        return MegaBrainVerticalAI(i);
                    }
                }
                Count = 0;
            }
            return false;
        }
        public bool BlockDiagAI()
        {
            for (int i = 0; i < 3; i++) //diag one
            {
                if (GameField[i, i] == 1)
                {
                    Count++;
                }
                if (Count == 2)
                {
                    Count = 0;
                    return MegaBrainDiagOneAI();
                }
            }
            int h = 2;
            Count = 0;

            for (int i = 0; i < 3; i++) // diag two
            {

                if (GameField[i, h] == 1)
                {
                    Count++;
                }
                if (Count == 2)
                {
                    Count = 0;
                    return MegaBrainDiagTwoAI();
                }

                h--;
            }
            Count = 0;
            return false;
        }
        public bool MegaBrainHorizontalAI(int blockThis)
        {
            for (int i = 0; i < 3; i++)
            {
                if (GameField[blockThis, i] == 0)
                {
                    GameField[blockThis, i] = 2;
                    return true;
                }
            }
            return false;
        }
        public bool MegaBrainVerticalAI(int blockThis)
        {
            for (int i = 0; i < 3; i++)
            {
                if (GameField[i, blockThis] == 0)
                {
                    GameField[i, blockThis] = 2;
                    return true;
                }
            }
            return false;
        }
        public bool MegaBrainDiagOneAI()
        {
            for (int i = 0; i < 3; i++)
            {
                if (GameField[i, i] == 0)
                {
                    GameField[i, i] = 2;
                    return true;
                }
            }
            return false;
        }
        public bool MegaBrainDiagTwoAI()
        {
            int h = 2;
            for (int i = 0; i < 3; i++)
            {
                if (GameField[i, h] == 0)
                {
                    GameField[i, h] = 2;
                    return true;
                }
                h--;
            }
            return false;
        }

    }
}
