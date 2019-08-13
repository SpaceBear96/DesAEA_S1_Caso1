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
using System.Configuration;

namespace Semana01_caso1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["BaseDeDatos"].ConnectionString);

        public void ListaProductos() {
            using (SqlDataAdapter df = new SqlDataAdapter("usp_listaclientes_neptuno", cn)) {
                df.SelectCommand.CommandType = CommandType.StoredProcedure;
                using (DataSet Da = new DataSet())
                {
                    df.Fill(Da, "Clientes");
                    dgCliente.DataSource = Da.Tables["Clientes"];
                    lblTotal.Text = Da.Tables["Clientes"].Rows.Count.ToString();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ListaProductos();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (txtFiltro.Text != "")
            {
                string var = String.Concat("select idCliente,NombreContacto,Direccion,Pais,Telefono from clientes where idCliente like '", txtFiltro.Text,"%';");
                using (SqlDataAdapter df = new SqlDataAdapter(var, cn))
                {
                    using (DataSet Da = new DataSet())
                    {
                        df.Fill(Da, "clientes");
                        dgCliente.DataSource = Da.Tables["clientes"];
                        lblTotal.Text = Da.Tables["clientes"].Rows.Count.ToString();
                    }
                }
            }
        }
    }
}
