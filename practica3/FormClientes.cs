using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace practica3
{
    public partial class FormClientes : Form
    {
        private List<Cliente> listaClientes = new List<Cliente>();

        public FormClientes()
        {
            InitializeComponent();
        }

        private void FormClientes_Load(object sender, EventArgs e)
        {
            cmbTipoCliente.Items.Add("Regular");
            cmbTipoCliente.Items.Add("VIP");
            cmbTipoCliente.Items.Add("Nuevo");

            dgvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClientes.MultiSelect = false;
            dgvClientes.DataSource = null;
            dgvClientes.DataSource = listaClientes;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text) || string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("ID y Nombre son obligatorios.");
                return;
            }

            if (listaClientes.Any(c => c.Id == Convert.ToInt32(txtId.Text)))
            {
                MessageBox.Show("Ya existe un cliente con este ID.");
                return;
            }

            Cliente nuevo = new Cliente
            {
                Id = Convert.ToInt32(txtId.Text),
                Nombre = txtNombre.Text,
                Telefono = txtTelefono.Text,
                Email = txtEmail.Text,
                Tipo = cmbTipoCliente.Text
            };

            listaClientes.Add(nuevo);
            RefrescarGrid();
            LimpiarCampos();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvClientes.SelectedRows[0].Cells["Id"].Value);
                Cliente c = listaClientes.FirstOrDefault(x => x.Id == id);
                if (c != null)
                {
                    c.Nombre = txtNombre.Text;
                    c.Telefono = txtTelefono.Text;
                    c.Email = txtEmail.Text;
                    c.Tipo = cmbTipoCliente.Text;

                    RefrescarGrid();
                    LimpiarCampos();
                    MessageBox.Show("Cliente actualizado correctamente.");
                }
            }
            else
            {
                MessageBox.Show("Selecciona una fila para actualizar.");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvClientes.SelectedRows[0].Cells["Id"].Value);
                Cliente c = listaClientes.FirstOrDefault(x => x.Id == id);
                if (c != null)
                {
                    listaClientes.Remove(c);
                    RefrescarGrid();
                    LimpiarCampos();
                    MessageBox.Show("Cliente eliminado correctamente.");
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

        private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvClientes.Rows[e.RowIndex];
                txtId.Text = fila.Cells["Id"].Value.ToString();
                txtNombre.Text = fila.Cells["Nombre"].Value.ToString();
                txtTelefono.Text = fila.Cells["Telefono"].Value.ToString();
                txtEmail.Text = fila.Cells["Email"].Value.ToString();
                cmbTipoCliente.Text = fila.Cells["Tipo"].Value.ToString();
            }
        }

        private void RefrescarGrid()
        {
            dgvClientes.DataSource = null;
            dgvClientes.DataSource = listaClientes;
        }

        private void LimpiarCampos()
        {
            txtId.Clear();
            txtNombre.Clear();
            txtTelefono.Clear();
            txtEmail.Clear();
            cmbTipoCliente.SelectedIndex = -1;
        }
    }

    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Tipo { get; set; }
    }
}