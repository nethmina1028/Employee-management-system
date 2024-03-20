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
using System.IO;// path by line 101

namespace Employee_Management
{
    public partial class Addemployee : UserControl
    {

        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\nethm\OneDrive\Documents\employee.mdf;");

        public Addemployee()
        {
            InitializeComponent();
            displayEmployeeData();
            
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }


        //Show all database details
        public void displayEmployeeData()
        {
            EmployeeData ed = new EmployeeData();
            List<EmployeeData> listData = ed.employeeListData();

            guna2DataGridView1.DataSource = listData;
        }




        private void addEmployee_addBtn_Click(object sender, EventArgs e)
        {
            if (addEmployee_id.Text == ""
                || addEmployee_fullName.Text == ""
                || addEmployee_gender.Text == ""
                || addEmployee_phoneNumber.Text == ""
                || addEmployee_position.Text == ""
                || addEmployee_status.Text == ""
                || addEmployee_picture.Image == null)
            {
                MessageBox.Show("plz fill all","Error messag",MessageBoxButtons.OK,MessageBoxIcon.Error);

            }
            else
            {
                if(connect.State == ConnectionState.Closed)
                {
                    try
                    {
                        connect.Open();

                        string checkEmID = "SELECT COUNT(*) FROM employees WHERE employee_id = @emID";

                        using(SqlCommand checkEm = new SqlCommand(checkEmID, connect))
                        {
                            checkEm.Parameters.AddWithValue("@emID",addEmployee_id.Text.Trim());

                            int count =(int)checkEm.ExecuteScalar();

                            if(count>=1)
                            {
                                MessageBox.Show(addEmployee_id.Text.Trim() + "is already taken", "error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                            else
                            {
                                DateTime today = DateTime.Today;
                                string insertData = "INSERT INTO employees" +
                                         "(employee_id,full_name,gender,contact_number" +
                                           ",position,image,salary,insert_date,status)" +

                                           "VALUES(@employeeID,@fullName,@gender,@contactNum" +
                                           ",@position, @image,@salary,@insertDate, @status)";

                                //add directary file path in here 

                                string path = Path.Combine(@"C:\Users\nethm\source\repos\Employee_Management\Employee_Management\Directory\"
                                       + addEmployee_id.Text.Trim() + ".jpg");

                                string directoryPath=Path.GetDirectoryName(path);

                                if (!Directory.Exists(directoryPath))
                                {
                                    Directory.CreateDirectory(directoryPath);
                                }
                                 File.Copy(addEmployee_picture.ImageLocation, path, true);
                                 

                                using(SqlCommand cmd = new SqlCommand(insertData,connect))
                                {
                                    cmd.Parameters.AddWithValue("@employeeID",addEmployee_id.Text.Trim());
                                    cmd.Parameters.AddWithValue("@fullName", addEmployee_fullName.Text.Trim());
                                    cmd.Parameters.AddWithValue("@gender", addEmployee_gender.Text.Trim());
                                    cmd.Parameters.AddWithValue("@contactNum", addEmployee_phoneNumber.Text.Trim());
                                    cmd.Parameters.AddWithValue("@position", addEmployee_position.Text.Trim());
                                    cmd.Parameters.AddWithValue("@image", path);
                                    cmd.Parameters.AddWithValue("@salary",0);
                                    cmd.Parameters.AddWithValue("@insertDate", today);
                                    cmd.Parameters.AddWithValue("@status", addEmployee_status.Text.Trim());

                                    cmd.ExecuteNonQuery();

                                    displayEmployeeData();


                                    MessageBox.Show("Added successfully!"
                                        , "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    //clear part
                                    clearField();
                                }
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                      MessageBox.Show("Error:"+ex,"Error Message",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connect.Close();
                    }
                }
            }
        }
            
           //image upload button handel 
        private void addEmployee_importButton_Click(object sender, EventArgs e)
        {
            try
            {

                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Image File(*.jpg;*.png)|*.jpg;*.png";
                string imagePath = "";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    imagePath = dialog.FileName;
                    addEmployee_picture.ImageLocation = imagePath;
                     
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("Error:"+ex,"Error Message"
                    ,MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex != -1)

            {
                //This retrieves the DataGridViewRow corresponding to the clicked cell's row

                DataGridViewRow row = guna2DataGridView1.Rows[e.RowIndex];

                addEmployee_id.Text = row.Cells[1].Value.ToString();
                addEmployee_fullName.Text = row.Cells[2].Value.ToString();
                addEmployee_gender.Text = row.Cells[3].Value.ToString();
                addEmployee_phoneNumber.Text = row.Cells[4].Value.ToString();
                addEmployee_position.Text = row.Cells[5].Value.ToString();

                string imagePath = row.Cells[6].Value.ToString();
                if(imagePath !=null)
                {
                    addEmployee_picture.Image=Image.FromFile(imagePath);
                }
                else
                {
                    addEmployee_picture = null;
                }
                addEmployee_status.Text = row.Cells[8].Value.ToString();


            }
        }
                    

        public void clearField()
        {
            addEmployee_id.Text = "";
            addEmployee_fullName.Text = "";
            addEmployee_gender.SelectedIndex = -1;
            addEmployee_phoneNumber.Text = "";
            addEmployee_position.SelectedIndex = -1;
            addEmployee_status.SelectedIndex = -1;
            addEmployee_picture.Image = null;
        }






          // update part
        private void addEmployee_updateBtn_Click(object sender, EventArgs e)
        {

            if (addEmployee_id.Text == ""
                || addEmployee_fullName.Text == ""
                || addEmployee_gender.Text == ""
                || addEmployee_phoneNumber.Text == ""
                || addEmployee_position.Text == ""
                || addEmployee_status.Text == ""
                || addEmployee_picture.Image == null)
            {
                MessageBox.Show("plz fill all", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                DialogResult check = MessageBox.Show("Are you sure you want to UPDATE"+
                    "Employee ID:"+ addEmployee_id.Text.Trim()+"?","Confirmation Message"
                    ,MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                     
                if (check == DialogResult.Yes) 
                {

                    try
                    {
                        connect.Open();

                        DateTime today = DateTime.Today;


                        string updateData = "UPDATE employees SET full_name = @fullName" +
                            ",gender = @gender,contact_number = @contactNum" +
                            ",position = @position,update_date = @updateDate,status = @status" +
                             "WHERE employee_id =@employeeID ";

                        using(SqlCommand cmd = new SqlCommand(updateData,connect))
                        {
                            cmd.Parameters.AddWithValue("@fullName",addEmployee_fullName.Text.Trim());
                            cmd.Parameters.AddWithValue("@gender", addEmployee_gender.Text.Trim());
                            cmd.Parameters.AddWithValue("@contactNum", addEmployee_phoneNumber.Text.Trim());
                            cmd.Parameters.AddWithValue("@position", addEmployee_position.Text.Trim());
                            cmd.Parameters.AddWithValue("@updateDate",today);
                            cmd.Parameters.AddWithValue("@status", addEmployee_status.Text.Trim());
                            cmd.Parameters.AddWithValue("@employeeID", addEmployee_id.Text.Trim());

                            cmd.ExecuteNonQuery();

                            displayEmployeeData();


                            MessageBox.Show("update successfully!"
                                , "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            clearField();
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error:" + ex, "Error Message"
                        , MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connect.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Cancelled", "Information Message"
                       , MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                
            }
        }

        private void addEmployee_clearBtn_Click(object sender, EventArgs e)
        {
            clearField();
        }

        private void addEmployee_deleteBtn_Click(object sender, EventArgs e)
        {
           // 2.10 blnna
        }
    }
}
