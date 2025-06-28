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
    public partial class StudentIndividualPage : Form
    {

        private int studentId;
        private string ConnectionString = "server=localhost;port=3306;database=StudntInfoDB;user=root;password=;";

        public StudentIndividualPage(int studentId)
        {
            InitializeComponent();
            this.studentId = studentId;
            LoadStudentDetails();
        }

        private void LoadStudentDetails()
        {
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                string query = @"
                    SELECT s.*, c.courseName, y.yearLvl 
                    FROM StudentRecordTB s
                    JOIN CourseTB c ON s.courseId = c.courseId
                    JOIN YearTB y ON s.yearId = y.yearId
                    WHERE s.studentId = @studentId";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@studentId", studentId);

                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    lblStudID.Text = "Student ID: " + reader["studentId"];
                    lblFullName.Text = "Full Name: " + reader["firstName"] + " " + reader["middleName"] + " " + reader["lastName"];
                    lblAddress.Text = "Address: " + reader["houseNo"] + " " + reader["brgyName"] + ", " + reader["municipality"] + ", " + reader["province"];
                    lblEmail.Text = "Email: " + reader["emailAddress"];
                    lblContact.Text = "Contact: " + reader["studContactNo"];
                    lblBirthdate.Text = "Birthdate: " + reader["birthdate"];
                    lblGuardian.Text = "Guardian: " + reader["guardianFirstName"] + " " + reader["guardianLastName"];
                    lblNickname.Text = "Nickname: " + reader["nickname"];
                    lblHobbies.Text = "Hobbies: " + reader["hobbies"];
                    lblCourseYear.Text = "Course & Year: " + reader["courseName"] + " - Year " + reader["yearLvl"];
                }
            }
        }

        private void StudentIndividualPage_Load(object sender, EventArgs e)
        {

        }
    }
}
