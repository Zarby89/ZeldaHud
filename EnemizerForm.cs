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

namespace Enemizer
{
    public partial class EnemizerForm : Form
    {
        public EnemizerForm()
        {
            InitializeComponent();
        }

        private void EnemizerForm_Load(object sender, EventArgs e)
        {
            //panel1.BackColor = Color.FromArgb(255, 128, 192);
            foreach(string f in Directory.GetFiles("sprites\\"))
            {
                comboBox1.Items.Add(f);
            }
        }
        Random rand = new Random();
        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);
            byte[] rom_data = new byte[fs.Length];
            fs.Read(rom_data, 0, (int)fs.Length);
            fs.Close();
            int seed = 0;
            if (textBox1.Text == "")
            {
                seed = rand.Next();
            }
            else
            {
                seed = int.Parse(textBox1.Text);
            }
            
            Randomization randomize = new Randomization(seed, flags, rom_data, openFileDialog1.FileName,comboBox1.Items[comboBox1.SelectedIndex].ToString(),checkBox1.Checked);
        }
        int flags = 0;
        int[] flags_setter = new int[16] { 0x00, 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80, 0x100, 0x200, 0x400,0x800,0x1000,0x2000,0x4000 };
        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {

        }

        private void checkedListBox1_MouseUp(object sender, MouseEventArgs e)
        {
            flags = 0;
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemCheckState(i) == CheckState.Checked)
                {
                    flags += flags_setter[i + 1];
                }
            }

            textBox2.Text = flags.ToString();
        }

        public string[] description = new string[15]
        {
            "Randomize the sprites inside the\ndungeons / houses / caves",
            "Randomize the sprites on the\noverworld",
            "Randomize the dungeons color\npalettes",
            "Randomize the sprites color\npalettes",
            "Randomize the overworld color\npalettes (can be weird)",
            "Randomize the sprites HP\nup to a max value of -10/+10\nexample a popo can't have\nmore than 11 hp",
            "Randomize the sprites Damage\nGroup sprite could do ganon damage\nor bumper damage(0)",
            "Set all sprites hp to 0",
            "Shuffle All bosses except\nArrghus (every dungeons have\n a unique boss in)",
            "Put Absorbable items like keys,\nfairy,rupees,bombs,arrows in\nthe sprites pool",
            "allow any bosses to spawn anywhere\nin bosses rooms",
            "Set all palettes pitch black\nexcept sprites, remove dark rooms\n",
            "Shuffle all the musics played\nwhen you enter dungeon,overworld,ect...",
            "Allow Custom Bosses\nto replace one of the original boss\nCurrently not working :(",
            "Allow Pots to be shuffled\nbetween same room"
        };
        // "Randomize All bosses, no unique\nbosses every bosses can be anywhere\nyou can have trinexx everywhere\nthis box overwrite shuffle bosses",

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            descriptionLabel.Text = description[checkedListBox1.SelectedIndex];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            flags = 0;
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (i != 7 && i != 10 && i != 13 && i != 11)
                {
                    checkedListBox1.SetItemCheckState(i, CheckState.Checked);
                    flags += flags_setter[i + 1];
                }
            }

            textBox2.Text = flags.ToString();
        }



        byte[] r_data;

        private void button3_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream("zeldapalettetest.sfc", FileMode.Open, FileAccess.Read);
            r_data = new byte[fs.Length];
            fs.Read(r_data, 0, (int)fs.Length);
            fs.Close();

            /* setColor(0x0DD744, Color.Red,2);
             setColor(0x0DD746, Color.Red,4);
             setColor(0x0DD748, Color.Red,6);
             setColor(0x0DD74A, Color.Red,8);
             setColor(0x0DD74C, Color.Red,10);
             */
            //randomize_wall(0);
            //randomize_floors(0);
            // = Color.FromArgb(255, 255, 255);

            byte[] changing = new byte[] //0 = no change, 1 = yes
    {
            0,1,1,1,0,1,1,0,0,1,1,1,0,1,1,
            0,1,1,1,0,1,1,0,0,1,1,1,0,1,1,
            0,1,1,1,0,1,1,0,0,0,0,0,0,0,0,
            0,1,1,1,0,1,1,0,0,1,1,1,0,1,1
    };

            byte[] aux_changing = new byte[] //0 = no change, 1 = yes
            {
        1, 1, 1, 1, 1, 1, 1,0, 1, 1, 1, 0, 1, 1,1, 1, 1, 1, 0, 1, 1,
        1, 1, 1, 1, 0, 1, 1,0, 1, 1, 1, 0, 1, 1,0, 1, 1, 1, 0, 1, 1,
        0, 1, 1, 1, 0, 1, 1,0, 1, 1, 1, 0, 1, 1,0, 1, 1, 1, 0, 1, 1,
        0, 1, 1, 1, 0, 1, 1,0, 1, 1, 1, 0, 1, 1,1, 1, 1, 1, 1, 1, 1,
        0, 1, 1, 1, 1, 1, 1,0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1,
        0, 0, 0, 0, 1, 1, 1,0, 0, 1, 1, 0, 1, 1,0, 1, 1, 1, 1, 1, 1,
        0, 1, 1, 1, 1, 1, 1,0, 0, 1, 1, 1, 1, 1,1, 1, 1, 1, 1, 1, 1,
        1, 1, 1, 1, 1, 1, 1,1, 1, 1, 1, 1, 1, 1,0, 1, 1, 1, 0, 1, 1,
        0, 1, 1, 1, 0, 1, 1,0, 1, 1, 1, 0, 1, 1, 0, 1, 1, 1, 0, 1, 1,
        0, 1, 1, 1, 0, 1, 1,0, 1, 1, 1, 0, 1, 1,1, 1, 1, 1, 0, 1, 1,
        0, 1, 1, 1, 0, 1, 1,0, 1, 1, 1, 0, 1, 1,0, 1, 1, 1, 0, 1, 1,
        0, 1, 1, 1, 0, 1, 1,0, 1, 1, 1, 0, 1, 1,0, 1, 1, 1, 0, 1, 1,
        0, 1, 1, 1, 0, 1, 1,0, 1, 1, 1, 0, 1, 1,0, 1, 1, 1, 0, 1, 1,
        0, 1, 1, 1, 0, 1, 1,0, 1, 1, 1, 0, 1, 1,1, 1, 1, 1, 0, 0, 1,
        0, 1, 1, 1, 0, 1, 1,0, 1, 1, 1, 0, 1, 1,1, 1, 1, 1, 0, 1, 1,
        0, 1, 1, 1, 0, 1, 1,1, 1, 1, 1, 1, 1, 1
            };


             byte[] palette_l = new byte[]
             {
                
             };

            
                int pos = 0x0DD290;
                richTextBox1.AppendText("Color c = Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));\n");
                for (int i =0;i<palette_l.Length;i++)
                {

                    if (palette_l[i] == 0)
                    {
                        pos += 2;
                    }
                    else
                    {
                        richTextBox1.AppendText("c = Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));\n");
                        for (int j = 0; j < palette_l[i]; j++)
                        {
                            richTextBox1.AppendText("setColor(0x" + pos.ToString("X6") + ",c," + ((palette_l[i] * 2) - (j * 2)).ToString() + ");\n");
                            pos += 2;
                        }
                    }
                }

 


            //0x0DD734 * 5 == wall1
            //0x0DD770 * 5 == wall2
            //0x0DD744 * 5 == wall3
            fs = new FileStream("zeldapalettetest.sfc", FileMode.OpenOrCreate, FileAccess.Write);
                        fs.Write(r_data, 0, r_data.Length);
                        fs.Close();
                    }

                    public void setColor(int address, Color col, byte shade = 0)
                    {

                        byte r = col.R;
                        byte g = col.G;
                        byte b = col.B;
                        if ((r / 8) - shade >= 0)
                        {
                            r = (byte)((r / 8) - shade);
                        }
                        else
                        {
                            r = 0;
                        }
                        if ((g / 8) - shade >= 0)
                        {
                            g = (byte)((g / 8) - shade);
                        }
                        else
                        {
                            g = 0;
                        }
                        if ((b / 8) - shade >= 0)
                        {
                            b = (byte)((b / 8) - shade);
                        }
                        else
                        {
                            b = 0;
                        }
                        short s = (short)(((b) << 10) | ((g) << 5) | ((r) << 0));

                        /* byte[] bb = BitConverter.GetBytes(s);
                         colorBytes[c * 2] = bb[0];
                         colorBytes[(c * 2) + 1] = bb[1];*/
            //Console.WriteLine("RED : " + (s & 0x1F));
            //Console.WriteLine("GREEN : " + ((s & 0x3E0)>>5) );
            //Console.WriteLine("BLUE : " + ((s & 0x7C00 )>>10));
            r_data[address] = (byte)(s & 0x00FF);
            r_data[address + 1] = (byte)((s >> 8) & 0x00FF);


        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            int flagsText = 0;
            Int32.TryParse(textBox2.Text, out flagsText);

            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemCheckState(i, CheckState.Unchecked);
                if ((flagsText & flags_setter[i+1]) == flags_setter[i+1])
                {
                    checkedListBox1.SetItemCheckState(i, CheckState.Checked);

                }
            }
            flags = flagsText;
        }
    }
}
