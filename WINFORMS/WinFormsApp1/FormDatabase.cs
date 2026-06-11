
using Microsoft.Data.SqlClient;
using System.Data;
using System.Runtime.Intrinsics.Arm;

namespace WinFormsApp1
{
    public partial class FormDatabase : Form
    {
        private void CaricaCategorie()
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                string sql = @"SELECT CategoryID, CategoryName, Description
                       FROM Categories
                       ORDER BY CategoryID";

                SqlDataAdapter da = new SqlDataAdapter(sql, connection);

                DataTable dt = new DataTable();
                da.Fill(dt);

                grid.DataSource = dt;
            }
        }

        private string connString = "Server=MTSWEBTEST\\SQLTEST;Database=Northwind;User Id=sa;Password=Mts.2010;TrustServerCertificate=true;";



        public FormDatabase()
        {
            InitializeComponent();
            CaricaCategorie();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }





        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();

            string sqlCommandText = "INSERT INTO Categories(CategoryName,[Description]) Values ('Accessori','Sono molto utili')";
            SqlCommand command = new SqlCommand(sqlCommandText, connection);

            int result = command.ExecuteNonQuery();
            connection.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {

            SqlConnection connection = new SqlConnection(connString);
            connection.Open();

            string sqlCommandText = "UPDATE Categories SET [Description]='Sono utilissimi' WHERE CategoryID=11";
            SqlCommand command = new SqlCommand(sqlCommandText, connection);

            int result = command.ExecuteNonQuery();
            connection.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();

            string sqlCommandText = "DELETE FROM Categories WHERE CategoryID=11";
            SqlCommand command = new SqlCommand(sqlCommandText, connection);

            int result = command.ExecuteNonQuery();
            connection.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            SqlConnection connection = new SqlConnection(connString);

            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }

            string sqlCommandText = "SELECT * FROM Categories";
            SqlCommand command = new SqlCommand(sqlCommandText, connection);

            SqlDataReader dr = command.ExecuteReader();
            listBox1.Items.Clear();
            if (dr.HasRows)
            {

                while (dr.Read())
                {
                    //string result = dr[1].ToString() + " - " + dr[1].ToString();

                    string result = dr["CategoryName"].ToString() + " - " + dr["Description"].ToString();





                    listBox1.Items.Add(result);
                }

            }

            connection.Close();
        }

        private void button6_Click(object sender, EventArgs e) { 
        
           
        
            CaricaCategorie();
        }


        private void grid_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = grid.Rows[e.RowIndex];

            if (row.Cells["CategoryID"].Value == null) return;

            txtId.Text = row.Cells["CategoryID"].Value.ToString();
            txtNome.Text = row.Cells["CategoryName"].Value.ToString();
            txtDescrizione.Text = row.Cells["Description"].Value.ToString();
        }

        private void btnNuovo_Click(object sender, EventArgs e)
        {
            txtId.Text = "0";
            txtNome.Clear();
            txtDescrizione.Clear();

            grid.ClearSelection();

            txtNome.Focus();
        }

        private void btnSalva_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("Inserire il nome della categoria");
                txtNome.Focus();
                return;
            }

            int id = Convert.ToInt32(txtId.Text);

            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();

                SqlCommand command;

                if (id == 0)
                {
                    string sql = @"INSERT INTO Categories (CategoryName, Description)  VALUES  (@nome, @descrizione)";
                    //string sql1 = @$"INSERT INTO Categories (CategoryName, Description)  VALUES  ('{txtNome.Text}', '{txtDescrizione.Text}')";
                    command = new SqlCommand(sql, connection);
                }
                else
                {
                    string sql =  @"UPDATE Categories  SET CategoryName = @nome, Description = @descrizione WHERE CategoryID = @id";

                    command = new SqlCommand(sql, connection);

                    command.Parameters.AddWithValue("@id", id);
                }

                command.Parameters.AddWithValue("@nome", txtNome.Text);
                command.Parameters.AddWithValue("@descrizione", txtDescrizione.Text);

                command.ExecuteNonQuery();
            }

            CaricaCategorie();

            MessageBox.Show("Salvataggio eseguito");
        }

        private void btnElimina_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtId.Text);

            if (id == 0)
            {
                MessageBox.Show("Nessun record selezionato");
                return;
            }

            DialogResult risposta =
                MessageBox.Show( "Confermi l'eliminazione?", "Elimina", MessageBoxButtons.YesNo,MessageBoxIcon.Question);

            if (risposta != DialogResult.Yes)
                return;

            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();

                string sql ="DELETE FROM Categories WHERE CategoryID = @id";

                SqlCommand command =
                    new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();
            }

            CaricaCategorie();

            btnNuovo_Click(null, null);

            MessageBox.Show("Record eliminato");
        }

        private void btnAnnulla_Click(object sender, EventArgs e)
        {

            if (grid.CurrentRow == null)
                return;

            txtId.Text = grid.CurrentRow.Cells["CategoryID"].Value.ToString();


            txtNome.Text = grid.CurrentRow.Cells["CategoryName"].Value.ToString();


            txtDescrizione.Text = grid.CurrentRow.Cells["Description"].Value.ToString();

        }

    }
}
