using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

//using System.Drawing;
//using System.Collections.Generic;
namespace TrainingSessionUtility
{
    public partial class Form1 : Form
    {
        Pen p;
        int graphmode; //0 = steps
        double graphzoom;
        int graphstartdatetime;

        double startTime = 0.0;
        Bitmap b = new Bitmap(1000, 300);
        Graphics g;

        float scale;
        public List<double> ID;
        public List<double> DATETIME;
        public List<string> TYPE;
        public List<double> DVALUE;
        public List<string> SVALUE;
        public List<int> VERSION;
        public List<double> ERROR;

        public List<int> AccXIndexes;
        public List<int> AccYIndexes;
        public List<int> AccZIndexes;

        public List<int> BeganIndexes;
        public List<int> LatIndexes;
        public List<int> LongIndexes;
        public List<int> AltIndexes;

        int sessionBeganIndex;
        double timeBegan;


        Bitmap b1;
        Graphics g1;
        Brush br1;
        Pen p1;
        
        public Form1()
        {
            graphmode = 0;
            graphzoom = 1.0;
            graphstartdatetime = 0;


            scale = 1.0f;
            timeBegan = 0;
            sessionBeganIndex = 0;
            AccXIndexes  = new List<int>();
            AccYIndexes = new List<int>();
            AccZIndexes = new List<int>();

            BeganIndexes = new List<int>();
            LatIndexes = new List<int>();
            LongIndexes = new List<int>();
            AltIndexes = new List<int>();


            ID = new List<double>();
            DATETIME = new List<double>();
            TYPE = new List<string>();
            DVALUE = new List<double>();
            SVALUE = new List<string>();
            VERSION = new List<int>();
            ERROR = new List<double>();

            InitializeComponent();
            g = Graphics.FromImage(b);
            draw();
        }

        private void importFromTextToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            log("Utility Starting");




            log("Utility Started Succesfully");
        }
        private void log(string msg)
        {
            //if (logBox.TextLength > 25*1000)
               // logBox.Text = "";
            logBox.Text += DateTime.Now.ToString() + DateTime.Now.Millisecond.ToString() + "> " + msg + "\n";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            showOnMap(DVALUE[LatIndexes[0]], DVALUE[LongIndexes[0]]);
            log("Test");
        }

        private void importFromTextToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            myOpenFileDialog.ShowDialog();
            log(myOpenFileDialog.FileName.ToString());

            StreamReader streamReader = new StreamReader(myOpenFileDialog.FileName.ToString());
            string text = streamReader.ReadToEnd();
            streamReader.Close();


            log("Test read in:");
            log(text);



            parse(text);         


        }
        private void draw()
        {
            b = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(b);
            g.FillRectangle(new SolidBrush(Color.Black), 0, 0, b.Width, b.Height);
            float Xcenter, Ycenter, Zcenter;
            Xcenter = 25;
            Ycenter = 50;
            Zcenter = 75;


            g.DrawLine(new Pen(new SolidBrush(Color.FromArgb(150, 255, 0, 0))), 0, 110, b.Width, 110);
            g.DrawLine(new Pen(new SolidBrush(Color.FromArgb(100, 255, 255, 255))), 0, 50, b.Width, 50);
            g.DrawLine(new Pen(new SolidBrush(Color.FromArgb(150, 255, 0, 0))), 0, -10, b.Width, -10);
            for (int i = 0; i < 500; i++)
            {
                g.DrawLine(new Pen(new SolidBrush(Color.FromArgb(150, 255, 0, 0))), (i * 30)*scale, 0,( i * 30)*scale, b.Height);
            }
            if(checkBox1.Checked)
            for (int i = 0; i <AccXIndexes.Count-2; i++)
            {

                //log((DATETIME[AccXIndexes[i]] - timeBegan).ToString());
                g.DrawLine(new Pen(new SolidBrush(Color.FromArgb(150,255,0,0))), (float)(DATETIME[AccXIndexes[i]] - timeBegan)*scale, (float)(DVALUE[AccXIndexes[i]] * -60) + 50, (float)(DATETIME[AccXIndexes[i]] - timeBegan)*scale, ((float)(DVALUE[AccXIndexes[i + 1]] * -60) + 50) + 1);//(float)(value2 *-60)+50);
                //pictureBox1.Image = b;
               // log("From X :" + Convert.ToString((float)count * 10)+" Y "+ Convert.ToString((float)(value1) + 15)+" TO X "+ Convert.ToString((float)count * 20)+" Y "+ Convert.ToString( (float)(value2 ) + 15));
            }
            if(checkBox3.Checked)
            for (int i = 0; i < AccYIndexes.Count - 2; i++)
            {


                g.DrawLine(new Pen(new SolidBrush(Color.FromArgb(150, 0, 0, 255))), (float)(DATETIME[AccXIndexes[i]] - timeBegan) * scale, (float)(DVALUE[AccYIndexes[i]] * -60) + 50, (float)(DATETIME[AccXIndexes[i]] - timeBegan) * scale, ((float)(DVALUE[AccYIndexes[i + 1]] * -60) + 50) + 1);//(float)(value2 *-60)+50);
                //pictureBox1.Image = b;
                // log("From X :" + Convert.ToString((float)count * 10)+" Y "+ Convert.ToString((float)(value1) + 15)+" TO X "+ Convert.ToString((float)count * 20)+" Y "+ Convert.ToString( (float)(value2 ) + 15));
            }
            if(checkBox2.Checked)
            for (int i = 0; i < AccZIndexes.Count - 2; i++)
            {


                g.DrawLine(new Pen(new SolidBrush(Color.FromArgb(150, 100, 100, 0))), (float)(DATETIME[AccXIndexes[i]] - timeBegan) * scale, (float)(DVALUE[AccZIndexes[i]] * -60) + 50, (float)(DATETIME[AccXIndexes[i]] - timeBegan) * scale, ((float)(DVALUE[AccZIndexes[i + 1]] * -60) + 50) + 1);//(float)(value2 *-60)+50);
                //pictureBox1.Image = b;
                // log("From X :" + Convert.ToString((float)count * 10)+" Y "+ Convert.ToString((float)(value1) + 15)+" TO X "+ Convert.ToString((float)count * 20)+" Y "+ Convert.ToString( (float)(value2 ) + 15));
            }
           // AltIndexes
            if(checkBox5.Checked)
            for (int i = 0; i < AltIndexes.Count - 2; i++)
            {


                g.DrawLine(new Pen(new SolidBrush(Color.FromArgb(150, 255, 255, 255))), (float)(DATETIME[AltIndexes[i]] - timeBegan) * scale, (float)(DVALUE[AltIndexes[i]] * 1) + 50, (float)(DATETIME[AltIndexes[i]] - timeBegan) * scale, ((float)(DVALUE[AltIndexes[i + 1]] * 1) + 50) + 1);//(float)(value2 *-60)+50);
                //pictureBox1.Image = b;
                // log("From X :" + Convert.ToString((float)count * 10)+" Y "+ Convert.ToString((float)(value1) + 15)+" TO X "+ Convert.ToString((float)count * 20)+" Y "+ Convert.ToString( (float)(value2 ) + 15));
            }
            if(checkBox4.Checked)
            for (int i = 0; i < LongIndexes.Count - 2; i++)
            {

                g.DrawString(Convert.ToString(LatIndexes[i]), new Font(FontFamily.GenericSansSerif, 6), new SolidBrush(Color.White), (float)(DATETIME[LongIndexes[i]] - timeBegan) * scale, 150 - (((float)(DATETIME[LongIndexes[i]] - timeBegan) * 15) % 150));
               // g.DrawLine(new Pen(new SolidBrush(Color.FromArgb(150, 255, 255, 255))), (float)(DATETIME[AltIndexes[i]] - timeBegan), (float)(DVALUE[AltIndexes[i]] * 1) + 50, (float)(DATETIME[AltIndexes[i]] - timeBegan), ((float)(DVALUE[AltIndexes[i + 1]] * 1) + 50) + 1);//(float)(value2 *-60)+50);
                //pictureBox1.Image = b;
                // log("From X :" + Convert.ToString((float)count * 10)+" Y "+ Convert.ToString((float)(value1) + 15)+" TO X "+ Convert.ToString((float)count * 20)+" Y "+ Convert.ToString( (float)(value2 ) + 15));
            }
            pictureBox1.Image = b;
            this.Invalidate();
            
        }
        private void parse(string stringIn)
        {
            System.DateTime startTime = System.DateTime.Now;
            string[] dataLine = stringIn.Split('\n');

            log("*********************   BEGIN PARSING  ****************");
            for (int i = 0; i < dataLine.Length;i++ )
            {
                string[] splited = dataLine[i].Split('$');
                string[] linePart = splited[1].Split(':');
               // log(i.ToString()+ ">" + splited[1]);
                if(linePart.Count() >= 6)
                //for(int j = 0; j < linePart.Length;j++)
                {
                    ID.Add(Convert.ToInt32( linePart[0]));
                    DATETIME.Add(Convert.ToDouble( linePart[1]));
                    TYPE.Add(linePart[2]);
                    DVALUE.Add(Convert.ToDouble(linePart[3]));
                    SVALUE.Add(linePart[4]);
                    VERSION.Add(Convert.ToInt32(linePart[5]));
                    ERROR.Add(Convert.ToDouble(linePart[6]));
                }

            }

           /* log("*********************   END PARSING  ****************");
            for (int k = 0; k < ID.Count; k++ )
            {
                log("k="+k.ToString()+":"+ID[k].ToString() + ">" + DATETIME[k].ToString() + ">" + TYPE[k] + ">" + DVALUE[k].ToString() + ">" + SVALUE[k].ToString() + ">" + VERSION[k].ToString() + ">" + ERROR[k].ToString());
            }*/
            findDataTypes();
            System.DateTime finishTime = System.DateTime.Now;
            label1.Text =ID.Count.ToString() + " Datapoints  covering " + (DATETIME[DATETIME.Count-1] - DATETIME[0]).ToString() +" Seconds In " + (finishTime-startTime).TotalSeconds.ToString();
            System.Console.Beep();
          //  draw();


            
        }
        private void showOnMap(double llat, double llong)
    {
        Uri googMap = new Uri("http://maps.google.com/?ie=UTF8&ll="+llat+","+llong);//+"&spn=5.080922,19.379883&z=6");
        webBrowser1.Url = googMap;
        webBrowser1.Refresh();
    }

        private void button4_Click(object sender, EventArgs e)
        {
            draw();
        }

        private void findDataTypes()
        {
            for (int i = 0; i < TYPE.Count; i++)
            {
                if (TYPE[i] == "TEST ACCELX")
                {
                    AccXIndexes.Add(i);
                }
                if (TYPE[i] == "TEST ACCELY")
                {
                    AccYIndexes.Add(i);
                }
                if (TYPE[i] == "TEST ACCELZ")
                {
                    AccZIndexes.Add(i);
                }




                if (TYPE[i] == "BEGAN")
                {
                    BeganIndexes.Add(i);
                    listBox1.Items.Add(i.ToString());
                }
                if (TYPE[i] == "LOCLAT")
                {
                    LatIndexes.Add(i);
                    listBox2.Items.Add(i.ToString());
                }
                if (TYPE[i] == "LOCLONG")
                {
                    LongIndexes.Add(i);
                }

                if (TYPE[i] == "LOCALT")
                {
                    AltIndexes.Add(i);
                }
            }

            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {

            sessionBeganIndex = Convert.ToInt32(BeganIndexes[listBox1.SelectedIndex]);
            log(BeganIndexes[listBox1.SelectedIndex].ToString());
            timeBegan = DATETIME[sessionBeganIndex];
            log("Began :" + timeBegan.ToString());
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            showOnMap(DVALUE[LatIndexes[listBox1.SelectedIndex]], DVALUE[LongIndexes[listBox1.SelectedIndex]]);
             
             
            //log(BeganIndexes[listBox1.SelectedIndex].ToString());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            scale++;
            draw();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            scale--;
            draw();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(500);
                this.Hide();
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                notifyIcon1.Visible = false;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            ExcelReport priceReport;

            priceReport = new ExcelReport(this);

            priceReport.GenerateSelf();

            System.Diagnostics.Process.Start
                ("excel.exe",
                @".\prices.xml");
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //System("http://keithloughnanesblog.blogspot.com/");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void label41_Click(object sender, EventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void graphDraw()
        {
            b1 = new Bitmap(picGraphBox.Width, picGraphBox.Height);
            g1 = Graphics.FromImage(b1);
            br1 = new SolidBrush(Color.Blue);
            p1 = new Pen(br1);
           p1.Width = 5;

           this.countSteps(0,0);





            //graphDrawEdge(0,0,200,200);
           MessageBox.Show("at switch");
            
                if(graphmode == 0){
                    for (int i = 0; i < AccYIndexes.Count-1;i++ )
                    {
                      //  MessageBox.Show("in graph loop");
                        g1.DrawLine(p1, i*1, (AccYIndexes[i]*1), (i+1)*1, (AccYIndexes[1]+1)*1);
                        g1.DrawEllipse(p1, 0, 0, 10, 10);
                    }
                }
            
            








            picGraphBox.Image = b1;

        }
        private void graphDrawEdge(int x1, int y1, int x2, int y2)
        {
        }

        private void button16_Click(object sender, EventArgs e)
        {
            graphDraw();
        }
        private int countSteps(double startDate, double endDate)
        {
            //Look for changes in direction on the Y axis
            int count = 0 ;
            bool movingup = false;
            double oldvalue;

            for (int i = 0; i < AccZIndexes.Count; i++)
            {
                if (AccZIndexes[i] < -1.02)
                {
                    if (movingup == true)
                    {
                        count++;
                    }
                    oldvalue = AccZIndexes[i];
                    movingup = false;
                }
                /*
                if (AccZIndexes[i] > -1.02)
                {
                    if (movingup == false)
                    {
                        count++;
                    }
                    movingup = true;
                }
                 * */
                //-1.02

                //Since the direction changes at the top AND the bottom of a step we divide by 2

            }

            MessageBox.Show(count.ToString());
            return count / 2;
        }

        private void button17_Click(object sender, EventArgs e)
        {

        }
    }
   
}
