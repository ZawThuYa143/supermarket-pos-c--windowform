using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SuperMarket
{
    public partial class Product : Form
    {
        public Product()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Zaw Thu Ya\Documents\supermarketdb.mdf"";Integrated Security=True;Connect Timeout=30");

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void populate()
        {
            Con.Open();
            string query = "select * from ProductTable";
            SqlDataAdapter sentdata = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sentdata);
            var ds = new DataSet();
            sentdata.Fill(ds);
            Product_grid.DataSource = ds.Tables[0];
            Con.Close();

        }

        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void username_TextChanged(object sender, EventArgs e)
        {

        }

        private void select_category_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void fullcombo()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select CatName from CategoryTable", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("CatName",typeof(string));
            dataTable.Load(rdr);
            selete_grid_category.ValueMember = "CatName";
            selete_grid_category.DataSource = dataTable;
            select_category.ValueMember ="CatName";
            select_category.DataSource = dataTable;
            Con.Close();
        }

        private void Product_Load(object sender, EventArgs e)
        {
            fullcombo();
            populate();
        }

        private void Category_btn_Click(object sender, EventArgs e)
        {
            Categories categories = new Categories();
            categories.Show();
            this.Hide();
        }

        private void Add_btn_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                string query = "insert into ProductTable values(" + prodID_txt.Text + ",'" + prodName_txt.Text + "'," + prodQuantity_txt.Text + "," + prodPrice_txt.Text + ",'" + select_category.SelectedValue.ToString() + "')";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product Added Successfully!!!");
                Con.Close();
                populate();
                prodID_txt.Text = "";
                prodName_txt.Text = "";
                prodQuantity_txt.Text = "";
                prodPrice_txt.Text = "";
            }
            catch (Exception ex)

            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Product_grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            prodID_txt.Text = Product_grid.SelectedRows[0].Cells[0].Value.ToString();
            prodName_txt.Text = Product_grid.SelectedRows[0].Cells[1].Value.ToString();
            prodQuantity_txt.Text = Product_grid.SelectedRows[0].Cells[2].Value.ToString();
            prodPrice_txt.Text = Product_grid.SelectedRows[0].Cells[3].Value.ToString();
            select_category.SelectedValue = Product_grid.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void Delete_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (prodID_txt.Text == "")
                {
                    MessageBox.Show("Select The Product to Delete!!!");
                }
                else
                {
                    Con.Open();
                    String query = "delete from ProductTable where CatId=" + prodID_txt.Text + "";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product Deleted Successfully!!!");
                    Con.Close();
                    populate();
                    prodID_txt.Text = "";
                    prodName_txt.Text = "";
                    prodQuantity_txt.Text = "";
                    prodPrice_txt.Text = "";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Edit_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (prodID_txt.Text == "" || prodName_txt.Text == "" || prodQuantity_txt.Text == "" || prodPrice_txt.Text == "")
                {
                    MessageBox.Show("Missing Information!!!");
                }
                else
                {
                    Con.Open();
                    string query = "update ProductTable set ProdName='" + prodName_txt.Text + "',ProdQty='" + prodQuantity_txt.Text + "',ProdPrice='" + prodPrice_txt.Text + "',ProdCat='" + select_category.SelectedValue.ToString() + "'where ProdId=" + prodID_txt.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product Successfully Updated!!!");
                    Con.Close();
                    populate();
                    prodID_txt.Text = "";
                    prodName_txt.Text = "";
                    prodQuantity_txt.Text = "";
                    prodPrice_txt.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Seller_btn_Click(object sender, EventArgs e)
        {
            Seller seller = new Seller();
            seller.Show();
            this.Hide();
        }

        private void logout_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginPage loginPage = new LoginPage();
            loginPage.Show();
        }
    }
}
