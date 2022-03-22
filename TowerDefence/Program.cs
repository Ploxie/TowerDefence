using GUI;
using System;
using System.Windows.Forms;

namespace TowerDefence
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            GUI.GUI gui = new GUI.GUI();

            Application.Run(gui);
            using (var game = new Game1(gui.StartInfo))
                game.Run();
        }
    }
}
