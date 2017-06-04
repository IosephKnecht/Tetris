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
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Событие пересылает числовые данные о поле на форму MainForm...
        /// В UML диаграмме связь...
        /// </summary>
        public event Action<int, int, int> ReturnSettings;

        private void button1_Click(object sender, EventArgs e)
        {
            //15,28,25
            ReturnSettings((int)numericUpDown1.Value, (int)numericUpDown2.Value, (int)numericUpDown3.Value);
        }
    }
}
