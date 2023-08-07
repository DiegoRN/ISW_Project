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
    public partial class GestDepApp : Form
    {
        private IGestDepService service;
        public GestDepApp(IGestDepService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void GestDepApp_Load(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e) // inscriure usuari
        {
            
            var newform = new InscriureUsuari(service);
            newform.Show();

        }

        private void button3_Click(object sender, EventArgs e) // Afegir monitor a act
        {
            var newform = new InstructorActivitat(service);
            newform.Show();
        }

        private void button5_Click(object sender, EventArgs e) // Consultar lliures 
        {
            var newform = new SalesLliures(service);
            newform.Show();
        }

        private void button2_Click(object sender, EventArgs e) // Afegir Usuari
        {
            var newform = new AfegirUsuari(service);
            newform.Show();
        }

        private void button1_Click(object sender, EventArgs e) // Afegir Activitat
        {
            var newform = new AfegirActivitat(service);
            newform.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}