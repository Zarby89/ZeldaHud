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
    public partial class TilesetChooserForm : Form
    {
        public TilesetChooserForm()
        {
            InitializeComponent();
        }

        private void TilesetChooserForm_Load(object sender, EventArgs e)
        {
            var files = Directory.EnumerateDirectories("IconsSets\\");
            
            foreach (string folder in files)
            {
                listBox1.Items.Add(folder);
            }

            files = Directory.EnumerateFiles("Backgrounds\\");
            listBox2.Items.Add("None");
            foreach (string file in files)
            {
                listBox2.Items.Add(file);
            }
            listBox1.SelectedIndex = 0;
            listBox2.SelectedIndex = 0;

        }
        Bitmap[] iconSet;
        public void loadIconsSet(string data)
        {
            iconSet = new Bitmap[25];
            for (int i = 0; i < 25; i++)
            {
                if (File.Exists(data + "\\" + i.ToString("D4") + ".png"))
                {
                    iconSet[i] = new Bitmap(data + "\\" + i.ToString("D4") + ".png");
                    iconSet[i].MakeTransparent(Color.Fuchsia);
                }
            }
            //GC.Collect();
            drawIcons();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                loadIconsSet((string)listBox1.Items[listBox1.SelectedIndex]);
            }
        }



        public void drawIcons()
        {
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            Graphics g = Graphics.FromImage(pictureBox1.Image);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.Clear(pictureBox1.BackColor);
            int icon = 0;
            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    if (bgr != null)
                    {
                        g.DrawImage(bgr, new Rectangle(x * 32, y * 32, 32, 32), 0, 0, 32, 32, GraphicsUnit.Pixel);
                    }
                    if (iconSet != null)
                    {
                        g.DrawImage(iconSet[icon], new Rectangle(x * 32, y * 32, 32, 32), 0, 0, 32, 32, GraphicsUnit.Pixel);
                    }
                        icon++;
                }
            }
            pictureBox1.Refresh();
        }
        public string selectetIconset = "";
        public string selectedBgr = "";
        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                selectetIconset = (string)listBox1.Items[listBox1.SelectedIndex];
            }
            if (listBox2.SelectedIndex != -1)
            {
                selectedBgr = (string)listBox2.Items[listBox2.SelectedIndex];
            }
        }
        Bitmap bgr;
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex <= 0)
            {
                bgr = null;
            }
            else
            {
                bgr = new Bitmap((string)listBox2.Items[listBox2.SelectedIndex]);
                bgr.MakeTransparent(Color.Fuchsia);
                drawIcons();
            }
        }
    }

}
