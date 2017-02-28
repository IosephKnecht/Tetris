using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TwoClassTetris
{
    class Figure
    {
        //Координаты расположения фигуры на поле...
        int X = 0;
        int Y = 0;
        int StopLine = 0;
        bool[,] Cube = new bool[4, 4]; //Расчетная матрица...
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
        public bool[,] _Cube 
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
            switch (ran.Next(4, 4) + 1)
            {
                case 1://Левая молния...
                    Cube[1, 2] = true;
                    Cube[1, 3] = true;
                    Cube[2, 1] = true;
                    Cube[2, 2] = true;
                    break;
                case 2://Правая молния...
                    Cube[1, 0] = true;
                    Cube[1, 1] = true;
                    Cube[2, 1] = true;
                    Cube[2, 2] = true;
                    break;
                case 3://Правая Г...
                    Cube[1, 1] = true;
                    Cube[1, 2] = true;
                    Cube[1, 3] = true;
                    Cube[2, 1] = true;
                    break;
                case 4://Левая Г...
                    Cube[1, 1] = true;
                    Cube[1, 2] = true;
                    Cube[1, 3] = true;
                    Cube[2, 3] = true;
                    break;
                case 5://Треугольник...
                    Cube[1, 1] = true;
                    Cube[1, 2] = true;
                    Cube[1, 3] = true;
                    Cube[2, 2] = true;
                    break;
                case 6://Палка...
                    Cube[1, 0] = true;
                    Cube[1, 1] = true;
                    Cube[1, 2] = true;
                    Cube[1, 3] = true;
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
                    if (Cube[i, j])
                    {
                        g.FillRectangle(Brushes.BlueViolet, i * size, j * size, size, size);
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
            bool[,] p2 = new bool[4, 4];
            for (int i = 0; i < Cube.GetLength(0); i++)
            {
                for (int j = 0; j < Cube.GetLength(1); j++)
                {
                    p2[3 - j, i] = Cube[i, j];
                }
            }
            Cube = p2;
            Console.ReadLine();
        }
        /// <summary>
        /// Процедура обратного транспонирования расчетной матрицы...
        /// </summary>
        public void uTransform() 
        {
            bool[,] p2 = new bool[4, 4];
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
