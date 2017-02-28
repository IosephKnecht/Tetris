using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TwoClassTetris
{
    class Pole
    {
        float height;//высота
        float width;//ширина
        float locationX = 0;
        float locationY = 0;
        int countLine;
        int countColumn;
        int[,] Area;
        int side_of_square;
        Bitmap BtmPole=new Bitmap(1000,1000);
        Brush[] ColorsPole = new Brush[] { Brushes.Aqua, Brushes.Orange, Brushes.Blue, Brushes.Red, Brushes.Green,
            Brushes.Azure, Brushes.Violet, Brushes.Tomato, Brushes.SteelBlue };
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
                        newBrush = ColorsPole[Area[i, j]];
                    }
                    g.FillRectangle(newBrush, j * side_of_square, i * side_of_square, side_of_square, side_of_square);
                }
            }
            return BtmPole;
        }
    }
}
