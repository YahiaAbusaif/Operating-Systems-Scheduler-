using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace OS2
{
    public partial class Form1 : Form
    {
        public class Process
        {
            public int ID, priority;
            public Double arrivalTime, burstTime, WaitingTime, FinishTime, ReminingTime;
        }; //save all data for each process
        public List<int> Final;
        public List<Process> Mylist; //list of process
        public int number; //number of process
        public Double TimeQuantum; //For RR
        public Double step=0.01;
        public Double Zero = 0.0001;
        public Double ContextSwitching; // time for switch from process to another -> assume if process ends CPU needs 1 ContextSwitching before start any another process
        
        public Form1() //initialize 
        {
            InitializeComponent();
            Algo.Items.Add("HPF");
            Algo.Items.Add("FCFS");
            Algo.Items.Add("RR");
            Algo.Items.Add("SRTN");
            
        }

        public Double PTQ(Double time) //add ContextSwitching slides between the 2 process
        {
            if (Final == null)
                return time;
            Double end = time + ContextSwitching;
            while (time < end)
            {
                Final.Add(0);
                time += step;
            }
            return time;
        }

        public void HPF() //algorithm Hogh piority first
        {
            if (Final == null || Mylist == null)
                return;
            int count = 0,mx,last=-1;
            Double time = 0.0;
            while (count < Mylist.Count) //while number of finish process != number of total process 
            {
                mx = -1;
                //search on maximum priorty 
                for (int i = 0; i < number; i++)
                {
                    if (Mylist[i].arrivalTime > time) continue;
                    if (Mylist[i].ReminingTime < Zero) continue;
                    if (mx == -1)
                        mx = i;
                    else if (Mylist[mx].priority < Mylist[i].priority)
                        mx = i;
                }
                if (mx == -1) //if no process has arrive yet add 0 to minimum arrival time 
                {
                    Final.Add(0);
                    time += step;
                    last = -1;
                }
                else if (last != mx && last != -1)
                {
                    time = PTQ(time);
                    last = -1;
                }
                else
                {
                    Final.Add(Mylist[mx].ID);
                    if (Mylist[mx].WaitingTime < Zero)
                        Mylist[mx].WaitingTime = time - Mylist[mx].arrivalTime;
                    Mylist[mx].ReminingTime -= step;
                    if (Mylist[mx].ReminingTime < Zero)
                    {
                        Mylist[mx].FinishTime = time;
                        count++;
                        time = PTQ(time);
                        last = -1;
                    }
                    else
                    {
                        last = mx;
                        time += step;
                    }     
                } 
            }
            time = PTQ(time);
        }
        public void SRTN() //Preemptive Shortest Remaining Time Next (same algorithm like HPF but choose minimum RT
        {
            if (Final == null || Mylist == null)
                return;
            int count = 0, mx, last = -1;
            Double time = 0.0;
            while (count < Mylist.Count)
            {
                mx = -1;
                for (int i = 0; i < number; i++)
                {
                    if (Mylist[i].arrivalTime > time) continue;
                    if (Mylist[i].ReminingTime < Zero) continue;
                    if (mx == -1)
                        mx = i;
                    else if (Mylist[mx].ReminingTime > Mylist[i].ReminingTime)
                        mx = i;
                }
                if (mx == -1) //if no process has arrive yet add 0 to minimum arrival time 
                {
                    Final.Add(0);
                    time += step;
                    last = -1;
                }
                else if (last != mx && last != -1)
                {
                    time = PTQ(time);
                    last = -1;
                }
                else
                {
                    Final.Add(Mylist[mx].ID);
                    if (Mylist[mx].WaitingTime < Zero)
                        Mylist[mx].WaitingTime = time - Mylist[mx].arrivalTime;
                    Mylist[mx].ReminingTime -= step;
                    if (Mylist[mx].ReminingTime < Zero)
                    {
                        Mylist[mx].FinishTime = time;
                        count++;
                        time = PTQ(time);
                        last = -1;
                    }
                    else
                    {
                        last = mx;
                        time += step;
                    }
                } 
            }
            time = PTQ(time);
        }
        public void FCFS() //first come first serve queue and add all process to it , start time of any one = max(finish time of the last +context time,arrival time)
        {
            if (Final == null || Mylist == null)
                return;
            int count = 0, mx;
            Double time = 0.0;
            while (count < Mylist.Count) //while number of finish process != number of total process 
            {
                mx = -1;
                //search on maximum priorty 
                for (int i = 0; i < number; i++)
                {
                    if (Mylist[i].arrivalTime > time) continue;
                    if (Mylist[i].ReminingTime < Zero) continue;
                    if (mx == -1)
                        mx = i;
                    else if (Mylist[mx].arrivalTime > Mylist[i].arrivalTime)
                        mx = i;
                }
                if (mx == -1) //if no process has arrive yet add 0 to minimum arrival time 
                {
                    Final.Add(0);
                    time += step;
                }
                else
                {
                    Mylist[mx].WaitingTime = time - Mylist[mx].arrivalTime;
                    double end = time + Mylist[mx].burstTime;
                    while (time < end)
                    {
                        Final.Add(Mylist[mx].ID);
                        time += step;
                    }
                    Mylist[mx].ReminingTime = 0.0000;
                    Mylist[mx].FinishTime = time;
                    count++;
                    time = PTQ(time);
                }
            }
            Final.Add(0);
        }
        public void RR() //Round Robin with fixed time quantum
        {
            if (Final == null || Mylist == null)
                return;
            int count=0,last=-1;
            Double time = 0.0;
            while (count < Mylist.Count)
            {
                bool test = true;
                for (int i = 0; i < number; i++)
                {
                    if (Mylist[i].arrivalTime > time) continue;
                    if (Mylist[i].ReminingTime < Zero) continue;
                    if (Mylist[i].WaitingTime  < Zero) Mylist[i].WaitingTime = time - Mylist[i].arrivalTime;
                    test = false;
                    if(last!=i && last!=-1)
                        time=PTQ(time);
                    double end = time + TimeQuantum;
                    last = i;
                    while (time < end && Mylist[i].ReminingTime > Zero)
                    {
                        Final.Add(Mylist[i].ID);
                        Mylist[i].ReminingTime -= step;
                        time += step;
                    }
                    if (Mylist[i].ReminingTime < Zero)
                    {
                        Mylist[i].FinishTime = time;
                        time = PTQ(time);
                        last = -1;
                        count++;
                    }
                }
                if (test)
                {
                    Final.Add(0);
                    time += step;
                    last = -1;
                }
            }
           
        }
        
        public bool Valid() //program will stop if any missing or wrong data
        {
            Double n;
            bool test = true;
            if (FileName.Text == null || Context.Text == null || string.IsNullOrEmpty(Algo.Text) || Algo.SelectedIndex == -1 || output.Text == null)
            {
                MessageBox.Show("missing or wrong data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            } 
            if (Algo.Text == "RR") //if RR has been choosen check for time quantum
            {
                if(Time.Text == null)
                {
                    MessageBox.Show("you must enter Time Quantum", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (!(Double.TryParse(Time.Text.ToString(), out n)))
                {
                    MessageBox.Show("Quantum must be number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                //*if no "." in the number add it , C# will make it 0 if there are no ".'
                test = true;
                for (int i = 0; i < Time.Text.Length; i++)
                {
                    if (Time.Text[i] == '.')
                        test = false;
                }
                if (test)
                    Time.Text += ".";
                //*
                TimeQuantum = Double.Parse(Time.Text);
                if (TimeQuantum < Zero)
                {
                    MessageBox.Show("Quantum must be greter than" + step.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            if (!(Double.TryParse(Context.Text.ToString(), out n)))
            {
                MessageBox.Show("Context is not number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!System.IO.File.Exists(@FileName.Text) || !System.IO.Directory.Exists(@output.Text))
            {
                MessageBox.Show("path file didn't exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
             
            //*if no "." in the number add it , C# will make it 0 if there are no ".'
            test = true;
            for (int i = 0; i < Context.Text.Length; i++)
            {
                if (Context.Text[i] == '.')
                    test = false;
            }
            if (test)
                Context.Text += ".";
            ContextSwitching = Double.Parse(Context.Text);
            //*
            string[] lines = System.IO.File.ReadAllLines(@FileName.Text);
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Valid())
            {
                
                string[] lines = System.IO.File.ReadAllLines(@FileName.Text);//read data from file
                //assume all data is valid on file
                //initalize data
                if(Mylist!=null)
                    Mylist.Clear();
                if(Final!=null)
                    Final.Clear();
                Mylist = new List<Process>();
                Final = new List<int>();
                number = int.Parse(lines[0]);
                for (int i = 1; i <= number; i++)
                {
                    Double[] values = lines[i].Split(' ').Select(Double.Parse).ToArray();
                    Process curr=new Process();
                    curr.ID=Convert.ToInt32(values[0]);
                    //if any value less than zero take abslute
                    if (values[1] < Zero)
                        values[1] *= -1;
                    if (values[2] < Zero)
                        values[2] *= -1;
                    curr.arrivalTime=values[1];
                    curr.burstTime=curr.ReminingTime=values[2];
                    curr.priority = Convert.ToInt32(values[3]);
                    curr.WaitingTime = curr.FinishTime = -1.0;
                    Mylist.Add(curr);
                }
                //process with the given algorithm
                if (Algo.Text == "HPF")
                    HPF();
                else if (Algo.Text == "FCFS")
                    FCFS();
                else if (Algo.Text == "RR")
                    RR();
                else if (Algo.Text == "SRTN")
                    SRTN();

                chart1_Click(sender, e); //draw chart
                //print to file
                Double sum1 = 0, sum2 = 0;
                string line = "process ID \t Waiting time \t Turnaround time \t Weighted Turnaround time \r\n";
                for (int i = 0; i < number; i++)
                {
                    string nLine = Mylist[i].ID.ToString() + "\t\t";
                    nLine += Mylist[i].WaitingTime.ToString() + "\t\t";
                    Double TAT = Mylist[i].FinishTime - Mylist[i].arrivalTime;
                    sum1 += TAT;
                    nLine += TAT.ToString() + " \t\t";
                    Double ATAT = TAT / Mylist[i].burstTime;
                    nLine += ATAT.ToString() + " \r\n";
                    sum2 += ATAT;
                    line += nLine;
                }
                sum1 /= number;
                sum2 /= number;
                line += "Average Turnaround time of the schedule " + sum1.ToString() + "\r\nAverage Weighted turnaround time of the schedule " + sum2.ToString();
                
                string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)+"\\output.txt" ;
                //File.WriteAllText(Path.Combine(path, "output.txt"), line);
                using (var writer = new StreamWriter(path))
                {
                    writer.WriteLine(line);
                }
                //System.IO.File.WriteAllText(path, line);
            }
        }
        private void Algo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {
            if (Final == null || Final.Count() == 0)
                return;
            this.chart1.Series["process"].Points.Clear();
            this.chart1.Series["process"].Points.AddXY(0.0,0);
            for (int i = 0; i < Final.Count(); i++)
                this.chart1.Series["process"].Points.AddXY(i/10.0, Final[i]);
            /*
            string path=output.Text.ToString();
            string Npath="";
            for(int i=0;i+3<path.Count();i++)
                Npath+=path[i];
            Npath += "png";
            this.chart1.SaveImage(@Npath,System.Drawing.Imaging.ImageFormat.Png);*/
        }

        private void output_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
