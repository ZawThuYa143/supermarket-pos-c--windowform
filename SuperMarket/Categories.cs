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
    public partial class Categories : Form
    {
        public Categories()
        {
            InitializeComponent();
        }

        private void Categories_Load(object sender, EventArgs e)
        {
            populate();            
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Zaw Thu Ya\Documents\supermarketdb.mdf"";Integrated Security=True;Connect Timeout=30");
        private void Add_btn_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                string query = "insert into CategoryTable values(" + catID_txt.Text + ",'" + catName_txt.Text + "','" + Description_txt.Text + "')";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Category Added Successfully!!!");
                Con.Close();
                populate();
                catID_txt.Text = "";
                catName_txt.Text = "";
                Description_txt.Text = "";

            } catch (Exception ex)

            {
                MessageBox.Show(ex.Message);
            }
        }

        private void populate()
        {
            Con.Open();
            string query = "select * from CategoryTable";
            SqlDataAdapter sentdata = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sentdata);
            var ds = new DataSet();
            sentdata.Fill(ds);
            Categories_grid.DataSource = ds.Tables[0];
            Con.Close();
            
        }

        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Categories_grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           catID_txt.Text = Categories_grid.SelectedRows[0].Cells[0].Value.ToString();
           catName_txt.Text = Categories_grid.SelectedRows[0].Cells[1].Value.ToString();
           Description_txt.Text = Categories_grid.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void catName_txt_TextChanged(object sender, EventArgs e)
        {

        }

        private void cat_dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void Delete_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (catID_txt.Text == "")
                {
                    MessageBox.Show("Select The Category to Delete!!!");
                }
                else
                {
                    Con.Open();
                    String query = "delete from CategoryTable where CatId=" + catID_txt.Text + "";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category Deleted Successfully!!!");
                    Con.Close();
                    populate();
                    catID_txt.Text = "";
                    catName_txt.Text = "";
                    Description_txt.Text = "";
                }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Edit_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if(catID_txt.Text=="" || catName_txt.Text == "" || Description_txt.Text == "")
                {
                    MessageBox.Show("Missing Information!!!");
                }
                else
                { 
                    Con.Open();
                    string query = "update CategoryTable set CatName='" + catName_txt.Text + "',CatDes='" + Description_txt.Text + "'where CatId=" + catID_txt.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category Successfully Updated!!!");
                    Con.Close();
                    populate();
                    catID_txt.Text = "";
                    catName_txt.Text = "";
                    Description_txt.Text = "";
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);    
            }
        }

        private void Product_btn_Click(object sender, EventArgs e)
        {
            Product prod = new Product();
            prod.Show();
            this.Hide();
        }

        private void Seller_btn_Click(object sender, EventArgs e)
        {
            Seller seller = new Seller();
            seller.Show();
            this.Hide();
        }

        private void Refresh_btn_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void logout_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginPage loginPage = new LoginPage();
            loginPage.Show();
        }
    }
}
