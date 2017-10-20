using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using winform_3d_visualizer;

namespace winform_3d_visualizer
{
    public partial class Form1 : Form
    {
        private UcCorrelogramChart panel1;
        public Form1()
        {
            this.panel1 = new UcCorrelogramChart();
            InitializeComponent();

            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(47, 44);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(405, 322);
            this.panel1.TabIndex = 0;

            this.Controls.Add(this.panel1);
        }
    }
}
