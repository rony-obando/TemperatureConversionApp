using Infraestructure.Temperature;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TemperatureConversionApp.Formularios
{
    public partial class FormTemperature : Form
    {
        public ConversionModel conversion;
        public FormTemperature()
        {
            conversion = new ConversionModel();
            InitializeComponent();
        }

        private void FormTemperature_Load(object sender, EventArgs e)
        {
            label1.Visible = false;
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            FrmRegistrar form = new FrmRegistrar(conversion,0);
            form.ShowDialog();
            label1.Visible=false;
            rtbView.Text = conversion.MostrarU();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            label1.Visible = true;
            rtbView.Text = conversion.Mostrar();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            label1.Visible = true;
            FrmRegistrar form = new FrmRegistrar(conversion, 1);
            form.ShowDialog();
            label1.Visible = false;
        }
    }
}
