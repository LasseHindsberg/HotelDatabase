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
using System.Xml.Linq;

namespace HotelDatabase
{
    public partial class Form1 : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        public Form1()
        {
            InitializeComponent();
            conn = new SqlConnection(@"Data Source=localhost;Initial Catalog=TrainingDatabase;Integrated Security=True");
            cmd = new SqlCommand();
            cmd.Connection = conn;
        }

        private void cleardata()
        {
            textBox1.Clear();
            textBox2.Clear();
        }
        private void displaydata()
        {
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select Facility_No, Name from dbo.Facilities";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            displaydata();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Clears previous Parameters(Text box) incase it/Others has been run before
            cmd.Parameters.Clear();

            string query = "INSERT INTO Facilities VALUES (@FacilityID, @FacilityName)";
            conn.Open();
            // For some reason this stuff has Swapped so textbox 2 = id and not name.
            cmd.Parameters.AddWithValue("@FacilityID", textBox2.Text);
            cmd.Parameters.AddWithValue("@FacilityName", textBox1.Text);

            cmd.CommandText = query;
            cmd.ExecuteNonQuery();
            cleardata();
            conn.Close();
            displaydata();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            // Clears previous Parameters(Text box) incase it/Others has been run before
            cmd.Parameters.Clear();

            conn.Open();
            cmd.CommandType = CommandType.Text;
            string query = "UPDATE Facilities SET Name=(@FacilityName) WHERE Facility_No=(@FacilityID)";
            cmd.Parameters.AddWithValue("@FacilityID", textBox2.Text);
            cmd.Parameters.AddWithValue("@FacilityName", textBox1.Text);

            cmd.CommandText = query;
            cmd.ExecuteNonQuery();
            conn.Close();
            displaydata();
            cleardata();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Clears previous Parameters(Text box) incase it/Others has been run before
            cmd.Parameters.Clear();


            string query = "DELETE Facilities WHERE Facility_No=(@FacilityID)";
            cmd.Parameters.AddWithValue("@FacilityID", textBox2.Text);

            cmd.CommandText = query;
            conn.Open();
            cmd.ExecuteNonQuery();
            dataGridView1.DataSource = query;
            cleardata();
            conn.Close();
            displaydata();
        }

    }
}
