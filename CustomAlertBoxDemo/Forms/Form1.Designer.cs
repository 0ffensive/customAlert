﻿namespace CustomAlertBoxDemo.Forms
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
            this.components = new System.ComponentModel.Container();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lblCounter = new System.Windows.Forms.Label();
            this.lblCheck = new System.Windows.Forms.Label();
            this.grpTimer = new System.Windows.Forms.GroupBox();
            this.btnTimer = new System.Windows.Forms.Button();
            this.timePickerTo = new System.Windows.Forms.DateTimePicker();
            this.timePickerFrom = new System.Windows.Forms.DateTimePicker();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.grpLokacja = new System.Windows.Forms.GroupBox();
            this.chbEdynburg = new System.Windows.Forms.CheckBox();
            this.chbManchester = new System.Windows.Forms.CheckBox();
            this.txbFrequency = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.grpTimer.SuspendLayout();
            this.grpLokacja.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lblCounter);
            this.splitContainer1.Panel1.Controls.Add(this.lblCheck);
            this.splitContainer1.Panel1.Controls.Add(this.grpTimer);
            this.splitContainer1.Panel1.Controls.Add(this.button3);
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1.Controls.Add(this.grpLokacja);
            this.splitContainer1.Panel1.Controls.Add(this.txbFrequency);
            this.splitContainer1.Panel1.Controls.Add(this.button4);
            this.splitContainer1.Panel1.Controls.Add(this.button2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.progressBar1);
            this.splitContainer1.Panel2.Controls.Add(this.textBox1);
            this.splitContainer1.Size = new System.Drawing.Size(917, 477);
            this.splitContainer1.SplitterDistance = 291;
            this.splitContainer1.TabIndex = 2;
            // 
            // lblCounter
            // 
            this.lblCounter.AutoSize = true;
            this.lblCounter.ForeColor = System.Drawing.Color.Red;
            this.lblCounter.Location = new System.Drawing.Point(217, 13);
            this.lblCounter.Name = "lblCounter";
            this.lblCounter.Size = new System.Drawing.Size(28, 21);
            this.lblCounter.TabIndex = 5;
            this.lblCounter.Text = "20";
            // 
            // lblCheck
            // 
            this.lblCheck.AutoSize = true;
            this.lblCheck.Location = new System.Drawing.Point(12, 13);
            this.lblCheck.Name = "lblCheck";
            this.lblCheck.Size = new System.Drawing.Size(108, 21);
            this.lblCheck.TabIndex = 2;
            this.lblCheck.Text = "Check every";
            // 
            // grpTimer
            // 
            this.grpTimer.Controls.Add(this.btnTimer);
            this.grpTimer.Controls.Add(this.timePickerTo);
            this.grpTimer.Controls.Add(this.timePickerFrom);
            this.grpTimer.Location = new System.Drawing.Point(16, 180);
            this.grpTimer.Name = "grpTimer";
            this.grpTimer.Size = new System.Drawing.Size(200, 131);
            this.grpTimer.TabIndex = 4;
            this.grpTimer.TabStop = false;
            this.grpTimer.Text = "Timer";
            // 
            // btnTimer
            // 
            this.btnTimer.Location = new System.Drawing.Point(7, 79);
            this.btnTimer.Name = "btnTimer";
            this.btnTimer.Size = new System.Drawing.Size(187, 35);
            this.btnTimer.TabIndex = 10;
            this.btnTimer.Text = "Start timer";
            this.btnTimer.UseVisualStyleBackColor = true;
            this.btnTimer.Click += new System.EventHandler(this.btnTimer_Click);
            // 
            // timePickerTo
            // 
            this.timePickerTo.CustomFormat = "HH:mm";
            this.timePickerTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.timePickerTo.Location = new System.Drawing.Point(118, 31);
            this.timePickerTo.Name = "timePickerTo";
            this.timePickerTo.ShowUpDown = true;
            this.timePickerTo.Size = new System.Drawing.Size(76, 27);
            this.timePickerTo.TabIndex = 9;
            this.timePickerTo.Value = new System.DateTime(2020, 7, 28, 9, 20, 0, 0);
            // 
            // timePickerFrom
            // 
            this.timePickerFrom.CustomFormat = "HH:mm";
            this.timePickerFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.timePickerFrom.Location = new System.Drawing.Point(7, 31);
            this.timePickerFrom.Name = "timePickerFrom";
            this.timePickerFrom.ShowUpDown = true;
            this.timePickerFrom.Size = new System.Drawing.Size(80, 27);
            this.timePickerFrom.TabIndex = 8;
            this.timePickerFrom.Value = new System.DateTime(2020, 7, 28, 8, 45, 0, 0);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.DarkRed;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(11, 352);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(119, 58);
            this.button3.TabIndex = 0;
            this.button3.Text = "Error";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.SeaGreen;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(11, 416);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(119, 58);
            this.button1.TabIndex = 0;
            this.button1.Text = "Success";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // grpLokacja
            // 
            this.grpLokacja.Controls.Add(this.chbEdynburg);
            this.grpLokacja.Controls.Add(this.chbManchester);
            this.grpLokacja.Location = new System.Drawing.Point(16, 50);
            this.grpLokacja.Name = "grpLokacja";
            this.grpLokacja.Size = new System.Drawing.Size(195, 114);
            this.grpLokacja.TabIndex = 3;
            this.grpLokacja.TabStop = false;
            this.grpLokacja.Text = "Lokacja";
            // 
            // chbEdynburg
            // 
            this.chbEdynburg.AutoSize = true;
            this.chbEdynburg.Location = new System.Drawing.Point(7, 66);
            this.chbEdynburg.Name = "chbEdynburg";
            this.chbEdynburg.Size = new System.Drawing.Size(104, 25);
            this.chbEdynburg.TabIndex = 1;
            this.chbEdynburg.Text = "Edynburg";
            this.chbEdynburg.UseVisualStyleBackColor = true;
            this.chbEdynburg.CheckedChanged += new System.EventHandler(this.chk_Click);
            // 
            // chbManchester
            // 
            this.chbManchester.AutoSize = true;
            this.chbManchester.Checked = true;
            this.chbManchester.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbManchester.Cursor = System.Windows.Forms.Cursors.Default;
            this.chbManchester.Location = new System.Drawing.Point(7, 32);
            this.chbManchester.Name = "chbManchester";
            this.chbManchester.Size = new System.Drawing.Size(123, 25);
            this.chbManchester.TabIndex = 0;
            this.chbManchester.Text = "Manchester";
            this.chbManchester.UseVisualStyleBackColor = true;
            this.chbManchester.CheckedChanged += new System.EventHandler(this.chk_Click);
            // 
            // txbFrequency
            // 
            this.txbFrequency.Location = new System.Drawing.Point(155, 10);
            this.txbFrequency.Name = "txbFrequency";
            this.txbFrequency.Size = new System.Drawing.Size(56, 27);
            this.txbFrequency.TabIndex = 1;
            this.txbFrequency.Text = "20";
            this.txbFrequency.Leave += new System.EventHandler(this.txbFrequency_Leave);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.RoyalBlue;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(136, 416);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(119, 58);
            this.button4.TabIndex = 0;
            this.button4.Text = "info";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.OrangeRed;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(136, 352);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(119, 58);
            this.button2.TabIndex = 0;
            this.button2.Text = "Warning";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(17, 13);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(593, 23);
            this.progressBar1.TabIndex = 1;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(17, 50);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textBox1.Size = new System.Drawing.Size(593, 415);
            this.textBox1.TabIndex = 0;
            // 
            // timer2
            // 
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(917, 477);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.grpTimer.ResumeLayout(false);
            this.grpLokacja.ResumeLayout(false);
            this.grpLokacja.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox chbEdynburg;
        private System.Windows.Forms.CheckBox chbManchester;
        private System.Windows.Forms.Label lblCheck;
        private System.Windows.Forms.TextBox txbFrequency;
        private System.Windows.Forms.DateTimePicker timePickerFrom;
        private System.Windows.Forms.GroupBox grpTimer;
        private System.Windows.Forms.DateTimePicker timePickerTo;
        private System.Windows.Forms.Button btnTimer;
        private System.Windows.Forms.GroupBox grpLokacja;
        private System.Windows.Forms.Label lblCounter;
        private System.Windows.Forms.Timer timer2;
    }
}

