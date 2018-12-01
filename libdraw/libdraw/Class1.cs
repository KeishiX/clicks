using System;
using System.Drawing;
using System.Drawing.Imaging;
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
        protected virtual char MakeBoardSymbol(int color)
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

        private Image imageTransp;
        private Image imageRed;
        private Image imageGreen;
        private Image imageYellow;
        private Image imageBrown;
        private Image imageBlue;
        private Image imagePurple;

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
            InitImage();
            InitShape();
        }

        public ClicksDraw(int rows, int cols) : this(rows, cols, 36)
        { }

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
        protected override char MakeBoardSymbol(int color)
        {
            switch (color)
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

        private void DrawShapeClicks(PaintEventArgs e, int locX, int locY)
        {
            Graphics cg = e.Graphics;
            var x = locX * SIZE;
            var y = locY * SIZE;

            var rtg = new Rectangle(x, y, SIZE, SIZE);
            // var rtge = new Rectangle(x + 1, y + 1, SIZE - 2, SIZE - 2);

            switch (gameBoard[locY, locX])
            {
                case (int)shapeColor.TRANSP:
                    // cg.DrawImage(imageTransp, rtge);
                    // break;
                    goto no_transparent_draw;
                case (int)shapeColor.RED:
                    cg.DrawImage(imageRed, rtg);
                    break;
                case (int)shapeColor.GREEN:
                    cg.DrawImage(imageGreen, rtg);
                    break;
                case (int)shapeColor.YELLOW:
                    cg.DrawImage(imageYellow, rtg);
                    break;
                case (int)shapeColor.BROWN:
                    cg.DrawImage(imageBrown, rtg);
                    break;
                case (int)shapeColor.BLUE:
                    cg.DrawImage(imageBlue, rtg);
                    break;
                case (int)shapeColor.PURPLE:
                    cg.DrawImage(imagePurple, rtg);
                    break;
            }
            cg.DrawRectangle(new Pen(Color.Indigo), rtg);

            no_transparent_draw:
            ;
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
        private void InitShape()
        {
            Graphics shapeGraphics;
            string sym = "";
            // string shapeFileName = "";

            var ptCenter = new PointF(SIZE / 2.0f, SIZE / 2.0f);
            var quadPoint = new[]
            {
                new Point(0, 0),
                new Point(0, SIZE),
                new Point(SIZE, SIZE),
                new Point(SIZE, 0)
            };

            var qbr = new PathGradientBrush(quadPoint) { CenterColor = Color.White, CenterPoint = ptCenter };
            var tpr = new SolidBrush(Color.Transparent);
            var fontBrush = new SolidBrush(Color.DarkSlateGray);
            var pb = new Pen(Color.Indigo, 1);
            var rtg = new Rectangle(0, 0, SIZE, SIZE);
            var rtge = new Rectangle(1, 1, SIZE - 2, SIZE - 2);
            //
            shapeGraphics = Graphics.FromImage(imageTransp);
            shapeGraphics.FillRectangle(tpr, rtge);
            //
            shapeGraphics = Graphics.FromImage(imageRed);
            sym = MakeBoardSymbol((int)shapeColor.RED).ToString();
            qbr.SurroundColors = colorRed;
            shapeGraphics.FillRectangle(qbr, rtg);
            shapeGraphics.DrawString(sym, uiFont, fontBrush, -2, 2);
            shapeGraphics.DrawRectangle(pb, rtg);
            // shapeFileName = "shape1.png";
            // imageRed.Save(shapeFileName, ImageFormat.Png);
            //
            shapeGraphics = Graphics.FromImage(imageGreen);
            sym = MakeBoardSymbol((int)shapeColor.GREEN).ToString();
            qbr.SurroundColors = colorGreen;
            shapeGraphics.FillRectangle(qbr, rtg);
            shapeGraphics.DrawString(sym, uiFont, fontBrush, -2, 2);
            shapeGraphics.DrawRectangle(pb, rtg);
            // shapeFileName = "shape2.png";
            // imageGreen.Save(shapeFileName, ImageFormat.Png);
            //
            shapeGraphics = Graphics.FromImage(imageYellow);
            sym = MakeBoardSymbol((int)shapeColor.YELLOW).ToString();
            qbr.SurroundColors = colorYellow;
            shapeGraphics.FillRectangle(qbr, rtg);
            shapeGraphics.DrawString(sym, uiFont, fontBrush, -2, 2);
            shapeGraphics.DrawRectangle(pb, rtg);
            // shapeFileName = "shape3.png";
            // imageYellow.Save(shapeFileName, ImageFormat.Png);
            //
            shapeGraphics = Graphics.FromImage(imageBrown);
            sym = MakeBoardSymbol((int)shapeColor.BROWN).ToString();
            qbr.SurroundColors = colorBrown;
            shapeGraphics.FillRectangle(qbr, rtg);
            shapeGraphics.DrawString(sym, uiFont, fontBrush, -2, 2);
            shapeGraphics.DrawRectangle(pb, rtg);
            // shapeFileName = "shape4.png";
            // imageBrown.Save(shapeFileName, ImageFormat.Png);
            //
            shapeGraphics = Graphics.FromImage(imageBlue);
            sym = MakeBoardSymbol((int)shapeColor.BLUE).ToString();
            qbr.SurroundColors = colorBlue;
            shapeGraphics.FillRectangle(qbr, rtg);
            shapeGraphics.DrawString(sym, uiFont, fontBrush, -2, 2);
            shapeGraphics.DrawRectangle(pb, rtg);
            // shapeFileName = "shape5.png";
            // imageBlue.Save(shapeFileName, ImageFormat.Png);
            //
            shapeGraphics = Graphics.FromImage(imagePurple);
            sym = MakeBoardSymbol((int)shapeColor.PURPLE).ToString();
            qbr.SurroundColors = colorPurple;
            shapeGraphics.FillRectangle(qbr, rtg);
            shapeGraphics.DrawString(sym, uiFont, fontBrush, -2, 2);
            shapeGraphics.DrawRectangle(pb, rtg);
            // shapeFileName = "shape6.png";
            // imagePurple.Save(shapeFileName, ImageFormat.Png);

            qbr.Dispose();
            tpr.Dispose();
            fontBrush.Dispose();
            pb.Dispose();
            shapeGraphics.Dispose();
        }
        private void InitImage()
        {
            imageTransp = new Bitmap(SIZE, SIZE, PixelFormat.Format32bppArgb);
            imageRed = new Bitmap(SIZE, SIZE, PixelFormat.Format32bppArgb);
            imageGreen = new Bitmap(SIZE, SIZE, PixelFormat.Format32bppArgb);
            imageYellow = new Bitmap(SIZE, SIZE, PixelFormat.Format32bppArgb);
            imageBrown = new Bitmap(SIZE, SIZE, PixelFormat.Format32bppArgb);
            imageBlue = new Bitmap(SIZE, SIZE, PixelFormat.Format32bppArgb);
            imagePurple = new Bitmap(SIZE, SIZE, PixelFormat.Format32bppArgb);
        }
    }
}

