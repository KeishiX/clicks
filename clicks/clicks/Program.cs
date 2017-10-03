using System;
using System.Windows.Forms;
using System.Reflection;

namespace ClicksGame
{
    public partial class ClicksForm1
    {
        [STAThread]
        private static void Main()
        {
            ver = Assembly.GetExecutingAssembly().GetName().Version;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ClicksForm1());
        }
    }
}
