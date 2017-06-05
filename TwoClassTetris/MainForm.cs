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
        /// <summary>
        /// Экземпляр класса SettingForm...
        /// Для подписки на событие ReturnSettings...
        /// </summary>
        SettingsForm set;

        public MainForm(SettingsForm set)
        {
            InitializeComponent();
            this.set = set;
            set.ReturnSettings += Set_ReturnSettings;
        }

        /// <summary>
        /// Событие реализуещее создание поля по задынным пользователем хар-кам...
        /// Связь на UML диаграмме...
        /// </summary>
        /// <param name="line">Количество линий...</param>
        /// <param name="stlb">Количество столбцов...</param>
        /// <param name="size">Размер сторны кубика в пикселях...</param>
        private void Set_ReturnSettings(int line, int stlb, int size)
        {
            pole = new Pole();
            pole.Width = line*size;
            pole.Height = stlb*size;
            pole.Side_of_square = size;
            timer1.Enabled = true;
            this.Show();
            set.Hide();
        }

        int Record = 0;

        /// <summary>
        /// Метод(событие) реализуещий подсчет "уничтоженных" линий и присвоение рекорда за сессию...
        /// Связь на UML диаграмме...
        /// </summary>
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
            //figure.EnterCubeMatrix();
            //pole = new Pole();
            pole.PlusLine += Pole_PlusLine;
            statBox.Location = new Point(Convert.ToInt32(pole.Width + pole.Side_of_square), Convert.ToInt32(pole.Height / 2));
            statBox.Visible = true;
            this.Height = (int)pole.Height+80;
            this.Width = (int)pole.Width + pole.Side_of_square + statBox.Width+30;
            int middle = (int)(pole.Width / pole.Side_of_square) / 2;
            NewFigure();
            //figure._X = middle * pole.Side_of_square;
            //recordLabel.Text = Record.ToString();
            RePaint();
        }

        Figure figure=new Figure();
        public Pole pole { get; set; }
        BufferedGraphicsContext currentContext;
        BufferedGraphics myBuffer;

        /// <summary>
        /// Вспомогательный метод реализующий логику перерисовки 
        /// экземпляра класса Pole и Figure...
        /// </summary>
        private void RePaint()
        {
            currentContext = BufferedGraphicsManager.Current;
            myBuffer = currentContext.Allocate(this.CreateGraphics(),
               new Rectangle(new Point(0,0),new Size((int)pole.Width,(int)pole.Height)));
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
            int middle = (int)(pole.Width / pole.Side_of_square) / 2;
            figure._X = middle * pole.Side_of_square;
            //if(pole.Width % pole.Side_of_square==0) figure._X=pole.Width/pole.Side_of_square
            //figure._X = Convert.ToInt32(pole.Width / (pole.Width % pole.Side_of_square));
        }

        int count = 0;

        /// <summary>
        /// Метод реализующий ход фигуры и проверку на условие останова фигуры...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Реализация события на перезапуск игры...
        /// Связь на UML диаграмме...
        /// </summary>
        /// <param name="dialog">Экземпляр класса DialogForm...</param>
        private void Dialog_RestartGame(object dialog)
        {
            EventArgs e = new EventArgs();
            int heiht = (int)pole.Height;
            int with = (int)pole.Width;
            int size = pole.Side_of_square;
            pole = new Pole();
            pole.Height = heiht;
            pole.Width = with;
            pole.Side_of_square = size;
            this.Form1_Load(this, e);
            lineLabel.Text = "0";
            recordLabel.Text = Record.ToString();
            ((DialogForm)dialog).Hide();
            timer1.Start();
        }

        /// <summary>
        /// Реализация управления фигурой...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    //Down...
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
                    //Up...
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

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
