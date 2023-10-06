﻿namespace ClassLibrary
{
    internal class CustomTTT : Match
    {
        #region Variables
        public bool p_twist { get; set; }
        public bool p_twistStat { get; set; }

        private Random p_random = new Random();
        #endregion

        public CustomTTT(bool p_twist)
            : base(
                  p_twist ? 8 : AskForRows(),
                  p_twist ? 5 : AskForColumns(),
                  p_twist ? 4 : AskForWinningLength()
              )
        {
            p_gameType = p_twist ? "Twist" : "CustomTTT";
            this.p_twist = p_twist;
            ResetBoard();
        }
        public override void GameMechanic(List<Player> p_player)
        {
            base.GameMechanic(p_player);

            if (p_firstTurn)
            {
                PrintBoard(true, true, p_player);
                p_firstTurn = false;
            }
            bool p_validInput = false;
            while (!p_validInput)
            {
                Console.WriteLine();
                Console.WriteLine($"{p_player[p_currentPlayerIndex].Name}, input a coordinate X/Y");
                SavePlayerToMatch(p_player[p_currentPlayerIndex].Ident, p_matchId);
                string p_input = Console.ReadLine();
                string[] p_parts = p_input.Split('/');

                if (p_parts.Length == 2
                    && int.TryParse(p_parts[0], out int p_row)
                    && int.TryParse(p_parts[1], out int p_col)
                    && p_row >= 1 && p_row <= p_rows
                    && p_col >= 1 && p_col <= p_columns)
                {
                    p_row--;
                    p_col--;

                    if (GetCell(p_row, p_col) == '0')
                    {
                        SetCell(p_row, p_col, p_player[p_currentPlayerIndex].Icon);
                        p_currentPlayerIndex = (p_currentPlayerIndex + 1) % p_player.Count;
                        p_validInput = true;
                        if (p_twist)
                        {
                            p_twistStat = TwistColumn(p_col);
                        }
                        SaveMoveHistory(p_player[p_currentPlayerIndex].Ident, p_input, p_matchId, p_twistStat);

                    }
                    else
                    {
                        Console.WriteLine("Cell already occupied. Try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Try again.");
                }
            }

            PrintBoard(true, true, p_player);
        }

        #region CustomSetup
        private static int AskForRows()
        {
            Console.Write("Enter the number of rows: ");
            int rows;
            if (int.TryParse(Console.ReadLine(), out rows))
            {
                return rows;
            }
            Console.WriteLine("Invalid input for rows. Using default value of 3.");
            return 3;
        }
        private static int AskForColumns()
        {
            Console.Write("Enter the number of columns: ");
            int columns;
            if (int.TryParse(Console.ReadLine(), out columns))
            {
                return columns;
            }
            Console.WriteLine("Invalid input for columns. Using default value of 3.");
            return 3;
        }
        private static int AskForWinningLength()
        {
            Console.Write("Enter the winning length: ");
            int winningLength;
            if (int.TryParse(Console.ReadLine(), out winningLength))
            {
                return winningLength;
            }
            Console.WriteLine("Invalid input for winning length. Using default value of 3.");
            return 3;
        }
        public bool TwistColumn(int p_chosenColumn)
        {
            bool p_shouldTwist = p_random.Next(0, 2) == 0;
            if (p_shouldTwist)
            {
                Console.WriteLine("Twist!");

                List<char> p_tempColumn = new List<char>();
                for (int p_rowIndex = 0; p_rowIndex < p_rows; p_rowIndex++)
                {
                    p_tempColumn.Add(GetCell(p_rowIndex, p_chosenColumn));
                }

                p_tempColumn.Reverse();

                for (int p_rowIndex = 0; p_rowIndex < p_rows; p_rowIndex++)
                {
                    SetCell(p_rowIndex, p_chosenColumn, p_tempColumn[p_rowIndex]);
                }
                return true;
            }
            return false;
        }

        #endregion






    }
}