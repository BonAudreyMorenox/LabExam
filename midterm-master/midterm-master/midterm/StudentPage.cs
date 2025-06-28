using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace midterm
{
    public partial class StudentPage : Form
    {
        private string ConnectionString = "server=localhost;port=3306;database=StudntInfoDB;user=root;password=;";
        public StudentPage()
        {
            InitializeComponent();
            LoadStudents();
        }

        private void LoadStudents()
        {
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                string query = @"SELECT studentId, CONCAT(firstName, ' ', middleName, ' ', lastName) AS fullName FROM StudentRecordTB";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                StudentsGrid.DataSource = dt;

                if (!StudentsGrid.Columns.Contains("ViewBtn"))
                {
                    DataGridViewButtonColumn viewBtn = new DataGridViewButtonColumn();
                    viewBtn.Name = "ViewBtn";
                    viewBtn.HeaderText = "Action";
                    viewBtn.Text = "VIEW";
                    viewBtn.UseColumnTextForButtonValue = true;
                    StudentsGrid.Columns.Add(viewBtn);
                }
            }
        }

        private void StudentGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == StudentsGrid.Columns["ViewBtn"].Index && e.RowIndex >= 0)
            {
                int studentId = Convert.ToInt32(StudentsGrid.Rows[e.RowIndex].Cells["studentId"].Value);
                StudentIndividualPage detailsForm = new StudentIndividualPage(studentId);
                detailsForm.Show(); 
            }
        }

    }
    
}
