using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;




namespace EscolaApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            button1.Click += button1_Click;
            button2.Click += button2_Click;
            button3.Click += button3_Click;

            Load += Form1_Load;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text) ||
                string.IsNullOrWhiteSpace(textBox5.Text))
            {
                MessageBox.Show(
                    "completa todos los campos antes de guardar.",
                    "datos incompletos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            //obtenemos datos del formulario
            try
            {
                using (OleDbConnection conexion = ConexionBD.ObtenerConexion())
                {
                    conexion.Open();
                    string consulta =
                        "INSERT INTO alumnos " +
                        "(matricula, nombre, semestre, grupo, especialidad) " +
                        "VALUES (?, ?, ?, ?, ?) ";
                    using (OleDbCommand comando =
                        new OleDbCommand(consulta, conexion))
                    {


                        comando.Parameters.Add(
                            "?", OleDbType.VarWChar, 20).Value = textBox1.Text.Trim();
                        comando.Parameters.Add(
                          "?", OleDbType.VarWChar, 20).Value = textBox2.Text.Trim();
                        comando.Parameters.Add(
                          "?", OleDbType.Integer).Value = (int)numericUpDown1.Value;
                        comando.Parameters.Add(
                         "?", OleDbType.VarWChar, 20).Value = textBox4.Text.Trim();
                        comando.Parameters.Add(
                            "?", OleDbType.VarWChar, 20).Value = textBox5.Text.Trim();
                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"error al guardar alumno: {ex.Message}",
                    "error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
            }
        }


        private void CargarAlumnos()
        {
            try
            {
                using (OleDbConnection conexion = ConexionBD.ObtenerConexion())
                {
                    conexion.Open();
                    string consulta =
                        "SELECT idAlumno, matricula, nombre," +
                        "semestre, grupo, especialidad " +
                        "FROM Alumnos Order By idAlumno";
                    using (OleDbDataAdapter adaptador = new OleDbDataAdapter(consulta, conexion))
                    {
                        DataTable tabla = new DataTable();
                        adaptador.Fill(tabla);

                        dgvalumnos.DataSource = tabla;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"error al cargar alumno: {ex.Message}",
                    "error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CargarAlumnos();
        }
    }
}
