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
    public partial class InstructorActivitat : Form
    {
        private IGestDepService service;
        public InstructorActivitat(IGestDepService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void InstructorActivitat_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) // Confirmar
        {
            try { 
            service.AssignInstructorToActivity(Int32.Parse(textBox1.Text), textBox2.Text);
            this.Close();
            System.Windows.Forms.MessageBox.Show("Monitor assignat correctament a l'activitat");
        }
            catch (Exception error)
            {
                System.Windows.Forms.MessageBox.Show(error.Message);
            }
         }
        private void button2_Click(object sender, EventArgs e) // Cancelar
        {
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
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
