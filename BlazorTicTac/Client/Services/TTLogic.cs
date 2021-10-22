namespace BlazorTicTac.Client.Services
{
    public class TTLogic
    {
        public TTLogic()
        {
            PlayerId = 1;
        }
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
                PlayerId= 1;
            }
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
            return false;
        }
    }
}
