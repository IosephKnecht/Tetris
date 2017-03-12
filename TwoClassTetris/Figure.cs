using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TwoClassTetris
{
    public class Figure
    {
        //Координаты расположения фигуры на поле...
        int X = 0;
        int Y = 0;
        int StopLine = 0;
        int[,] Cube = new int[4, 4]; //Расчетная матрица...
        Brush[] ColorsPole = new Brush[] { Brushes.Aqua, Brushes.Orange, Brushes.Blue, Brushes.Red, Brushes.Green,
            Brushes.Azure, Brushes.Violet, Brushes.Tomato, Brushes.SteelBlue };

        public int _StopLine 
        {
            get { return StopLine; }
            set { StopLine = value; }
        }
        public int _X 
        {
            get { return X; }
            set { X = value; }
        }
        public int _Y
        {
            get { return Y; }
            set { Y = value; }
        }
        public int[,] _Cube 
        {
            set { Cube = value; }
            get { return Cube; }
        }
        /// <summary>
        /// Процедура заполнения расчетной матрицы...
        /// </summary>
        public void EnterCubeMatrix()
        {
            Random ran = new Random();
            int color = ran.Next(0, 9)+1;
            switch (ran.Next(1, 6) + 1)
            {
                case 1://Левая молния...
                    Cube[1, 2] = color;
                    Cube[1, 3] = color;
                    Cube[2, 1] = color;
                    Cube[2, 2] = color;
                    break;
                case 2://Правая молния...
                    Cube[1, 0] = color;
                    Cube[1, 1] = color;
                    Cube[2, 1] = color;
                    Cube[2, 2] = color;
                    break;
                case 3://Правая Г...
                    Cube[1, 1] = color;
                    Cube[1, 2] = color;
                    Cube[1, 3] = color;
                    Cube[2, 1] = color;
                    break;
                case 4://Левая Г...
                    Cube[1, 1] = color;
                    Cube[1, 2] = color;
                    Cube[1, 3] = color;
                    Cube[2, 3] = color;
                    break;
                case 5://Треугольник...
                    Cube[1, 1] = color;
                    Cube[1, 2] = color;
                    Cube[1, 3] = color;
                    Cube[2, 2] = color;
                    break;
                case 6://Палка...
                    Cube[1, 0] = color;
                    Cube[1, 1] = color;
                    Cube[1, 2] = color;
                    Cube[1, 3] = color;
                    break;
            }
        }
        /// <summary>
        /// Функция заполнения Bitmap'a основываясь на расчетной матрице...
        /// </summary>
        /// <param name="size">Размер одного блока поля...</param>
        /// <returns>Заполненный Bitmap...</returns>
        public Bitmap DrawingCubeMatrix(int size) 
        {
            Bitmap btm = new Bitmap(4 * size, 4 * size);
            Graphics g = Graphics.FromImage(btm);
            for (int i = 0; i < Cube.GetLength(0); i++) 
            {
                for (int j = 0; j < Cube.GetLength(1); j++) 
                {
                    if (Cube[i, j]!=0)
                    {
                        g.FillRectangle(ColorsPole[Cube[i,j]-1], i * size, j * size, size, size);
                        int x1 = i * size-1;
                        int y1 = j * size-1;
                        //Переделать в процедуру...
                        g.DrawLine(Pens.Black, x1, y1, x1, y1+size);
                        g.DrawLine(Pens.Black, x1+size, y1, x1+size, y1 + size);
                        g.DrawLine(Pens.Black, x1, y1, x1 + size, y1);
                        g.DrawLine(Pens.Black, x1, y1+size, x1 + size, y1+size);
                    }
                }
            }
            return btm;
        }
        /// <summary>
        /// Процедура транспонирования расчетной матрицы...
        /// </summary>
        public void Transform() 
        {
            int[,] p2 = new int[4, 4];
            for (int i = 0; i < Cube.GetLength(0); i++)
            {
                for (int j = 0; j < Cube.GetLength(1); j++)
                {
                    p2[3 - j, i] = Cube[i, j];
                }
            }
            Cube = p2;
        }
        /// <summary>
        /// Процедура обратного транспонирования расчетной матрицы...
        /// </summary>
        public void uTransform() 
        {
            int[,] p2 = new int[4, 4];
            for (int i = 0; i < Cube.GetLength(0); i++)
            {
                for (int j = 0; j < Cube.GetLength(1); j++)
                {
                    p2[i, j] = Cube[3 - j, i];
                }
            }
            Cube = p2;
        }
        
    }
}
