using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperMarket
{
    public partial class Selling : Form
    {
        public Selling()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Zaw Thu Ya\Documents\supermarketdb.mdf"";Integrated Security=True;Connect Timeout=30");

        int total_bill = 0, n = 0;
        private void Refresh_btn_Click(object sender, EventArgs e)
        {
            if( selling_prodName_txt.Text == "" || selling_qty_txt.Text == "")
            {
                MessageBox.Show("Missing Information");
            } 
            else 
            { 
                int total = Convert.ToInt32(selling_price_txt.Text)*Convert.ToInt32(selling_qty_txt.Text);
                DataGridViewRow newrow = new DataGridViewRow();
                newrow.CreateCells(Selling_grid);
                newrow.Cells[0].Value = n+1;
                newrow.Cells[1].Value = selling_prodName_txt.Text;
                newrow.Cells[2].Value = selling_price_txt.Text;
                newrow.Cells[3].Value = selling_qty_txt.Text;
                newrow.Cells[4].Value = total;
                Selling_grid.Rows.Add(newrow);
                n++;
                total_bill = total_bill + total;
                show_total_price.Text = total_bill.ToString();
                selling_prodName_txt.Text = "";
                selling_price_txt.Text = "";
                selling_qty_txt.Text = "";
            }
        }

        private void fullcombo()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select CatName from CategoryTable", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("CatName", typeof(string));
            dataTable.Load(rdr);
            selete_product_category.ValueMember = "CatName";
            selete_product_category.DataSource = dataTable;
            Con.Close();
        }

        private void populate()
        {
            Con.Open();
            string query = "select ProdName,ProdPrice from ProductTable";
            SqlDataAdapter sentdata = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sentdata);
            var ds = new DataSet();
            sentdata.Fill(ds);
            product_grid_show.DataSource = ds.Tables[0];
            Con.Close();

        }

        private void populatebill()
        {
            Con.Open();
            string query = "select * from BillTable";
            SqlDataAdapter sentdata = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sentdata);
            var ds = new DataSet();
            sentdata.Fill(ds);
            selled_list.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Selling_Load(object sender, EventArgs e)
        {
            populate();
            populatebill();
            fullcombo();
            sellername.Text = LoginPage.SellerName;
        }

        private void selete_grid_category_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Con.Open();
            string query = "select ProdName,ProdPrice from ProductTable where ProdCat='" + selete_product_category.SelectedValue.ToString()+"'";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var sd = new DataSet();
            sda.Fill(sd);
            product_grid_show.DataSource = sd.Tables[0];
            Con.Close();
        }

        
        private void product_grid_show_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            selling_prodName_txt.Text = product_grid_show.SelectedRows[0].Cells[0].Value.ToString();
            selling_price_txt.Text = product_grid_show.SelectedRows[0].Cells[1].Value.ToString();
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            datelabel.Text = DateTime.Today.Day.ToString() + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();
        }

        private void date_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void Selling_grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Add_btn_Click(object sender, EventArgs e)
        {
            if ( selling_ID_txt.Text == "" )
            {
                MessageBox.Show("Missing Bill ID !!!");
            }
            else 
            { 
                try
                {
                    Con.Open();
                    string query = "insert into BillTable values(" + selling_ID_txt.Text + ",'" + sellername.Text + "','" + datelabel.Text + "','" + show_total_price.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Order Added Successfully!!!");
                    Con.Close();
                    selling_ID_txt.Text = "";
                    populatebill();
                    Selling_grid.Rows.Clear();
                    Selling_grid.Refresh();
                    show_total_price.Text = "0";
                }
                catch (Exception ex)

                {
                    MessageBox.Show(ex.Message);
                }
            }
        
        }

        private void selled_list_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("FAMILY SUPERMARKET", new Font("Century Gothic", 25, FontStyle.Bold),Brushes.Black, new Point(230));
            e.Graphics.DrawString("Bill ID : " + selled_list.SelectedRows[0].Cells[0].Value.ToString(), new Font("Century Gothic", 25, FontStyle.Bold), Brushes.Blue, new Point(100,70));
            e.Graphics.DrawString("Seller Name : " + selled_list.SelectedRows[0].Cells[1].Value.ToString(), new Font("Century Gothic", 25, FontStyle.Bold), Brushes.Blue, new Point(100, 100));
            e.Graphics.DrawString("Bill Date : " + selled_list.SelectedRows[0].Cells[2].Value.ToString(), new Font("Century Gothic", 25, FontStyle.Bold), Brushes.Blue, new Point(100, 130));
            e.Graphics.DrawString("Total Amount : " + selled_list.SelectedRows[0].Cells[3].Value.ToString(), new Font("Century Gothic", 25, FontStyle.Bold), Brushes.Blue, new Point(100, 160));
            e.Graphics.DrawString("Thanks For Your Shopping.", new Font("Century Gothic", 25, FontStyle.Italic), Brushes.Black, new Point(230, 230));
        }

        private void Print_btn_Click(object sender, EventArgs e)
        {
            if(printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void logout_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginPage loginPage = new LoginPage();
            loginPage.Show();
        }

        private void show_total_price_Click(object sender, EventArgs e)
        {

        }

        private void Refresh_btn_Click_1(object sender, EventArgs e)
        {
            populate();
        }
    }
}
