using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace BD
{   

    public partial class Osn : Form
    {
        public Osn()
        {
            InitializeComponent();
        }
        public int perecl=1;

        private void bunifuGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        public static int perecl1 = 0;

        private void Osn_Load(object sender, EventArgs e)
        {

            label1.Text = "Заказы";
                using (var context = new testEntities1())
                {
                    bunifuDataGridView1.DataSource = context.Заказы.Select(z => new
                    {
                        id = z.id_заказа,
                        АдресДоставки = z.Клиент.Адрес_Доставки,
                        КонтактныйТел = z.Клиент.НомерТелефона,
                        Сотрудник = z.Сотрудники.ФИО,
                        ОбщаяСумма = z.Общая_сумма_заказа,
                        Дата = z.Дата
                    }).ToList();
                bunifuDataGridView1.Columns[0].Visible = false;
            }
           
            
        }
        public void Zagr()
        {
            if (perecl == 0)
            {
                using (var context = new testEntities1())
                {
                    bunifuDataGridView1.DataSource = context.Заказы.Select(z => new
                    {
                        id = z.id_заказа,
                        АдресДоставки = z.Клиент.Адрес_Доставки,
                        КонтактныйТел = z.Клиент.НомерТелефона,
                        Сотрудник = z.Сотрудники.ФИО,
                        ОбщаяСумма = z.Общая_сумма_заказа,
                        Дата = z.Дата
                    }).ToList();
                    perecl = 1;
                    perecl1 = 0;
                    label1.Text = "Заказы";
                    bunifuDataGridView1.Columns[0].Visible = false;
                }
            }
            else
            {
                using (var context = new testEntities1())
                {
                    bunifuDataGridView1.DataSource = context.Блюда.Select(z1 => new
                    {
                        id = z1.id_Блюда,
                        Название = z1.Название_блюда,
                        Поставщик = z1.Поставщики.Название,
                        Цена = z1.Цена_одной_порции
                    }).ToList();
                }
                label1.Text = "Блюда";
                perecl = 0;
                bunifuDataGridView1.Columns[0].Visible = false;
            }
        }
        
        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuTileButton1_Click(object sender, EventArgs e)
        {
            ADD add = new ADD();
            add.t = false;
            add.ShowDialog(this);
        }
        
        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            if (label1.Text=="Заказы")
             {
                ADD.chis = false;
                ADD add = new ADD();
                add.bunifuImageButton3.Visible = true;
                add.bunifuImageButton2.Visible = false;
                add.t = true;
                ADD.zakaz = int.Parse(bunifuDataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                add.ShowDialog(this);
            }
            else
            {
                using (var context = new testEntities1())
                {
                    Tovar tov = new Tovar();
                    var ed = context.Блюда.Find(int.Parse(bunifuDataGridView1.SelectedRows[0].Cells[0].Value.ToString()));
                    var post = ed.Поставщики.Название;
                    tov.bunifuTextBox4.Text = ed.Название_блюда.ToString();
                    tov.bunifuTextBox1.Text = post.ToString();
                    tov.bunifuTextBox2.Text = ed.Поставщики.Филиалы.Адрес_Филиала.ToString();
                    tov.bunifuTextBox3.Text= ed.Поставщики.Филиалы.Номер_Телефона.ToString();
                    tov.bunifuTextBox5.Text = ed.Цена_одной_порции.ToString();
                    bl = ed;
                    tov.bunifuButton1.Text = "Изменить";
                    tov.ShowDialog(this);
                }
                
            }
            
        }
        public static Блюда bl;
        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            if (label1.Text=="Заказы")
            {
                using (var context = new testEntities1())
                {

                    Заказы strd = context.Заказы.Find(int.Parse(bunifuDataGridView1.SelectedRows[0].Cells[0].Value.ToString()));
                    context.Заказы.Remove(strd);
                    context.SaveChanges();
                    bunifuDataGridView1.DataSource = context.Заказы.Select(z => new
                    {
                        id = z.id_заказа,
                        АдресДоставки = z.Клиент.Адрес_Доставки,
                        КонтактныйТел = z.Клиент.НомерТелефона,
                        Сотрудник = z.Сотрудники.ФИО,
                        ОбщаяСумма = z.Общая_сумма_заказа,
                        Дата = z.Дата
                    }).ToList();
                }
            }
            else
            {
                using (var context = new testEntities1())
                {

                    Блюда strd = context.Блюда.Find(int.Parse(bunifuDataGridView1.SelectedRows[0].Cells[0].Value.ToString()));
                    context.Блюда.Remove(strd);
                    context.SaveChanges();
                    
                        bunifuDataGridView1.DataSource = context.Блюда.Select(z1 => new
                        {
                            id = z1.id_Блюда,
                            Название = z1.Название_блюда,
                            Поставщик = z1.Поставщики.Название,
                            Цена = z1.Цена_одной_порции
                        }).ToList();
                    
                }
            }
            MessageBox.Show("Удаление прошло успешно", "Уведомеление", MessageBoxButtons.OK);


        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
             using (var context = new testEntities1())
            {
                object missing = Type.Missing;
                Word.Application app = new Word.Application();
             
                Word.Document document = app.Documents.Open(@"C:\Users\The Witcher\source\repos\BD\Chek.docx", ref missing, true);
                document.Application.Visible = true;

                //page orintation
                document.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape;
                

                dynamic oRange = document.Content.Application.Selection.Range;
                string oTemp = "";
                var zakaz = context.Заказы.Find(int.Parse(bunifuDataGridView1.SelectedRows[0].Cells[0].Value.ToString()));
                var client = context.Клиент.Find(zakaz.id_клиента);
                var Sotr = context.Сотрудники.Find(zakaz.id_Сотрудника);
                var summa = zakaz.Общая_сумма_заказа;
                var data = zakaz.Дата;
                var all = zakaz.Строка_заказа.Select(str=> str.Блюда.Название_блюда +"*"+ str.колличество_блюд).ToList();
                document.Bookmarks["numer"].Range.Text = zakaz.id_заказа.ToString();
                document.Bookmarks["adres"].Range.Text = client.Адрес_Доставки.ToString();
                document.Bookmarks["data"].Range.Text = data.ToString();
                document.Bookmarks["summa"].Range.Text = summa.ToString();
                document.Bookmarks["sotr"].Range.Text = Sotr.ФИО.ToString();
                foreach (var item in all)
                {
                    var ran=document.Paragraphs.Add(document.Bookmarks["table"].Range).Range.Text=item+" \n";
                    
                }
                

            }
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            Tovar tov = new Tovar();
            tov.ShowDialog(this);
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
          
            Zagr();
        }

        private void bunifuDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            if (bunifuButton5.Text=="Фильтр")
            {
                bunifuCustomLabel1.Visible = true;
                bunifuCustomLabel2.Visible = true;
                bunifuDatePicker1.Visible = true;
                bunifuDatePicker2.Visible = true;
                bunifuButton5.Text = "Ок";
            }
            else
            {
                
                
                using(var context= new testEntities1())
                {
                    var start = bunifuDatePicker1.Value.Date;
                    var end = bunifuDatePicker2.Value.Date;
                    bunifuDataGridView1.DataSource = context.Заказы.ToList().Where(zzz =>
                        zzz.Дата.Date >= start && zzz.Дата.Date <=end).
                        Select(zp=>new
                    {
                        id = zp.id_заказа,
                        АдресДоставки = zp.Клиент.Адрес_Доставки,
                        КонтактныйТел = zp.Клиент.НомерТелефона,
                        Сотрудник = zp.Сотрудники.ФИО,
                        ОбщаяСумма = zp.Общая_сумма_заказа,
                        Дата = zp.Дата
                    }).ToList();
                    
                    bunifuCustomLabel1.Visible = false;
                    bunifuCustomLabel2.Visible = false;
                    bunifuDatePicker1.Visible = false;
                    bunifuDatePicker2.Visible = false;
                    bunifuButton5.Text = "Фильтр";
                }
            }
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            Zagr();
        }

        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            otch otch = new otch();
            otch.ShowDialog(this);
        }
    }
}
