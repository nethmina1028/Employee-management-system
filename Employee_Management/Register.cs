using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//add first
using System.Data;
using System.Data.SqlClient;
using Microsoft.VisualBasic;

namespace Employee_Management
{
    public partial class Register : Form
    {
       SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\nethm\OneDrive\Documents\employee.mdf;Integrated Security=True;Connect Timeout=30");
        public Register()
        {
            InitializeComponent();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void signup_loginBtn_Click(object sender, EventArgs e)
        {
          /*  Form1 loginform = new Form1();
            loginform.Show();
            this.Hide();
          */
        }

        private void signup_signupBtn_Click(object sender, EventArgs e)
        {
           
             if(signup_username.Text==""|| signup_password.Text=="")
             {
                 MessageBox.Show("Plz fill all blank","Error Message",MessageBoxButtons.OK,MessageBoxIcon.Error);
             }
             else
             {
                 if(connect.State != ConnectionState.Open)
                 {
                     try
                     {
                         connect.Open();

                        // to cheack if the user is existing already 30.02 in video
                        /*
                        string selectUsername = "SELECT COUNT(id) FROM users WHERE username = @user";


                        using (SqlCommand checkUser = new SqlCommand(selectUsername, connect)) ;

                        {
                            int count = c

                        }

                        */
                            DateTime today = DateTime.Today;
                         string insertData = "INSERT INTO users" +"(username,password,date_register)"+"VALUES(@username,@password,@dateReg)";

                         using(SqlCommand cmd = new SqlCommand(insertData,connect))
                         {
                             cmd.Parameters.AddWithValue("@username",signup_username.Text.Trim());
                             cmd.Parameters.AddWithValue("@password",signup_password.Text.Trim());
                             cmd.Parameters.AddWithValue("@dateReg", today);


                             cmd.ExecuteNonQuery();


                             MessageBox.Show("Registerd successfuly", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                             Form1 loginform = new Form1();
                             loginform.Show();
                             this.Hide();

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
             
        }
    }
}
