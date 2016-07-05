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
using System.Drawing.Imaging;

namespace HudZelda
{
    public partial class HudForm : Form
    {
        public HudForm()
        {
            InitializeComponent();
        }
        //byte[,] items_placement = new byte[6,6]; // contains items id in [Read in order from loop x:y:]
        public byte[] items = new byte[40];
        string[] itemsName = new string[40];
        private void HudForm_Load(object sender, EventArgs e)
        {
            this.pictureBox1.Size = this.Size;
            this.pictureBox1.Location = new Point(0, 0);
            //init code for items
            imageList1.Images.AddStrip(new Bitmap("items.png"));
            updateGraphics();
            
            defineitemsname();
            for(int i = 0;i<41;i++)
            {
                if (i == 0)
                {
                    contextMenuStrip1.Items.Add(254.ToString("000") + itemsName[i]);
                }
                else
                {
                    contextMenuStrip1.Items.Add((i-1).ToString("000") + itemsName[i]);
                }
                
            }


                /*GlobalSettings.items_placement = new byte[GlobalSettings.sizeX, GlobalSettings.sizeY];
                for (int i = 0; i < GlobalSettings.sizeX; i++)
                {
                    for (int j = 0; j < GlobalSettings.sizeY; j++)
                    {
                        GlobalSettings.items_placement[i, j] = (byte)(i + (j * GlobalSettings.sizeX));
                        if (GlobalSettings.items_placement[i, j] > 36)
                        {
                            GlobalSettings.items_placement[i, j] = 255;
                        }
                    }
                }*/
            }

        Graphics g;
        private void HudForm_SizeChanged(object sender, EventArgs e)
        {
            this.pictureBox1.Size = this.Size;
        }
        int lsizex = 0;
        int lsizey = 0;
        public void updateGraphics()
        {
            //public static byte[,] items_placement;
        byte[,] items_placementold = new byte[GlobalSettings.sizeX,GlobalSettings.sizeY];
            for (int i = 0; i < GlobalSettings.sizeX; i++)
            {
                for (int j = 0; j < GlobalSettings.sizeY; j++)
                {
                    if (i < GlobalSettings.items_placement.GetLength(0) && j < GlobalSettings.items_placement.GetLength(1))
                    {
                        items_placementold[i, j] = GlobalSettings.items_placement[i, j];
                    }
                    else
                    {
                        items_placementold[i, j] = 254;
                    }
                    
                }
            }
            GlobalSettings.items_placement = new byte[GlobalSettings.sizeX, GlobalSettings.sizeY];
            for (int i = 0; i < GlobalSettings.sizeX; i++)
            {
                for (int j = 0; j < GlobalSettings.sizeY; j++)
                {
                    GlobalSettings.items_placement[i, j] = items_placementold[i, j];
                }
            }


            ColorMatrix colorMatrix = new ColorMatrix(
new float[][]
{
                new float[] {.3f, .3f, .3f, 0, 0},
                new float[] {.59f, .59f, .59f, 0, 0},
                new float[] {.11f, .11f, .11f, 0, 0},
                new float[] {0, 0, 0,.5f, 0},
                new float[] {0, 0, 0, 0, .5f}
});
            pictureBox1.Image = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);
            g = Graphics.FromImage(pictureBox1.Image);
            ImageAttributes attributes = new ImageAttributes();
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            attributes.SetColorMatrix(colorMatrix);

            
            int ii = 0;
            
            //PseudoDraw Code
            for (int x = 0; x < GlobalSettings.sizeX; x++)
            {
                for (int y = 0; y < GlobalSettings.sizeY; y++)
                {
                    if (GlobalSettings.items_placement[x, y] != 254)
                    {
                        int imgid = 0;
                        if (items[GlobalSettings.items_placement[x, y]] == 0)
                        {
                            //Use Color matrix

                            g.DrawImage(imageList1.Images[GlobalSettings.items_placement[x, y]], new Rectangle(x * (GlobalSettings.size * 16), y * (GlobalSettings.size * 16), GlobalSettings.size * 16, GlobalSettings.size * 16), 0, 0, 16, 16, GraphicsUnit.Pixel, attributes);
                            if (GlobalSettings.items_placement[x, y] == 25)//Mail
                            {
                                g.DrawImage(imageList1.Images[25], new Rectangle(x * (GlobalSettings.size * 16), y * (GlobalSettings.size * 16), GlobalSettings.size * 16, GlobalSettings.size * 16), 0, 0, 16, 16, GraphicsUnit.Pixel);
                            }
                        }
                        else
                        {
                            imgid = GlobalSettings.items_placement[x, y];
                            byte itemid = GlobalSettings.items_placement[x, y];
                            if (itemid == 0)//Bow
                            {
                                if (items[itemid] == 2)
                                {
                                    imgid =51;
                                }
                                else if (items[itemid] == 3)
                                {
                                    imgid = 52;
                                }
                            }
                            else if (itemid == 1)//Boomerang
                            {
                                if (items[itemid] == 2)
                                {
                                    imgid = 40;

                                }
                            }
                            else if (itemid == 4)//Powder
                            {
                                if (items[itemid] == 2)
                                {
                                    imgid = 49;

                                }
                            }
                            else if (itemid == 12)//Shovel/Flute
                            {
                                if (items[itemid] == 2)
                                {
                                    imgid = 50;

                                }
                                else if (items[itemid] == 3)
                                {
                                    imgid = 50;

                                }
                            }
                            else if (itemid == 19)//Gloves
                            {
                                if (items[itemid] == 2)
                                {
                                    imgid = 46;

                                }
                            }
                            else if (itemid == 23)//Sword
                            {
                                if (items[itemid] == 2)
                                {
                                    imgid = 41;

                                }
                                else if (items[itemid] == 3)
                                {
                                    imgid = 42;

                                }
                                else if (items[itemid] == 4)
                                {
                                    imgid = 43;

                                }
                            }
                            else if (itemid == 24)//Shield
                            {
                                if (items[itemid] == 2)
                                {
                                    imgid = 47;

                                }
                                else if (items[itemid] == 3)
                                {
                                    imgid = 48;

                                }
                            }
                            else if (itemid == 25)//Mail
                            {
                                if (items[itemid] == 0)
                                {
                                    imgid =25;

                                }
                                if (items[itemid] == 1)
                                {
                                    imgid = 44;

                                }
                                else if (items[itemid] == 2 )
                                {
                                    imgid = 45;

                                }
                            }
                            else if (itemid == 26 | itemid == 27 | itemid == 28 | itemid == 29)//Bottles
                            {
                                if (items[itemid] == 0)//nobottle
                                {
                                    imgid = 26;
                                }
                                if (items[itemid] == 1)//unused
                                {
                                    imgid = 26;
                                }
                                if (items[itemid] == 2)//empty
                                {
                                    imgid = 26;
                                }
                                else if(items[itemid] == 3)//r
                                {
                                    imgid = 53;
                                }
                                else if (items[itemid] == 4)//g
                                {
                                    imgid = 54;
                                }
                                else if (items[itemid] == 5)//b
                                {
                                    imgid = 55;
                                }
                                else if (items[itemid] == 6)//fairy
                                {
                                    imgid = 56;
                                }
                                else if (items[itemid] == 7)//bee
                                {
                                    imgid = 57;
                                }
                                else if (items[itemid] == 8)//good bee
                                {
                                    imgid = 57;
                                }
                            }
                            /*else if (items[itemid] < 1)
                            {
                                if (items[itemid] < 8)
                                {
                                    items[GlobalSettings.items_placement[msx, msy]] += 1;
                                }
                            }*/
                            g.DrawImage(imageList1.Images[imgid], new Rectangle(x * (GlobalSettings.size * 16), y * (GlobalSettings.size * 16), GlobalSettings.size * 16, GlobalSettings.size * 16), 0, 0, 16, 16, GraphicsUnit.Pixel);
                        }
                    }
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

            
        }
        int msx, msy;
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            msx = e.X / (16 * GlobalSettings.size);
            msy = e.Y / (16 * GlobalSettings.size);

                if (e.Button == MouseButtons.Right)
                {
                if (GlobalSettings.editmode == true)
                {
                    if (GlobalSettings.configartionshow)
                    {

                            contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
                            msx = e.X / (16 * GlobalSettings.size);
                            msy = e.Y / (16 * GlobalSettings.size);
                        }
                    }
                if (GlobalSettings.configartionshow != true)
                {
                    if (GlobalSettings.items_placement[msx, msy] != 254)
                    {
                        msx = e.X / (16 * GlobalSettings.size);
                        msy = e.Y / (16 * GlobalSettings.size);
                        if (items[GlobalSettings.items_placement[msx, msy]] > 0)
                        {
                            items[GlobalSettings.items_placement[msx, msy]] -= 1;
                        }
                        updateGraphics();
                    }
                }
                else
                {
                    if (GlobalSettings.editmode == false)
                    {
                        if (GlobalSettings.items_placement[msx, msy] != 254)
                        {
                            msx = e.X / (16 * GlobalSettings.size);
                            msy = e.Y / (16 * GlobalSettings.size);
                            if (items[GlobalSettings.items_placement[msx, msy]] > 0)
                            {
                                items[GlobalSettings.items_placement[msx, msy]] -= 1;
                            }
                            updateGraphics();
                        }
                    }
                }
                }
            if (GlobalSettings.items_placement[msx, msy] != 254)
            {
                if (e.Button == MouseButtons.Left)
                {
                    msx = e.X / (16 * GlobalSettings.size);
                    msy = e.Y / (16 * GlobalSettings.size);
                    byte itemid = GlobalSettings.items_placement[msx, msy];
                    if (itemid == 0)//Bow
                    {
                        if (items[itemid] < 3)
                        {
                            items[GlobalSettings.items_placement[msx, msy]] += 1;
                        }
                    }
                    else if (itemid == 1)//Boomerang
                    {
                        if (items[itemid] < 2)
                        {
                            items[GlobalSettings.items_placement[msx, msy]] += 1;
                        }
                    }
                    else if (itemid == 4)//Mush
                    {
                        if (items[itemid] < 2)
                        {
                            items[GlobalSettings.items_placement[msx, msy]] += 1;
                        }
                    }
                    else if (itemid == 12)//Shovel
                    {
                        if (items[itemid] < 3)
                        {
                            items[GlobalSettings.items_placement[msx, msy]] += 1;
                        }
                    }
                    else if (itemid == 19)//Gloves
                    {
                        if (items[itemid] < 2)
                        {
                            items[GlobalSettings.items_placement[msx, msy]] += 1;
                        }
                    }
                    else if (itemid == 23)//Sword
                    {
                        if (items[itemid] < 4)
                        {
                            items[GlobalSettings.items_placement[msx, msy]] += 1;
                        }
                    }
                    else if (itemid == 24)//Shield
                    {
                        if (items[itemid] < 3)
                        {
                            items[GlobalSettings.items_placement[msx, msy]] += 1;
                        }
                    }
                    else if (itemid == 25)//Mail
                    {
                        if (items[itemid] < 3)
                        {
                            items[GlobalSettings.items_placement[msx, msy]] += 1;
                        }
                    }
                    else if (itemid == 26)//Bottle1
                    {
                        if (items[itemid] < 7)
                        {
                            items[GlobalSettings.items_placement[msx, msy]] += 1;
                        }
                    }
                    else if (itemid == 27)//Bottle2
                    {
                        if (items[itemid] < 7)
                        {
                            items[GlobalSettings.items_placement[msx, msy]] += 1;
                        }
                    }
                    else if (itemid == 28)//Bottle3
                    {
                        if (items[itemid] < 7)
                        {
                            items[GlobalSettings.items_placement[msx, msy]] += 1;
                        }
                    }
                    else if (itemid == 29)//Bottle4
                    {
                        if (items[itemid] < 7)
                        {
                            items[GlobalSettings.items_placement[msx, msy]] += 1;
                        }
                    }
                    else if (items[itemid] < 1)
                    {
                        if (items[itemid] < 8)
                        {
                            items[GlobalSettings.items_placement[msx, msy]] += 1;
                        }
                    }
                    updateGraphics();

                }
            }
        }

        public void defineitemsname()
        {
            itemsName = new string[41]{"Empty","Bow","Boomerang","Hookshot","Bombs","Mushroom","Fire Rod","Ice Rod","Bombos Medaillon",
                "Ether Medaillon","Quake Medaillon","Lamp","Hammer","Shovel","Bug Catching Net","Book of Mudora","Cane of Somaria",
                "Cane of Byrna","Magic Cape","Magic Mirror","Gloves","Boots","Flippers","Moonpearl","Sword","Shield","Mail",
                "Bottle1", "Bottle2","Bottle3","Bottle4","Pendant Eastern","Pendant Desert","Pendant Hera","Crystal6 MM","Crystal1 Pod",
                "Crystal5 IP","Crystal7 TR","Crystal2 SP", "Crystal4 TT","Crystal3 SW" };
        }

        private void contextMenuStrip1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }
        int a;
        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //label1.Text = "x" + msx.ToString() + ",y" + msy.ToString();
            
           GlobalSettings.items_placement[msx, msy] =  (byte)Convert.ToInt32(e.ClickedItem.Text.Substring(0,3));
           updateGraphics();
        }
    }


    //Items List in order : 
    //[0]Bow - 1 = bow without arrow, 2 = with arrow, 3 = silver arrow
    //[1]Boomerang - 1 = Blue Boomerang, 2 = Red Boomerang
    //[2]Hookshot
    //[3]Bombs - Numbers of bombs
    //[4]Mushroom - 1 = Mushroom, 2 = Powder
    //[5]Fire Rod
    //[6]Ice Rod
    //[7]Bombos Medaillon
    //[8]Ether Medaillon
    //[9]Quake Medaillon
    //[10]Lamp
    //[11]Hammer
    //[12]Shovel - 1 = Shovel, 2 = Flute, 3 = Active Flute
    //[13]Bug Catching Net
    //[14]Book of Mudora
    //[15]Cane of Somaria
    //[16]Cane of Byrna
    //[17]Magic Cape
    //[18]Magic Mirror
    //[19]Gloves - 1 = Power Glove, 2 = Titans Mitts
    //[20]Boots
    //[21]Flippers
    //[22]Moonpearl
    //[23]Sword - 1 = Fighter Sword, 2 = Master Sword, 3 = Tempered Sword, 4 = Gold Sword
    //[24]Shield - 1 = Fighter Shield, 2 = Red Shield, 3 = Mirror Shield
    //[25]Mail - 0 = Green Mail, 1 = Blue Mail, 2 = Red Mail
    //[26]Bottle1 - 1(unused), 2 = Empty Bottle, 3 = Red Potion, 4 = Green Potion, 5 = Blue Potion, 6 = Fairy, 7 = Bee, 8 = Good Bee
    //[27]Bottle2
    //[28]Bottle3
    //[29]Bottle4
    //[30-32]Pendants (Bitwise)Bit 0: Courage,Bit 1: Wisdom,Bit 2: Power
    //[33-39]Crystals (Bitwise)Bit 0: MM,Bit 1: POD,Bit 2: IP,Bit 3: TR,Bit 4: SP,Bit 5: TT,Bit 6: SW
    //[]

}
