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
    public partial class registration : Form
    {
        public registration()
        {
            InitializeComponent();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            if (bunifuTextBox1.Text== "" || bunifuTextBox2.Text=="" || bunifuTextBox3.Text=="" || bunifuTextBox4.Text=="" )
            {
                MessageBox.Show("Проверьте заполненные поля", "Ошибка", MessageBoxButtons.OK);
            }
            else {
                using (var context = new testEntities1())
                {
                    string login = bunifuTextBox1.Text;
                    string pass = bunifuTextBox2.Text;
                    int admin = 0;
                    var user = context.Сотрудники.FirstOrDefault(ss => ss.Логин == login && ss.Пароль == pass);
                    if (checkBox1.Checked)
                    {
                        admin = 1;
                    }
                    if (user == null)
                    {
                        Сотрудники sotr = new Сотрудники()
                        {
                            Логин = login,
                            Пароль = pass,
                            ФИО = bunifuTextBox3.Text,
                            Номер_Телефона = bunifuTextBox4.Text,
                            Admin = admin.ToString()
                        };
                        context.Сотрудники.Add(sotr);
                        context.SaveChanges();
                        MessageBox.Show("Пользователь успешно зарегистрирован", "Уведомление", MessageBoxButtons.OK);
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Такой пользователь уже существует", "Ошибка", MessageBoxButtons.OK);
                    }
                }
            }
        }

        private void bunifuTextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsLetter(e.KeyChar) && e.KeyChar!=8 )
            {
                e.Handled = true;

            }
        }

        private void bunifuTextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void registration_Load(object sender, EventArgs e)
        {
           
        }

        private void bunifuTextBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;

            }
        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
