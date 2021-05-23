using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Delone
{
    public partial class Form1 : Form
    {
        #region переменные
        Random rand;
        Graphics g;
        Image img;
        int N_point, timer_index, level_count = 8;
        bool solved;
        double r1, r2, l;
        double delta;
        double UpGU = 9, DownGU = -9;

        List<triangle> triangles = new List<triangle>();
        List<DPoint> points = new List<DPoint>();
        List<DPoint> vnutrennie = new List<DPoint>();
        List<DPoint> temp_points = new List<DPoint>();
        List<DPoint> cilinders = new List<DPoint>();
        List<DPoint> knots = new List<DPoint>();
        List<DPoint> edge = new List<DPoint>();
        List<DPoint> all_points = new List<DPoint>();
        List<DPoint> angel_points = new List<DPoint>();
        List<DPoint> gran = new List<DPoint>();
        List<double> levels = new List<double>();
        List<List<double>> A = new List<List<double>>();
        List<double> B = new List<double>();
        List<double> C = new List<double>();
        #endregion

        public Form1()
        {
            InitializeComponent();
            rand = new Random();
        }

        public double sqr(double a)
        {
            return a * a;
        }

        double spec_f(DPoint one, DPoint two)
        {
            return (sqr(one.x) + sqr(one.y) - sqr(two.x) - sqr(two.y));
        }

        bool equal(DPoint a, DPoint b)
        {
            if (a.x == b.x && a.y == b.y)
                return true;
            else
                return false;
        }

        double module(DPoint a, DPoint b)
        {
            return Math.Sqrt(sqr(a.x - b.x) + sqr(a.y - b.y));
        }

        #region Структуры DPoint (координата х, у), triangle (точки p1, p2, p3, центр окружности c, радиус R)
        struct DPoint //структура точек
        {
            public double x;
            public double y;
            public double fi;

            public static bool operator ==(DPoint p1, DPoint p2)
            {
                if (p1.x == p2.x && p1.y == p2.y)
                {
                    return true;
                }
                else { return false; }
            }
            public static bool operator !=(DPoint p1, DPoint p2)
            {
                if (p1.x != p2.x && p1.y != p2.y)
                {
                    return true;
                }
                else { return false; }
            }
        }
        struct triangle //структура для треугольника
        {
            public DPoint p1; // точка 1
            public DPoint p2; // точка 2
            public DPoint p3; // точка 3
            public DPoint c; // центр описанной у треугольника окружности
            public double R; // радиус описанной окружности
            public double A;
            public double B;
            public double C;

            public double get_smth()
            {
                double square = 0;
                square = Math.Abs((p2.x - p1.x) * (p3.y - p1.y) - (p2.y - p1.y) * (p3.x - p1.x)) / 2;
                return square;
            }

            public void get_abc()
            {
                double delta = p1.x * (p2.y * p3.fi - p3.y * p2.fi) - p1.y * (p2.x * p3.fi - p3.x * p2.fi) + p1.fi * (p2.x * p3.y + p3.x * p2.y);
                A = (1 * (p2.y * p3.fi - p3.y * p2.fi) - p1.y * (1 * p3.fi - 1 * p2.fi) + p1.fi * (1 * p3.y + 1 * p2.y)) / delta;
                B = (p1.x * (1 * p3.fi - 1 * p2.fi) - 1 * (p2.x * p3.fi - p3.x * p2.fi) + p1.fi * (p2.x * 1 + p3.x * 1)) / delta;
                C = (p1.x * (p2.y * 1 - p3.y * 1) - p1.y * (p2.x * 1 - p3.x * 1) + 1 * (p2.x * p3.y + p3.x * p2.y)) / delta;
            }

            public void change_fi(int numb, double new_fi)
            {
                if (numb == 1)
                    p1.fi = new_fi;
                if (numb == 2)
                    p2.fi = new_fi;
                if (numb == 3)
                    p3.fi = new_fi;
            }

            public void create(DPoint pt1, DPoint pt2, DPoint pt3)
            {
                p1 = pt1;
                p2 = pt2;
                p3 = pt3;
            }
        }
        #endregion

        #region функции отрисовки 

        /// <summary>
        /// отрисовка точек
        /// </summary>
        /// <param name="g"> графика </param>
        /// <param name="p">лист координаты точек</param>
        /// <param name="count">количество точек</param>
        /// <param name="brush"> кисть </param>
        void draw_points(Graphics g, List<DPoint> p, int count, Brush brush) //отрисовка точек
        {
            //рисуем точки прямоугольничками (кисть, ось х, ось у, ширина прямоугольнка, высота прямоугольника)
            for (int i = 0; i < count; i++) g.FillRectangle(brush, (int)p[i].x - 1, (int)p[i].y - 1, 3, 3);
        }

        /// <summary>
        /// отрисовка треугольников
        /// </summary>
        /// <param name="g"> графика </param>
        /// <param name="t"> треугольник </param>
        /// <param name="color"> цвет ручки </param>
        void draw_triangle(Graphics g, triangle t, Color color)//рисуем треугольники 
        {
            //ручка
            Pen pen = new Pen(color);
            //х
            Point pt1 = new Point();
            // у
            Point pt2 = new Point();

            //точка 1 и 2
            pt1.X = (int)t.p1.x;
            pt1.Y = (int)t.p1.y;
            pt2.X = (int)t.p2.x;
            pt2.Y = (int)t.p2.y;
            g.DrawLine(pen, pt1, pt2); //соединяем

            //точка 2 и 3
            pt1.X = (int)t.p2.x;
            pt1.Y = (int)t.p2.y;
            pt2.X = (int)t.p3.x;
            pt2.Y = (int)t.p3.y;
            g.DrawLine(pen, pt1, pt2);

            //точка 3 и 1
            pt1.X = (int)t.p3.x;
            pt1.Y = (int)t.p3.y;
            pt2.X = (int)t.p1.x;
            pt2.Y = (int)t.p1.y;
            g.DrawLine(pen, pt1, pt2);
        }

        /// <summary>
        /// отрисовка точек
        /// </summary>
        /// <param name="g"> графика </param>
        /// <param name="p">массив координаты точек</param>
        /// <param name="count">количество точек</param>
        /// <param name="brush"> кисть </param>
        void draw_points(Graphics g, DPoint[] p, int count, Brush brush)
        {
            for (int i = 0; i < count; i++)
                g.FillRectangle(brush, (int)p[i].x - 1, (int)p[i].y - 1, 3, 3);
        }

        /// <summary>
        /// отрисовка окружностей
        /// </summary>
        /// <param name="g"> графика </param>
        /// <param name="R"> радиус </param>
        /// <param name="O"> центр окружности </param>
        /// <param name="brush"> цвет кисточки </param>
        void draw_ellipse(Graphics g, float R, DPoint O, Pen brush)
        {
            g.DrawEllipse(brush, (int)O.x - R, (int)O.y - R, (float)(2.0 * R), (float)(2.0 * R));
        }

        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timer_index < temp_points.Count)
            {
                pictureBox1.Image = img;
                g = Graphics.FromImage(pictureBox1.Image);

                List<DPoint> erased_points = new List<DPoint>();
                List<int> erased_index = new List<int>();
                for (int j = 0; j < triangles.Count; j++)
                {
                    if (module(triangles[j].c, temp_points[timer_index]) < triangles[j].R)
                    {
                        draw_triangle(g, triangles[j], Color.White);
                        erased_index.Add(j);

                        bool flag = true;

                        DPoint buf;
                        buf = triangles[j].p1;
                        for (int k = 0; k < erased_points.Count; k++)
                            if (equal(erased_points[k], buf))
                            {
                                flag = false;
                                break;
                            }
                        if (flag)
                            erased_points.Add(buf);
                        flag = true;

                        buf = triangles[j].p2;
                        for (int k = 0; k < erased_points.Count; k++)
                            if (equal(erased_points[k], buf))
                            {
                                flag = false;
                                break;
                            }
                        if (flag)
                            erased_points.Add(buf);
                        flag = true;

                        buf = triangles[j].p3;
                        for (int k = 0; k < erased_points.Count; k++)
                            if (equal(erased_points[k], buf))
                            {
                                flag = false;
                                break;
                            }
                        if (flag)
                            erased_points.Add(buf);
                    }
                }
                erased_points.Add(temp_points[timer_index]);

                for (int i = 0; i < erased_index.Count; i++)
                {
                    triangles.RemoveAt(erased_index[i]);
                    for (int j = i; j < erased_index.Count; j++)
                        erased_index[j]--;
                }
                if (true)
                    for (int m = 0; m < erased_points.Count - 2; m++)
                        for (int n = 0; n < erased_points.Count - 1; n++)
                            for (int l = 0; l < erased_points.Count; l++)
                            {
                                if (equal(erased_points[m], temp_points[timer_index]) ||
                                    equal(erased_points[n], temp_points[timer_index]) ||
                                    equal(erased_points[l], temp_points[timer_index]))
                                {
                                    if (m == n || n == l || l == m ||
                                        equal(erased_points[m], erased_points[n]) ||
                                        equal(erased_points[n], erased_points[l]) ||
                                        equal(erased_points[l], erased_points[m]))
                                        continue;
                                    get_triangle(g,
                                        erased_points[m],
                                        erased_points[n],
                                        erased_points[l],
                                        erased_points.ToArray(), erased_points.Count);
                                }
                            }
                draw_points(g, erased_points.ToArray(), erased_points.Count, Brushes.Maroon);

                erased_points.Clear();
            }
            else
            {
                for (int l = 0; l < angel_points.Count; l++)
                {
                    List<DPoint> erased_points = new List<DPoint>();
                    List<int> erased_index = new List<int>();
                    for (int j = 0; j < triangles.Count; j++)
                    {
                        if (equal(triangles[j].p1, angel_points[l]) || equal(triangles[j].p2, angel_points[l]) || equal(triangles[j].p3, angel_points[l]))
                        {
                            draw_triangle(g, triangles[j], Color.White);
                            erased_index.Add(j);

                            bool flag = true;

                            DPoint buf;
                            buf = triangles[j].p1;
                            for (int k = 0; k < erased_points.Count; k++)
                                if (equal(erased_points[k], buf))
                                {
                                    flag = false;
                                    break;
                                }
                            if (flag)
                                erased_points.Add(buf);
                            flag = true;

                            buf = triangles[j].p2;
                            for (int k = 0; k < erased_points.Count; k++)
                                if (equal(erased_points[k], buf))
                                {
                                    flag = false;
                                    break;
                                }
                            if (flag)
                                erased_points.Add(buf);
                            flag = true;

                            buf = triangles[j].p3;
                            for (int k = 0; k < erased_points.Count; k++)
                                if (equal(erased_points[k], buf))
                                {
                                    flag = false;
                                    break;
                                }
                            if (flag)
                                erased_points.Add(buf);
                        }
                    }
                    erased_points.Add(temp_points[l]);

                    for (int i = 0; i < erased_index.Count; i++)
                    {
                        triangles.RemoveAt(erased_index[i]);
                        for (int j = i; j < erased_index.Count; j++)
                            erased_index[j]--;
                    }

                    draw_points(g, erased_points.ToArray(), erased_points.Count, Brushes.Maroon);
                    erased_index.Clear();
                    erased_points.Clear();
                }
                for (int q = 0; q < vnutrennie.Count; q++)
                {
                    List<DPoint> erased_points = new List<DPoint>();
                    List<int> erased_index = new List<int>();
                    for (int j = 0; j < triangles.Count; j++)
                    {
                        if (equal(triangles[j].p1, vnutrennie[q]) || equal(triangles[j].p2, vnutrennie[q]) || equal(triangles[j].p3, vnutrennie[q]))
                        {
                            draw_triangle(g, triangles[j], Color.White);
                            erased_index.Add(j);

                            bool flag = true;

                            DPoint buf;
                            buf = triangles[j].p1;
                            for (int k = 0; k < erased_points.Count; k++)
                                if (equal(erased_points[k], buf))
                                {
                                    flag = false;
                                    break;
                                }
                            if (flag)
                                erased_points.Add(buf);
                            flag = true;

                            buf = triangles[j].p2;
                            for (int k = 0; k < erased_points.Count; k++)
                                if (equal(erased_points[k], buf))
                                {
                                    flag = false;
                                    break;
                                }
                            if (flag)
                                erased_points.Add(buf);
                            flag = true;

                            buf = triangles[j].p3;
                            for (int k = 0; k < erased_points.Count; k++)
                                if (equal(erased_points[k], buf))
                                {
                                    flag = false;
                                    break;
                                }
                            if (flag)
                                erased_points.Add(buf);
                        }
                    }
                    erased_points.Add(temp_points[q]);

                    for (int i = 0; i < erased_index.Count; i++)
                    {
                        triangles.RemoveAt(erased_index[i]);
                        for (int j = i; j < erased_index.Count; j++)
                            erased_index[j]--;
                    }

                    draw_points(g, erased_points.ToArray(), erased_points.Count, Brushes.Maroon);
                    erased_index.Clear();
                    erased_points.Clear();
                }

                pictureBox1.Image = img;
                g = Graphics.FromImage(pictureBox1.Image);
                timer1.Stop();
                for (int i = 0; i < triangles.Count; i++)
                    draw_triangle(g, triangles[i], Color.Navy);
                draw_points(g, points, points.Count, Brushes.Red);
                draw_points(g, temp_points, temp_points.Count, Brushes.Red);
                draw_points(g, vnutrennie, vnutrennie.Count, Brushes.White);
            }
            timer_index++;
        }

        private void yel_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(pictureBox1.Image);
            g.Clear(Color.White);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            for (int i = 0; i < triangles.Count; i++)
                draw_triangle(g, triangles[i], Color.DarkBlue);

        }

        private void New_Points_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = img;
            g = Graphics.FromImage(pictureBox1.Image);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            timer1.Start();
        }


        private void Triangulation_Click(object sender, EventArgs e)
        {
            Points_Click(sender, e);
            Triangle_Click(sender, e);
            New_Points_Click(sender, e);
            yel_Click(sender, e);
        }

        triangle create_triangle(DPoint a, DPoint b, DPoint c, DPoint O, double R)//создание треугольника
        {
            triangle temp = new triangle();//переменная к структуре треугольников
            temp.p1 = a;//точка а
            temp.p2 = b;//точка б
            temp.p3 = c;//точка с
            temp.c = O;//центр окружности
            temp.R = R;//радиус
            return temp;
        }

        private void Points_Click(object sender, EventArgs e)
        {
            // Начальная инициализация
            N_point = Convert.ToInt16(N.Text);
            delta = Convert.ToDouble(Delta.Text);
            l = Convert.ToDouble(Distance.Text);
            r1 = Convert.ToDouble(Outside_R.Text);
            r2 = Convert.ToDouble(Inside_R.Text);

            int accuracy;
            solved = false;

            triangles.Clear();
            points.Clear();
            vnutrennie.Clear();
            temp_points.Clear();
            edge.Clear();
            knots.Clear();
            cilinders.Clear();
            all_points.Clear();
            levels.Clear();
            angel_points.Clear();
            gran.Clear();

            timer_index = 0;

            int width = pictureBox1.Width;
            int height = pictureBox1.Height;

            double delta_x = (double)width * delta / (N_point - 1);
            double delta_y = (double)height * delta / (N_point - 1);

            int offset_x = (int)(width * 0.2);
            int offset_y = (int)(height * 0.2);

            pictureBox1.Image = new Bitmap(width, height);
            g = Graphics.FromImage(pictureBox1.Image);
            g.Clear(Color.White);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            DPoint temp = new DPoint();

            // Добавляем точки в углы
            temp.fi = 0;
            {
                temp.x = 0;
                temp.y = 0;
                points.Add(temp);
                angel_points.Add(temp);

                temp.x = width - 4;
                temp.y = 0;
                points.Add(temp);
                angel_points.Add(temp);

                temp.x = width - 4;
                temp.y = height - 4;
                points.Add(temp);
                angel_points.Add(temp);

                temp.x = 0;
                temp.y = height - 4;
                points.Add(temp);
                angel_points.Add(temp);
            }

            // Добавляем точки в центры
            temp.fi = 0;
            for (int i = 0; i < N_point * 2 + 1; i++)
            {
                if (i == 0 || i == 2 * N_point) continue;
                else
                if (i % 2 == 1)
                {
                    temp.x = width / 2 + (r2 + (r1 - r2) * 0.3) * Math.Cos(Math.PI * (double)i / N_point / 2) + (-0.5 + rand.NextDouble()) * delta_x;
                    temp.y = height / 2 + l + (r2 + (r1 - r2) * 0.3) * Math.Sin(Math.PI * (double)i / N_point / 2) + (-0.5 + rand.NextDouble()) * delta_y;

                    points.Add(temp);
                    vnutrennie.Add(temp);

                    temp.x = width / 2 + (r2 + (r1 - r2) * 0.3) * Math.Cos(Math.PI * (double)i / N_point / 2 + Math.PI) + (-0.5 + rand.NextDouble()) * delta_x;
                    temp.y = height / 2 - l + (r2 + (r1 - r2) * 0.3) * Math.Sin(Math.PI * (double)i / N_point / 2 + Math.PI) + (-0.5 + rand.NextDouble()) * delta_y;

                    points.Add(temp);
                    vnutrennie.Add(temp);

                    temp.x = width / 2 + (r2 + (r1 - r2) * 0.7) * Math.Cos(Math.PI * (double)i / N_point / 2) + (-0.5 + rand.NextDouble()) * delta_x;
                    temp.y = height / 2 + l + (r2 + (r1 - r2) * 0.7) * Math.Sin(Math.PI * (double)i / N_point / 2) + (-0.5 + rand.NextDouble()) * delta_y;

                    points.Add(temp);
                    vnutrennie.Add(temp);

                    temp.x = width / 2 + (r2 + (r1 - r2) * 0.7) * Math.Cos(Math.PI * (double)i / N_point / 2 + Math.PI) + (-0.5 + rand.NextDouble()) * delta_x;
                    temp.y = height / 2 - l + (r2 + (r1 - r2) * 0.7) * Math.Sin(Math.PI * (double)i / N_point / 2 + Math.PI) + (-0.5 + rand.NextDouble()) * delta_y;

                    points.Add(temp);
                    vnutrennie.Add(temp);
                }
            }

            // Создаем очертания границы области
            temp.fi = 0;
            for (int i = 0; i < N_point; i++)
            {
                for (int j = 0; j < N_point; j++)
                {
                    if (i == 0 || i == N_point - 1 || j == 0 || j == N_point - 1)
                    {
                        temp.x = offset_x + ((-0.5 + rand.NextDouble()) * delta_x + (width - 2 * offset_x) * (double)i / (N_point - 1));
                        temp.y = offset_y + ((-0.5 + rand.NextDouble()) * delta_y + (height - 2 * offset_y) * (double)j / (N_point - 1));
                        points.Add(temp);
                        gran.Add(temp);
                    }
                }
            }

            // Добавляем точки в области между окружностями
            accuracy = 3;
            for (int i = 1; i < accuracy; i++)
            {
                temp.fi = DownGU;
                temp.x = width / 2 + (r2 + i * (r1 - r2) / accuracy) * Math.Cos(Math.PI * 0) + (-0.5 + rand.NextDouble()) * delta_x;
                temp.y = height / 2 + l + (-0.5 + rand.NextDouble()) * delta_y;
                points.Add(temp);
                cilinders.Add(temp);
                gran.Add(temp);

                temp.fi = UpGU;
                temp.x = width / 2 + (r2 + i * (r1 - r2) / accuracy) * Math.Cos(Math.PI * 0) + (-0.5 + rand.NextDouble()) * delta_x;
                temp.y = height / 2 - l + (-0.5 + rand.NextDouble()) * delta_y;
                points.Add(temp);
                cilinders.Add(temp);
                gran.Add(temp);

                temp.fi = DownGU;
                temp.x = width / 2 + (r2 + i * (r1 - r2) / accuracy) * Math.Cos(Math.PI * 1) + (-0.5 + rand.NextDouble()) * delta_x;
                temp.y = height / 2 + l + (-0.5 + rand.NextDouble()) * delta_y;
                points.Add(temp);
                cilinders.Add(temp);
                gran.Add(temp);

                temp.fi = UpGU;
                temp.x = width / 2 + (r2 + i * (r1 - r2) / accuracy) * Math.Cos(Math.PI * 1) + (-0.5 + rand.NextDouble()) * delta_x;
                temp.y = height / 2 - l + (-0.5 + rand.NextDouble()) * delta_y;
                points.Add(temp);
                cilinders.Add(temp);
                gran.Add(temp);
            }

            // Создаем очертания верхней половины цилиндра
            DPoint temp1 = new DPoint();
            accuracy = N_point * 4 / 3;
            for (int i = 0; i < accuracy + 1; i++)
            {
                temp1.fi = DownGU;
                temp1.x = width / 2 + r1 * Math.Cos(Math.PI * (double)i / accuracy) + (-0.5 + rand.NextDouble()) * delta_x;
                temp1.y = height / 2 + l + r1 * Math.Sin(Math.PI * (double)i / accuracy) + (-0.5 + rand.NextDouble()) * delta_y;

                points.Add(temp1);
                cilinders.Add(temp1);
                gran.Add(temp1);

                temp1.fi = UpGU;
                temp1.x = width / 2 + r1 * Math.Cos(Math.PI * (double)i / accuracy + Math.PI) + (-0.5 + rand.NextDouble()) * delta_x;
                temp1.y = height / 2 - l + r1 * Math.Sin(Math.PI * (double)i / accuracy + Math.PI) + (-0.5 + rand.NextDouble()) * delta_y;

                points.Add(temp1);
                cilinders.Add(temp1);
                gran.Add(temp1);
            }

            // Создаем очертания нижней половины цилиндра
            DPoint temp2 = new DPoint();
            accuracy = N_point * 2 / 3;
            for (int i = 0; i < accuracy + 1; i++)
            {
                temp2.fi = DownGU;
                temp2.x = width / 2 + r2 * Math.Cos(Math.PI * (double)i / accuracy) + (-0.5 + rand.NextDouble()) * delta_x;
                temp2.y = height / 2 + l + r2 * Math.Sin(Math.PI * (double)i / accuracy) + (-0.5 + rand.NextDouble()) * delta_y;

                points.Add(temp2);
                cilinders.Add(temp2);
                gran.Add(temp2);

                temp2.fi = UpGU;
                temp2.x = width / 2 + r2 * Math.Cos(Math.PI * (double)i / accuracy + Math.PI) + (-0.5 + rand.NextDouble()) * delta_x;
                temp2.y = height / 2 - l + r2 * Math.Sin(Math.PI * (double)i / accuracy + Math.PI) + (-0.5 + rand.NextDouble()) * delta_y;

                points.Add(temp2);
                cilinders.Add(temp2);
                gran.Add(temp2);
            }

            // Создаем дополнительные точки цилиндров
            accuracy = (int)(1.6 * N_point);
            temp.fi = 0;
            for (int i = 0; i < accuracy; i++)
            {
                for (int j = 0; j < accuracy; j++)
                {
                    if (!(i == 0 || i == accuracy - 1 || j == 0 || j == accuracy - 1))
                    {
                        DPoint c1 = new DPoint();
                        c1.x = width / 2;
                        c1.y = height / 2 + l;

                        DPoint c2 = new DPoint();
                        c2.x = width / 2;
                        c2.y = height / 2 - l;

                        var x = offset_x + (width - 2 * offset_x) * (double)i / (accuracy - 1);
                        var y = offset_y + (height - 2 * offset_y) * (double)j / (accuracy - 1);

                        if ((sqr(x - c1.x) + sqr(y - c1.y) < sqr(r1) * (1 + 3 * delta)) &&
                            (sqr(x - c1.x) + sqr(y - c1.y) > sqr(r2) * (1 - 5 * delta)) &&
                            (y > height / 2 + l - delta_y * (5)) ||
                            (sqr(x - c2.x) + sqr(y - c2.y) < sqr(r1) * (1 + 3 * delta)) &&
                            (sqr(x - c2.x) + sqr(y - c2.y) > sqr(r2) * (1 - 5 * delta)) &&
                            (y < height / 2 - l + delta_y * (5)))
                        {
                            continue;
                        }
                        else
                        {
                            temp.x = offset_x + ((-0.5 + rand.NextDouble()) * delta_x + (width - 2 * offset_x) * (double)i / (accuracy - 1));
                            temp.y = offset_y + ((-0.5 + rand.NextDouble()) * delta_y + (height - 2 * offset_y) * (double)j / (accuracy - 1));
                            temp_points.Add(temp);
                        }
                    }
                }
            }

            // Создаем массив всех не граничных точек
            temp.fi = 0;
            for (int i = 0; i < temp_points.Count; i++)
            {
                temp = temp_points[i];
                knots.Add(temp);
            }

            // Создаем массив всех граничных точек
            bool flag;
            for (int i = 0; i < points.Count; i++)
            {
                flag = true;
                for (int idx = 0; idx < vnutrennie.Count; idx++)
                {
                    if (equal(vnutrennie[idx], points[i]))
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                    edge.Add(points[i]);
            }



            // Отрисовка
            {
                draw_points(g, temp_points, temp_points.Count, Brushes.Red);
                draw_points(g, points, points.Count, Brushes.Navy);
                draw_points(g, vnutrennie, vnutrennie.Count, Brushes.Violet);
                img = pictureBox1.Image;
            }

            for (int i = 0; i < level_count; i++)
            {
                levels.Add(DownGU + (UpGU - DownGU) / (level_count - 1) * i);
            }
        }
        void get_triangle(Graphics g, DPoint a, DPoint b, DPoint c, DPoint[] arr, int size)
        {
            double R, m, n, l, p;
            DPoint O = new DPoint();


            //центр по х у описанной окружности у треугольника
            O.x = -0.5 * (a.y * spec_f(b, c) + b.y * spec_f(c, a) + c.y * spec_f(a, b)) / (a.x * (b.y - c.y) + b.x * (c.y - a.y) + c.x * (a.y - b.y));
            //центр по н у описанной окружности у треугольника
            O.y = 0.5 * (a.x * spec_f(b, c) + b.x * spec_f(c, a) + c.x * spec_f(a, b)) / (a.x * (b.y - c.y) + b.x * (c.y - a.y) + c.x * (a.y - b.y));//центр по н у описанной окружности у треугольника
            Pen vertexPen = new Pen(Color.Bisque, 2);
            m = module(a, b);//первая сторона треугольника
            n = module(b, c);//вторая сторона теугольника
            l = module(c, a);//третья сторона треугольника

            p = (m + n + l) / 2;//полупериметр
            R = m * n * l / 4 / Math.Sqrt(p * (p - m) * (p - n) * (p - l));//радиус = mnl/(4S)

            if (R < 1000)
            {

                bool flag = true;

                for (int i = 0; i < size; i++)
                {
                    DPoint buf = arr[i];
                    if (equal(buf, a) || equal(buf, b) || equal(buf, c))
                        continue;//условие
                    if (sqr(O.x - buf.x) + sqr(O.y - buf.y) < R * R)
                    {
                        flag = false;
                        goto next;
                    }
                }
            next:
                if (flag)
                {
                    var temp = create_triangle(a, b, c, O, R);
                    triangles.Add(temp);
                    draw_triangle(g, temp, Color.Navy);
                    // draw_ellipse(g, (float)R, O, vertexPen);
                }
            }
        }

        private void Triangle_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = img;
            g = Graphics.FromImage(pictureBox1.Image);
            g.Clear(Color.White);

            for (int i = 0; i < points.Count - 2; i++)
                for (int j = 0; j < points.Count - 1; j++)
                    for (int k = 0; k < points.Count; k++)
                    {
                        if (i == j || j == k || k == i) continue;
                        get_triangle(g, points[i], points[j], points[k], points.ToArray(), points.Count);
                    }
            draw_points(g, points, points.Count, Brushes.Red);
        }
       
        void draw_potencial(Graphics g, List<DPoint> arr)
        {
            double max = 0, min = 0;
            Color clr = new Color();
            Brush brh;
            DPoint temp = new DPoint();
            List<DPoint> buf_mas = new List<DPoint>();
            List<Brush> brushes = new List<Brush>();

            for (int i = 0; i < arr.Count; i++)
            {
                if (arr[i].fi > 0)
                {
                    if (arr[i].fi > max)
                        max = arr[i].fi;
                }
                else
                {
                    if (arr[i].fi < min)
                        min = arr[i].fi;
                }
            }
            for (int i = 0; i < arr.Count; i++)
            {
                temp.x = arr[i].x;
                temp.y = arr[i].y;
                if (arr[i].fi > 0)
                    temp.fi = arr[i].fi / max;
                else
                    temp.fi = arr[i].fi / Math.Abs(min);
                buf_mas.Add(temp);
            }

            for (int i = 0; i < arr.Count; i++)
            {
                if (buf_mas[i].fi > 0)
                {
                    clr = Color.FromArgb((int)(buf_mas[i].fi * 255), 0, 0);
                    brh = new System.Drawing.SolidBrush(clr);
                    g.FillRectangle(brh, (int)arr[i].x - 1, (int)arr[i].y - 1, 3, 3);
                }
                if (buf_mas[i].fi <= 0)
                {
                    clr = Color.FromArgb(0, 0, (int)(Math.Abs(buf_mas[i].fi) * 255));
                    brh = new System.Drawing.SolidBrush(clr);
                    g.FillRectangle(brh, (int)arr[i].x - 1, (int)arr[i].y - 1, 3, 3);
                }
            }
        }

        private void Draw_Click(object sender, EventArgs e)
        {
            Galerkin();
        }


        void draw_iso_lines(Graphics g, List<DPoint> arr)
        {
            Pen pen0 = new Pen(Color.Yellow);
            Point pt1 = new Point();
            Point pt2 = new Point();

            //DPoint[] lines = new DPoint[level_count];
            DPoint start = new DPoint();
            DPoint end = new DPoint();
            DPoint buf = new DPoint();

            for (int idx = 0; idx < level_count; idx++)
            {
                int treug_idx = 0;
                start = cilinders[idx * cilinders.Count / (level_count)];
                double pot = start.fi;

                for (int i = 0; i < triangles.Count; i++)
                {
                    if (start == triangles[i].p1 || start == triangles[i].p2 || start == triangles[i].p3)
                    {
                        treug_idx = i;
                        start = triangles[i].c;
                        start.fi = pot;
                        break;
                    }
                }

                bool exit = true;
                do
                {
                    var tr = triangles[treug_idx];
                    tr.get_abc();
                    triangles[treug_idx].get_abc();
                    end.x = start.x + triangles[treug_idx].A;
                    end.y = start.y + triangles[treug_idx].B;

                    pt1.X = (int)start.x;
                    pt1.Y = (int)start.y;
                    pt2.X = (int)end.x;
                    pt2.Y = (int)end.y;
                    g.DrawLine(pen0, pt1, pt2);

                    start = end;
                    exit = next_step(end, treug_idx, 10);
                } while (exit);
            }
        }
        bool next_step(DPoint end, int i, double eps)
        {
            bool to_return = false;
            triangle tr1 = new triangle();
            triangle tr2 = new triangle();
            triangle tr3 = new triangle();
            tr1.create(end, triangles[i].p2, triangles[i].p3);
            tr2.create(end, triangles[i].p3, triangles[i].p1);
            tr3.create(end, triangles[i].p1, triangles[i].p2);
            if (Math.Abs(tr1.get_smth() + tr2.get_smth() + tr3.get_smth() - triangles[i].get_smth()) < eps)
            {
                to_return = true;
            }
            return to_return;
        }

        void draw_power_lines()
        {

        }

        public void Galerkin()
        {
            CREATE_A();
            CREATE_B();
            C.Clear();

            List<double> buf = new List<double>();
            double[] a = new double[A[0].Count * A[0].Count];
            double[] b; //= new double[B.Count];

            int idx = 0;
            for (int i = 0; i < A[0].Count; i++)
                for (int j = 0; j < A[0].Count; j++)
                {
                    a[idx] = A[i][j];
                    idx++;
                }
            b = B.ToArray();
            C.AddRange(MethodKachmarzh(a, b, B.Count, B.Count));

            List<DPoint> PotentionalPoints = new List<DPoint>();
            for (int i = 0; i < knots.Count; i++)
            {
                DPoint temp = new DPoint();
                temp = knots[i];
                temp.fi = C[i];
                PotentionalPoints.Add(temp);
            }
            knots.Clear();
            knots.AddRange(PotentionalPoints);

            int width = pictureBox1.Width;
            int height = pictureBox1.Height;

            pictureBox1.Image = new Bitmap(width, height);
            g = Graphics.FromImage(pictureBox1.Image);
            g.Clear(Color.Black);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            all_points.AddRange(knots);
            all_points.AddRange(edge);

            if (DRAW_POT.Checked)
            {
                draw_potencial(g, all_points);
            }
            if (DRAW_ISO.Checked)
            {
                draw_iso_lines(g, all_points);
            }
            if (DRAW_POWER.Checked)
            {
                draw_power_lines();
            }

        }
       

        public List<double> MethodKachmarzh(double[] a, double[] b, int nn, int ny)
        {
            /* матрица А, столбец свободных членов, массив неизвестных,
            nn - количество неизвестных;  ny - количество уравнений*/
            double[] x = new double[nn];
            double eps = 1e-6f;
            //double s;
            int i, j, k;
            double s1, s2, fa1, t;
            double[] x1 = new double[nn];

            x[0] = 0.5f;
            for (i = 1; i < nn; i++) x[i] = 0f;

            s1 = s2 = 1f;
            while (s1 > eps * s2)
            {
                for (i = 0; i < nn; i++) x1[i] = x[i];

                for (i = 0; i < ny; i++)
                {
                    s1 = 0f;
                    s2 = 0f;
                    for (j = 0; j < nn; j++)
                    {
                        fa1 = a[i * nn + j];
                        s1 += fa1 * x[j];
                        s2 += fa1 * fa1;
                    }
                    t = (b[i] - s1) / s2;
                    for (k = 0; k < nn; k++) x[k] += a[i * nn + k] * t;
                }

                s1 = 0f;
                s2 = 0f;
                for (i = 0; i < nn; i++)
                {
                    s1 += (x[i] - x1[i]) * (x[i] - x1[i]);
                    s2 += x[i] * x[i];
                }
                s1 = Math.Sqrt(s1);
                s2 = Math.Sqrt(s2);
            }
            return x.ToList();
        }


        //ебанная хирабора взятая из какой-то методичке в инете:

        private double AAAAA(triangle triangle, DPoint pt)
        {
            DPoint p1 = pt, p2 = pt, p3 = pt; //просто начальная инициализация
            if (equal(triangle.p1, pt)) { p1 = pt; p2 = triangle.p2; p3 = triangle.p3; }
            if (equal(triangle.p2, pt)) { p1 = pt; p2 = triangle.p1; p3 = triangle.p3; }
            if (equal(triangle.p3, pt)) { p1 = pt; p2 = triangle.p2; p3 = triangle.p1; }

            double value = (p2.y - p1.y) * (-1) - (p3.y - p1.y) * (-1);
            return value;
        }

        private double BBBBBB(triangle triangle, DPoint pt)
        {
            DPoint p1 = pt, p2 = pt, p3 = pt; //просто начальная инициализация
            if (equal(triangle.p1, pt)) { p1 = pt; p2 = triangle.p2; p3 = triangle.p3; }
            if (equal(triangle.p2, pt)) { p1 = pt; p2 = triangle.p1; p3 = triangle.p3; }
            if (equal(triangle.p3, pt)) { p1 = pt; p2 = triangle.p2; p3 = triangle.p1; }

            double value = (p2.x - p1.x) - (p3.x - p1.x);
            return value;
        }


        public void CREATE_A()
        {
            A.Clear();
            int size_tri = triangles.Count();

            for (int i = 0; i < knots.Count; i++)
            {
                List<double> AAA = new List<double>();
                for (int j = 0; j < knots.Count; j++)
                {
                    double value = 0;
                    if (i == j)
                    {
                        List<triangle> tempTringles = new List<triangle>();

                        for (int e = 0; e < size_tri; e++)
                        {
                            triangle treugol = triangles[e];

                            if (equal(treugol.p1, knots[i]) || equal(treugol.p2, knots[i]) || equal(treugol.p3, knots[i]))
                                tempTringles.Add(treugol);
                        }

                        foreach (triangle treugol in tempTringles)
                        {
                            value += treugol.get_smth() * (AAAAA(treugol, knots[i]) * AAAAA(treugol, knots[i]) + BBBBBB(treugol, knots[i]) * BBBBBB(treugol, knots[i]));
                        }

                        AAA.Add(value);
                    }

                    else if (i != j)
                    {
                        List<triangle> tempTringles = new List<triangle>();
                        for (int counter = 0; counter < size_tri; counter++)
                        {
                            triangle triag = triangles[counter];
                            DPoint pi = knots[i], pj = knots[j];
                            if ((triag.p1 == pi && triag.p2 == pj) ||
                                   (triag.p1 == pi && triag.p3 == pj) ||
                                   (triag.p2 == pi && triag.p1 == pj) ||
                                   (triag.p2 == pi && triag.p3 == pj) ||
                                   (triag.p3 == pi && triag.p1 == pj) ||
                                   (triag.p3 == pi && triag.p2 == pj))
                                tempTringles.Add(triag);
                        }

                        if (tempTringles.Count == 0) { value += 0; AAA.Add(value); }
                        else
                        {
                            foreach (triangle triag in tempTringles)
                            {
                                DPoint pt1 = new DPoint();
                                DPoint pt2 = new DPoint();
                                DPoint pt3 = new DPoint();

                                if (triag.p1 == knots[i])
                                {
                                    if (triag.p2 == knots[j])
                                    {
                                        pt1 = triag.p1;
                                        pt2 = triag.p2;
                                        pt3 = triag.p3;
                                    }
                                    if (triag.p3 == knots[j])
                                    {
                                        pt1 = triag.p1;
                                        pt2 = triag.p3;
                                        pt3 = triag.p2;
                                    }
                                }
                                if (triag.p2 == knots[i])
                                {
                                    if (triag.p1 == knots[j])
                                    {
                                        pt1 = triag.p2;
                                        pt2 = triag.p1;
                                        pt3 = triag.p3;
                                    }
                                    if (triag.p3 == knots[j])
                                    {
                                        pt1 = triag.p2;
                                        pt2 = triag.p3;
                                        pt3 = triag.p1;
                                    }
                                }
                                if (triag.p3 == knots[i])
                                {
                                    if (triag.p2 == knots[j])
                                    {
                                        pt1 = triag.p3;
                                        pt2 = triag.p2;
                                        pt3 = triag.p1;
                                    }
                                    if (triag.p1 == knots[j])
                                    {
                                        pt1 = triag.p3;
                                        pt2 = triag.p1;
                                        pt3 = triag.p2;
                                    }
                                }

                                double koef_Ai = (pt2.y - pt1.y) * (-1) - (pt3.y - pt1.y) * (-1); //спс инету за формулки
                                double koef_Bi = (pt3.x - pt1.x) * (-1) - (pt2.x - pt1.x) * (-1);

                                double koef_Aj = (pt2.y - pt1.y) * (0) - (pt3.y - pt1.y) * (1);
                                double koef_Bj = (pt3.x - pt1.x) * (1) - (pt2.x - pt1.x) * (0);

                                value += triag.get_smth() * (koef_Ai * koef_Aj + koef_Bi * koef_Bj);
                            }
                            AAA.Add(value);

                        }
                    }

                }
                A.Add(AAA);
            }
        }

        public void CREATE_B()
        {
            B.Clear();
            int size_triangles = triangles.Count;
            List<DPoint> edgePotentional = new List<DPoint>();
            edgePotentional.AddRange(cilinders);

            for (int i = 0; i < knots.Count; i++)
            {
                double value = 0;
                for (int j = 0; j < edgePotentional.Count; j++)
                {
                    List<triangle> tempTringles = new List<triangle>();
                    for (int counter = 0; counter < size_triangles; counter++)
                    {
                        triangle triag = triangles[counter];
                        DPoint pi = knots[i], pj = edgePotentional[j];
                        if ((triag.p1 == pi && triag.p2 == pj) ||
                            (triag.p1 == pi && triag.p3 == pj) ||
                            (triag.p2 == pi && triag.p1 == pj) ||
                            (triag.p2 == pi && triag.p3 == pj) ||
                            (triag.p3 == pi && triag.p1 == pj) ||
                            (triag.p3 == pi && triag.p2 == pj))
                            tempTringles.Add(triag);
                    }

                    if (tempTringles.Count != 0)
                    {
                        foreach (triangle triag in tempTringles)
                        {
                            DPoint pt1 = new DPoint();
                            DPoint pt2 = new DPoint();
                            DPoint pt3 = new DPoint();


                            if (triag.p1 == knots[i])
                            {
                                if (triag.p2 == edgePotentional[j])
                                {
                                    pt1 = triag.p1;
                                    pt2 = triag.p2;
                                    pt3 = triag.p3;
                                }
                                if (triag.p3 == edgePotentional[j])
                                {
                                    pt1 = triag.p1;
                                    pt2 = triag.p3;
                                    pt3 = triag.p2;
                                }
                            }
                            if (triag.p2 == knots[i])
                            {
                                if (triag.p1 == edgePotentional[j])
                                {
                                    pt1 = triag.p2;
                                    pt2 = triag.p1;
                                    pt3 = triag.p3;
                                }
                                if (triag.p3 == edgePotentional[j])
                                {
                                    pt1 = triag.p2;
                                    pt2 = triag.p3;
                                    pt3 = triag.p1;
                                }
                            }
                            if (triag.p3 == knots[i])
                            {
                                if (triag.p2 == edgePotentional[j])
                                {
                                    pt1 = triag.p3;
                                    pt2 = triag.p2;
                                    pt3 = triag.p1;
                                }
                                if (triag.p1 == edgePotentional[j])
                                {
                                    pt1 = triag.p3;
                                    pt2 = triag.p1;
                                    pt3 = triag.p2;
                                }
                            }

                            double koef_Ai = (pt2.y - pt1.y) * (-1) - (pt3.y - pt1.y) * (-1);
                            double koef_Bi = (pt3.x - pt1.x) * (-1) - (pt2.x - pt1.x) * (-1);

                            double koef_Aj = (pt2.y - pt1.y) * (0) - (pt3.y - pt1.y) * (1);
                            double koef_Bj = (pt3.x - pt1.x) * (1) - (pt2.x - pt1.x) * (0);

                            value += edgePotentional[j].fi * triag.get_smth() * (koef_Ai * koef_Aj + koef_Bi * koef_Bj);
                        }
                    }
                }
                B.Add(value * (-1));
            }
        }

    }
}
