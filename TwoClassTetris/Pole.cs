using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TwoClassTetris
{
    public class Pole
    {
        float height;//высота
        float width;//ширина
        float locationX = 0;
        float locationY = 0;
        int countLine;
        int countColumn;
        int[,] Area;
        int side_of_square;
        Bitmap BtmPole;//=new Bitmap(1000,1000);
        Brush[] ColorsPole = new Brush[] { Brushes.Aqua, Brushes.Orange, Brushes.Blue, Brushes.Red, Brushes.Green,
            Brushes.Azure, Brushes.Violet, Brushes.Tomato, Brushes.SteelBlue };

        public event Action PlusLine;

        public float Width
        {
            get { return width; }
            set { width = value; }
        }

        public float Height
        {
            get { return height; }
            set { height=value; }
        }

        public int Side_of_square
        {
            get { return side_of_square; }
            set
            {
                if (width > 0 && height > 0)
                {
                    countLine = Convert.ToInt32(height /value);
                    countColumn = Convert.ToInt32(width /value);
                    Area = new int[countLine, countColumn];
                    BtmPole = new Bitmap((int)width, (int)height);
                }
                side_of_square = value;
            }
        }

        public Bitmap PaintPole() 
        {
            //side_of_square = 50;
            //width = 1000;
            //height = 400;
            //countLine = Convert.ToInt32(height / 50);
            //countColumn = Convert.ToInt32(width / 50);
            //Area = new int[countLine, countColumn];//все наоборот...

            Graphics g = Graphics.FromImage(BtmPole);
            Brush newBrush;
            for (int i = 0; i < Area.GetLength(0); i++)
            {
                for (int j = 0; j < Area.GetLength(1); j++)
                {
                    if (Area[i, j] == 0)
                    {
                        newBrush = Brushes.BlueViolet;
                    }
                    else
                    {
                        newBrush = ColorsPole[Area[i, j]-1];
                    }
                    g.FillRectangle(newBrush, j * side_of_square, i * side_of_square, side_of_square, side_of_square);
                }
            }

            for (int i = 1; i <= countLine; i++)
            {
                g.DrawLine(Pens.Black, new Point(1, side_of_square * i), new Point(Convert.ToInt32(width), side_of_square * i));//горизонталь
            }

            for (int j = 1; j <= countColumn; j++) 
            {
                g.DrawLine(Pens.Black, new Point(side_of_square*j, Convert.ToInt32(height)), new Point(side_of_square*j, 1));//вертикаль
            }
                return BtmPole;
        }

        public void CheckArea(Figure figure, float X, float Y)
        {
            for (int i = 0; i < figure._Cube.GetLength(0); i++)
            {
                for (int j = 0; j < figure._Cube.GetLength(1); j++)
                {
                    if (figure._Cube[i, j]!=0)
                    {
                        try
                        {
                            Area[(int)(Y / side_of_square) + j, (int)(X / side_of_square) + i] = figure._Cube[i,j];
                        }
                        catch
                        {

                        }
                    }
                }
            }
            SearchLine();
            //Console.ReadLine();
        }

        public bool PositionOK(Figure figure,float X,float Y)
        {
            for (int i = 0; i < figure._Cube.GetLength(0); i++)
            {
                for(int j=0;j < figure._Cube.GetLength(1); j++)
                {
                    if (figure._Cube[i, j]!=0)
                    {
                        try
                        {
                            if (Area[(int)(Y / side_of_square) + j, (int)(X / side_of_square) + i] != 0)
                            {
                                //CheckArea(figure, X, Y);
                                return false;
                            }
                        }
                        catch
                        {
                            //CheckArea(figure, X, Y);
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private void DeleteLine(int LineNum)
        {
            int[,] cash = new int[LineNum, Area.GetLength(1)];
            for(int i = 0; i <= LineNum; i++)
            {
                for(int j = 0; j < Area.GetLength(1); j++)
                {
                    if (i < LineNum) cash[i, j] = Area[i, j];
                    else
                    {
                        Area[i, j] = 0;
                    }
                }
            }
            for (int i = 1; i <= LineNum; i++)
            {
                for (int j = 0; j < Area.GetLength(1); j++)
                {
                    Area[i, j] = cash[i - 1, j];
                }
            }
            PlusLine();
        }

        private void SearchLine()
        {
            for (int i = 0; i <Area.GetLength(0); i++)
            {
                int count = 0;
                for (int j = 0; j < Area.GetLength(1); j++)
                {
                    if (Area[i, j] != 0) count++;
                }
                if (count == Area.GetLength(1)) DeleteLine(i);
            }
        }

    }
}
