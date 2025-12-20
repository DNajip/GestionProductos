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
using System.Text.RegularExpressions;


namespace GestionProductos.Forms
{
    public partial class Registro : Form
    {
        public Registro()
        {
            InitializeComponent();
        }

        private void Registro_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }
        private bool CorreoValido(string correo)
        {
            return correo.Contains("@") && correo.Contains(".");
        }
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            // 1. Validar campos vacíos
            if (string.IsNullOrWhiteSpace(txtNombre.Text) &&
                string.IsNullOrWhiteSpace(txtApellido.Text) &&
                string.IsNullOrWhiteSpace(txtUsuario.Text) &&
                string.IsNullOrWhiteSpace(txtPassword.Text) &&
                string.IsNullOrWhiteSpace(txtCorreo.Text) &&
                string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                MessageBox.Show("Debe llenar los campos de registro");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El campo nombre está vacío");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                MessageBox.Show("El campo apellido está vacío");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                MessageBox.Show("El campo usuario está vacío");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("El campo contraseña está vacío");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCorreo.Text))
            {
                MessageBox.Show("El campo correo está vacío");
                return;
            }

            if (!CorreoValido(txtCorreo.Text))
            {
                MessageBox.Show("Formato de correo inválido Debe ser: usuario@gmail.com");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                MessageBox.Show("El campo teléfono está vacío");
                return;
            }
            if (!txtTelefono.MaskCompleted)
            {
                MessageBox.Show("El teléfono debe tener el formato 0000-0000");
                return;
            }



            try
            {
                using (SqlConnection conexion = ConexionBD.ObtenerConexion())
                {
                    conexion.Open();

                    // 2. Validar usuario existente
                    SqlCommand cmdUsuario = new SqlCommand(
                        "select count(*) from usuarios where usuario = @usuario",
                        conexion);
                    cmdUsuario.Parameters.AddWithValue("@usuario", txtUsuario.Text);

                    int existeUsuario = (int)cmdUsuario.ExecuteScalar();

                    if (existeUsuario > 0)
                    {
                        MessageBox.Show("Usuario ya existe, por favor cambiar su usuario");
                        txtUsuario.Clear();
                        txtUsuario.Focus();
                        return;
                    }

                    // 3. Validar correo existente
                    SqlCommand cmdCorreo = new SqlCommand(
                        "select count(*) from usuarios where email = @correo",
                        conexion);
                    cmdCorreo.Parameters.AddWithValue("@correo", txtCorreo.Text);

                    int existeCorreo = (int)cmdCorreo.ExecuteScalar();

                    if (existeCorreo > 0)
                    {
                        MessageBox.Show("El correo ya tiene un usuario, ingrese un correo nuevo");
                        txtCorreo.Clear();
                        txtCorreo.Focus();
                        return;
                    }

                    // 4. Registrar usuario (procedimiento almacenado)
                    SqlCommand cmd = new SqlCommand("crear_usuario", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@nombre", txtNombre.Text.Trim());
                    cmd.Parameters.AddWithValue("@apellido", txtApellido.Text.Trim());
                    cmd.Parameters.AddWithValue("@usuario", txtUsuario.Text.Trim());
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());
                    cmd.Parameters.AddWithValue("@email", txtCorreo.Text.Trim());
                    cmd.Parameters.AddWithValue("@telefono", txtTelefono.Text);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Usuario registrado correctamente");

                    // 5. Volver al login
                    Login frmlogin = new Login();
                    frmlogin.Show();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar usuario: " + ex.Message);
            }
        }


        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }
       

        private void txtCorreo_TextChanged(object sender, EventArgs e)
        {


        }

        private void txtApellido_TextChanged(object sender, EventArgs e)
        {

        }
       

        private void txtTelefono_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCorreo_Validating(object sender, CancelEventArgs e)
        {
            if (!CorreoValido(txtCorreo.Text))
            {
                MessageBox.Show("El correo debe tener el formato nombreusuario@correo.com");
                e.Cancel = true; // No deja salir del campo
            }
        }

        private void txtTelefono_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {
            Login frmlogin = new Login();
            frmlogin.Show();
            this.Hide();
        }
    }
}
