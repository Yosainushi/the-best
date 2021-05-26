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
    public partial class Tovar : Form
    {
        public Tovar()
        {
            InitializeComponent();
        }
        
        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            if (bunifuTextBox1.Text == "" || bunifuTextBox2.Text == "" || bunifuTextBox3.Text == "" || bunifuTextBox4.Text == "" || bunifuTextBox5.Text=="")
            {
                MessageBox.Show("Проверьте заполненные поля", "Ошибка", MessageBoxButtons.OK);
            }
            else
            {
                if (bunifuButton1.Text == "Добавить")
                {


                    using (var context = new testEntities1())
                    {
                        Филиалы fil = new Филиалы()
                        {
                            Адрес_Филиала = bunifuTextBox2.Text,
                            Номер_Телефона = bunifuTextBox3.Text
                        };
                        context.Филиалы.Add(fil);
                        context.SaveChanges();
                        Поставщики post = new Поставщики()
                        {
                            Название = bunifuTextBox1.Text,
                            Код_Филиала = fil.Id_Филиала
                        };
                        context.Поставщики.Add(post);
                        context.SaveChanges();
                        decimal? price = decimal.Parse(bunifuTextBox5.Text);
                        Блюда bl = new Блюда()
                        {
                            Код_Поставщика = post.id_Поставщика,
                            Название_блюда = bunifuTextBox4.Text,
                            Цена_одной_порции = price
                        };
                        context.Блюда.Add(bl);
                        context.SaveChanges();
                        MessageBox.Show("Блюдо успешно добавлено", "Уведомление", MessageBoxButtons.OK);
                        var osn = this.Owner as Osn;
                        osn.bunifuDataGridView1.DataSource = context.Блюда.Select(z1 => new
                        {
                            id = z1.id_Блюда,
                            Название = z1.Название_блюда,
                            Поставщик = z1.Поставщики.Название,
                            Цена = z1.Цена_одной_порции
                        }).ToList();
                        this.Close();
                    }
                }
                else
                {
                    using (var context = new testEntities1())
                    {
                      
                        var bludo = context.Блюда.Find(Osn.bl.id_Блюда);
                        var fil = context.Филиалы.Find(int.Parse(bludo.Код_Поставщика.ToString()));

                        fil.Адрес_Филиала = bunifuTextBox2.Text;
                        fil.Номер_Телефона = bunifuTextBox3.Text;
                        var post = context.Поставщики.Find(int.Parse(bludo.Код_Поставщика.ToString()));
                        post.Название = bunifuTextBox1.Text;
                        
                        decimal? price = decimal.Parse(bunifuTextBox5.Text);

                        bludo.Код_Поставщика = post.id_Поставщика;
                            bludo.Название_блюда = bunifuTextBox4.Text;
                            bludo.Цена_одной_порции = price;
                        
                        context.SaveChanges();
                        MessageBox.Show("Блюдо успешно Изменено", "Уведомление", MessageBoxButtons.OK);
                        Osn.perecl1 = 1;
                            var osn = this.Owner as Osn;
                            osn.bunifuDataGridView1.DataSource = context.Блюда.Select(z1 => new
                            {
                                id = z1.id_Блюда,
                                Название = z1.Название_блюда,
                                Поставщик = z1.Поставщики.Название,
                                Цена = z1.Цена_одной_порции
                            }).ToList();
                        bunifuButton1.Text = "Добавить";
                        this.Close();
                    }
                    
                }


            }
        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;

            }
        }
        public int kol()
        {
            int k=0;
            foreach (var item in bunifuTextBox5.Text)
            {
                if (item==',')
                {
                    k++;
                }
            }
            return k;
        }
        private void bunifuTextBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            
           


                if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8)
                {

                    e.Handled = true;

                }
                int ko = kol();
                if (e.KeyChar == ',')
                {
                    
                    if (ko == 0)
                    {
                        e.Handled = false;
                    }

                }
            
        }

        private void bunifuTextBox5_TextChanged(object sender, EventArgs e)
        {
            int ind = bunifuTextBox5.Text.IndexOf(',');
            if (ind!=-1)
            {
                bunifuTextBox5.MaxLength = ind + 3;
            }
            
        }
    }
}
