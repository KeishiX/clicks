using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace texturegen
{
    class Program
    {
        static readonly int SIZE = 36;
        static readonly int numberOfShapes = 7;
        static Color[] colorRed;
        static Color[] colorGreen;
        static Color[] colorYellow;
        static Color[] colorBrown;
        static Color[] colorBlue;
        static Color[] colorPurple;

        enum shapeColor
        {
            TRANSP,
            RED,
            GREEN,
            YELLOW,
            BROWN,
            BLUE,
            PURPLE
        };
        static Font uiFont = new Font("MS Mincho", (float)Math.Round(SIZE * 0.6388888f));

        static void Main(string[] args)
        {
            MakeShapeTexture();
        }

        private static char MakeBoardSymbol(int color)
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
        private static void InitColor()
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
        private static void MakeShapeTexture()
        {
            Bitmap txImage = new Bitmap(SIZE * 7, SIZE, PixelFormat.Format32bppArgb);
            Graphics shapeGraphics;
            string sym = "";
            int x = 0, y = 0;
            string shapeFileName = "texture_image.png";

            PointF ptCenter = new PointF(x + SIZE / 2.0f, y + SIZE / 2.0f);
            Point[] quadPoint = new[]
            {
                new Point(x, y),
                new Point(x, y + SIZE),
                new Point(x + SIZE, y + SIZE),
                new Point(x + SIZE, y)
            };
            Rectangle rtg;
            PathGradientBrush qbr = new PathGradientBrush(quadPoint) { CenterColor = Color.White, CenterPoint = ptCenter };
            var tpr = new SolidBrush(Color.Transparent);
            var fontBrush = new SolidBrush(Color.DarkSlateGray);

            InitColor();
            shapeGraphics = Graphics.FromImage(txImage);
            shapeGraphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;

            shapeGraphics.FillRectangle(tpr, new Rectangle(x, y, SIZE, SIZE));
            for (int i = 1; i < numberOfShapes; i++)
            {
                x = i * SIZE;
                ptCenter = new PointF(x + SIZE / 2.0f, y + SIZE / 2.0f);
                quadPoint = new[]
                {
                    new Point(x, y),
                    new Point(x, y + SIZE),
                    new Point(x + SIZE, y + SIZE),
                    new Point(x + SIZE, y)
                };
                qbr = new PathGradientBrush(quadPoint) { CenterColor = Color.White, CenterPoint = ptCenter };
                rtg = new Rectangle(x, y, SIZE, SIZE);
                switch (i)
                {
                    case 1:
                        qbr.SurroundColors = colorRed;
                        break;
                    case 2:
                        qbr.SurroundColors = colorGreen;
                        break;
                    case 3:
                        qbr.SurroundColors = colorYellow;
                        break;
                    case 4:
                        qbr.SurroundColors = colorBrown;
                        break;
                    case 5:
                        qbr.SurroundColors = colorBlue;
                        break;
                    case 6:
                        qbr.SurroundColors = colorPurple;
                        break;
                }
                sym = MakeBoardSymbol(i).ToString();
                shapeGraphics.FillRectangle(qbr, rtg);
                shapeGraphics.DrawString(sym, uiFont, fontBrush, x - 2, y + 2);
            }
            txImage.Save(shapeFileName, ImageFormat.Png);
            qbr.Dispose();
            tpr.Dispose();
            fontBrush.Dispose();
            shapeGraphics.Dispose();
        }
    }
}
