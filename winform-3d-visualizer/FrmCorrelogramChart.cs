using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using winform_3d_visualizer;

namespace winform_3d_visualizer
{
    public partial class FrmCorrelogramChart : Form
    {
        private UcBrowser _chart;
        private double[][] _correlogram;
        private string[] _labels;

        public FrmCorrelogramChart()
        {
            this._chart = new UcBrowser();
            InitializeComponent();

            this.SuspendLayout();
            _chart = new UcBrowser();
            _chart.Dock = DockStyle.Fill;
            this.Controls.Add(_chart);
            this.ResumeLayout();

            _correlogram = new double[2][];
            _correlogram[0] = new double[2] { 0.5, -0.5 };
            _correlogram[1] = new double[2] { -0.5, 0.5 };

            _labels = new string[2] { "x1", "x2" };
        }

        private void FrmCorrelogramChart_Load(object sender, EventArgs e)
        {
            _chart.IsBrowserInitializedChanged += (sender2, e2) =>
            {
                if (e2.IsBrowserInitialized)
                {
                    PostDelay(() =>
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("<html>");

                        sb.AppendLine("<head>");

                        sb.AppendLine("<style>");
                        sb.AppendLine(Properties.Resources.correxplorer_css);
                        sb.AppendLine("</style>");

                        sb.AppendLine("<script>");
                        sb.AppendLine(Properties.Resources.d3_v3_min_js);
                        sb.AppendLine("</script>");

                        sb.AppendLine("<script>");
                        sb.AppendLine(Properties.Resources.correxplorer_js);
                        sb.AppendLine("</script>");

                        sb.AppendLine("<script>");
                        sb.AppendLine("window.onload=function()");
                        sb.AppendLine("{");
                        sb.AppendLine("d3.select(\"svg\").remove();");
                        sb.AppendLine("var js_table = [");

                        for (int i = 0; i < _correlogram.Length; ++i)
                        {
                            if (i != 0)
                            {
                                sb.Append(",");
                            }
                            sb.Append("[");
                            double[] row = _correlogram[i];
                            for (int j = 0; j < row.Length; ++j)
                            {
                                if (j != 0)
                                {
                                    sb.Append(",");
                                }
                                sb.AppendFormat("{0}", row[j]);
                            }
                            sb.Append("]");
                        }

                        sb.AppendLine("];");
                        sb.AppendLine("var js_labels = [");

                        for (int i = 0; i < _labels.Length; ++i)
                        {
                            if (i != 0)
                            {
                                sb.Append(",");
                            }
                            sb.AppendFormat("\"{0}\"", _labels[i]);
                        }

                        sb.AppendLine("];");
                        sb.AppendLine("main(js_table, js_labels, js_labels);");
                        sb.AppendLine("}");

                        sb.AppendLine("</script>");

                        sb.AppendLine("</head>");

                        sb.AppendLine("<body>");


                        sb.AppendLine("<p>");


                        sb.AppendLine("<input type = \"checkbox\" id = \"transpose\" />");

                        sb.AppendLine("Sort by:");
                        sb.AppendLine("<select id = \"sort_func\">");
                        sb.AppendLine("<option value = \"abs_value\"> by Absolute Value</ option>");
                        sb.AppendLine("<option value = \"value\" selected = \"selected\"> by Value</ option>");
                        sb.AppendLine("<option value = \"similarity\"> by Similarity</ option>");
                        sb.AppendLine("<option value = \"alphabetic\"> Alphabetic</ option>");
                        sb.AppendLine("<option value = \"original\"> Original</ option>");
                        sb.AppendLine("</ select>");

                        sb.AppendLine("Keep symmetry: ");
                        sb.AppendLine("<input type = 'checkbox' id = \"keep_symmetry\" checked= \"true\">");
                        sb.AppendLine("Zoom: ");
                        sb.AppendLine("<input type = 'text' id = \"zoom\" value = \"1.\">");
                        sb.AppendLine("</ p>");
                        sb.AppendLine("<div class=\"tooltip\" style=\"opacity: 0.01\"></div>");

                        sb.AppendLine("</body>");
                        sb.AppendLine("</html>");

                        string html = sb.ToString();

                        _chart.DocumentText = html;
                    }, 600);
                }
            };



        }

        public void PostDelay(Action action, int delay)
        {
            System.Threading.Timer timer = null;
            SynchronizationContext context = SynchronizationContext.Current;

            timer = new System.Threading.Timer(
                (ignore) =>
                {
                    timer.Dispose();

                    context.Post(ignore2 =>
                    {
                        action();
                    }, null);
                }, null, delay, -1);
        }
    }
}
