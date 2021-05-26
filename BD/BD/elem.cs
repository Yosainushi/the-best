using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BD
{
    public partial class elem : Form
    {
        public elem()
        {
            InitializeComponent();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            if (bunifuTextBox1.Text != null && bunifuTextBox1.Text != "")
            {
                ADD.kolvo = int.Parse(bunifuTextBox1.Text);
                var add= this.Owner as ADD;
                add.Loading();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Введите кол-во", "Ошибка", MessageBoxButtons.OK);
            }
            
        }

        private void bunifuTextBox1_TextChange(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar!=8)
            {
                e.Handled = true;
            }
        }
    }
}
