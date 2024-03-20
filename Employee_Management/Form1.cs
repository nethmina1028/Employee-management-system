using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using Microsoft.VisualBasic.ApplicationServices;

namespace Employee_Management
{
    public partial class Form1 : Form
    {

        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\nethm\OneDrive\Documents\employee.mdf;Integrated Security=True;Connect Timeout=30");

        public Form1()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

            string user = login_username.Text;
            string pass = login_password.Text;


            if (login_username.Text == "" ||  login_password.Text == "")
            {
                MessageBox.Show("Fill all field", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    connect.Open();


                    // Query to validate user login
                    string query = "SELECT * FROM Users WHERE Username = @username AND password = @password";




                    //    string selectData = "SELECT * FROM users WHERE username = @username" + "AND password = @password";

                    // using(SqlCommand cmd = new SqlCommand(selectData,connect))

                       using (SqlCommand cmd = new SqlCommand(query, connect))
                    
                    {


                        cmd.Parameters.AddWithValue("@username",user);
                        cmd.Parameters.AddWithValue("@password",pass);




                        // cmd.Parameters.AddWithValue("@username",login_username.Text.Trim());
                        // cmd.Parameters.AddWithValue("@password", login_password.Text.Trim());


                        //SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        //DataTable table = new DataTable();  
                        //adapter.Fill(table);

                        //if(table.Rows.Count >= 1)

                       
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())

                        {
                            MessageBox.Show("Login successfuly", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            MainForm mainForm = new MainForm();
                            mainForm.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Incorect username or password", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                    }


                }catch(Exception ex)
                {
                    MessageBox.Show("Error:" + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                
                {
                
                    connect.Close();
                
                }
            }
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void login_signupButton_Click(object sender, EventArgs e)
        {
           Register register = new Register();
            register.Show();
            this.Hide();
        }

        private void login_showPass_CheckedChanged(object sender, EventArgs e)
        {
            
        }
    }
}
