namespace BlazorTicTac.Client.Services
{
    public class TTLogic
    {
        public int playerId = 1;
        public bool winner = false;
        public bool invalidMove = false;
        public int[,] gameField = new int[3, 3] {
            { 0, 0, 0 },
            { 0, 0, 0, },
            { 0, 0, 0, }
        };

        public bool lastMove = false;
        int lastRow;
        int lastCol;
        int count = 0;

        public void Mark(int row, int col, int playerId)  // validate and place your mark on board
        {
            if (winner)
            {
                return;
            }
            if (gameField[row, col] == 1 || gameField[row, col] == 2)
            {
                return;
            }
            if (lastMove) // reset previous field on board if player chose multiple
            {
                gameField[lastRow, lastCol] = 0;
            }

            gameField[row, col] = playerId;

            lastMove = true;    
            invalidMove = false;

            lastRow = row;   //obtain last selected cell
            lastCol = col;
        }

        public void ConfirmMove() // confirm button
        {
            if (!lastMove)
            {
                invalidMove = true;
                return;
            }
            lastMove = false;
            if (HasWon(playerId))
            {
                winner = true;
                return;
            }
            if (playerId == 1)
            {
                playerId = 2;
            }
            else
            {
                playerId = 1;
            }

        }

        public void Reset() // reset game board
        {
            gameField = new int[3, 3] {
            { 0, 0, 0 },
            { 0, 0, 0, },
            { 0, 0, 0, }
            };  
            winner = false;
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
                    if (gameField[i, j] == playerId)
                    {
                        count++;
                    }
                    if (count == 3)
                    {
                        count = 0;
                        return true;
                    }
                }
                count = 0;
            }
            return false;
        }

        public bool WinVertical(int playerId)
        {
            for (int i = 0; i < 3; i++) // vertical check
            {

                for (int j = 0; j < 3; j++)
                {
                    if (gameField[j, i] == playerId)
                    {
                        count++;
                    }
                    if (count == 3)
                    {
                        count = 0;
                        return true;
                    }
                }
                count = 0;
            }
            return false;
        }


        public bool WinDiag(int playerId)
        {
            for (int i = 0; i < 3; i++) //diag one
            {
                if (gameField[i, i] == playerId)
                {
                    count++;
                }
                if (count == 3)
                {
                    count = 0;
                    return true;
                }
            }
            int h = 2;
            count = 0;

            for (int i = 0; i < 3; i++) // diag two
            {
                
                    if (gameField[i, h] == playerId)
                    {
                        count++;
                    }

                    if (count == 3)
                    {
                    count = 0;
                    return true;
                    }
                h--;
            }
            return false;
        }
    }
}
