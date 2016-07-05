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

namespace HudZelda
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        HudForm f = new HudForm();
        bool init = false;
        private void Form1_Load(object sender, EventArgs e)
        {
            GlobalSettings.editmode = false;
            if (File.Exists("datalayout.zhl"))
            {
                BinaryReader br = new BinaryReader(new FileStream("datalayout.zhl", FileMode.Open, FileAccess.Read));
                GlobalSettings.sizeX = br.ReadInt32();
                GlobalSettings.sizeY = br.ReadInt32();
                GlobalSettings.items_placement = new byte[GlobalSettings.sizeX+4, GlobalSettings.sizeY+6];
                for (int x = 0; x < GlobalSettings.sizeX; x++)
                {
                    for (int y = 0; y < GlobalSettings.sizeY; y++)
                    {
                        GlobalSettings.items_placement[x, y] = br.ReadByte();
                    }
                }
                GlobalSettings.size = br.ReadByte();
                checkBox2.Checked = br.ReadBoolean();
                GlobalSettings.bgrColor = Color.FromArgb(br.ReadByte(), br.ReadByte(), br.ReadByte());
                this.Location = new Point(br.ReadInt32(), br.ReadInt32());

                br.Close();

            }
            else
            {
                GlobalSettings.items_placement = new byte[GlobalSettings.sizeX, GlobalSettings.sizeY];
                for (int x = 0; x < GlobalSettings.sizeX; x++)
                {
                    for (int y = 0; y < GlobalSettings.sizeY; y++)
                    {
                            GlobalSettings.items_placement[x, y] = 254;
                        }
                }
                }

            byte[] defmenu =new byte[42] { 26, 0, 1, 2, 3, 4, 30,
                                             27, 5, 6, 7, 8, 9, 31,
                                             28, 10, 11, 12, 13, 14, 32,
                                             29, 24, 15, 16, 17, 18, 254,
                                             20, 19, 21, 22, 23, 25, 254,
                                             34, 37, 39, 35, 38, 33, 36 };
            //(Bitwise)Bit 0: MM,Bit 1: POD,Bit 2: IP,Bit 3: TR,Bit 4: SP,Bit 5: TT,Bit 6: SW
            int yc = 0;
            int xc = 0;
            for (int i = 0;i<42; i++)
            {
            
                GlobalSettings.items_placement[xc, yc] = defmenu[i];
                xc++;
                if (xc >= 7)
                {
                    yc++;
                    xc = 0;
                }
                
            }

                //Default Menu

        f.Show(this);
            f.Location = new Point(this.Location.X + 8, this.Location.Y + 232);

            GlobalSettings.bgrColor = Color.Black;

            ToolTip yourToolTip = new ToolTip();
            //The below are optional, of course,

            yourToolTip.ToolTipIcon = ToolTipIcon.Info;
            //yourToolTip.IsBalloon = true;
            yourToolTip.ShowAlways = true;

            numericUpDown1.Value = GlobalSettings.sizeX;
            numericUpDown2.Value = GlobalSettings.sizeY;
            numericUpDown3.Value = GlobalSettings.size;

            yourToolTip.SetToolTip(numericUpDown4, "Refreshing Timer for auto-update in seconds 5 is a good value");
            init = true;
            setSetting();
           
        }

        private void Form1_Move(object sender, EventArgs e)
        {
            if (!f.IsDisposed)
            {
                if (GlobalSettings.configartionshow)
                {
                    f.Location = new Point(this.Location.X + 8, this.Location.Y + 232);
                }
                else
                {
                    f.Location = new Point(this.Location.X + 8, this.Location.Y + 52);
                }
            }
            else
            {
                f = new HudForm();
                f.Show(this);
                f.Location = new Point(this.Location.X + 8, this.Location.Y + 232);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (GlobalSettings.configartionshow)
            {
                this.panel1.Visible = false;
                this.Size = new Size(240, 60);
                this.button1.Text = "vvvvvvvvvvvvvvvvv";
                this.button1.Location = new Point(this.button1.Location.X, 0);
                GlobalSettings.configartionshow = false;
            }
            else
            {
                this.panel1.Visible = true;
                this.Size = new Size(240, 240);
                this.button1.Text = "^^^^^^^^^^^^^^^^";
                this.button1.Location = new Point(this.button1.Location.X, 180);
                GlobalSettings.configartionshow = true;
            }
            if (GlobalSettings.configartionshow)
            {
                f.Location = new Point(this.Location.X + 8, this.Location.Y + 232);
            }
            else
            {
                f.Location = new Point(this.Location.X + 8, this.Location.Y + 52);
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            f.Size = new Size((int)numericUpDown1.Value * (16 * (int)numericUpDown3.Value), (int)numericUpDown2.Value * (16 * (int)numericUpDown3.Value));
            setSetting();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            f.Size = new Size((int)numericUpDown1.Value * (16 * (int)numericUpDown3.Value), (int)numericUpDown2.Value * (16 * (int)numericUpDown3.Value));
            setSetting();
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            f.Size = new Size((int)numericUpDown1.Value * (16 * (int)numericUpDown3.Value), (int)numericUpDown2.Value * (16 * (int)numericUpDown3.Value));
            setSetting();
        }

        public void setSetting()
        {
            if (init == true)
            {
                GlobalSettings.sizeX = (int)numericUpDown1.Value;
                GlobalSettings.sizeY = (int)numericUpDown2.Value;
                GlobalSettings.size = (int)numericUpDown3.Value;

                f.updateGraphics();
            }
        }


        private void checkBox2_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox2.Checked == false)
            {
                this.TopMost = false;
                f.TopMost = false;
            }
            else
            {
                this.TopMost = true;
                f.TopMost = true;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            var window = MessageBox.Show("Do you want to save your layout?", "Exiting...", MessageBoxButtons.YesNo);
            if (window == DialogResult.Yes)
            {
                setSetting();
                BinaryWriter bw = new BinaryWriter(new FileStream("datalayout.zhl", FileMode.OpenOrCreate, FileAccess.Write));
                bw.Write((int)GlobalSettings.sizeX);
                bw.Write((int)GlobalSettings.sizeY);
                for(int x = 0; x<GlobalSettings.sizeX;x++)
                {
                    for(int y = 0;y<GlobalSettings.sizeY;y++)
                    {
                        bw.Write((byte)GlobalSettings.items_placement[x,y]);

                    }
                }
                bw.Write((byte)GlobalSettings.size);
                bw.Write((bool)checkBox2.Checked);
                bw.Write((byte)GlobalSettings.bgrColor.R);
                bw.Write((byte)GlobalSettings.bgrColor.G);
                bw.Write((byte)GlobalSettings.bgrColor.B);
                bw.Write((int)this.Location.X);
                bw.Write((int)this.Location.Y);
                bw.Close();
            }//Data: sizeX,sizeY,x:y:item_placement[x,y],size,topmost,r,g,b,wx,wy;
            //if (window == DialogResult.No) e.Cancel = false;
            //else e.Cancel = false;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == false)
            {
                GlobalSettings.editmode = false;
            }
            else
            {
                GlobalSettings.editmode = true;
            }
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            timer1.Interval = (int)(numericUpDown4.Value * 1000);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            timer1.Enabled = checkBox1.Checked;
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            //label5 = info
            //if no save found in > 1E00/2A13 (< V3) 
            //AutoUpdate Code : 
            //Read SRM 
            byte[] data = new byte[255];
            bool supported = false;
            if (selectedFile != "")
            {
                
                FileStream fs = new FileStream(selectedFile, FileMode.Open, FileAccess.Read);
                fs.Position = 0x1E00;
                fs.Read(data, 0, 255);
                fs.Close();
                
                for (int i = 0; i < 10; i++)
                {
                    if (data[i] != 96)
                    {
                        supported = true;
                    }
                }
                if (supported == false)
                {
                    label5.Text = "V3 auto-update only on Save+Quit";
                }
                if (supported == true)
                {
                    label5.Text = "Auto-Update Running";
                }
                //data[0] = items[0]
                //data[15] skipped
                //data[29] = end
                //data[52] = pendants
                //data[58] = crystals


                if (supported == false)
                {
                    //TEST CODE TEST CODE
                    byte[] datax = new byte[255];
                    FileStream fsx = new FileStream(selectedFile, FileMode.Open, FileAccess.Read);
                    fsx.Position = 0x0340;
                    fsx.Read(datax, 0, 255);
                    fsx.Close();
                    //TEST CODE TEST CODE




                    for (int i = 0; i < 30; i++)
                    {
                        int v = i;
                        if (i >= 15)
                            v = i + 1;
                        if (i >= 23)
                            v = v + 1;

                        f.items[i] = datax[v];


                    }
                    //f.items[23] = datax[25];

                    //Pendants : 
                    //Eastern :
                    if ((datax[52] & 4) == 4)
                        f.items[30] = 1;
                    //Desert
                    if ((datax[52] & 2) == 2)
                        f.items[31] = 1;
                    //Hera
                    if ((datax[52] & 1) == 1)
                        f.items[32] = 1;

                    //Crystals : 64,32,16,8,4,2,1
                    //Bit 0: MM,Bit 1: POD,Bit 2: IP,Bit 3: TR,Bit 4: SP,Bit 5: TT,Bit 6: SW
                    if ((datax[58] & 64) == 64)
                        f.items[39] = 1;
                    if ((datax[58] & 32) == 32)
                        f.items[38] = 1;
                    if ((datax[58] & 16) == 16)
                        f.items[37] = 1;
                    if ((datax[58] & 8) == 8)
                        f.items[36] = 1;
                    if ((datax[58] & 4) == 4)
                        f.items[35] = 1;
                    if ((datax[58] & 2) == 2)
                        f.items[34] = 1;
                    if ((datax[58] & 1) == 1)
                        f.items[33] = 1;
                    f.updateGraphics();
                }
                else
                {
                    for (int i = 0; i < 30; i++)
                    {
                        int v = i;
                        if (i >= 15)
                            v = i + 1;
                        if (i >= 23)
                            v = v + 1;

                        f.items[i] = data[v];


                    }
                    //f.items[23] = datax[25];

                    //Pendants : 
                    //Eastern :
                    if ((data[52] & 4) == 4)
                        f.items[30] = 1;
                    //Desert
                    if ((data[52] & 2) == 2)
                        f.items[31] = 1;
                    //Hera
                    if ((data[52] & 1) == 1)
                        f.items[32] = 1;

                    //Crystals : 64,32,16,8,4,2,1
                    //Bit 0: MM,Bit 1: POD,Bit 2: IP,Bit 3: TR,Bit 4: SP,Bit 5: TT,Bit 6: SW
                    if ((data[58] & 64) == 64)
                        f.items[39] = 1;
                    if ((data[58] & 32) == 32)
                        f.items[38] = 1;
                    if ((data[58] & 16) == 16)
                        f.items[37] = 1;
                    if ((data[58] & 8) == 8)
                        f.items[36] = 1;
                    if ((data[58] & 4) == 4)
                        f.items[35] = 1;
                    if ((data[58] & 2) == 2)
                        f.items[34] = 1;
                    if ((data[58] & 1) == 1)
                        f.items[33] = 1;
                    f.updateGraphics();
                }
            }
            else
            {
                label5.Text = "No SRM File Selected!";
            }
        }
        string selectedFile ="" ;
        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName!="")
            {
                selectedFile = openFileDialog1.FileName;
            }
        }
    }



    public static class GlobalSettings
    {
        public static int sizeX = 7;
        public static int sizeY = 6;
        public static int size = 2;
        public static Color bgrColor = Color.Black;
        public static byte[,] items_placement;
        public static bool configartionshow =true;
        public static bool topmost = false;
        public static bool editmode = false;
    }
}
