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
    public partial class Seller : Form
    {
        public Seller()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Zaw Thu Ya\Documents\supermarketdb.mdf"";Integrated Security=True;Connect Timeout=30");
        
        private void populate()
        {
            Con.Open();
            string query = "select * from SellerTable";
            SqlDataAdapter sentdata = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sentdata);
            var ds = new DataSet();
            sentdata.Fill(ds);
            Seller_grid.DataSource = ds.Tables[0];
            Con.Close();

        }

        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Product_btn_Click(object sender, EventArgs e)
        {
            Product prod = new Product();
            prod.Show();
            this.Hide();
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
                string query = "insert into SellerTable values(" + sellerID_txt.Text + ",'" + sellerName_txt.Text + "'," + sellerAge_txt.Text + "," + sellerPhone_txt.Text + ",'" + sellerPassoword_set_txt.Text + "')";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Seller Added Successfully!!!");
                Con.Close();
                populate();
                sellerID_txt.Text = "";
                sellerAge_txt.Text = "";
                sellerName_txt.Text = "";
                sellerPhone_txt.Text = "";
                sellerPassoword_set_txt.Text = "";
            }
            catch (Exception ex)

            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Seller_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void Edit_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (sellerID_txt.Text == "" || sellerName_txt.Text == "" || sellerAge_txt.Text == "" || sellerPhone_txt.Text == "" || sellerPassoword_set_txt.Text == "")
                {
                    MessageBox.Show("Missing Information!!!");
                }
                else
                {
                    Con.Open();
                    string query = "update SellerTable set SellerName='" + sellerName_txt.Text + "',SellerAge='" + sellerAge_txt.Text + "',SellerPhone='" + sellerPhone_txt.Text + "',SellerPass='" + sellerPassoword_set_txt.Text + "'where SellerId=" + sellerID_txt.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Seller Successfully Updated!!!");
                    Con.Close();
                    populate();
                    sellerID_txt.Text = "";
                    sellerAge_txt.Text = "";
                    sellerName_txt.Text = "";
                    sellerPhone_txt.Text = "";
                    sellerPassoword_set_txt.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Delete_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (sellerID_txt.Text == "")
                {
                    MessageBox.Show("Select Seller to Delete!!!");
                }
                else
                {
                    Con.Open();
                    String query = "delete from SellerTable where SellerId=" + sellerID_txt.Text + "";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Seller Deleted Successfully!!!");
                    Con.Close();
                    populate();
                    sellerID_txt.Text = "";
                    sellerAge_txt.Text = "";
                    sellerName_txt.Text = "";
                    sellerPhone_txt.Text = "";
                    sellerPassoword_set_txt.Text = "";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Seller_grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            sellerID_txt.Text = Seller_grid.SelectedRows[0].Cells[0].Value.ToString();
            sellerName_txt.Text = Seller_grid.SelectedRows[0].Cells[1].Value.ToString();
            sellerAge_txt.Text = Seller_grid.SelectedRows[0].Cells[2].Value.ToString();
            sellerPhone_txt.Text = Seller_grid.SelectedRows[0].Cells[3].Value.ToString();
            sellerPassoword_set_txt.Text = Seller_grid.SelectedRows[0].Cells[4].Value.ToString();
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
