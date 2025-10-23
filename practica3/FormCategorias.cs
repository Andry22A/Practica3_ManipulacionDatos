using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace practica3
{
    public partial class FormCategorias : Form
    {
        // Lista de categorías en memoria
        private List<Categoria> listaCategorias = new List<Categoria>();

        public FormCategorias()
        {
            InitializeComponent();
        }

        private void FormCategorias_Load(object sender, EventArgs e)
        {
            ActualizarGrid();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtld.Text) || string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Debe completar todos los campos.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (listaCategorias.Any(c => c.Id == txtld.Text))
            {
                MessageBox.Show("Ya existe una categoría con ese ID.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            listaCategorias.Add(new Categoria
            {
                Id = txtld.Text,
                Nombre = txtNombre.Text
            });

            LimpiarCampos();
            ActualizarGrid();
            MessageBox.Show("Categoría agregada correctamente.");
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            var cat = listaCategorias.FirstOrDefault(c => c.Id == txtld.Text);
            if (cat == null)
            {
                MessageBox.Show("No se encontró una categoría con ese ID.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cat.Nombre = txtNombre.Text;
            ActualizarGrid();
            MessageBox.Show("Categoría actualizada correctamente.");
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            var cat = listaCategorias.FirstOrDefault(c => c.Id == txtld.Text);
            if (cat == null)
            {
                MessageBox.Show("No se encontró una categoría con ese ID.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            listaCategorias.Remove(cat);
            LimpiarCampos();
            ActualizarGrid();
            MessageBox.Show("Categoría eliminada correctamente.");
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            txtld.Clear();
            txtNombre.Clear();
            txtld.Focus();
        }

        private void ActualizarGrid()
        {
            dgvCategorias.DataSource = null;
            dgvCategorias.DataSource = listaCategorias;
        }

        private void txtld_TextChanged(object sender, EventArgs e)
        {
            // No se necesita lógica aquí por ahora
        }
    }

    public class Categoria
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
    }
}