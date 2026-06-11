using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        int contatore = 0;
        int risultato = 0;
        int numero = 0;
        int numero2 = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonAumenta_Click(object sender, EventArgs e)
        {
            contatore++;
            labelContatore.Text = contatore.ToString();
        }

        private void labelContatore_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonDiminuisci_Click(object sender, EventArgs e)
        {
            contatore--;
            labelContatore.Text = contatore.ToString();
        }

        private void labelCont_Click(object sender, EventArgs e)
        {

        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            contatore = 0;
            labelContatore.Text = contatore.ToString();
        }

        private void numerouno_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(numerouno.Text, out numero);

        }

        private void numerodue_TextChanged(object sender, EventArgs e)
        {
            try
            {
                numero2 = Convert.ToInt32(numerodue.Text);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + " - " + ex.StackTrace, "ERRORE!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {


            }

            //int.TryParse(numerodue.Text, out numero2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            risultato = numero + numero2;
            labelrisultato.Text = risultato.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            risultato = numero - numero2;
            labelrisultato.Text = risultato.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            risultato = numero * numero2;
            labelrisultato.Text = risultato.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (numero2 == 0)
            {
                MessageBox.Show("Non puoi dividere per zero");
                return;

            }
            else
            {
                // double risultato = (double)numero / numero2;


                double risultato = Convert.ToDouble(numero / numero2);
                labelrisultato.Text = risultato.ToString();
            }

        }

        private void labelrisultato_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            var f = new FormDatabase();

            f.Show();



        }
    }
}
