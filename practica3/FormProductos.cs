using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Globalization;

namespace practica3
{
    public partial class FormProductos : Form
    {
        private List<Producto> listaProductos = new List<Producto>();
        private List<string> listaCategorias = new List<string>()
        {
            "Electrónica",
            "Hogar",
            "Ropa",
            "Alimentos"
        };

        public FormProductos()
        {
            InitializeComponent();
        }

        private void FormProductos_Load(object sender, EventArgs e)
        {
            cmbCategoria.DataSource = listaCategorias;
            cmbCategoria.SelectedIndex = -1;
            ActualizarGrid();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtId.Text) ||
                    string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    string.IsNullOrWhiteSpace(txtPrecio.Text) ||
                    cmbCategoria.SelectedIndex == -1)
                {
                    MessageBox.Show("Completa todos los campos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtId.Text, out int id))
                {
                    MessageBox.Show("El ID debe ser un número válido.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!decimal.TryParse(txtPrecio.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal precio))
                {
                    MessageBox.Show("Verifica que el precio sea un número válido.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (listaProductos.Any(p => p.Id == id))
                {
                    MessageBox.Show("El ID ya existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Producto nuevo = new Producto
                {
                    Id = id,
                    Nombre = txtNombre.Text,
                    Precio = precio,
                    Categoria = cmbCategoria.SelectedItem.ToString()
                };

                listaProductos.Add(nuevo);
                ActualizarGrid();
                LimpiarCampos();

                MessageBox.Show("Producto agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar producto: " + ex.Message);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtId.Text) ||
                    string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    string.IsNullOrWhiteSpace(txtPrecio.Text) ||
                    cmbCategoria.SelectedIndex == -1)
                {
                    MessageBox.Show("Completa todos los campos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtId.Text, out int id))
                {
                    MessageBox.Show("El ID debe ser un número válido.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!decimal.TryParse(txtPrecio.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal precio))
                {
                    MessageBox.Show("Verifica que el precio sea un número válido.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Producto prod = listaProductos.FirstOrDefault(p => p.Id == id);
                if (prod == null)
                {
                    MessageBox.Show("Producto no encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                prod.Nombre = txtNombre.Text;
                prod.Precio = precio;
                prod.Categoria = cmbCategoria.SelectedItem.ToString();

                ActualizarGrid();
                LimpiarCampos();

                MessageBox.Show("Producto actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar producto: " + ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtId.Text, out int id))
                {
                    MessageBox.Show("El ID debe ser un número válido.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Producto prod = listaProductos.FirstOrDefault(p => p.Id == id);
                if (prod == null)
                {
                    MessageBox.Show("Producto no encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var confirm = MessageBox.Show("¿Estás seguro de eliminar este producto?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    listaProductos.Remove(prod);
                    ActualizarGrid();
                    LimpiarCampos();
                    MessageBox.Show("Producto eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar producto: " + ex.Message);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            txtId.Clear();
            txtNombre.Clear();
            txtPrecio.Clear();
            cmbCategoria.SelectedIndex = -1;
        }

        private void ActualizarGrid()
        {
            dgvProductos.DataSource = null;
            dgvProductos.DataSource = listaProductos;
        }

        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvProductos.Rows[e.RowIndex];
                txtId.Text = fila.Cells["Id"].Value.ToString();
                txtNombre.Text = fila.Cells["Nombre"].Value.ToString();
                txtPrecio.Text = fila.Cells["Precio"].Value.ToString();
                cmbCategoria.SelectedItem = fila.Cells["Categoria"].Value.ToString();
            }
        }

        // 🔹 Métodos vacíos para eventos que existen en el Designer
        private void lblld_Click(object sender, EventArgs e) { }
        private void dgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void lblCategoria_Click(object sender, EventArgs e) { }
        private void lblPrecio_Click(object sender, EventArgs e) { }
        private void txtPrecio_TextChanged(object sender, EventArgs e) { }
        private void lblNombre_Click(object sender, EventArgs e) { }
        private void txtNombre_TextChanged(object sender, EventArgs e) { }
        private void cmbCategoria_SelectedIndexChanged(object sender, EventArgs e) { }
        private void txtId_TextChanged(object sender, EventArgs e) { }
    }

    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public string Categoria { get; set; }
    }
}