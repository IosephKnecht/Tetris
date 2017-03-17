using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace TwoClassTetris
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            figure.EnterCubeMatrix();
            pole = new Pole();
            pole.Width = 450;
            pole.Height = 700;
            pole.Side_of_square = 50;
            RePaint();
        }

        Figure figure=new Figure();
        public Pole pole { get; set; }
        BufferedGraphicsContext currentContext;
        BufferedGraphics myBuffer;

        private void RePaint()
        {
            currentContext = BufferedGraphicsManager.Current;
            myBuffer = currentContext.Allocate(this.CreateGraphics(),
               new Rectangle(new Point(0,0),new Size(450,700)));
            Bitmap BtmPole = pole.PaintPole();
            Bitmap DrawFigure = figure.DrawingCubeMatrix(50);
            myBuffer.Graphics.DrawImage(BtmPole, 0, 0);
            myBuffer.Graphics.DrawImage(DrawFigure, figure._X, figure._Y);
            myBuffer.Render();
            myBuffer.Dispose();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //Bitmap BtmPole = pole.PaintPole();
            //Bitmap DrawFigure = figure.DrawingCubeMatrix(50);
            //e.Graphics.DrawImage(BtmPole,0,0);
            //e.Graphics.DrawImage(DrawFigure, figure._X, figure._Y);
        }

        private void NewFigure()
        {
            figure = new Figure();
            figure.EnterCubeMatrix();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (pole.PositionOK(figure, figure._X, figure._Y + 50))
            {
                figure._Y += 50;
                RePaint();
            }
            else
            {
                pole.CheckArea(figure, figure._X, figure._Y);
                NewFigure();
                RePaint();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            switch (e.KeyValue)
            {
                //Left
                case 37:
                    if(pole.PositionOK(figure,figure._X-50,figure._Y))
                    {
                        figure._X -= 50;
                        RePaint();
                    }
                    break;
                //Right
                case 39:
                    if (pole.PositionOK(figure, figure._X + 50, figure._Y))
                    {
                        figure._X += 50;
                        RePaint();
                    }
                    break;
                case 40:
                    if (pole.PositionOK(figure, figure._X, figure._Y+50))
                    {
                        figure._Y += 50;
                        RePaint();
                    }
                    break;
                case 38:
                    figure.Transform();
                    if (!pole.PositionOK(figure, figure._X, figure._Y)) figure.uTransform();
                    RePaint();
                    break;
            }
        }
    }
}
