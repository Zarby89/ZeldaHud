using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace zeldaGui
{
    public partial class OptionsForm : Form
    {
        public OptionsForm()
        {
            InitializeComponent();
        }
        public string iconset = @"IconsSets\Defaults";
        private void panel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            colorDialog1.Color = Form1.clearColor;
            colorDialog1.ShowDialog();
            Form1.clearColor = colorDialog1.Color;
            panel1.BackColor = colorDialog1.Color;
        }

        private void OptionsForm_Load(object sender, EventArgs e)
        {
            panel1.BackColor = Form1.clearColor;
            label3.MaximumSize = new Size(250, 200);
            label3.AutoSize = true;
            label3.Text = iconset;
        }
        public string bgr;
        private void button1_Click(object sender, EventArgs e)
        {
            //Path.
            /*folderBrowserDialog1.SelectedPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            if (folderBrowserDialog1.ShowDialog() == DialogResult.Cancel)
            {

            }
            else
            {
                label3.MaximumSize = new Size(250, 200);
                label3.AutoSize = true;

                label3.Text = folderBrowserDialog1.SelectedPath;
                iconset = folderBrowserDialog1.SelectedPath;
            }*/
            TilesetChooserForm tcf = new TilesetChooserForm();
            if (tcf.ShowDialog() == DialogResult.OK)
            {
                iconset = tcf.selectetIconset;
                label3.Text = iconset;
                bgr = tcf.selectedBgr;
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowDialog();
            
        }

        private void fontDialog1_Apply(object sender, EventArgs e)
        {
        }
    }
}
