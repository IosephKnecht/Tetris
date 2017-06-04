using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace TwoClassTetris
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        int Record = 0;

        private void Pole_PlusLine()
        {
            int line = Convert.ToInt32(lineLabel.Text);
            line++;
            lineLabel.Text = line.ToString();
            if (line %5==1&&timer1.Interval>=300) timer1.Interval -= 100;
            if (Record < line)
            {
                Record = line;
                recordLabel.Text = Record.ToString();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            figure.EnterCubeMatrix();
            pole = new Pole();
            pole.PlusLine += Pole_PlusLine;
            pole.Width = 450;
            pole.Height = 700;
            pole.Side_of_square = 25;
            statBox.Location = new Point(Convert.ToInt32(pole.Width + pole.Side_of_square) , Convert.ToInt32(pole.Height / 2));
            statBox.Visible = true;
            recordLabel.Text = Record.ToString();
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
            Bitmap DrawFigure = figure.DrawingCubeMatrix(pole.Side_of_square);
            myBuffer.Graphics.DrawImage(BtmPole, 0, 0);
            myBuffer.Graphics.DrawImage(DrawFigure, figure._X, figure._Y);
            myBuffer.Render(this.CreateGraphics());
            myBuffer.Dispose();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            RePaint();
        }

        private void NewFigure()
        {
            figure = new Figure();
            figure.EnterCubeMatrix();
        }

        int count = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (pole.PositionOK(figure, figure._X, figure._Y + pole.Side_of_square))
            {
                figure._Y += pole.Side_of_square;
                count++;
                RePaint();
            }
            else
            {
                if (count==0)
                {
                    timer1.Stop();
                    DialogForm dialog = new DialogForm();
                    dialog.RestartGame += Dialog_RestartGame;
                    dialog.ShowDialog();
                    myBuffer.Dispose();
                }
                else
                {
                    pole.CheckArea(figure, figure._X, figure._Y);
                    NewFigure();
                    RePaint();
                }
                count = 0;
            }
        }

        private void Dialog_RestartGame(object dialog)
        {
            EventArgs e = new EventArgs();
            this.Form1_Load(this, e);
            timer1.Start();
            lineLabel.Text = "0";
            recordLabel.Text = Record.ToString();
            ((DialogForm)dialog).Close();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            switch (e.KeyValue)
            {
                //Left
                case 37:
                    if(pole.PositionOK(figure,figure._X-pole.Side_of_square,figure._Y))
                    {
                        Thread.Sleep(100);
                        figure._X -= pole.Side_of_square;
                        RePaint();
                    }
                    break;
                //Right
                case 39:
                    if (pole.PositionOK(figure, figure._X + pole.Side_of_square, figure._Y))
                    {
                        Thread.Sleep(100);
                        figure._X += pole.Side_of_square;
                        RePaint();
                    }
                    break;
                case 40:
                    if (pole.PositionOK(figure, figure._X, figure._Y+ pole.Side_of_square))
                    {
                        EventArgs k = new EventArgs();
                        timer1_Tick(this, k);
                        //figure._Y += pole.Side_of_square;
                        //Thread.Sleep(100);
                        //count++;
                        RePaint();
                    }
                    break;
                case 38:
                    Thread.Sleep(100);
                    figure.Transform();
                    if (!pole.PositionOK(figure, figure._X, figure._Y)) figure.uTransform();
                    RePaint();
                    break;
            }
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void statBox_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
