using Domain.Entities;
using Domain.Enums;
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
    public partial class FrmRegistrar : Form
    {
        public ConversionModel conversion;
        int b = 0;
        public FrmRegistrar(ConversionModel c,int a)
        {
            conversion = c;
            b = a;
            InitializeComponent();
        }

        private void FrmRegistrar_Load(object sender, EventArgs e)
        {
            if (b==0)
            {
                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                cmbCovertir.Visible = true;
                cmbEspecifico.Visible = true;
                lblDelete.Visible = false;
                nudGrado.Minimum = -1000000;
                nudGrado.Increment = 10;
                nudGrado.DecimalPlaces = 2;
                btnOk.Text = "Convertir";
            }
            else
            {
                label1.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                cmbCovertir.Visible = false;
                cmbEspecifico.Visible = false;
                lblDelete.Visible = true;
                nudGrado.Minimum = 0;
                nudGrado.Increment = 1;
                nudGrado.DecimalPlaces = 0;
                btnOk.Text = "Borrar";
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (b == 0)
                {
                    conversion.Verificar(cmbCovertir.Text, cmbEspecifico.Text);
                    Temperaturas t = new Temperaturas
                    {
                        Dato = (double)nudGrado.Value,
                        pos = conversion.Getpos(),
                        tipounidad = (TipoUnidad)cmbCovertir.SelectedIndex,
                    };
                    conversion.Convertir(t, (TipoUnidad)cmbEspecifico.SelectedIndex);
                    Close();
                }
                else
                {
                    Temperaturas t = new Temperaturas
                    {
                        pos = (int)nudGrado.Value,
                    };
                    conversion.Delete(t);
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
