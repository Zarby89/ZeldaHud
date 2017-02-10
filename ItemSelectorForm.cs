using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace zeldaGui
{
    public partial class ItemSelectorForm : Form
    {
        public ItemSelectorForm()
        {
            InitializeComponent();
        }
        public int selectedItem = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count > 0)
            {
                selectedItem = listView1.SelectedIndices[0];
            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ItemSelectorForm_Load(object sender, EventArgs e)
        {
            imageList1.Images.AddRange(Form1.iconSet);
            listView1.LargeImageList = imageList1;
            for (int i = 0; i < Form1.itemsList.Count; i++)
            {
                listView1.Items.Add(Form1.itemsList[i].name, Form1.itemsList[i].iconsId[0]);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            selectedItem = 255;
            this.Close();
        }
    }
}
