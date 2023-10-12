using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using Swashbuckle.Swagger;
using System.Web.Http.Filters;
using AForge.Video.DirectShow;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;

namespace YerIstasyonu_5
{
    public partial class Form1 : Form
    {
        float x = 0, y = 0, z = 0;
        bool cx = false, cy = false, cz = false, csıfır = true, v3rı=false;
        public Form1()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\junio\OneDrive\Masaüstü\Yer_İstasyonu_veri.xlsx;Extended Properties='Excel 12.0 Xml;HDR=YES;' "); //Veri çekme
        FilterInfoCollection filterInfoCollection; // kamera
        VideoCaptureDevice videoCaptureDevice;

        public FilterInfoCollection FilterInfoCollection { get; private set; }

        void Veriler()//veri çekme
        {
            OleDbDataAdapter da=new OleDbDataAdapter("Select * From[Sheet1$]",baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void glControl1_Load(object sender, EventArgs e)
        {
            GL.ClearColor(0.0f, 0.0f, 0.0f, 0.0f);
            GL.Enable(EnableCap.DepthTest);//sonradan yazdık
        }
        private void silindir(float step, float topla, float radius, float dikey1, float dikey2)
         {
            float eski_step = 0.1f;
            GL.Begin(BeginMode.Quads);//Y EKSEN CIZIM DAİRENİN
            while (step<=360)
            { if (step < 45)
                    GL.Color3(Color.FromArgb(205, 133, 63));
                else if (step < 90)
                    GL.Color3(Color.FromArgb(245, 245, 200));
                else if (step < 135)
                    GL.Color3(Color.FromArgb(205, 133, 63));
                else if (step < 180)
                    GL.Color3(Color.FromArgb(245, 245, 220));
                else if (step < 225)
                    GL.Color3(Color.FromArgb(205, 133, 63));
                else if (step < 270)
                    GL.Color3(Color.FromArgb(245, 245, 220));
                else if (step < 315)
                    GL.Color3(Color.FromArgb(205, 133, 63));
                else if (step < 360)
                    GL.Color3(Color.FromArgb(245, 245, 220));

                float ciz1_x = (float)(radius * Math.Cos(step * Math.PI / 180F));
                float ciz1_y = (float)(radius * Math.Sin(step * Math.PI / 180F));
                GL.Vertex3(ciz1_x, dikey1, ciz1_y);

                float ciz2_x = (float)(radius * Math.Cos((step + 2) * Math.PI / 180F));
                float ciz2_y = (float)(radius * Math.Sin((step + 2) * Math.PI / 180F));
                GL.Vertex3(ciz2_x, dikey1, ciz2_y);

                GL.Vertex3(ciz1_x, dikey2, ciz1_y);
                GL.Vertex3(ciz2_x, dikey2, ciz2_y);
                step += topla;
            }

            
            GL.End();
            GL.Begin(BeginMode.Lines);
            step = eski_step;
            topla = step;
            while (step <=180)//UST KAPAK
            {
                if (step < 45)
                    GL.Color3(Color.FromArgb(205, 133, 63));
                else if (step < 90)
                    GL.Color3(Color.FromArgb(245, 245, 220));
                else if (step < 135)
                    GL.Color3(Color.FromArgb(205, 133, 63));
                else if (step < 180)
                    GL.Color3(Color.FromArgb(245, 245, 220));
                else if (step < 225)
                    GL.Color3(Color.FromArgb(205, 133, 63));
                else if (step < 270)
                    GL.Color3(Color.FromArgb(245, 245, 220));
                else if (step < 315)
                    GL.Color3(Color.FromArgb(205, 133, 63));
                else if (step < 360)
                    GL.Color3(Color.FromArgb(245, 245, 220));

                float ciz1_x = (float)(radius * Math.Cos(step * Math.PI / 180F));
                float ciz1_y = (float)(radius * Math.Sin(step * Math.PI / 180F));
                GL.Vertex3(ciz1_x, dikey1, ciz1_y);

                float ciz2_x = (float)(radius * Math.Cos((step + 180) * Math.PI / 180F));
                float ciz2_y = (float)(radius * Math.Sin((step + 180) * Math.PI / 180F));
                GL.Vertex3(ciz2_x, dikey1, ciz2_y);

                GL.Vertex3(ciz1_x, dikey1, ciz1_y);
                GL.Vertex3(ciz2_x, dikey1, ciz2_y);
                step += topla;
            }
            step = eski_step;
            topla = step;
            while(step <=180)//Alt Kapak
            {
                if (step < 45)
                    GL.Color3(Color.FromArgb(205, 133, 63));
                else if (step < 90)
                    GL.Color3(Color.FromArgb(245, 245, 220));
                else if (step < 135)
                    GL.Color3(Color.FromArgb(205, 133, 63));
                else if (step < 180)
                    GL.Color3(Color.FromArgb(245, 245, 220));
                else if (step < 225)
                    GL.Color3(Color.FromArgb(205, 133, 63));
                else if (step < 270)
                    GL.Color3(Color.FromArgb(245, 245, 220));
                else if (step < 315)
                    GL.Color3(Color.FromArgb(205, 133, 63));
                else if (step < 360)
                    GL.Color3(Color.FromArgb(245, 245, 220));

                float ciz1_x = (float)(radius * Math.Cos(step * Math.PI / 180F));
                float ciz1_y = (float)(radius * Math.Sin(step * Math.PI / 180F));
                GL.Vertex3(ciz1_x, dikey2, ciz1_y);

                float ciz2_x = (float)(radius * Math.Cos((step + 180) * Math.PI / 180F));
                float ciz2_y = (float)(radius * Math.Sin((step + 180) * Math.PI / 180F));
                GL.Vertex3(ciz2_x, dikey2, ciz2_y);

                GL.Vertex3(ciz1_x, dikey2, ciz1_y);
                GL.Vertex3(ciz2_x, dikey2, ciz2_y);
                step += topla;
            }
            GL.End();
        } 
        private void koni (float step, float topla, float radius1, float radius2, float dikey1, float dikey2)
        {
            float eski_step = 0.1f;
            GL.Begin(BeginMode.Lines);//Y EKSEN CIZIM DAİRENİN
            while (step <= 360)
            {
                if (step < 45)
                    GL.Color3(Color.FromArgb(205, 133, 63));
                else if (step < 90)
                    GL.Color3(Color.FromArgb(245, 245, 220));
                else if (step < 135)
                    GL.Color3(Color.FromArgb(205, 133, 63));
                else if (step < 180)
                    GL.Color3(Color.FromArgb(245, 245, 220));
                else if (step < 225)
                    GL.Color3(Color.FromArgb(205, 133, 63));
                else if (step < 270)
                    GL.Color3(Color.FromArgb(245, 245, 220));
                else if (step < 315)
                    GL.Color3(Color.FromArgb(205, 133, 63));
                else if (step < 360)
                    GL.Color3(Color.FromArgb(245, 245, 220));


                float ciz1_x = (float)(radius1 * Math.Cos(step * Math.PI / 180F));
                float ciz1_y = (float)(radius1 * Math.Sin(step * Math.PI / 180F));
                GL.Vertex3(ciz1_x, dikey1, ciz1_y);

                float ciz2_x = (float)(radius2 * Math.Cos(step * Math.PI / 180F));
                float ciz2_y = (float)(radius2 * Math.Sin(step * Math.PI / 180F));
                GL.Vertex3(ciz2_x, dikey2, ciz2_y);
                step += topla;
            }
            GL.End();

            GL.Begin(BeginMode.Lines);
            step = eski_step;
            topla = step;
            while (step <= 180)// UST KAPAK
            {
                if (step < 45)
                    GL.Color3(Color.FromArgb(205, 133, 63));
                else if (step < 90)
                    GL.Color3(Color.FromArgb(245, 245, 220));
                else if (step < 135)
                    GL.Color3(Color.FromArgb(205, 133, 63));
                else if (step < 180)
                    GL.Color3(Color.FromArgb(245, 245, 220));
                else if (step < 225)
                    GL.Color3(Color.FromArgb(205, 133, 63));
                else if (step < 270)
                    GL.Color3(Color.FromArgb(245, 245, 220));
                else if (step < 315)
                    GL.Color3(Color.FromArgb(205, 133, 63));
                else if (step < 360)
                    GL.Color3(Color.FromArgb(245, 245, 220));


                float ciz1_x = (float)(radius2 * Math.Cos(step * Math.PI / 180F));
                float ciz1_y = (float)(radius2 * Math.Sin(step * Math.PI / 180F));
                GL.Vertex3(ciz1_x, dikey2, ciz1_y);

                float ciz2_x = (float)(radius2 * Math.Cos((step + 180) * Math.PI / 180F));
                float ciz2_y = (float)(radius2 * Math.Sin((step + 180) * Math.PI / 180F));
                GL.Vertex3(ciz2_x, dikey2, ciz2_y);

                GL.Vertex3(ciz1_x, dikey2, ciz1_y);
                GL.Vertex3(ciz2_x, dikey2, ciz2_y);
                step += topla;
            }
            step = eski_step;
            topla = step;
            GL.End();
        }
        private void Pervane(float yukseklik, float uzunluk, float kalinlik, float egiklik)
        {
            float radius = 10, angle = 45.0f;
            GL.Begin(BeginMode.Quads);

            GL.Color3(Color.White);
            GL.Vertex3(uzunluk, yukseklik, kalinlik);
            GL.Vertex3(uzunluk, yukseklik + egiklik, -kalinlik);
            GL.Vertex3(0.0, yukseklik + egiklik, -kalinlik);
            GL.Vertex3(0.0, yukseklik, kalinlik);

            GL.Color3(Color.White);
            GL.Vertex3(-uzunluk, yukseklik + egiklik, kalinlik);
            GL.Vertex3(-uzunluk, yukseklik, -kalinlik);
            GL.Vertex3(0.0, yukseklik, -kalinlik);
            GL.Vertex3(0.0, yukseklik + egiklik, kalinlik);

            GL.Color3(Color.White);
            GL.Vertex3(kalinlik, yukseklik, -uzunluk);
            GL.Vertex3(-kalinlik, yukseklik + egiklik, -uzunluk);
            GL.Vertex3(-kalinlik, yukseklik + egiklik, 0.0);//+
            GL.Vertex3(kalinlik, yukseklik, 0.0);//-

            GL.Color3(Color.White);
            GL.Vertex3(kalinlik, yukseklik + egiklik, +uzunluk);
            GL.Vertex3(-kalinlik, yukseklik, +uzunluk);
            GL.Vertex3(-kalinlik, yukseklik, 0.0);
            GL.Vertex3(kalinlik, yukseklik + egiklik, 0.0);
            GL.End();
        }
        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            float step = 1.0f;
            float topla = step;
            float radius = 5.0f;
            float dikey1 = radius, dikey2 = -radius;
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);

            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(1.04f, 4 / 3, 1, 10000);
            Matrix4 lookat = Matrix4.LookAt(40, 0, 15, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.LoadMatrix(ref perspective);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.LoadMatrix(ref lookat);
            GL.Viewport(0, 0, glControl1.Width, glControl1.Height);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);

            GL.Rotate(x, 1.0, 0.0, 0.0);//ÖNEMLİ
            GL.Rotate(z, 0.0, 1.0, 0.0);
            GL.Rotate(y, 0.0, 0.0, 1.0);

            silindir(step, topla, radius, 3, -5);
            silindir(0.01f, topla, 0.5f, 9, 9.7f);
            silindir(0.01f, topla, 0.1f, 5, dikey1 + 5);
            koni(0.01f, 0.01f, radius, 3.0f, 3, 5);
            koni(0.01f, 0.01f, radius, 2.0f, -5.0f, -10.0f);
            Pervane(9.0f, 11.0f, 0.2f, 0.5f);

            GL.Begin(BeginMode.Lines);

            GL.Color3(Color.FromArgb(250, 0, 0));
            GL.Vertex3(-30.0, 0.0, 0.0);
            GL.Vertex3(30.0, 0.0, 0.0);


            GL.Color3(Color.FromArgb(0, 0, 0));
            GL.Vertex3(0.0, 30.0, 0.0);
            GL.Vertex3(0.0, -30.0, 0.0);

            GL.Color3(Color.FromArgb(0, 0, 250));
            GL.Vertex3(0.0, 0.0, 30.0);
            GL.Vertex3(0.0, 0.0, -30.0);

            GL.End();
            glControl1.SwapBuffers();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cx == false)
                cx = true;
            else
                cx = false;
            TimerXYZ.Start();
        }

        private void Form1_Load(object sender, EventArgs e) // kamera
        {

            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            for (int i = 0;i< filterInfoCollection.Count;i++)
            {
                comboBox1.Items.Add(filterInfoCollection[i].Name);
            }
            videoCaptureDevice = new VideoCaptureDevice();


            GL.ClearColor(Color.FromArgb(99, 184, 255));
            TimerXYZ.Interval = 1;
            Pressure.Series["Pressure"].Points.AddXY("1.Second", 70);
            Pressure.Series["Pressure"].Points.AddXY("2.Second", 69);
            Pressure.Series["Pressure"].Points.AddXY("3.Second", 67);
            Pressure.Series["Pressure"].Points.AddXY("4.Second", 65);
            Pressure.Series["Pressure"].Points.AddXY("5.Second", 64);
            high.Series["High"].Points.AddXY("1.Second", 1000m);
            high.Series["High"].Points.AddXY("2.Second", 1050m);
            high.Series["High"].Points.AddXY("3.Second", 1060m);
            high.Series["High"].Points.AddXY("4.Second", 1070m);
            high.Series["High"].Points.AddXY("5.Second", 1080);
            Inıs_Hızı.Series["Inıs_hızı"].Points.AddXY("1.Second", 15);
            Inıs_Hızı.Series["Inıs_hızı"].Points.AddXY("2.Second", 20);
            Inıs_Hızı.Series["Inıs_hızı"].Points.AddXY("3.Second", 25);
            Inıs_Hızı.Series["Inıs_hızı"].Points.AddXY("4.Second", 30);
            Inıs_Hızı.Series["Inıs_hızı"].Points.AddXY("5.Second", 35);
            Tempature.Series["Tempature"].Points.AddXY("1.Second", 25);
            Tempature.Series["Tempature"].Points.AddXY("2.Second", 45);
            Tempature.Series["Tempature"].Points.AddXY("3.Second", 35);
            Tempature.Series["Tempature"].Points.AddXY("4.Second", 15);
            Tempature.Series["Tempature"].Points.AddXY("5.Second", 10);
            Tempature.Series["Tempature"].Points.AddXY("6.Second", 50);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

       

        
        private void button7_Click(object sender, EventArgs e)
        {
            videoCaptureDevice=new VideoCaptureDevice(filterInfoCollection[comboBox1.SelectedIndex].MonikerString);
            videoCaptureDevice.NewFrame += VideoCaptureDevice_NewFrame1;
            videoCaptureDevice.Start();
        }

        private void VideoCaptureDevice_NewFrame1(object sender, AForge.Video.NewFrameEventArgs eventArgs) //kamera
        {
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoCaptureDevice.IsRunning)
            {
                videoCaptureDevice.Stop();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnLoadİntoMap_Click(object sender, EventArgs e)
        {
            map.MapProvider = GMapProviders.BingMap;
            double lat = Convert.ToDouble(txtLat.Text);
            double longt = Convert.ToDouble(txtLong.Text);
            map.Position = new GMap.NET.PointLatLng(lat, longt);
            map.MinZoom = 5;
            map.MaxZoom = 100;
            map.Zoom = 10;
            map.DragButton = MouseButtons.Left;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (v3rı == false)
            {
                v3rı = true;

                Veriler();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (cy == false)
                cy = true;
            else
                cy = false;
            TimerXYZ.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (cz == false)
                cz = true;
            else
                cz = false;
            TimerXYZ.Start();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (cx == false)
                cx = true;
            else
                cx = false;
            TimerXYZ.Start();
            if (cy == false)
                cy = true;
            else
                cy = false;
            TimerXYZ.Start();
            if (cz == false)
                cz = true;
            else
                cz = false;
            TimerXYZ.Start();
        }

        private void TimerXYZ_Tick(object sender, EventArgs e)
        {
            if (cx == true)
            {
                if (x < 360)
                    x += 5;
                else
                    x = 0;
                label1.Text = x.ToString();
            }
            if (cy == true)
            {
                if (y < 360)
                    y += 5;
                else
                    y = 0;
                label2.Text = y.ToString();
            }
            if (cz == true)
            {
                if (z < 360)
                    z += 5;
                else
                    z = 0;
                label3.Text = z.ToString();
            }
            glControl1.Invalidate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (csıfır == true)
            {
                cx = false;
                cy = false;
                cz = false;
                x = 0;
                y = 0;
                z = 0;
                label1.Text = "0";
                label2.Text = "0";
                label3.Text = "0";

            }
        }
    }
}
