using System;
using System.Drawing;
// using System.Drawing.Imaging;
// using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace libdraw
{
    public class GameDraw
    {
        protected delegate void ShapeDraw(PaintEventArgs e, int locX, int locY);

        protected ShapeDraw BoardShape;
        protected Font uiFont;
        protected int[,] gameBoard;
        protected int bdRows, bdCols, SIZE; 
        // SIZE is a shape size in pixels, which means the shape is a square, and this perfectly suits chess-like games (and Clicks)
        // use a multiplier for rectangular shapes with different side length.

        public GameDraw(int rows, int cols, int size)
        {
            bdRows = Math.Abs(rows);
            bdCols = Math.Abs(cols);
            SIZE = Math.Abs(size);
        }

        public int[,] GameBrd
        {
            set { gameBoard = value; }
        }

        public void DisplayBoard(PaintEventArgs e)
        {
            for(int s = 0; s < bdRows; s++)
                for (int t = 0; t < bdCols; t++)
                    BoardShape(e, t, s);
        }
        /// <summary>
        /// Override this method to initialize board glyphs using board coordinates
        /// to find the required glyph number
        /// </summary>
        /// <param name="i">board first dim</param>
        /// <param name="j">board second dim</param>
        /// <returns></returns>
        protected virtual char MakeBoardSymbol(int i, int j)
        {
            MessageBox.Show("This method must not be run");
            return '\0';
        }
        /// <summary>
        /// Override this method to initialize board glyphs using known glyph number
        /// </summary>
        /// <param name="n">Location on the board which represents glyph number</param>
        /// <returns></returns>
        protected virtual char MakeBoardSymbol(int n)
        {
            MessageBox.Show("This method must not be run");
            return '\0';
        }
    }

    public class ClicksDraw : GameDraw
    {
        private Image shapeImage;

        private enum shapeColor
        {
            TRANSP,
            RED,
            GREEN,
            YELLOW,
            BROWN,
            BLUE,
            PURPLE
        };

        public ClicksDraw(int rows, int cols, int size) : base(rows, cols, size)
        {
            BoardShape = DrawShapeClicks;
            // font is no longer used in version 1.3+ to draw Clicks board
            // uiFont = new Font("MS Mincho", (float)Math.Round(SIZE * 0.6388888f));
            shapeImage = Properties.Resources.shapeTexture;
        }

        public ClicksDraw(int rows, int cols) : this(rows, cols, 36)
        { }

        /* 
        protected override char MakeBoardSymbol(int i, int j)
        {
            switch (gameBoard[i, j])
            {
                case (int)shapeColor.RED:
                    return (char)0x96C0;
                case (int)shapeColor.GREEN:
                    return (char)0x5144;
                case (int)shapeColor.YELLOW:
                    return (char)0x9EBB;
                case (int)shapeColor.BROWN:
                    return (char)0x5F1F;
                case (int)shapeColor.BLUE:
                    return (char)0x6D99;
                case (int)shapeColor.PURPLE:
                    return (char)0x661F;
                default:
                    return '\0';
            }
        }
        */

        private void DrawShapeClicks(PaintEventArgs e, int locX, int locY)
        {
            Graphics cg = e.Graphics;
            var x = locX * SIZE;
            var y = locY * SIZE;

            var rtg = new Rectangle(x, y, SIZE, SIZE);

            switch (gameBoard[locY, locX])
            {
                case (int)shapeColor.TRANSP:
                    // There's no need to draw anything in this case since libdraw v. 1.3, 
                    // but this case is REQUIRED to skip grid drawing over empty spaces
                    goto no_grid_draw;
                case (int)shapeColor.RED:
                    cg.DrawImage(shapeImage, rtg, new Rectangle(SIZE * (int)shapeColor.RED, 0, SIZE, SIZE), GraphicsUnit.Pixel);
                    break;
                case (int)shapeColor.GREEN:
                    cg.DrawImage(shapeImage, rtg, new Rectangle(SIZE * (int)shapeColor.GREEN, 0, SIZE, SIZE), GraphicsUnit.Pixel);
                    break;
                case (int)shapeColor.YELLOW:
                    cg.DrawImage(shapeImage, rtg, new Rectangle(SIZE * (int)shapeColor.YELLOW, 0, SIZE, SIZE), GraphicsUnit.Pixel);
                    break;
                case (int)shapeColor.BROWN:
                    cg.DrawImage(shapeImage, rtg, new Rectangle(SIZE * (int)shapeColor.BROWN, 0, SIZE, SIZE), GraphicsUnit.Pixel);
                    break;
                case (int)shapeColor.BLUE:
                    cg.DrawImage(shapeImage, rtg, new Rectangle(SIZE * (int)shapeColor.BLUE, 0, SIZE, SIZE), GraphicsUnit.Pixel);
                    break;
                case (int)shapeColor.PURPLE:
                    cg.DrawImage(shapeImage, rtg, new Rectangle(SIZE * (int)shapeColor.PURPLE, 0, SIZE, SIZE), GraphicsUnit.Pixel);
                    break;
            }
            using (Pen pb = new Pen(Color.Indigo))
            {
                cg.DrawRectangle(pb, rtg);
            }

            no_grid_draw:
             ;
        }
    }
}

