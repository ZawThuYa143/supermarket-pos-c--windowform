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
using System.Web.UI.Design;

namespace SuperMarket
{
    public partial class LoginPage : Form
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        public static string SellerName = "";
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Zaw Thu Ya\Documents\supermarketdb.mdf"";Integrated Security=True;Connect Timeout=30");

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void clearall_Click(object sender, EventArgs e)
        {
            username.Text = "";
            password.Text = "";
        }

        private void login_btn_Click(object sender, EventArgs e)
        {
            if( username.Text =="" || password.Text =="")
            {
                MessageBox.Show("Enter Username and Password!!!");
            } else
            {
                if(selectrole.SelectedIndex > -1)
                { 
                    if(selectrole.SelectedItem.ToString() == "Admin")
                    {
                        if( username.Text == "Admin" && password.Text =="12345")
                        {
                            Product prod = new Product();
                            prod.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("If You Are Admin, Please Enter Correct Name And Password!!!");
                        }
                    }
                    else
                    {
                        //MessageBox.Show("Now, You Are In Selling Section!!!");
                        Con.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter("select count(8) from SellerTable where SellerName='"+ username.Text + "'and SellerPass ='" + password.Text + "'",Con);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        if (dt.Rows[0][0].ToString() == "1")
                        {
                            SellerName = username.Text;
                            this.Hide();
                            Selling selling = new Selling();
                            selling.Show();
                            Con.Close();
                        }
                        else
                        {
                            MessageBox.Show("Wrong Username or Password!!!");
                        }
                        Con.Close();
                    }
                } else
                {
                    MessageBox.Show("Please Select a Role!!!");
                }                
            }
        }

        private void selectrole_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
