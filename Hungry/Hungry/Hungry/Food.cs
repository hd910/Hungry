using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hungry
{
    class Food
    {
        public string description { get; set; }
        public string uri { get; set; }

        public Food(string description, string uri)
        {
            this.description = description;
            this.uri = uri;
        }

        
    }
}
