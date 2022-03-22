using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class GUI : Form
    {

        private StartInfo startInfo;

        public GUI()
        {
            InitializeComponent();
            startInfo = new StartInfo();
        }

        public StartInfo StartInfo => startInfo;

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void startGameButton_Click(object sender, EventArgs e)
        {
            startInfo.StartEditor = false;
            this.Close();
        }

        private void editorButton_Click(object sender, EventArgs e)
        {
            startInfo.StartEditor = true;
            this.Close();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(1);
        }
    }
}
