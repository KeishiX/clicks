/* ********************************************************************** *
 *                                                                        *
 *   Game logic coding done by Vitaly Sinitsky (1977 - 2022)              *
 *                                                                        *
 * ********************************************************************** */

using System;
using libdraw;
using System.Collections;
using System.Threading;

namespace ClicksGame
{
    class ClicksComp
    {
        private Random rInit = new Random();
        private Random rBase = null;
        private Stack undoContainer = new Stack();
        private int bdRows, bdCols, compSkill;
        private int[,] board, bdUndo;
        private int[] delta_i = new[] { -1, 0, 1, 0 };
        private int[] delta_j= new[] { 0, 1, 0, -1 };
        private const int SIZE = 36;
        private bool noBackupTurns;
        private bool infiniteMode;
        private bool infiniteEndlessMode;

        private Thread initThread;

        public ClicksComp(int s, int r, int c)
	    {
            rBase = new Random(rInit.Next());
            initThread = new Thread(new ThreadStart(InitBoard));
            undoContainer.Clear();
            compSkill = Math.Abs(s);
            bdRows = Math.Abs(r);
            bdCols = Math.Abs(c);
            if (compSkill > 6)
            {
                compSkill = 0;
            }
            board = new int[bdRows, bdCols];
            bdUndo = new int[bdRows, bdCols];
            initThread.Start();
            // InitBoard();
        }
        public int [,] Bd
        {
            get { return board; }
        }
        // Win condition: if there's empty cell in the lowest left corner then
        // the whole board must be cleared. So get this value when checking 
        // NoTurn condition 
        public int CurrentBdState
        {
            get { return board[bdRows - 1, 0]; }
        }
        public bool NoUndo
        {
            get { return noBackupTurns; }
        }
        public bool Infinite
        {
            set { infiniteMode = value; }
            get { return infiniteMode; }
        }
        public bool InfiniteEndless
        {
            set { infiniteEndlessMode = value; }
            get { return infiniteEndlessMode; }
        }
        private int GenerateShape(int gameSkill)
        {
            var rGen = new Random(rBase.Next(rInit.Next()) - rInit.Next());
            return rGen.Next(1, gameSkill + 1);
        }
        private void InitBoard()
        {
            for (int i = 0; i < bdRows; i++)
            {
                for (int j = 0; j < bdCols; j++)
                {
                    board[i, j] = GenerateShape(compSkill);
                }
            }
        }
        private void AddShape()
        {
            for (int j = 0; j < bdCols; j++)
            {
                if (board[0, j] == 0)
                {
                    board[0, j] = GenerateShape(compSkill);
                }
            }
        }
        private void ShiftShape()
        {
            int temp = 0;
            for (int x = 0; x < bdCols; x++)
            {
                temp = board[0, x];
                board[0, x] = 0;
                for (int y = 1; y < bdRows; y++)
                {
                    if(board[y, x] == 0)
                    {
                        // y++;
                        // looks like this increment is not needed
                        // at least its removal fixed bug #2 with endless mode
                        // but more tests are also needed
                        // the same fix is also applied to pure endless mode
                        // added 03.08.2016
                        continue;
                    }
                    else
                    {
                        board[y - 1, x] = temp;
                        break;
                    }
                }
            }
        }
        private void ShiftShapeEndless()
        {
            int temp = 0;
            for (int x = 0; x < bdCols; x++)
            {
                temp = board[0, x];
                board[0, x] = 0;
                for (int y = 1; y < bdRows; y++)
                {
                    if (board[y, x] == 0)
                    {
                        if(y != bdRows - 1)
                        {
                            // y++;
                            // commented out for reasons as mentioned above, in ShiftShape()
                            continue;
                        }
                        else
                        {
                            board[y, x] = temp;
                            break;
                        }
                    }
                    else
                    {
                        board[y - 1, x] = temp;
                        break;
                    }
                }
            }
        }
        public void UndoTurn()
        {
            noBackupTurns = false;
            for (var i = bdRows - 1; i >= 0; i--)
            {
                for (var j = bdCols - 1; j >= 0; j--)
                {
                    try
                    {
                        bdUndo[i, j] = (int)undoContainer.Pop();
                        board[i, j] = bdUndo[i, j];
                    }
                    catch (InvalidOperationException)
                    {
                        noBackupTurns = true;
                        return;
                    }
                }
            }
            try
            {
                undoContainer.Peek();
            }
            catch (InvalidOperationException)
            {
                noBackupTurns = true;
            }
        }
        private void BackupTurn()
        {
            if (infiniteEndlessMode)
            {
                undoContainer.Clear();
            }
            for (var i = 0; i < bdRows; i++)
            {
                for (var j = 0; j < bdCols; j++)
                {
                    bdUndo[i, j] = board[i, j];
                    undoContainer.Push(bdUndo[i, j]);
                }
            }
        }
        public bool NoTurnCheck()
        {
            int i_max = bdRows - 1;
            int j_max = bdCols - 1;
            for (var j = 0; j <= j_max; j++)
            {
                if (board[i_max, j] == 0) break;
                for (var i = i_max; i >= 0; i--)
                {
                    if (board[i, j] == 0) break;
                    var color = board[i, j];
                    if ((i - 1) >= 0)
                        if (board[i - 1, j] == color)
                            return false;
                    if ((j + 1) <= j_max) // 3.04.2021 change   -- check for bugs after change --
                        if (board[i, j + 1] == color)
                            return false;
                }
            }
            return true;
        }
        public bool ComputeTurn(int xPt, int yPt)
        {
            int j0 = (int)Math.Floor(xPt / (float)SIZE);
            int i0 = (int)Math.Floor(yPt / (float)SIZE);
            int i_max = bdRows - 1;
            int j_max = bdCols - 1;
            /* *** */
            int i, j;
            var stack = new int[60];
            var color = board[i0, j0];

            bool fail = true;

            if (color == 0) return !fail;
            for (var k = 0; k < 4; k++)
            {
                i = i0 + delta_i[k];
                j = j0 + delta_j[k];
                if (i < 0 || j < 0 || i > i_max || j > j_max) continue;
                if (board[i, j] != color) continue;
                fail = false;
                break;
            }
            if (fail) return !fail;
            BackupTurn();
            stack[0] = i0;
            stack[1] = j0;
            var car = 2;
            while (car != 0)
            {
                car -= 2;
                i0 = stack[car];
                j0 = stack[car + 1];
                board[i0, j0] = 0;
                for (var k = 0; k < 4; k++)
                {
                    i = i0 + delta_i[k];
                    j = j0 + delta_j[k];
                    if (i < 0 || j < 0 || i > i_max || j > j_max) continue;
                    if (board[i, j] == color)
                    {
                        try
                        {
                            board[i, j] = -1;
                            stack[car] = i;
                            stack[car + 1] = j;
                            car += 2;
                        }
                        catch (IndexOutOfRangeException ex)
                        {
                            System.Windows.Forms.MessageBox.Show("ERROR!", ex.Message, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        }
                    }
                }
            }
            for (j = 0; j <= j_max; j++)
            {
                i0 = i_max;
                while (i0 > 0)
                {
                    if (board[i0, j] == 0) break;
                    i0--;
                }
                i = i0 - 1;
                while (true)
                {
                    if (i0 == 0) break;
                    while (i >= 0)
                    {
                        if (board[i, j] != 0) break;
                        i--;
                    }
                    if (i < 0) break;
                    board[i0--, j] = board[i, j];
                    board[i--, j] = 0;
                }
            }
            // Сдвиг массива по горизонтали
            if (!infiniteEndlessMode)    /* this condition fixes pure endless mode unintended horizontal shift */
            {
                j0 = 0;
                while (j0 < j_max)
                {
                    if (board[i_max, j0] == 0) break;
                    j0++;
                }
                j = j0 + 1;
                while (true)
                {
                    if (j0 == j_max) break;
                    while (j <= j_max)
                    {
                        if (board[i_max, j] != 0) break;
                        j++;
                    }
                    if (j > j_max) break;
                    for (i = i_max; i >= 0; i--)
                    {
                        if (board[i, j] == 0) break;
                        board[i, j0] = board[i, j];
                        board[i, j] = 0;
                    }
                    j0++;
                    j++;
                }
            }
            /* *** */
            if(infiniteMode)
            {
                AddShape();
                if (infiniteEndlessMode)
                {
                    ShiftShapeEndless();
                }
                else
                {
                    ShiftShape();
                }
                // TODO:
                // Implement undo action so that it works as "real" undo, preserving already generated shapes
                // instead of generating the new ones each turn after undo action was taken.
            }
            return true;
        }
    }
}