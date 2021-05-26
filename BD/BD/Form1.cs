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
    public partial class Form1 : Form
    {
        public static string user;
        public Form1()
        {
            InitializeComponent();
        }

        public void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            using(var context=new testEntities1())
            {
                Сотрудники sotr = new Сотрудники()
                {
                    
                };
                string name = bunifuTextBox1.Text;
                string passw = bunifuTextBox2.Text;
                var res = context.Сотрудники.FirstOrDefault(s => s.Логин == name && s.Пароль == passw);
                if (res==null)
                {
                    MessageBox.Show("Ошибка, проверьте данные", "Ошибка", MessageBoxButtons.OK);
                }
                else
                {
                    Osn osn = new Osn();
                    user = res.Id_Сотрудника + " " + res.ФИО;
                    if (res.Admin=="1")
                    {
                        osn.bunifuButton4.Visible = true;
                    }
                    else
                    {
                        osn.bunifuButton4.Visible = false;
                    }
                    osn.Show(this);
                    this.Hide();
                    
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            registration registration = new registration();
            registration.ShowDialog(this);
        }
    }
}
