using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        }
        Figure figure=new Figure();
        public Pole pole { get; set; }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Bitmap BtmPole = pole.PaintPole();
            Bitmap DrawFigure = figure.DrawingCubeMatrix(50);
            e.Graphics.DrawImage(BtmPole,0,0);
            e.Graphics.DrawImage(DrawFigure, figure._X, figure._Y);
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
                this.Refresh();
            }
            else
            {
                pole.CheckArea(figure, figure._X, figure._Y);
                NewFigure();
                this.Refresh();
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
                        this.Refresh();
                    }
                    break;
                //Right
                case 39:
                    if (pole.PositionOK(figure, figure._X + 50, figure._Y))
                    {
                        figure._X += 50;
                        this.Refresh();
                    }
                    break;
                case 40:
                    if (pole.PositionOK(figure, figure._X, figure._Y+50))
                    {
                        figure._Y += 50;
                        this.Refresh();
                    }
                    break;
                case 38:
                    figure.Transform();
                    if (!pole.PositionOK(figure, figure._X, figure._Y)) figure.uTransform();
                    this.Refresh();
                    break;
            }
        }
    }
}
