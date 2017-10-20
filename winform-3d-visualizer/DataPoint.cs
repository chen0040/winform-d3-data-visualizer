using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictiveModels.Tools.Charting
{
    public class DataPoint
    {
        public double Value { get; set; }
        public string Category { get; set; }

        public DataPoint(string cat, double val)
        {
            Category = cat;
            Value = val;
        }

        public DataPoint()
        {

        }
    }
}
