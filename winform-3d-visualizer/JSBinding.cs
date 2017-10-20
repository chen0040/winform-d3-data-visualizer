using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winform_3d_visualizer
{
    public class JSBinding
    {
        public string temp = "";

        public string getTemp()
        {
            return temp;
        }

        public void setTemp(string value)
        {
            this.temp = value;
        }
    }
}
