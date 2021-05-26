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
    public partial class ADD : Form
    {
        public static int kolvo;
        public class strokaz{

            public int idbl { get; set; }
            public int kolvobl { get; set; }

        }
        public ADD()
        {
            InitializeComponent();
            
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
            

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        List<Iteme> allItems = new List<Iteme>();
        public bool t;
        public static Заказы zak;
        public bool isSearchRunning;
        private void ADD_Load(object sender, EventArgs e)
        {
            bunifuCustomLabel3.Text = DateTime.Now.ToString("d");
            bunifuCustomLabel2.Text = Form1.user;
            if (t)
            {

                using (var context = new testEntities1())
                {
                    allItems = context.Блюда.ToList().Select(b => new Iteme
                    {
                        Name = MyPadLeft(b.id_Блюда + " " + b.Название_блюда + " " + b.Поставщики.Название, 70) + " " + b.Цена_одной_порции
                    }).ToList();


                    foreach (var item in context.Блюда.ToList().Select(b =>
                       MyPadLeft(b.id_Блюда + " " + b.Название_блюда + " " + b.Поставщики.Название, 70) + " " + b.Цена_одной_порции).ToList())
                    {
                        string s = item.ToString();
                        checkedListBox1.Items.Add(s);
                    }
                    bunifuTextBox1.Text = context.Заказы.Find(zakaz).Клиент.Адрес_Доставки;
                    bunifuTextBox2.Text = context.Заказы.Find(zakaz).Клиент.НомерТелефона;
                    var ff = checkedListBox1.Items;
                    foreach (var item in context.Строка_заказа.Where(sss => sss.id_Заказа == zakaz).ToList())
                    {
                        строказаказаBindingSource.Add(item);
                    }

                    foreach (var item1 in context.Строка_заказа.Where(s => s.id_Заказа == zakaz).ToList())
                    {
                        for (int i = 0; i < checkedListBox1.Items.Count; i++)
                        {
                            if (item1.id_блюда == int.Parse(ff[i].ToString().Split(' ')[0]))
                            {
                                allItems.First(i1=>i1.Name== ff[i].ToString()).isChecked=true;
                                checkedListBox1.SetItemChecked(i, true);
                                
                            }
                        }
                    }
                    chis = true;



                }
            }
            else
            {

                using (var context = new testEntities1())
                {
                    allItems = context.Блюда.ToList().Select(b => new Iteme
                    {
                        Name = MyPadLeft(b.id_Блюда + " " + b.Название_блюда + " " + b.Поставщики.Название, 70) + " " + b.Цена_одной_порции
                    }).ToList();
                    foreach (var item in context.Блюда.ToList().Select(b =>
                       MyPadLeft(b.id_Блюда + " " + b.Название_блюда + " " + b.Поставщики.Название, 70) + " " + b.Цена_одной_порции).ToList())
                    {
                        string s = item.ToString();
                        checkedListBox1.Items.Add(s);
                    }
                    

                }
            }
        }
            
        
        public static int zakaz;
        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            if (bunifuTextBox1.Text!="" || bunifuTextBox2.Text!="")
            {
                using (var context = new testEntities1())
                {
                    Клиент client = new Клиент()
                    {
                        НомерТелефона = bunifuTextBox2.Text,
                        Адрес_Доставки = bunifuTextBox1.Text,
                        Дата_Заказа = DateTime.Now
                    };
                    context.Клиент.Add(client);
                    context.SaveChanges();


                    Заказы zak = new Заказы()
                    {
                        id_клиента = client.id_клиента,
                        id_Сотрудника = int.Parse(bunifuCustomLabel2.Text.Split(' ')[0]),
                        Дата = DateTime.Parse(bunifuCustomLabel3.Text)

                    };
                    context.Заказы.Add(zak);
                    context.SaveChanges();
                    zakaz = zak.id_заказа;
                    decimal? sum = 0;
                    foreach (DataGridViewRow row in bunifuDataGridView1.Rows)
                    {
                        var ddd = row.DataBoundItem.GetType();
                        var item = row.DataBoundItem as Строка_заказа;
                        item.id_Заказа = zakaz;
                        context.Строка_заказа.Add(item);
                        decimal? f = item.колличество_блюд;
                        decimal? s = context.Блюда.Find(item.id_блюда).Цена_одной_порции;
                        sum += f * s;

                    }
                    zak.Общая_сумма_заказа = sum;
                    context.SaveChanges();
                    var osn = this.Owner as Osn;
                    osn.bunifuDataGridView1.DataSource = context.Заказы.Select(zp => new
                    {
                        id = zp.id_заказа,
                        АдресДоставки = zp.Клиент.Адрес_Доставки,
                        КонтактныйТел = zp.Клиент.НомерТелефона,
                        Сотрудник = zp.Сотрудники.ФИО,
                        ОбщаяСумма = zp.Общая_сумма_заказа,
                        Дата = zp.Дата
                    }).ToList();

                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Заполните поля", "Ошибка", MessageBoxButtons.OK);
            }
            
        }

        private void bunifuGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void checkedListBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            
        }
        public static int bludo;
        public static bool chis = true;
        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (isSearchRunning==false)
            {
                if (e.NewValue == CheckState.Checked && chis)
                {
                    bludo = int.Parse(checkedListBox1.Items[e.Index].ToString().Split(' ')[0]);
                    elem el = new elem();
                    el.ShowDialog(this);
                }
                else
                {
                    e.NewValue = CheckState.Checked;


                }
            }

            if (allItems.Count>0)
            {
                allItems.First(i => i.Name == checkedListBox1.Items[e.Index].ToString()).isChecked = e.NewValue == CheckState.Checked;
            }
            


        }
        public  void Loading()
        {
            if (bunifuImageButton3.Visible)
            {
                var str = new Строка_заказа()
                {
                    id_Заказа=zakaz,
                    id_блюда = bludo,
                    колличество_блюд = kolvo
                };
                строказаказаBindingSource.Add(str);
            }
            else
            {
                var str = new Строка_заказа()
                {
                    id_блюда = bludo,
                    колличество_блюд = kolvo
                };
                строказаказаBindingSource.Add(str);
            }
            

            

        }

        private string MyPadLeft(string str, int lenght)
        {
            while(str.Length < lenght)
            {
                str += " ";
            }
            var l = str.Length;
            return str;
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            if (bunifuTextBox1.Text != "" || bunifuTextBox2.Text != "")
            {
                using (var context = new testEntities1())
                {
                    int idclient = int.Parse(context.Заказы.Find(zakaz).id_клиента.ToString());
                    Клиент client = context.Клиент.Find(idclient);

                    client.НомерТелефона = bunifuTextBox2.Text;
                    client.Адрес_Доставки = bunifuTextBox1.Text;
                    client.Дата_Заказа = DateTime.Now;
                    context.SaveChanges();
                    Заказы zak = context.Заказы.Find(zakaz);
                    decimal? sum = 0;
                    foreach (DataGridViewRow row in bunifuDataGridView1.Rows)
                    {
                        var ddd = row.DataBoundItem.GetType();
                        var item = row.DataBoundItem as Строка_заказа;
                        if (item.Id_СоставаЗаказа == 0)
                        {
                            item.id_Заказа = zakaz;
                            context.Строка_заказа.Add(item);
                        }


                        decimal? f = item.колличество_блюд;
                        decimal? s = context.Блюда.Find(item.id_блюда).Цена_одной_порции;
                        sum += f * s;

                    }
                    zak.Общая_сумма_заказа = sum;
                    context.SaveChanges();
                    var osn = this.Owner as Osn;
                    osn.bunifuDataGridView1.DataSource = context.Заказы.Select(zp => new
                    {
                        id = zp.id_заказа,
                        АдресДоставки = zp.Клиент.Адрес_Доставки,
                        КонтактныйТел = zp.Клиент.НомерТелефона,
                        Сотрудник = zp.Сотрудники.ФИО,
                        ОбщаяСумма = zp.Общая_сумма_заказа,
                        Дата = zp.Дата
                    }).ToList();

                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Заполните поля", "Ошибка", MessageBoxButtons.OK);
            }
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            using(var context=new testEntities1())
            {
                Строка_заказа str = bunifuDataGridView1.SelectedRows[0].DataBoundItem as Строка_заказа;
                Строка_заказа strd = context.Строка_заказа.Find(str.Id_СоставаЗаказа);
                if (str.Id_СоставаЗаказа==0)
                {
                    строказаказаBindingSource.Remove(str);
                }
                else
                {
                    context.Строка_заказа.Remove(strd);
                    строказаказаBindingSource.Remove(str);
                }
                context.SaveChanges();
                checkedListBox1.Items.Clear();
                foreach (var item in context.Блюда.ToList().Select(b =>
                       MyPadLeft(b.id_Блюда + " " + b.Название_блюда + " " + b.Поставщики.Название, 70) + " " + b.Цена_одной_порции).ToList())
                {
                    string s = item.ToString();
                    checkedListBox1.Items.Add(s);
                }
                foreach (var item in context.Строка_заказа.Where(sss => sss.id_Заказа == zakaz).ToList())
                {
                    foreach (Строка_заказа item1 in строказаказаBindingSource )
                    {
                        if (item.id_блюда !=item1.id_блюда)
                        {
                            строказаказаBindingSource.Add(item);
                        } 
                    }
                    
                }
                var ff = checkedListBox1.Items;
                chis = false;
                foreach (var item1 in context.Строка_заказа.Where(s => s.id_Заказа == zakaz).ToList())
                {
                    for (int i = 0; i < checkedListBox1.Items.Count; i++)
                    {
                        if (item1.id_блюда == int.Parse(ff[i].ToString().Split(' ')[0]))
                        {
                            checkedListBox1.SetItemChecked(i, true);
                        }
                    }
                }
                foreach (Строка_заказа item1 in строказаказаBindingSource)
                {
                    for (int i = 0; i < checkedListBox1.Items.Count; i++)
                    {
                        if (item1.id_блюда == int.Parse(ff[i].ToString().Split(' ')[0]))
                        {
                            checkedListBox1.SetItemChecked(i, true);
                        }
                    }
                }
                chis = true;
            }
        }

        private void bunifuMetroTextbox1_OnValueChanged(object sender, EventArgs e)
        {
            isSearchRunning = true;
            
                var filtered = allItems.Where(all => all.Name.ToLower().Contains(bunifuMetroTextbox1.Text.ToLower())).ToList();
                checkedListBox1.Items.Clear();
                foreach (var item in filtered)
                {
                    checkedListBox1.Items.Add(item.Name, item.isChecked);
                }
            
            isSearchRunning = false;
            


        }

        private void bunifuTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;

            }
        }

        private void bunifuTextBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }

    internal class STRZ
    {
        public int Id_Заказа { get; }
        public int Id_блюда { get; }
        public int? Колличество_блюд { get; }

        public STRZ(int id_Заказа, int id_блюда, int? колличество_блюд)
        {
            Id_Заказа = id_Заказа;
            Id_блюда = id_блюда;
            Колличество_блюд = колличество_блюд;
        }

        public override bool Equals(object obj)
        {
            return obj is STRZ other &&
                   Id_Заказа == other.Id_Заказа &&
                   Id_блюда == other.Id_блюда &&
                   Колличество_блюд == other.Колличество_блюд;
        }

        public override int GetHashCode()
        {
            int hashCode = -818427247;
            hashCode = hashCode * -1521134295 + Id_Заказа.GetHashCode();
            hashCode = hashCode * -1521134295 + Id_блюда.GetHashCode();
            hashCode = hashCode * -1521134295 + Колличество_блюд.GetHashCode();
            return hashCode;
        }
    }
}
