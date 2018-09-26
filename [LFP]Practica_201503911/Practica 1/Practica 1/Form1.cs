using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practica_1
{
    public partial class VP502 : Form
    {
        //Application.EnableVisualStyles;
        NuevaPestana instancia;
        int contador = 1;
        public VP502()
        {
            InitializeComponent();
        }

        private void ayudaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void nuevaPestañaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            instancia = new NuevaPestana(tabControl1, contador);
            contador++;
            //label1.Text = "Estado: Texto sin Analizar";
            //label1.BackColor = Color.SkyBlue;
        }

        private void cargarArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                instancia.Abrir();
            }

            catch (NullReferenceException)
            {
                MessageBox.Show($"No existe area de trabajo, porfavor crea una nueva pestaña. :v", "Error",
            MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void guardarArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                instancia.Guardar();
            }

            catch (NullReferenceException)
            {
                MessageBox.Show($"No existe Documento que guardar :v", "Error",
            MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void analizarLexicamenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                instancia.ExtraerTexto();

            MessageBox.Show($"El Texto ha sido analizado con exito vea los resultados en archivos de salida", "ANALIZADOR LEXICO",
           MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                label1.BackColor = Color.Pink;
                label1.Text = "Estado: Texto Analizado";
            }

            catch (NullReferenceException)
            {
                MessageBox.Show($"No se ha analizado ningun archivo :v", "Error",
            MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void generaraVallaPublicitariaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                instancia.pintarllamado();
                button1.Visible = true;
                button2.Visible = true;


            }

            catch (System.NullReferenceException obj)
            {
                MessageBox.Show($"No se ha creado algun archivo para la valla publicitaria :v", "Error",
            MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void archivosDeSalidaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                instancia.htmlllamado();
                MessageBox.Show($"Se crearon los archivos html en el escritorio que contienen los resultados..", "RESULTADOS",
               MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (NullReferenceException)
            {
                MessageBox.Show($"No existe archivo que analizado para los resultados :v", "Error",
            MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void manualTecnicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                
                Process.Start("ManualTecnico.pdf");
            }
            catch (System.ComponentModel.Win32Exception)
            {
                MessageBox.Show($"Error al brir el archivo :v", "Error",
           MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"JUAN PABLO OSUNA DE LEON {Environment.NewLine}201503911{Environment.NewLine}LENGUAJES FORMALES Y DE PROGRAMACION " + "  A-", "DATOS DEL PROGRAMADOR",
           MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void manualDeUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(@"ManualUsuario.pdf");
            }
            catch (System.ComponentModel.Win32Exception)
            {
                MessageBox.Show($"Error al brir el archivo :v", "Error",
           MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            instancia.exportarimagen();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            instancia.limpiarvalla();
        }
    }
}
