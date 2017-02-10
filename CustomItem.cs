using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zeldaGui
{
    public class CustomItem
    {
        public byte[] iconsId;
        public byte level;
        public bool on;
        public string name;
        public bool loop = false;
        public bool bottle = false;
        public bool count = false;
        public byte counter = 0;
        public CustomItem(byte[] iconsId, string name, bool loop = false,bool bottle = false,bool count = false)
        {
            this.iconsId = iconsId;
            this.level = 0;
            this.on = false;
            this.name = name;
            this.bottle = bottle;
            this.loop = loop;
            this.count = count;
        }

    }
}
