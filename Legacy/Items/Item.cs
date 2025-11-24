using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Items
{
    public class Item(string name)
    {
        public string Name { get; protected set; } = name;
        public string Description { get; protected set; } = string.Empty;
    }
}
