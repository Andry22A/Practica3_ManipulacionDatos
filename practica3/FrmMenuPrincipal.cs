using System;
using System.Windows.Forms;

namespace practica3
{
    public partial class FrmMenuPrincipal : Form
    {
        public FrmMenuPrincipal()
        {
            InitializeComponent();
        }

        // Botón para abrir el formulario de Categorías
        private void btnCategorias_Click(object sender, EventArgs e)
        {
            FormCategorias frm = new FormCategorias();
            frm.Show(); // Muestra el formulario
        }

        // Botón para abrir el formulario de Proveedores
        private void btnProveedores_Click(object sender, EventArgs e)
        {
            FormProveedores frm = new FormProveedores();
            frm.Show();
        }

        // Botón para abrir el formulario de Productos
        private void btnProductos_Click(object sender, EventArgs e)
        {
            FormProductos frm = new FormProductos();
            frm.Show();
        }

        // Botón para abrir el formulario de Clientes
        private void btnClientes_Click(object sender, EventArgs e)
        {
            FormClientes frm = new FormClientes();
            frm.Show();
        }
    }
}
