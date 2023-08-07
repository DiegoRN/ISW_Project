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
using GestDep.Entities;

namespace GestDepGUI
{
    public partial class AfegirActivitat : Form
    {
        private IGestDepService service;
        Days dies = Days.None;
        String descripcio;
        TimeSpan duracio;
        double preu;
        DateTime inici;
        int sala;
        int minenrollment;
        int maxenrollment;
        DateTime datainici;
        DateTime datafinalitzacio;
        public AfegirActivitat(IGestDepService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e) //Confirmar
        {
            try
            {
                if (checkedListBox1.GetSelected(0)) dies = Days.Mon;
                if (checkedListBox1.GetSelected(1))
                {
                    if (dies != Days.None) dies = dies | Days.Tue;
                    else dies = Days.Tue;
                }
                if (checkedListBox1.GetSelected(2))
                {
                    if (dies != Days.None) dies = dies | Days.Wed;
                    else dies = Days.Wed;
                }
                if (checkedListBox1.GetSelected(3))
                {
                    if (dies != Days.None) dies = dies | Days.Thu;
                    else dies = Days.Thu;
                }
                if (checkedListBox1.GetSelected(4))
                {
                    if (dies != Days.None) dies = dies | Days.Fri;
                    else dies = Days.Fri;
                }
                if (checkedListBox1.GetSelected(5))
                {
                    if (dies != Days.None) dies = dies | Days.Sat;
                    else dies = Days.Sat;
                }
                if (checkedListBox1.GetSelected(6))
                {
                    if (dies != Days.None) dies = dies | Days.Sun;
                    else dies = Days.Sun;
                }
                ICollection<int> idrooms = new List<int>();
                idrooms.Add(sala);
                duracio = TimeSpan.Parse(textBox2.Text);
                descripcio = textBox1.Text;
                datafinalitzacio = DateTime.ParseExact(textBox9.Text, "yyyy-MM-dd", null);
                maxenrollment = Int32.Parse(textBox7.Text);
                minenrollment = Int32.Parse(textBox6.Text);
                datainici = DateTime.ParseExact(textBox8.Text, "yyyy-MM-dd", null);
                sala = Int32.Parse(textBox5.Text);
                preu = Double.Parse(textBox3.Text);
                inici = DateTime.ParseExact(textBox4.Text, "HH:mm:ss", null);
                service.AddNewActivity(dies, descripcio, duracio, datafinalitzacio, maxenrollment,
                    minenrollment, preu, datainici, inici, idrooms);
                this.Close();
                System.Windows.Forms.MessageBox.Show("Activitat anyadida correctament");
            } catch(Exception error) {
                System.Windows.Forms.MessageBox.Show(error.Message);
            }
        }
        private void button2_Click(object sender, EventArgs e) // Cancelar
        {
            
            this.Close();
        }
        private void AfegirActivitat_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
