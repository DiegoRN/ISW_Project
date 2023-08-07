using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using GestDep.Services;
using GestDep.Persistence;

namespace GestDepGUI
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            IGestDepService service = new GestDepService(new EntityFrameworkDAL(new GestDepDbContext()));
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GestDepApp(service));
        }
    }
}
