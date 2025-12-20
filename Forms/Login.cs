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
using GestionProductos.Data;
using GestionProductos.Forms;

namespace GestionProductos
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            // 1. Validación de campos
            if (string.IsNullOrWhiteSpace(txtusuario.Text) ||
                string.IsNullOrWhiteSpace(txtpassword.Text))
            {
                MessageBox.Show("Debe ingresar usuario y contraseña",
                                "Validación",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conexion = ConexionBD.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("login_usuario", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@usuario", txtusuario.Text.Trim());
                        cmd.Parameters.AddWithValue("@password", txtpassword.Text.Trim());

                        conexion.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // 2. Login correcto
                                string nombreUsuario = reader["nombre"].ToString();

                                MessageBox.Show($"Bienvenido {nombreUsuario}",
                                                "Acceso permitido",
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Information);

                                // 3. Abrir formulario principal
                                GProductos frmPrincipal = new GProductos();
                                frmPrincipal.Show();

                                // 4. Cerrar login
                                this.Hide();
                            }
                            else
                            {
                                // 5. Credenciales incorrectas
                                MessageBox.Show("Usuario o contraseña incorrectos",
                                                "Error de acceso",
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de conexión: " + ex.Message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtusuario_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtpassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnRegistro_Click(object sender, EventArgs e)
        {
            Registro frmRegistro = new Registro();
            frmRegistro.Show();
            this.Hide();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
            Registro frmRegistro = new Registro();
            frmRegistro.Show();
            this.Hide();
        }
    }
}
