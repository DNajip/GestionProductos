using GestionProductos.Data;
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
using System.Data.SqlClient;
using GestionProductos.Data;    

namespace GestionProductos.Forms
{
    public partial class GProductos : Form
    {
        public GProductos()
        {
            InitializeComponent();
            CargarProductos();
            CargarEstados();
         

        }


        private void GestionProductos_Load(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Login frmLogin = new Login();
            frmLogin.Show();
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void BuscarProductos(string texto)
        {
            try
            {
                using (SqlConnection conexion = ConexionBD.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("buscar_productos", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@texto", texto);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        dgvProductos.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar productos: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                // Si no escribe nada, vuelve a cargar todos
                CargarProductos();
                return;
            }

            BuscarProductos(txtBuscar.Text.Trim());
        }
        private void CargarProductos()
        {
            // 1. Cargar productos desde la base de datos
            try
            {
                using (SqlConnection conexion = ConexionBD.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("listar_productos", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();

                        da.Fill(dt);

                        dgvProductos.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos: " + ex.Message);
            }

            // 2. Configurar las columnas del DataGridView
            //if (dgvProductos.Columns.Count == 0) return;


            //dgvProductos.Columns[0].HeaderText = "Código";
            //dgvProductos.Columns[1].HeaderText = "Producto";
            //dgvProductos.Columns[2].HeaderText = "Unidades";
            //dgvProductos.Columns[3].HeaderText = "Estado";
            //dgvProductos.Columns[4].HeaderText = "Proveedor";


            if (dgvProductos.Columns.Count == 0) return;

            dgvProductos.Columns["id"].HeaderText = "Código";
            dgvProductos.Columns["producto"].HeaderText = "Producto";
            dgvProductos.Columns["existencia"].HeaderText = "Unidades";
            dgvProductos.Columns["estado"].HeaderText = "Estado";
            dgvProductos.Columns["proveedor"].HeaderText = "Proveedor";

            dgvProductos.Columns["id"].DisplayIndex = 0;
            dgvProductos.Columns["producto"].DisplayIndex = 1;
            dgvProductos.Columns["existencia"].DisplayIndex = 2;
            dgvProductos.Columns["estado"].DisplayIndex = 3;
            dgvProductos.Columns["proveedor"].DisplayIndex = 4;

            if (dgvProductos.Columns.Contains("btnOpciones"))
            {
                dgvProductos.Columns["btnOpciones"].HeaderText = "Opciones";
                dgvProductos.Columns["btnOpciones"].DisplayIndex = 5;
            }


            // 3. Crear botón opciones
            bool existe = false;
            foreach (DataGridViewColumn col in dgvProductos.Columns)
            {
                if (col is DataGridViewButtonColumn)
                {
                    existe = true;
                    break;
                }
            }if (!existe)
            {
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                btn.HeaderText = "Opciones";
                btn.Text = "Ver";
                btn.UseColumnTextForButtonValue = true;
                dgvProductos.Columns.Add(btn);
            }
        }
        
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 &&
               dgvProductos.Columns[e.ColumnIndex].Name == "btnOpciones")
            {
                int idProducto = Convert.ToInt32(
                    dgvProductos.Rows[e.RowIndex].Cells["id"].Value
                );

                OpcionesProducto frm = new OpcionesProducto(idProducto);
                frm.ShowDialog();
            }
        }

        private void dgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //Filtro estado
        private void FiltroPorEstado(string estado)
        {
            try
            {
                using (SqlConnection conexion = ConexionBD.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("listar_productos_por_estado", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@estado", estado);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        dgvProductos.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar productos: " + ex.Message);
            }
        }
        //Cargaremos valores al combobox
        private void CargarEstados()
        {
            cbbEstado.Items.Clear();
            cbbEstado.Items.Add("Todos");
            cbbEstado.Items.Add("Activo");
            cbbEstado.Items.Add("Inactivo");

            cbbEstado.SelectedIndex = 0; // Todos por defecto
        }

        private void cbbEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbEstado.SelectedItem == null) return;

            string estado = cbbEstado.SelectedItem.ToString();

            if (estado == "Todos")
            {
                CargarProductos();
            }
            else
            {
                FiltroPorEstado(estado);
            }
        }
    }
}
