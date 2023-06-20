using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.Globalization;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.CompilerServices;

namespace graphpr
{
    public partial class Form1 : Form
    {
        private List<List<PointF>> gpoints;
        private double minx = -10;
        private double maxx = 10;
        private double miny = -10;
        private double maxy = 10;
        private int sindex = -1;
        public Form1()
        {
            InitializeComponent();
          
            gpoints = new List<List<PointF>>();
           
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (sindex != -1)
            {
                List<PointF> selectedGraph = gpoints[sindex];

                switch (keyData)
                {
                    case Keys.W:
                        MoveGraph(selectedGraph, 0, 1);
                        Drawgraph();
                        return true;
                    case Keys.A:
                        MoveGraph(selectedGraph, -1, 0);
                        Drawgraph();
                        return true;
                    case Keys.S:
                        MoveGraph(selectedGraph, 0, -1);
                        Drawgraph();
                        return true;
                    case Keys.D:
                        MoveGraph(selectedGraph, 1, 0);
                        Drawgraph();
                        return true;
                    case Keys.E:
                        for (int i = 0; i < selectedGraph.Count; i++)
                        {
                            if (!selectedGraph[i].IsEmpty)
                            {
                                selectedGraph[i] = new PointF((float)(selectedGraph[i].X * 1.1), (float)(selectedGraph[i].Y));
                            }
                        }

                        Drawgraph();
                        return true;
                    case Keys.Q:
                        for (int i = 0; i < selectedGraph.Count; i++)
                        {
                            if (!selectedGraph[i].IsEmpty)
                            {
                                selectedGraph[i] = new PointF((float)(selectedGraph[i].X * 0.9), (float)(selectedGraph[i].Y));
                            }
                        }

                        Drawgraph();
                        return true;
                    case Keys.Z:
                        for (int i = 0; i < selectedGraph.Count; i++)
                        {
                            if (!selectedGraph[i].IsEmpty)
                            {
                                selectedGraph[i] = new PointF((float)(selectedGraph[i].X ), (float)(selectedGraph[i].Y * 1.1));
                            }
                        }

                        Drawgraph();
                        return true;
                    case Keys.X:
                        for (int i = 0; i < selectedGraph.Count; i++)
                        {
                            if (!selectedGraph[i].IsEmpty)
                            {
                                selectedGraph[i] = new PointF((float)(selectedGraph[i].X ), (float)(selectedGraph[i].Y * 0.9));
                            }
                        }

                        Drawgraph();
                        return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void MoveGraph(List<PointF> graph, float deltaX, float deltaY)
        {
            for (int i = 0; i < graph.Count; i++)
            {
                PointF point = graph[i];
                if (!point.IsEmpty)
                {
                    point.X += deltaX;
                    point.Y += deltaY;
                    graph[i] = point;
                }
            }
        }

        private List<PointF> Load(string filep)
        {
            List<PointF> list = new List<PointF>();

            string[] lines = File.ReadAllLines(filep);
            foreach (string line in lines)
            {
                string tline = line.Trim();
                if (tline.Equals("break"))
                {
                    list.Add(PointF.Empty);
                }
                else
                {
                    string[] val = tline.Split(' ');
                    if (val.Length == 2)
                    {
                        double x = double.Parse(val[0], CultureInfo.InvariantCulture);
                        double y = double.Parse(val[1], CultureInfo.InvariantCulture);
                        list.Add(new PointF((float)x, (float)y));
                    }
                }
            }
            return list;
        }
        private void Drawgraph()
        {
            panel1.Invalidate();
        }
        private void DrawGrid(Graphics g, float w, float h, double sx, double sy)
        {
            double stx = (maxx - minx) / 80;
            double sty = (maxy - miny) / 40;

            for (double x = minx + stx; x < maxx; x += stx)
            {
                float scx = (float)((x - minx) * sx);
                g.DrawLine(Pens.LightGray, scx, 0, scx, h);
                if (x % 1 == 0)
                {
                    g.DrawString(x.ToString(), new Font("Arial", 8), Brushes.Black, scx, h / 2+5);
                }
            }
            for (double y = miny + sty; y < maxy; y += sty)
            {
                float scy = (float)(h - (y - miny) * sy);
                g.DrawLine(Pens.LightGray, 0, scy, w, scy);
                if (y % 1 == 0)
                {
                    g.DrawString(y.ToString(), new Font("Arial", 8), Brushes.Black, w / 2+5, scy);}
            }
        }

        private void panel1_p(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            float w = panel1.Width;
            float h = panel1.Height;
            double sx = w / (maxx - minx);
            double sy = h / (maxy - miny);
            g.Clear(Color.White);
            DrawGrid(g, w, h, sx, sy);
            g.DrawLine(Pens.Black, 0, h / 2, w, h / 2);
            g.DrawLine(Pens.Black, w / 2, 0, w / 2, h);
            string xal = "X";
            string yal = "Y";
            Font labelfont = new Font("Times New Roman", 12);
            PointF xalp = new PointF(w - 20, h / 2 - 20);
            PointF yalp = new PointF(w / 2 - 10, 10);
            g.DrawString(xal, labelfont, Brushes.Black, xalp);
            g.DrawString(yal, labelfont, Brushes.Black, yalp);
            int numberOfXTicks = 40; 
            int numberOfYTicks = 20;
            float tickSize = 5;

            
            float xTickSpacing = w / numberOfXTicks;
            for (int i = 0; i < numberOfXTicks; i++)
            {
                float x = i * xTickSpacing;
                g.DrawLine(Pens.Black, x, h / 2 - tickSize, x, h / 2 + tickSize);
            }

            float yTickSpacing = h / numberOfYTicks;
            for (int i = 0; i < numberOfYTicks; i++)
            {
                float y = i * yTickSpacing;
                g.DrawLine(Pens.Black, w / 2 - tickSize, y, w / 2 + tickSize, y);
            }
            for (int i = 0; i < gpoints.Count; i++)
            {
                List<PointF> points = gpoints[i];

                for (int j = 1; j < points.Count; j++)
                {
                    PointF p1 = points[j - 1];
                    PointF p2 = points[j];

                    if (!points[j - 1].IsEmpty && !points[j].IsEmpty)
                    {
                        double x1 = p1.X - minx;
                        double y1 = maxy - p1.Y;
                        double x2 = p2.X - minx;
                        double y2 = maxy - p2.Y;

                        float scx1 = (float)(x1 * sx);
                        float scy1 = (float)(y1 * sy);
                        float scx2 = (float)(x2 * sx);
                        float scy2 = (float)(y2 * sy);

                        if (i == sindex)
                        {
                            g.DrawLine(Pens.Red, scx1, scy1, scx2, scy2);
                        }
                        else
                        {
                            g.DrawLine(Pens.Blue, scx1, scy1, scx2, scy2);
                        }
                    }
                }
            }
        }
        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            float mouseX = (float)e.X;
            float mouseY = (float)e.Y;

            int selectedGraph = -1;
            double sx = panel1.Width / (maxx - minx);
            double sy = panel1.Height / (maxy - miny);
            for (int i = 0; i < gpoints.Count; i++)
            {
                List<PointF> points = gpoints[i];

                for (int j = 1; j < points.Count; j++)
                {
                    PointF p1 = points[j - 1];
                    PointF p2 = points[j];

                    if (!points[j - 1].IsEmpty && !points[j].IsEmpty)
                    {
                        double x1 = p1.X - minx;
                        double y1 = maxy - p1.Y;
                        double x2 = p2.X - minx;
                        double y2 = maxy - p2.Y;

                        float scx1 = (float)(x1 * sx);
                        float scy1 = (float)(y1 * sy);
                        float scx2 = (float)(x2 * sx);
                        float scy2 = (float)(y2 * sy);

                        double distance = CalcDTl(scx1, scy1, scx2, scy2, mouseX, mouseY);

                        if (distance <= 5) // Задайте значение, которое определяет радиус выбора графика
                        {
                            selectedGraph = i;
                            break;
                        }
                    }
                }

                if (selectedGraph != -1)
                {
                    break;
                }
            }

            sindex = selectedGraph;
            Drawgraph();
        }

        private double CalcDTl(float x1, float y1, float x2, float y2, float x, float y)
        {
            double a = x - x1;
            double b = y - y1;
            double c = x2 - x1;
            double d = y2 - y1;
            double dot = a * c + b * d;
            double sq = c * c + d * d;
            double p = dot / sq;
            double xn, yn;
            if (p < 0)
            {
                xn = x1;
                yn = y1;
            }
            else if (p > 1)
            {
                xn = x2; yn = y2;
            }
            else
            {
                xn = (float)(x1 * p * c);
                yn = (float)(y1 * p * d);
            }

            double dx = x - xn;
            double dy = y - yn;
            return Math.Sqrt(dx * dx + dy * dy);
        }
        
        private void button1_click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filep = openFileDialog.FileName;
                List<PointF> points = Load(filep);
                gpoints.Add(points);
                Drawgraph();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            // Отображение сообщения с помощью MessageBox
            MessageBox.Show("W,A,S,D - рух\nQ - зменшення графіку по осі Х\nE - збільшення графіку по осі Х", "Управління", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}

