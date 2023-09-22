using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace Practica_parcial_2_completa
{
    public partial class Form1 : Form
    {
        List<Comprador> ListaCompradores = new List<Comprador>();

        public delegate decimal DelegadoDescuento(decimal pAporte);

        DelegadoDescuento delegado1; 

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public class Comprador: ICloneable
        {
            private decimal aporteComprador; 

            public event EventHandler ModificacionDeAporte;  
            public string Nombre { get; set; }

            public string Localidad { get; set; }

            public decimal AporteComprador 
            { 
                get { return aporteComprador; }

                set { aporteComprador = value ;if(value > 0) ModificacionDeAporte?.Invoke(this, null); } 
            
            }

            public decimal Comprar(DelegadoDescuento delegadoDescuento)
            {
                return delegadoDescuento(AporteComprador);

            }

            public object Clone()
            {
                return this.MemberwiseClone();
            }





            public class NombreAscendente : IComparer<Comprador>
            {

                public int Compare (Comprador pComprador1, Comprador pComprador2)
                {
                    return string.Compare(pComprador1.Nombre,pComprador2.Nombre);
                }


            }


            public class NombreDescendente : IComparer<Comprador>
            {

                public int Compare(Comprador pComprador1, Comprador pComprador2)
                {
                    return string.Compare(pComprador1.Nombre, pComprador2.Nombre)*-1;
                }
            }


            public class AporteAscendente : IComparer<Comprador>
            {

                public int Compare(Comprador pComprador1, Comprador pComprador2)
                {
                    int resultado = 0;
                    if (pComprador1.AporteComprador > pComprador2.AporteComprador) resultado = 1;
                    if (pComprador1.AporteComprador == pComprador2.AporteComprador) resultado = 0;
                    if (pComprador1.AporteComprador < pComprador2.AporteComprador) resultado = -1;
                    return resultado;
                
                }
            }


            public class AporteDescendente : IComparer<Comprador>
            {

                public int Compare(Comprador pComprador1, Comprador pComprador2)
                {
                    int resultado = 0;
                    if (pComprador1.AporteComprador > pComprador2.AporteComprador) resultado = 1;
                    if (pComprador1.AporteComprador == pComprador2.AporteComprador) resultado = 0;
                    if (pComprador1.AporteComprador < pComprador2.AporteComprador) resultado = -1;
                    return resultado*-1;

                }
            }


        }


        public void ModificacionDeAporteFuncion(object sender, EventArgs e)
        {
            MessageBox.Show("El aporte del comprador ha sido modificado");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Comprador NuevoComprador = new Comprador();
            NuevoComprador.Nombre = Interaction.InputBox("Ingrese el nombre del comprador: ");
            NuevoComprador.Localidad = Interaction.InputBox("Ingrese la localidad: ");
            NuevoComprador.AporteComprador = decimal.Parse(Interaction.InputBox("Ingrese el aporte del comprador: "));

            ListaCompradores.Add(NuevoComprador);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = ListaCompradores;


        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(dataGridView1.Rows.Count > 0)
            { 
                ListaCompradores.Remove(dataGridView1.SelectedRows[0].DataBoundItem as Comprador); 
                dataGridView1.DataSource= null;
                dataGridView1.DataSource= ListaCompradores;

            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(dataGridView1.Rows.Count> 0)
            {


                (dataGridView1.SelectedRows[0].DataBoundItem as Comprador).Nombre = Interaction.InputBox("Ingrese el nuevo nombre: ");
                (dataGridView1.SelectedRows[0].DataBoundItem as Comprador).Localidad = Interaction.InputBox("Ingrese nueva localidad: ");
                (dataGridView1.SelectedRows[0].DataBoundItem as Comprador).ModificacionDeAporte += ModificacionDeAporteFuncion;
                (dataGridView1.SelectedRows[0].DataBoundItem as Comprador).AporteComprador = decimal.Parse(Interaction.InputBox("Ingrese el nuevo aporte: "));
                (dataGridView1.SelectedRows[0].DataBoundItem as Comprador).ModificacionDeAporte -= ModificacionDeAporteFuncion;

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = ListaCompradores;

            }



        }

        private void button4_Click(object sender, EventArgs e)
        {

            Comprador compradorAux = new Comprador();
            compradorAux = dataGridView1.SelectedRows[0].DataBoundItem as Comprador;

            if(radioButton1.Checked)
            {
                delegado1 = (new DescuentoInternacional()).AplicarDescuento;
                decimal totalPagar = compradorAux.Comprar(delegado1);
                MessageBox.Show($"El comprador {compradorAux.Nombre} se le aplico el descuento y debe pagar: {totalPagar}");


            }
            else if(radioButton2.Checked)
            {

                delegado1 = (new DescuentoNacional()).AplicarDescuento;
                decimal totalPagar = compradorAux.Comprar(delegado1);
                MessageBox.Show($"El comprador {compradorAux.Nombre} se le aplico el descuento y debe pagar: {totalPagar}");


            }
            else
            {
                delegado1 = (new DescuentoProvincial()).AplicarDescuento;
                decimal totalPagar = compradorAux.Comprar(delegado1);
                MessageBox.Show($"EL comprador {compradorAux.Nombre} se le aplico el descuento y debe pagar: {totalPagar}");

            }


        }

        private void button5_Click(object sender, EventArgs e)
        {

            if(radioButton4.Checked)
            {
                ListaCompradores.Sort(new Comprador.NombreAscendente());
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = ListaCompradores;

            }
            else
            {
                ListaCompradores.Sort(new Comprador.NombreDescendente());
                dataGridView1.DataSource = null;
                dataGridView1.DataSource= ListaCompradores;


            }



        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

            if(radioButton4.Checked)
            {
                ListaCompradores.Sort(new Comprador.AporteAscendente());
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = ListaCompradores;


            }
            else
            {
                ListaCompradores.Sort(new Comprador.AporteDescendente());
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = ListaCompradores;
            }



        }

        private void button7_Click(object sender, EventArgs e)
        {
            var Consulta = from c in ListaCompradores
                           where c.Localidad == "Berazategui"
                           select c;

            dataGridView2.DataSource = null; 
            dataGridView2.DataSource= Consulta.ToList<Comprador>();



        }

        private void button8_Click(object sender, EventArgs e)
        {
            Comprador comprador1 = dataGridView1.SelectedRows[0].DataBoundItem as Comprador;

            Comprador clon = new Comprador();

            clon = (Comprador)comprador1.Clone();

            textBox1.Text = "Datos del original :" + Environment.NewLine +
                            comprador1.Nombre + Environment.NewLine +
                            comprador1.Localidad + Environment.NewLine +
                            comprador1.AporteComprador + Environment.NewLine + Environment.NewLine +
                            "Datos clon: " + Environment.NewLine +
                            clon.Nombre + Environment.NewLine +
                            clon.Localidad + Environment.NewLine +
                            clon.AporteComprador + Environment.NewLine;

            clon.Nombre = Interaction.InputBox("Ingrese el nombre del clon: ");
            clon.Localidad = Interaction.InputBox("Ingrese la localidad: ");
            clon.AporteComprador = decimal.Parse(Interaction.InputBox("Ingrese el aporte clon: "));

            textBox1.Text = "Datos del original :" + Environment.NewLine +
                comprador1.Nombre + Environment.NewLine +
                comprador1.Localidad + Environment.NewLine +
                comprador1.AporteComprador + Environment.NewLine + Environment.NewLine +
                "Datos clon: " + Environment.NewLine +
                clon.Nombre + Environment.NewLine +
                clon.Localidad + Environment.NewLine +
                clon.AporteComprador + Environment.NewLine;



        }
    }
}
