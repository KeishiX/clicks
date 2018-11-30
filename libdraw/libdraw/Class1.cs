using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace libdraw
{
    public class GameDraw
    {
        protected delegate void ShapeDraw(PaintEventArgs e, int locX, int locY);

        protected ShapeDraw BoardShape;
        protected Font uiFont;
        protected int[,] gameBoard;
        protected int bdRows, bdCols, SIZE; // SIZE = size in pixels of one element on the board

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

        protected virtual char MakeBoardSymbol(int i, int j)
        {
            MessageBox.Show("Этот метод не должен быть выполнен");
            return '\0';
        }
    }

    public class ClicksDraw : GameDraw
    {
        private Color[] colorRed;
        private Color[] colorGreen;
        private Color[] colorYellow;
        private Color[] colorBrown;
        private Color[] colorBlue;
        private Color[] colorPurple;

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
            uiFont = new Font("MS Mincho", (float)Math.Round(SIZE * 0.6388888f));
            InitColor();
        }

        public ClicksDraw(int rows, int cols) : this(rows, cols, 36)
        { }

        protected override char MakeBoardSymbol(int i, int j)
        {
            switch (gameBoard[i, j])
            {
                case (int)shapeColor.RED:
                    return (char) 0x96C0;
                case (int)shapeColor.GREEN:
                    return (char) 0x5144;
                case (int)shapeColor.YELLOW:
                    return (char) 0x9EBB;
                case (int)shapeColor.BROWN:
                    return (char) 0x5F1F;
                case (int)shapeColor.BLUE:
                    return (char) 0x6D99;
                case (int)shapeColor.PURPLE:
                    return (char) 0x661F;
                default:
                    return '\0';
            }
        }

        private void DrawShapeClicks(PaintEventArgs e, int locX, int locY)
        {
            // char sym = MakeBoardSymbol(locY, locX);
            string sym = MakeBoardSymbol(locY, locX).ToString();
            int sShift = 4;
            Graphics cg = e.Graphics;

            var x = locX * SIZE;
            var y = locY * SIZE;

            var ptCenter = new PointF(x + SIZE / 2.0f, y + SIZE / 2.0f);
            /*
            var quadPoint = new[]
            {
                new Point(x,y),
                new Point(x, y + SIZE), 
                new Point(x + SIZE, y + SIZE), 
                new Point(x + SIZE, y)
            };
            */
            var quadPoint = new[]
            {
                new Point(x + sShift, y + sShift),
                new Point(x + sShift, (y + SIZE) - sShift), 
                new Point((x + SIZE) - sShift, (y + SIZE) - sShift), 
                new Point((x + SIZE) - sShift, y + sShift)
            };
            var qbr = new PathGradientBrush(quadPoint) { CenterColor = Color.White, CenterPoint = ptCenter };
            var tpr = new SolidBrush(Color.Transparent);
            var foreBrush = new SolidBrush(Color.DarkSlateGray);
            var pb = new Pen(Color.Indigo, 1);
            var rtg = new Rectangle(x, y, SIZE, SIZE);
            var rtge = new Rectangle(x + 1, y + 1, SIZE - 2, SIZE - 2);

            // moved to ctor
            // InitColor();
            switch (gameBoard[locY, locX])
            {
                case (int)shapeColor.TRANSP:
                    cg.FillRectangle(tpr, rtge);
                    goto clean;
                case (int)shapeColor.RED:
                    qbr.SurroundColors = colorRed;
                    break;
                case (int)shapeColor.GREEN:
                    qbr.SurroundColors = colorGreen;
                    break;
                case (int)shapeColor.YELLOW:
                    qbr.SurroundColors = colorYellow;
                    break;
                case (int)shapeColor.BROWN:
                    qbr.SurroundColors = colorBrown;
                    break;
                case (int)shapeColor.BLUE:
                    qbr.SurroundColors = colorBlue;
                    break;
                case (int)shapeColor.PURPLE:
                    qbr.SurroundColors = colorPurple;
                    break;
            }
            cg.FillRectangle(qbr, rtg);
            // cg.DrawString(sym.ToString(), uiFont, foreBrush, (x - 2),  (y + 2));
            cg.DrawString(sym, uiFont, foreBrush, (x - 2), (y + 2));
            cg.DrawRectangle(pb, rtg);
            clean:
            qbr.Dispose();
            tpr.Dispose();
            pb.Dispose();
        }

        private void InitColor()
        {
            colorRed = new[]
            {
                Color.FromArgb(255,153,153),
                Color.FromArgb(255,153,153),
                Color.FromArgb(255,153,153),
                Color.FromArgb(255,153,153)
            };
            colorGreen = new[]
            {
                Color.PaleGreen,
                Color.PaleGreen,
                Color.PaleGreen,
                Color.PaleGreen
            };
            colorYellow = new[]
            {
                Color.FromArgb(255,255,102),
                Color.FromArgb(255,255,102),
                Color.FromArgb(255,255,102),
                Color.FromArgb(255,255,102)
            };
            colorBrown = new[]
            {
                Color.FromArgb(204, 153, 102),
                Color.FromArgb(204, 153, 102),
                Color.FromArgb(204, 153, 102),
                Color.FromArgb(204, 153, 102)
            };
            colorBlue = new[]
            {
                Color.FromArgb(153, 204, 255),
                Color.FromArgb(153, 204, 255),
                Color.FromArgb(153, 204, 255),
                Color.FromArgb(153, 204, 255)
            };
            colorPurple = new[]
            {
                Color.FromArgb(204, 102, 204),
                Color.FromArgb(204, 102, 204),
                Color.FromArgb(204, 102, 204),
                Color.FromArgb(204, 102, 204)
            };
        }
/*        private InitShape(PaintEventArgs e)
        {
            // 
        }
*/
    }

}
