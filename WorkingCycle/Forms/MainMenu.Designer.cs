namespace DutyCycle.Forms
{
    partial class MainMenu
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenu));
            panel1 = new Panel();
            pbMinimize = new PictureBox();
            pbClose = new PictureBox();
            pictureBox1 = new PictureBox();
            label4 = new Label();
            label2 = new Label();
            label3 = new Label();
            label1 = new Label();
            flowLayoutPanel1 = new FlowLayoutPanel();
            panel4 = new Panel();
            btnDutyCycle = new Button();
            panel5 = new Panel();
            btnAdjustments = new Button();
            panel6 = new Panel();
            btnReference = new Button();
            panel9 = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            panel13 = new Panel();
            pbNeg3 = new PictureBox();
            lblPos3 = new Label();
            lblNeg3 = new Label();
            tbCmdPos3 = new TextBox();
            pbPos3 = new PictureBox();
            panel10 = new Panel();
            lblNeg0 = new Label();
            tbCmdPos0 = new TextBox();
            lblPos0 = new Label();
            pbPos0 = new PictureBox();
            pbNeg0 = new PictureBox();
            panel11 = new Panel();
            lblNeg1 = new Label();
            lblPos1 = new Label();
            tbCmdPos1 = new TextBox();
            pbPos1 = new PictureBox();
            pbNeg1 = new PictureBox();
            panel12 = new Panel();
            lblPos2 = new Label();
            lblNeg2 = new Label();
            tbCmdPos2 = new TextBox();
            pbPos2 = new PictureBox();
            pbNeg2 = new PictureBox();
            posTimer = new System.Windows.Forms.Timer(components);
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbMinimize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbClose).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            flowLayoutPanel1.SuspendLayout();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            panel6.SuspendLayout();
            panel9.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            panel13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbNeg3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbPos3).BeginInit();
            panel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbPos0).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbNeg0).BeginInit();
            panel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbPos1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbNeg1).BeginInit();
            panel12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbPos2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbNeg2).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(pbMinimize);
            panel1.Controls.Add(pbClose);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1280, 84);
            panel1.TabIndex = 0;
            // 
            // pbMinimize
            // 
            pbMinimize.Image = (Image)resources.GetObject("pbMinimize.Image");
            pbMinimize.Location = new Point(1211, 0);
            pbMinimize.Name = "pbMinimize";
            pbMinimize.Size = new Size(30, 30);
            pbMinimize.SizeMode = PictureBoxSizeMode.StretchImage;
            pbMinimize.TabIndex = 7;
            pbMinimize.TabStop = false;
            pbMinimize.Click += pbMinimize_Click;
            // 
            // pbClose
            // 
            pbClose.Image = (Image)resources.GetObject("pbClose.Image");
            pbClose.Location = new Point(1247, 0);
            pbClose.Name = "pbClose";
            pbClose.Size = new Size(30, 30);
            pbClose.SizeMode = PictureBoxSizeMode.StretchImage;
            pbClose.TabIndex = 6;
            pbClose.TabStop = false;
            pbClose.Click += pbClose_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.White;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(786, 0);
            pictureBox1.Margin = new Padding(0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(88, 85);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.White;
            label4.Font = new Font("Roboto Light", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label4.ForeColor = Color.FromArgb(143, 143, 143);
            label4.Location = new Point(882, 62);
            label4.Name = "label4";
            label4.Size = new Size(307, 13);
            label4.TabIndex = 5;
            label4.Text = "СБОРОЧНОЕ ОБОРУДОВАНИЕ ДЛЯ МИКРОЭЛЕКТРОНИКИ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.White;
            label2.Font = new Font("Roboto Light", 12F);
            label2.ForeColor = Color.FromArgb(143, 143, 143);
            label2.Location = new Point(256, 14);
            label2.Name = "label2";
            label2.Size = new Size(105, 57);
            label2.TabIndex = 5;
            label2.Text = "УСТАНОВКА \r\nКОНТРОЛЯ\r\nПРОЧНОСТИ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.White;
            label3.Font = new Font("Oswald SemiBold", 47.9999962F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label3.ForeColor = Color.FromArgb(13, 30, 100);
            label3.Location = new Point(860, -24);
            label3.Name = "label3";
            label3.Size = new Size(359, 109);
            label3.TabIndex = 5;
            label3.Text = "ПЛАНАР-СО";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.White;
            label1.Font = new Font("Oswald SemiBold", 48F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.ForeColor = Color.FromArgb(13, 30, 100);
            label1.Location = new Point(0, -17);
            label1.Name = "label1";
            label1.Size = new Size(275, 109);
            label1.TabIndex = 5;
            label1.Text = "ЭМ-6705";
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.BackColor = SystemColors.ControlDark;
            flowLayoutPanel1.BorderStyle = BorderStyle.FixedSingle;
            flowLayoutPanel1.Controls.Add(panel4);
            flowLayoutPanel1.Controls.Add(panel5);
            flowLayoutPanel1.Controls.Add(panel6);
            flowLayoutPanel1.Dock = DockStyle.Left;
            flowLayoutPanel1.Location = new Point(0, 84);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(90, 940);
            flowLayoutPanel1.TabIndex = 1;
            // 
            // panel4
            // 
            panel4.BorderStyle = BorderStyle.FixedSingle;
            panel4.Controls.Add(btnDutyCycle);
            panel4.Location = new Point(0, 0);
            panel4.Margin = new Padding(0);
            panel4.Name = "panel4";
            panel4.Size = new Size(90, 125);
            panel4.TabIndex = 3;
            // 
            // btnDutyCycle
            // 
            btnDutyCycle.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnDutyCycle.Image = (Image)resources.GetObject("btnDutyCycle.Image");
            btnDutyCycle.ImageAlign = ContentAlignment.TopCenter;
            btnDutyCycle.Location = new Point(-22, -6);
            btnDutyCycle.Name = "btnDutyCycle";
            btnDutyCycle.Padding = new Padding(0, 8, 0, 0);
            btnDutyCycle.Size = new Size(122, 190);
            btnDutyCycle.TabIndex = 0;
            btnDutyCycle.Text = " Рабочий\r\n  цикл";
            btnDutyCycle.UseVisualStyleBackColor = true;
            btnDutyCycle.Click += btnDutyCycle_Click;
            // 
            // panel5
            // 
            panel5.BorderStyle = BorderStyle.FixedSingle;
            panel5.Controls.Add(btnAdjustments);
            panel5.Location = new Point(0, 125);
            panel5.Margin = new Padding(0);
            panel5.Name = "panel5";
            panel5.Size = new Size(90, 125);
            panel5.TabIndex = 5;
            // 
            // btnAdjustments
            // 
            btnAdjustments.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnAdjustments.Image = (Image)resources.GetObject("btnAdjustments.Image");
            btnAdjustments.ImageAlign = ContentAlignment.TopCenter;
            btnAdjustments.Location = new Point(-22, -6);
            btnAdjustments.Name = "btnAdjustments";
            btnAdjustments.Padding = new Padding(0, 8, 0, 0);
            btnAdjustments.Size = new Size(122, 183);
            btnAdjustments.TabIndex = 0;
            btnAdjustments.Text = "  Наладка";
            btnAdjustments.UseVisualStyleBackColor = true;
            btnAdjustments.Click += btnAdjustments_Click;
            // 
            // panel6
            // 
            panel6.BorderStyle = BorderStyle.FixedSingle;
            panel6.Controls.Add(btnReference);
            panel6.Location = new Point(0, 250);
            panel6.Margin = new Padding(0);
            panel6.Name = "panel6";
            panel6.Size = new Size(90, 125);
            panel6.TabIndex = 4;
            panel6.Visible = false;
            // 
            // btnReference
            // 
            btnReference.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnReference.Image = (Image)resources.GetObject("btnReference.Image");
            btnReference.ImageAlign = ContentAlignment.TopCenter;
            btnReference.Location = new Point(-20, -6);
            btnReference.Name = "btnReference";
            btnReference.Padding = new Padding(0, 8, 0, 0);
            btnReference.Size = new Size(128, 185);
            btnReference.TabIndex = 0;
            btnReference.Text = "Справка";
            btnReference.UseVisualStyleBackColor = true;
            // 
            // panel9
            // 
            panel9.BorderStyle = BorderStyle.FixedSingle;
            panel9.Controls.Add(tableLayoutPanel1);
            panel9.Dock = DockStyle.Bottom;
            panel9.Location = new Point(90, 980);
            panel9.Name = "panel9";
            panel9.Size = new Size(1190, 44);
            panel9.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.Controls.Add(panel13, 3, 0);
            tableLayoutPanel1.Controls.Add(panel10, 0, 0);
            tableLayoutPanel1.Controls.Add(panel11, 1, 0);
            tableLayoutPanel1.Controls.Add(panel12, 2, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(1188, 42);
            tableLayoutPanel1.TabIndex = 5;
            // 
            // panel13
            // 
            panel13.Controls.Add(pbNeg3);
            panel13.Controls.Add(lblPos3);
            panel13.Controls.Add(lblNeg3);
            panel13.Controls.Add(tbCmdPos3);
            panel13.Controls.Add(pbPos3);
            panel13.Dock = DockStyle.Fill;
            panel13.Enabled = false;
            panel13.Location = new Point(889, 1);
            panel13.Margin = new Padding(0);
            panel13.Name = "panel13";
            panel13.Size = new Size(298, 40);
            panel13.TabIndex = 5;
            // 
            // pbNeg3
            // 
            pbNeg3.BackColor = SystemColors.ScrollBar;
            pbNeg3.BorderStyle = BorderStyle.FixedSingle;
            pbNeg3.Location = new Point(45, 5);
            pbNeg3.Name = "pbNeg3";
            pbNeg3.Size = new Size(32, 32);
            pbNeg3.TabIndex = 4;
            pbNeg3.TabStop = false;
            // 
            // lblPos3
            // 
            lblPos3.AutoSize = true;
            lblPos3.Enabled = false;
            lblPos3.Font = new Font("Segoe UI", 13F);
            lblPos3.Location = new Point(226, 8);
            lblPos3.Name = "lblPos3";
            lblPos3.Size = new Size(37, 25);
            lblPos3.TabIndex = 3;
            lblPos3.Text = "+φ";
            // 
            // lblNeg3
            // 
            lblNeg3.AutoSize = true;
            lblNeg3.Font = new Font("Segoe UI", 13F);
            lblNeg3.Location = new Point(9, 8);
            lblNeg3.Name = "lblNeg3";
            lblNeg3.Size = new Size(32, 25);
            lblNeg3.TabIndex = 3;
            lblNeg3.Text = "-φ";
            // 
            // tbCmdPos3
            // 
            tbCmdPos3.Font = new Font("Segoe UI", 14F);
            tbCmdPos3.Location = new Point(83, 5);
            tbCmdPos3.Name = "tbCmdPos3";
            tbCmdPos3.ReadOnly = true;
            tbCmdPos3.Size = new Size(100, 32);
            tbCmdPos3.TabIndex = 2;
            // 
            // pbPos3
            // 
            pbPos3.BackColor = SystemColors.ScrollBar;
            pbPos3.BorderStyle = BorderStyle.FixedSingle;
            pbPos3.Enabled = false;
            pbPos3.Location = new Point(189, 5);
            pbPos3.Name = "pbPos3";
            pbPos3.Size = new Size(32, 32);
            pbPos3.TabIndex = 4;
            pbPos3.TabStop = false;
            // 
            // panel10
            // 
            panel10.Controls.Add(lblNeg0);
            panel10.Controls.Add(tbCmdPos0);
            panel10.Controls.Add(lblPos0);
            panel10.Controls.Add(pbPos0);
            panel10.Controls.Add(pbNeg0);
            panel10.Dock = DockStyle.Fill;
            panel10.Location = new Point(1, 1);
            panel10.Margin = new Padding(0);
            panel10.Name = "panel10";
            panel10.Size = new Size(295, 40);
            panel10.TabIndex = 5;
            // 
            // lblNeg0
            // 
            lblNeg0.AutoSize = true;
            lblNeg0.Font = new Font("Segoe UI", 13F);
            lblNeg0.Location = new Point(6, 10);
            lblNeg0.Name = "lblNeg0";
            lblNeg0.Size = new Size(30, 25);
            lblNeg0.TabIndex = 3;
            lblNeg0.Text = "-X";
            // 
            // tbCmdPos0
            // 
            tbCmdPos0.Font = new Font("Segoe UI", 14F);
            tbCmdPos0.Location = new Point(80, 5);
            tbCmdPos0.Margin = new Padding(3, 7, 3, 3);
            tbCmdPos0.Name = "tbCmdPos0";
            tbCmdPos0.ReadOnly = true;
            tbCmdPos0.Size = new Size(100, 32);
            tbCmdPos0.TabIndex = 2;
            // 
            // lblPos0
            // 
            lblPos0.AutoSize = true;
            lblPos0.Font = new Font("Segoe UI", 13F);
            lblPos0.Location = new Point(225, 10);
            lblPos0.Name = "lblPos0";
            lblPos0.Size = new Size(35, 25);
            lblPos0.TabIndex = 3;
            lblPos0.Text = "+X";
            // 
            // pbPos0
            // 
            pbPos0.BackColor = SystemColors.ScrollBar;
            pbPos0.BorderStyle = BorderStyle.FixedSingle;
            pbPos0.Location = new Point(187, 5);
            pbPos0.Name = "pbPos0";
            pbPos0.Size = new Size(32, 32);
            pbPos0.TabIndex = 4;
            pbPos0.TabStop = false;
            // 
            // pbNeg0
            // 
            pbNeg0.BackColor = SystemColors.ScrollBar;
            pbNeg0.BorderStyle = BorderStyle.FixedSingle;
            pbNeg0.Location = new Point(42, 5);
            pbNeg0.Name = "pbNeg0";
            pbNeg0.Size = new Size(32, 32);
            pbNeg0.TabIndex = 4;
            pbNeg0.TabStop = false;
            // 
            // panel11
            // 
            panel11.Controls.Add(lblNeg1);
            panel11.Controls.Add(lblPos1);
            panel11.Controls.Add(tbCmdPos1);
            panel11.Controls.Add(pbPos1);
            panel11.Controls.Add(pbNeg1);
            panel11.Dock = DockStyle.Fill;
            panel11.Location = new Point(297, 1);
            panel11.Margin = new Padding(0);
            panel11.Name = "panel11";
            panel11.Size = new Size(295, 40);
            panel11.TabIndex = 5;
            // 
            // lblNeg1
            // 
            lblNeg1.AutoSize = true;
            lblNeg1.Font = new Font("Segoe UI", 13F);
            lblNeg1.Location = new Point(4, 10);
            lblNeg1.Name = "lblNeg1";
            lblNeg1.Size = new Size(29, 25);
            lblNeg1.TabIndex = 3;
            lblNeg1.Text = "-Y";
            // 
            // lblPos1
            // 
            lblPos1.AutoSize = true;
            lblPos1.Font = new Font("Segoe UI", 13F);
            lblPos1.Location = new Point(221, 10);
            lblPos1.Name = "lblPos1";
            lblPos1.Size = new Size(34, 25);
            lblPos1.TabIndex = 3;
            lblPos1.Text = "+Y";
            // 
            // tbCmdPos1
            // 
            tbCmdPos1.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 204);
            tbCmdPos1.Location = new Point(77, 5);
            tbCmdPos1.Name = "tbCmdPos1";
            tbCmdPos1.ReadOnly = true;
            tbCmdPos1.Size = new Size(100, 32);
            tbCmdPos1.TabIndex = 2;
            // 
            // pbPos1
            // 
            pbPos1.BackColor = SystemColors.ScrollBar;
            pbPos1.BorderStyle = BorderStyle.FixedSingle;
            pbPos1.Location = new Point(183, 5);
            pbPos1.Name = "pbPos1";
            pbPos1.Size = new Size(32, 32);
            pbPos1.TabIndex = 4;
            pbPos1.TabStop = false;
            // 
            // pbNeg1
            // 
            pbNeg1.BackColor = SystemColors.ScrollBar;
            pbNeg1.BorderStyle = BorderStyle.FixedSingle;
            pbNeg1.Location = new Point(39, 5);
            pbNeg1.Name = "pbNeg1";
            pbNeg1.Size = new Size(32, 32);
            pbNeg1.TabIndex = 4;
            pbNeg1.TabStop = false;
            // 
            // panel12
            // 
            panel12.Controls.Add(lblPos2);
            panel12.Controls.Add(lblNeg2);
            panel12.Controls.Add(tbCmdPos2);
            panel12.Controls.Add(pbPos2);
            panel12.Controls.Add(pbNeg2);
            panel12.Dock = DockStyle.Fill;
            panel12.Location = new Point(593, 1);
            panel12.Margin = new Padding(0);
            panel12.Name = "panel12";
            panel12.Size = new Size(295, 40);
            panel12.TabIndex = 5;
            // 
            // lblPos2
            // 
            lblPos2.AutoSize = true;
            lblPos2.Enabled = false;
            lblPos2.Font = new Font("Segoe UI", 13F);
            lblPos2.Location = new Point(220, 10);
            lblPos2.Name = "lblPos2";
            lblPos2.Size = new Size(34, 25);
            lblPos2.TabIndex = 3;
            lblPos2.Text = "+Z";
            // 
            // lblNeg2
            // 
            lblNeg2.AutoSize = true;
            lblNeg2.Font = new Font("Segoe UI", 13F);
            lblNeg2.Location = new Point(3, 10);
            lblNeg2.Name = "lblNeg2";
            lblNeg2.Size = new Size(29, 25);
            lblNeg2.TabIndex = 3;
            lblNeg2.Text = "-Z";
            // 
            // tbCmdPos2
            // 
            tbCmdPos2.Font = new Font("Segoe UI", 14F);
            tbCmdPos2.Location = new Point(76, 5);
            tbCmdPos2.Name = "tbCmdPos2";
            tbCmdPos2.ReadOnly = true;
            tbCmdPos2.Size = new Size(100, 32);
            tbCmdPos2.TabIndex = 2;
            // 
            // pbPos2
            // 
            pbPos2.BackColor = SystemColors.ScrollBar;
            pbPos2.BorderStyle = BorderStyle.FixedSingle;
            pbPos2.Enabled = false;
            pbPos2.Location = new Point(182, 5);
            pbPos2.Name = "pbPos2";
            pbPos2.Size = new Size(32, 32);
            pbPos2.TabIndex = 4;
            pbPos2.TabStop = false;
            // 
            // pbNeg2
            // 
            pbNeg2.BackColor = SystemColors.ScrollBar;
            pbNeg2.BorderStyle = BorderStyle.FixedSingle;
            pbNeg2.Location = new Point(38, 5);
            pbNeg2.Name = "pbNeg2";
            pbNeg2.Size = new Size(32, 32);
            pbNeg2.TabIndex = 4;
            pbNeg2.TabStop = false;
            // 
            // posTimer
            // 
            posTimer.Interval = 50;
            posTimer.Tick += posTimer_Tick;
            // 
            // MainMenu
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1280, 1024);
            Controls.Add(panel9);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            IsMdiContainer = true;
            Name = "MainMenu";
            Text = "MainMenu";
            FormClosed += MainMenu_FormClosed;
            Load += MainMenu_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbMinimize).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbClose).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            flowLayoutPanel1.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel6.ResumeLayout(false);
            panel9.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            panel13.ResumeLayout(false);
            panel13.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbNeg3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbPos3).EndInit();
            panel10.ResumeLayout(false);
            panel10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbPos0).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbNeg0).EndInit();
            panel11.ResumeLayout(false);
            panel11.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbPos1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbNeg1).EndInit();
            panel12.ResumeLayout(false);
            panel12.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbPos2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbNeg2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private FlowLayoutPanel flowLayoutPanel1;
        private Panel panel9;
        private Label lblNeg2;
        private Label lblPos1;
        private Label lblNeg1;
        private Label lblPos0;
        private Label lblNeg0;
        private TextBox tbCmdPos3;
        private TextBox tbCmdPos2;
        private TextBox tbCmdPos1;
        private TextBox tbCmdPos0;
        private PictureBox pbNeg3;
        private PictureBox pbNeg2;
        private PictureBox pbPos3;
        private PictureBox pbPos2;
        private PictureBox pbNeg1;
        private PictureBox pbPos1;
        private PictureBox pbNeg0;
        private PictureBox pbPos0;
        private Label lblPos3;
        private Label lblNeg3;
        private Label lblPos2;
        private Panel panel13;
        private Panel panel10;
        private Panel panel11;
        private Panel panel12;
        private System.Windows.Forms.Timer posTimer;
        private Label label1;
        private Label label2;
        private Label label4;
        private Label label3;
        private PictureBox pictureBox1;
        private PictureBox pbClose;
        private PictureBox pbMinimize;
        private TableLayoutPanel tableLayoutPanel1;
        private Button btnDutyCycle;
        private Panel panel4;
        private Panel panel5;
        private Button btnAdjustments;
        private Panel panel6;
        private Button btnReference;
    }
}