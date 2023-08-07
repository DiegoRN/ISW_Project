using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GestDep.Services;

namespace GestDepGUI
{
    public partial class AfegirUsuari : Form
    {

        private IGestDepService service;
        String direccio;
        String iban;
        String dni;
        String nom;
        int codipostal;
        DateTime naixement;
        bool retirat;
        public AfegirUsuari(IGestDepService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void AfegirUsuari_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e) // Confirmar
        {
            try { 
            nom = textBox7.Text;
            dni = textBox8.Text;
            naixement = dateTimePicker1.Value;
            direccio = textBox9.Text;
            codipostal = Int32.Parse(textBox10.Text);
            iban = textBox11.Text;
            retirat = checkBox2.Checked;
            this.service.AddNewUser(direccio, iban, dni, nom, codipostal, naixement, retirat);
            this.Close();
            System.Windows.Forms.MessageBox.Show("Usuari anyadit correctament");
            }
            catch (Exception error)
            {
                System.Windows.Forms.MessageBox.Show(error.Message);
            }
            
        }
        private void button1_Click(object sender, EventArgs e) // Cancelar
        {
            this.Close();
        }

        private void textNom_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textDNI_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void dateNaixement(object sender, EventArgs e)
        {
            

        }

        private void textDirecció_TextChanged(object sender, EventArgs e)
        {
            

        }


        private void textCodiPostal_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textIBAN_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBoxRetirat(object sender, EventArgs e)
        {
            

        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // If you want, you can allow decimal (float) numbers
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        
    }
}
