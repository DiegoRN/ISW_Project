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
    public partial class SalesLliures : Form
    {
        private IGestDepService service;
        public SalesLliures(IGestDepService service)
        {
            InitializeComponent();
            this.service = service;
            
        }

        public void LoadData()
        {
            try {
                Dictionary<DateTime, int> AvailableRooms = service.GetListAvailableRoomsPerWeek(dateTimePicker1.Value);
            BindingList<object> bindinglist = new BindingList<object>();
            
                if (AvailableRooms != null) {
                    foreach (KeyValuePair<DateTime, int> i in AvailableRooms)
                    {
                        if (i.Key.DayOfWeek == DayOfWeek.Monday)
                        {
                            DateTime nuevo = i.Key;
                            nuevo = nuevo.AddDays(1);
                            AvailableRooms.TryGetValue(nuevo, out int value1);
                            nuevo = nuevo.AddDays(1);
                            AvailableRooms.TryGetValue(nuevo, out int value2);
                            nuevo = nuevo.AddDays(1);
                            AvailableRooms.TryGetValue(nuevo, out int value3);
                            nuevo = nuevo.AddDays(1);
                            AvailableRooms.TryGetValue(nuevo, out int value4);
                            nuevo = nuevo.AddDays(1);
                            AvailableRooms.TryGetValue(nuevo, out int value5);
                            nuevo = nuevo.AddDays(1);
                            AvailableRooms.TryGetValue(nuevo, out int value6);
                            bindinglist.Add(new
                            {
                                ds_horas = nuevo.ToString("HH:mm") + " - " + nuevo.AddMinutes(45).ToString("HH:mm"),
                                ds_dilluns = i.Value,
                                ds_dimarts = value1,
                                ds_dimecres = value2,
                                ds_dijous = value3,
                                ds_divendres = value4,
                                ds_dissabte = value5,
                                ds_diumenge = value6,
                            });
                        }
                    }
                    bindingSource1.DataSource = bindinglist;
                }
            }
            catch (Exception error)
            {
                System.Windows.Forms.MessageBox.Show(error.Message);
            }
        }

        private void SalesLliures_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            LoadData();

        }

        private void button1_Click(object sender, EventArgs e) //Botó  enrere sales lliures
        {
            // var newform = new GestDepApp(service);
            this.Close();
            // newform.Show();

            // No se com fer que en el menu principar al apretar una de les 5 opcions se tanque el menú i no se quede en 2n pla,
            //si li fique el this.close se tanca massa ràpid o algo i se tanca tot el programa
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
