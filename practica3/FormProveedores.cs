using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace practica3
{
    public partial class FormProveedores : Form
    {
        private List<Proveedor> listaProveedores = new List<Proveedor>();

        public FormProveedores()
        {
            InitializeComponent();
        }

        private void FormProveedores_Load(object sender, EventArgs e)
        {
            dgvProveedores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProveedores.MultiSelect = false;
            dgvProveedores.DataSource = null;
            dgvProveedores.DataSource = listaProveedores;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text) || string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("ID y Nombre son obligatorios.");
                return;
            }

            if (listaProveedores.Any(p => p.Id == Convert.ToInt32(txtId.Text)))
            {
                MessageBox.Show("Ya existe un proveedor con este ID.");
                return;
            }

            Proveedor nuevo = new Proveedor
            {
                Id = Convert.ToInt32(txtId.Text),
                Nombre = txtNombre.Text,
                Telefono = txtTelefono.Text,
                Email = txtEmail.Text,
                Direccion = txtDireccion.Text
            };

            listaProveedores.Add(nuevo);
            RefrescarGrid();
            LimpiarCampos();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (dgvProveedores.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvProveedores.SelectedRows[0].Cells["Id"].Value);
                Proveedor p = listaProveedores.FirstOrDefault(x => x.Id == id);
                if (p != null)
                {
                    p.Nombre = txtNombre.Text;
                    p.Telefono = txtTelefono.Text;
                    p.Email = txtEmail.Text;
                    p.Direccion = txtDireccion.Text;

                    RefrescarGrid();
                    LimpiarCampos();
                    MessageBox.Show("Proveedor actualizado correctamente.");
                }
            }
            else
            {
                MessageBox.Show("Selecciona una fila para actualizar.");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProveedores.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvProveedores.SelectedRows[0].Cells["Id"].Value);
                Proveedor p = listaProveedores.FirstOrDefault(x => x.Id == id);
                if (p != null)
                {
                    listaProveedores.Remove(p);
                    RefrescarGrid();
                    LimpiarCampos();
                    MessageBox.Show("Proveedor eliminado correctamente.");
                }
            }
            else
            {
                MessageBox.Show("Selecciona una fila para eliminar.");
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void dgvProveedores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvProveedores.Rows[e.RowIndex];
                txtId.Text = fila.Cells["Id"].Value.ToString();
                txtNombre.Text = fila.Cells["Nombre"].Value.ToString();
                txtTelefono.Text = fila.Cells["Telefono"].Value.ToString();
                txtEmail.Text = fila.Cells["Email"].Value.ToString();
                txtDireccion.Text = fila.Cells["Direccion"].Value.ToString();
            }
        }

        private void RefrescarGrid()
        {
            dgvProveedores.DataSource = null;
            dgvProveedores.DataSource = listaProveedores;
        }

        private void LimpiarCampos()
        {
            txtId.Clear();
            txtNombre.Clear();
            txtTelefono.Clear();
            txtEmail.Clear();
            txtDireccion.Clear();
        }
    }

    public class Proveedor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
    }
}