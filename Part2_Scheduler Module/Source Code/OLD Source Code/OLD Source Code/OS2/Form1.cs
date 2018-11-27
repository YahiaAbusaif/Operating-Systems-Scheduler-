using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OS2
{
    public partial class Form1 : Form
    {
        public class Process
        {
            public int ID, priority;
            public Double arrivalTime, burstTime, WaitingTime, FinishTime, ReminingTime;
        }; //save all data for each process
        public class slides
        {
            public int ID;
            public Double start, end;
        }; //save the id of the process that run on cpu from start to end
        public List<Process> Mylist; //list of process
        List<slides> Final; //list of slides time 
        public int number; //number of process
        public Double TimeQuantum; //For RR
        public Double step=0.001; // for Double number differance that will be ignored
        public Double ContextSwitching; // time for switch from process to another -> assume if process ends before switch to another then take 0 time 
        
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
            slides nslid2 = new slides();
            nslid2.ID = 0;
            nslid2.start = time;
            time += ContextSwitching;
            nslid2.end = time;
            Final.Add(nslid2);
            return time;
        }
        public void HPF() //algorithm Hogh piority first
        {
            if (Final == null || Mylist == null)
                return;
            int count = 0,mx,last=-1;
            Double time = 0.0,mn;
            while (count < Mylist.Count) //while number of finish process != number of total process 
            {
                mn = 10000.0;
                mx = -1;
                //search on maximum priorty 
                for (int i = 0; i < number; i++)
                {
                    if (Mylist[i].FinishTime >step) continue;
                    if (Mylist[i].arrivalTime > time)
                    {
                        mn = Mylist[i].arrivalTime;
                        break;
                    } 
                    if (mx == -1)
                        mx = i;
                    else if (Mylist[mx].priority < Mylist[i].priority)
                        mx = i;
                }
                if (mx == -1) //if no process has arrive yet add 0 to minimum arrival time 
                {
                    slides nslide = new slides();
                    nslide.ID = 0;
                    nslide.start = time;
                    nslide.end = mn;
                    time = mn;
                    Final.Add(nslide);
                    last = -1;
                }
                else //cpu will run highest priorty untill it finish or new process arrive
                {
                    if (last != mx && last != -1)
                        time = PTQ(time);
                    slides nslide = new slides();
                    nslide.ID =Mylist[mx].ID;
                    nslide.start = time;
                    if (Mylist[mx].burstTime - Mylist[mx].ReminingTime < step)
                        Mylist[mx].WaitingTime = time - Mylist[mx].arrivalTime;
                    if (time + Mylist[mx].ReminingTime < mn)
                    {
                        mn = time + Mylist[mx].ReminingTime;
                        Mylist[mx].ReminingTime = 0.00001;
                        Mylist[mx].FinishTime = mn;
                        count++;
                    }
                    else
                        Mylist[mx].ReminingTime -= mn - time;
                    nslide.end = mn;
                    time = mn;
                    Final.Add(nslide);
                    last = mx;
                }
            }
            time = PTQ(time);
        }
        public void SRTN() //Preemptive Shortest Remaining Time Next (same algorithm like HPF but choose minimum RT
        {
            if (Final == null || Mylist == null)
                return;
            int count = 0, mx, last = -1;
            Double time = 0.0, mn;
            while (count < Mylist.Count)
            {
                mn = 10000.0;
                mx = -1;
                for (int i = 0; i < number; i++)
                {
                    if (Mylist[i].ReminingTime < step) continue;
                    if (Mylist[i].arrivalTime > time)
                    {
                        mn = Mylist[i].arrivalTime;
                        break;
                    }
                    if (mx == -1)
                        mx = i;
                    else if (Mylist[mx].ReminingTime > Mylist[i].ReminingTime)
                        mx = i;
                }
                if (mx == -1)
                {
                    slides nslide = new slides();
                    nslide.ID = 0;
                    nslide.start = time;
                    nslide.end = mn;
                    time = mn;
                    Final.Add(nslide);
                    last = -1;
                }
                else
                {
                    if (last != mx && last != -1)
                        time = PTQ(time);
                    slides nslide = new slides();
                    nslide.ID = Mylist[mx].ID;
                    nslide.start = time;
                    if (Mylist[mx].burstTime - Mylist[mx].ReminingTime < step)
                        Mylist[mx].WaitingTime = time - Mylist[mx].arrivalTime;
                    if (time + Mylist[mx].ReminingTime < mn)
                    {
                        mn = time + Mylist[mx].ReminingTime;
                        Mylist[mx].ReminingTime = 0.00001;
                        Mylist[mx].FinishTime = mn;
                        count++;
                    }
                    else
                        Mylist[mx].ReminingTime -= mn - time;
                    nslide.end = mn;
                    time = mn;
                    Final.Add(nslide);
                    last = mx;
                }
            }
            time = PTQ(time);
        }
        public void FCFS() //first come first serve queue and add all process to it , start time of any one = max(finish time of the last +context time,arrival time)
        {
            if (Final == null || Mylist == null)
                return;
            Double time = 0.0;
            for (int i = 0; i < number; i++)
            {
                slides nslid = new slides();
                nslid.ID = Mylist[i].ID;
                if (time < Mylist[i].arrivalTime)
                    time = Mylist[i].arrivalTime;
                nslid.start = time;
                Mylist[i].WaitingTime = time - Mylist[i].arrivalTime;
                time += Mylist[i].burstTime;
                Mylist[i].FinishTime = time;
                nslid.end = time;
                Final.Add(nslid);
                time = PTQ(time);
            }

        }
        public void RR() //Round Robin with fixed time quantum
        {
            if (Final == null || Mylist == null)
                return;
            int count=0;
            Double time = 0.0;
            while (count < Mylist.Count)
            {
                Double mn = 10000.0;
                //any process that already arrive will take min(TimeQuantum,ReminingTime)  
                for (int i = 0; i < number; i++)
                {
                    if (Mylist[i].ReminingTime < step) continue;
                    if (Mylist[i].arrivalTime < mn) mn = Mylist[i].arrivalTime;
                    if (Mylist[i].arrivalTime > time) break;
                    if (Mylist[i].burstTime - Mylist[i].ReminingTime < step) Mylist[i].WaitingTime = time-Mylist[i].arrivalTime;
                    slides nslide = new slides();
                    nslide.ID = Mylist[i].ID;
                    nslide.start = time;
                    if (Mylist[i].ReminingTime > TimeQuantum)
                    {
                        Mylist[i].ReminingTime -= TimeQuantum;
                        time += TimeQuantum;
                    }
                    else
                    {
                        time += Mylist[i].ReminingTime;
                        Mylist[i].ReminingTime=0.00001;
                        Mylist[i].FinishTime = time;
                        count++;
                    }
                    nslide.end = time;
                    Final.Add(nslide);
                    time = PTQ(time);
                }
                if (mn > time)
                {
                    slides nslide = new slides();
                    nslide.ID = 0;
                    nslide.start = time;
                    nslide.end = mn;
                    time = mn;
                    Final.Add(nslide);
                }
            }
           
        }
        
        public bool Valid() //program will stop if any missing or wrong data
        {
            Double n;
            if (FileName.Text == null || Context.Text == null || string.IsNullOrEmpty(Algo.Text) || Algo.SelectedIndex == -1 ||output.Text==null)
                return false;
            if (Algo.Text == "RR") //if RR has been choosen check for time quantum
            {
                if(Time.Text == null)
                    return false;
                if (!(Double.TryParse(Time.Text, out n)))
                    return false;
                if (Double.Parse(Time.Text) < step)
                    return false;
            }
            if (!(Double.TryParse(Context.Text, out n)))
                return false;
            if (!System.IO.File.Exists(@FileName.Text) || !System.IO.File.Exists(@output.Text))
                return false;
            string[] lines = System.IO.File.ReadAllLines(@FileName.Text);
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (!Valid())
                MessageBox.Show("missing or wrong data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                
                //*if no "." in the number add it , C# will make it 0 if there are no ".'
                bool test = true;
                for (int i = 0; i < Time.Text.Length; i++)
                {
                    if (Time.Text[i] == '.')
                        test = false;
                }
                if (test)
                    Time.Text += ".";
                test = true;
                for (int i = 0; i < Context.Text.Length; i++)
                {
                    if (Context.Text[i] == '.')
                        test = false;
                }
                if (test)
                    Context.Text += ".";
                //*
                string[] lines = System.IO.File.ReadAllLines(@FileName.Text);//read data from file
                //assume all data is valid on folder
                //initalize data
                TimeQuantum = Double.Parse(Time.Text);
                ContextSwitching = Double.Parse(Context.Text);
                if(Mylist!=null)
                    Mylist.Clear();
                if(Final!=null)
                    Final.Clear();
                Mylist = new List<Process>();
                Final = new List<slides>();
                number = int.Parse(lines[0]);
                for (int i = 1; i <= number; i++)
                {
                    Double[] values = lines[i].Split(' ').Select(Double.Parse).ToArray();
                    Process curr=new Process();
                    curr.ID=Convert.ToInt32(values[0]);
                    //if any value less than zero take abslute
                    if (values[1] < step)
                        values[1] *= -1;
                    if (values[2] < step)
                        values[2] *= -1;
                    curr.arrivalTime=values[1];
                    curr.burstTime=curr.ReminingTime=values[2];
                    curr.priority = Convert.ToInt32(values[3]);
                    curr.WaitingTime = curr.FinishTime = -1.0;
                    Mylist.Add(curr);
                }
                Mylist.Sort((x, y) => x.arrivalTime.CompareTo(y.arrivalTime));
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
                System.IO.File.WriteAllText(@output.Text, line);
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
            {
                if (Final[i].end - Final[i].start < step)
                    continue;
                this.chart1.Series["process"].Points.AddXY(Final[i].start, Final[i].ID);
                this.chart1.Series["process"].Points.AddXY(Final[i].end, Final[i].ID);
            }
        }

        private void output_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
