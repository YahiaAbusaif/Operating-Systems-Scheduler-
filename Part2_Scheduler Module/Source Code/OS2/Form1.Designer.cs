namespace OS2
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.Algo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Time = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Context = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.FileName = new System.Windows.Forms.TextBox();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label5 = new System.Windows.Forms.Label();
            this.output = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(766, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(145, 17);
            this.label4.TabIndex = 18;
            this.label4.Text = "scheduling algorithms";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1451, 14);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(76, 41);
            this.button1.TabIndex = 17;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Algo
            // 
            this.Algo.FormattingEnabled = true;
            this.Algo.Location = new System.Drawing.Point(920, 23);
            this.Algo.Name = "Algo";
            this.Algo.Size = new System.Drawing.Size(80, 24);
            this.Algo.TabIndex = 16;
            this.Algo.SelectedIndexChanged += new System.EventHandler(this.Algo_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(588, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 17);
            this.label3.TabIndex = 15;
            this.label3.Text = "Time Quantum";
            // 
            // Time
            // 
            this.Time.Location = new System.Drawing.Point(693, 23);
            this.Time.Name = "Time";
            this.Time.Size = new System.Drawing.Size(64, 22);
            this.Time.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(409, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 17);
            this.label2.TabIndex = 13;
            this.label2.Text = "Context Switching";
            // 
            // Context
            // 
            this.Context.Location = new System.Drawing.Point(533, 23);
            this.Context.Name = "Context";
            this.Context.Size = new System.Drawing.Size(52, 22);
            this.Context.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 17);
            this.label1.TabIndex = 11;
            this.label1.Text = "file name";
            // 
            // FileName
            // 
            this.FileName.Location = new System.Drawing.Point(73, 23);
            this.FileName.Name = "FileName";
            this.FileName.Size = new System.Drawing.Size(330, 22);
            this.FileName.TabIndex = 10;
            // 
            // chart1
            // 
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(15, 72);
            this.chart1.Name = "chart1";
            series2.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            series2.BackImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            series2.BackSecondaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            series2.BorderColor = System.Drawing.Color.Red;
            series2.BorderWidth = 3;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StepLine;
            series2.Color = System.Drawing.Color.Red;
            series2.Legend = "Legend1";
            series2.Name = "process";
            series2.ShadowColor = System.Drawing.Color.Lime;
            series2.YValuesPerPoint = 4;
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(1584, 713);
            this.chart1.TabIndex = 19;
            this.chart1.Text = "chart1";
            this.chart1.Click += new System.EventHandler(this.chart1_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1006, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 17);
            this.label5.TabIndex = 22;
            this.label5.Text = "output path";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // output
            // 
            this.output.Location = new System.Drawing.Point(1092, 23);
            this.output.Name = "output";
            this.output.Size = new System.Drawing.Size(353, 22);
            this.output.TabIndex = 21;
            this.output.TextChanged += new System.EventHandler(this.output_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1538, 797);
            this.Controls.Add(this.Algo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.output);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Time);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Context);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FileName);
            this.Name = "Form1";
            this.Text = "Scheduler Module";
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox Algo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Time;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Context;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox FileName;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox output;
    }
}

