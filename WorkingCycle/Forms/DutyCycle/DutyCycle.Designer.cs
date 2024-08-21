﻿namespace DutyCycle.Forms.DutyCycle
{
    partial class DutyCycleForm
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
            testTimer = new System.Windows.Forms.Timer(components);
            testPlot = new ScottPlot.WinForms.FormsPlot();
            tableLayoutPanel1 = new TableLayoutPanel();
            panel1 = new Panel();
            tbForceData = new TextBox();
            pictureBox1 = new PictureBox();
            operatorPanel = new Panel();
            groupBox2 = new GroupBox();
            groupBox6 = new GroupBox();
            btnStartTest = new Button();
            btnMoveToStart = new Button();
            btnHome = new Button();
            gpTestPoint = new GroupBox();
            lblTestPointCaption0 = new Label();
            lblTestPointCaption1 = new Label();
            lblTestPoint2 = new Label();
            btnSetTestPoint = new Button();
            lblTestPoint0 = new Label();
            lblTestPoint1 = new Label();
            lblTestPointCaption2 = new Label();
            gbMagnets = new GroupBox();
            btnLockUpperBond = new Button();
            pbOut7 = new PictureBox();
            btnLockLowerBond = new Button();
            pbOut6 = new PictureBox();
            gpForceData = new GroupBox();
            rtbTestValues = new RichTextBox();
            lblTestResult = new Label();
            lblTestValues = new Label();
            tbTestResult = new TextBox();
            groupBox1 = new GroupBox();
            lblReferenceLoad = new Label();
            btnTare = new Button();
            btnCalibrate = new Button();
            cmbReferenceWeights = new ComboBox();
            label1 = new Label();
            groupBox3 = new GroupBox();
            groupBox5 = new GroupBox();
            nudBreakDelay = new NumericUpDown();
            gbForceBound = new GroupBox();
            cbBoundSet = new CheckBox();
            nudLoadBound = new NumericUpDown();
            gbTestType = new GroupBox();
            rbBreakTest = new RadioButton();
            rbStretchTest = new RadioButton();
            rbShearTest = new RadioButton();
            gbTestVelocity = new GroupBox();
            lblTestSpeed = new Label();
            btnSaveTestVelocity = new Button();
            nudTestSpeed = new NumericUpDown();
            btnLoadTestVelocity = new Button();
            groupBox4 = new GroupBox();
            btnSaveTests = new Button();
            btnLoadTests = new Button();
            cmbTests = new ComboBox();
            lblTestId = new Label();
            btnOpenCam = new Button();
            cmbCams = new ComboBox();
            btnResetErrors = new Button();
            btnUpdateCamList = new Button();
            timerBasing = new System.Windows.Forms.Timer(components);
            outsTimer = new System.Windows.Forms.Timer(components);
            calibrationTimer = new System.Windows.Forms.Timer(components);
            btnGain = new Button();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            operatorPanel.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox6.SuspendLayout();
            gpTestPoint.SuspendLayout();
            gbMagnets.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbOut7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbOut6).BeginInit();
            gpForceData.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudBreakDelay).BeginInit();
            gbForceBound.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudLoadBound).BeginInit();
            gbTestType.SuspendLayout();
            gbTestVelocity.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudTestSpeed).BeginInit();
            groupBox4.SuspendLayout();
            SuspendLayout();
            // 
            // testTimer
            // 
            testTimer.Enabled = true;
            testTimer.Interval = 250;
            testTimer.Tick += testTimer_Tick;
            // 
            // testPlot
            // 
            testPlot.DisplayScale = 1F;
            testPlot.Dock = DockStyle.Fill;
            testPlot.Location = new Point(0, 0);
            testPlot.Name = "testPlot";
            testPlot.Size = new Size(578, 398);
            testPlot.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            tableLayoutPanel1.Controls.Add(panel1, 0, 1);
            tableLayoutPanel1.Controls.Add(pictureBox1, 0, 0);
            tableLayoutPanel1.Controls.Add(operatorPanel, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 55F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 45F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(1062, 896);
            tableLayoutPanel1.TabIndex = 22;
            // 
            // panel1
            // 
            panel1.Controls.Add(tbForceData);
            panel1.Controls.Add(testPlot);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 495);
            panel1.Name = "panel1";
            panel1.Size = new Size(578, 398);
            panel1.TabIndex = 0;
            // 
            // tbForceData
            // 
            tbForceData.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            tbForceData.Font = new Font("DSEG7 Modern", 13.999999F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbForceData.ForeColor = Color.Navy;
            tbForceData.Location = new Point(475, 3);
            tbForceData.Name = "tbForceData";
            tbForceData.Size = new Size(100, 28);
            tbForceData.TabIndex = 26;
            tbForceData.Text = "0";
            // 
            // pictureBox1
            // 
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Location = new Point(3, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(578, 486);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 24;
            pictureBox1.TabStop = false;
            // 
            // operatorPanel
            // 
            operatorPanel.Controls.Add(btnGain);
            operatorPanel.Controls.Add(groupBox2);
            operatorPanel.Controls.Add(groupBox1);
            operatorPanel.Controls.Add(label1);
            operatorPanel.Controls.Add(groupBox3);
            operatorPanel.Controls.Add(groupBox4);
            operatorPanel.Controls.Add(btnOpenCam);
            operatorPanel.Controls.Add(cmbCams);
            operatorPanel.Controls.Add(btnResetErrors);
            operatorPanel.Controls.Add(btnUpdateCamList);
            operatorPanel.Location = new Point(587, 3);
            operatorPanel.Name = "operatorPanel";
            tableLayoutPanel1.SetRowSpan(operatorPanel, 2);
            operatorPanel.Size = new Size(472, 890);
            operatorPanel.TabIndex = 2;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(groupBox6);
            groupBox2.Controls.Add(gpTestPoint);
            groupBox2.Controls.Add(gbMagnets);
            groupBox2.Controls.Add(gpForceData);
            groupBox2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox2.Location = new Point(3, 247);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(466, 259);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Текущий тест";
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(btnStartTest);
            groupBox6.Controls.Add(btnMoveToStart);
            groupBox6.Controls.Add(btnHome);
            groupBox6.Location = new Point(17, 22);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new Size(136, 225);
            groupBox6.TabIndex = 4;
            groupBox6.TabStop = false;
            // 
            // btnStartTest
            // 
            btnStartTest.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnStartTest.Location = new Point(16, 164);
            btnStartTest.Name = "btnStartTest";
            btnStartTest.Size = new Size(105, 50);
            btnStartTest.TabIndex = 0;
            btnStartTest.Text = "Начать измерение";
            btnStartTest.UseVisualStyleBackColor = true;
            btnStartTest.Click += btnStartTest_Click;
            // 
            // btnMoveToStart
            // 
            btnMoveToStart.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnMoveToStart.Location = new Point(16, 92);
            btnMoveToStart.Name = "btnMoveToStart";
            btnMoveToStart.Size = new Size(105, 50);
            btnMoveToStart.TabIndex = 21;
            btnMoveToStart.Text = "В точку теста";
            btnMoveToStart.UseVisualStyleBackColor = true;
            btnMoveToStart.Click += btnMoveToTestPoint_Click;
            // 
            // btnHome
            // 
            btnHome.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnHome.Location = new Point(13, 22);
            btnHome.Name = "btnHome";
            btnHome.Size = new Size(110, 50);
            btnHome.TabIndex = 0;
            btnHome.Text = "Базирование";
            btnHome.UseVisualStyleBackColor = true;
            btnHome.Click += StartBasing;
            // 
            // gpTestPoint
            // 
            gpTestPoint.Controls.Add(lblTestPointCaption0);
            gpTestPoint.Controls.Add(lblTestPointCaption1);
            gpTestPoint.Controls.Add(lblTestPoint2);
            gpTestPoint.Controls.Add(btnSetTestPoint);
            gpTestPoint.Controls.Add(lblTestPoint0);
            gpTestPoint.Controls.Add(lblTestPoint1);
            gpTestPoint.Controls.Add(lblTestPointCaption2);
            gpTestPoint.Location = new Point(173, 110);
            gpTestPoint.Name = "gpTestPoint";
            gpTestPoint.Size = new Size(120, 137);
            gpTestPoint.TabIndex = 29;
            gpTestPoint.TabStop = false;
            gpTestPoint.Text = "Точка теста";
            // 
            // lblTestPointCaption0
            // 
            lblTestPointCaption0.AutoSize = true;
            lblTestPointCaption0.Location = new Point(30, 25);
            lblTestPointCaption0.Name = "lblTestPointCaption0";
            lblTestPointCaption0.Size = new Size(18, 15);
            lblTestPointCaption0.TabIndex = 19;
            lblTestPointCaption0.Text = "X:";
            // 
            // lblTestPointCaption1
            // 
            lblTestPointCaption1.AutoSize = true;
            lblTestPointCaption1.Location = new Point(30, 41);
            lblTestPointCaption1.Name = "lblTestPointCaption1";
            lblTestPointCaption1.Size = new Size(17, 15);
            lblTestPointCaption1.TabIndex = 20;
            lblTestPointCaption1.Text = "Y:";
            // 
            // lblTestPoint2
            // 
            lblTestPoint2.AutoSize = true;
            lblTestPoint2.Location = new Point(53, 55);
            lblTestPoint2.Name = "lblTestPoint2";
            lblTestPoint2.Size = new Size(35, 15);
            lblTestPoint2.TabIndex = 20;
            lblTestPoint2.Text = "1500";
            // 
            // btnSetTestPoint
            // 
            btnSetTestPoint.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnSetTestPoint.Location = new Point(6, 82);
            btnSetTestPoint.Name = "btnSetTestPoint";
            btnSetTestPoint.Size = new Size(105, 49);
            btnSetTestPoint.TabIndex = 21;
            btnSetTestPoint.Text = "Обучить точку теста";
            btnSetTestPoint.UseVisualStyleBackColor = true;
            btnSetTestPoint.Click += btnSetTestPoint_Click;
            // 
            // lblTestPoint0
            // 
            lblTestPoint0.AutoSize = true;
            lblTestPoint0.Location = new Point(53, 25);
            lblTestPoint0.Name = "lblTestPoint0";
            lblTestPoint0.Size = new Size(35, 15);
            lblTestPoint0.TabIndex = 19;
            lblTestPoint0.Text = "1500";
            // 
            // lblTestPoint1
            // 
            lblTestPoint1.AutoSize = true;
            lblTestPoint1.Location = new Point(53, 40);
            lblTestPoint1.Name = "lblTestPoint1";
            lblTestPoint1.Size = new Size(35, 15);
            lblTestPoint1.TabIndex = 20;
            lblTestPoint1.Text = "1500";
            // 
            // lblTestPointCaption2
            // 
            lblTestPointCaption2.AutoSize = true;
            lblTestPointCaption2.Location = new Point(30, 55);
            lblTestPointCaption2.Name = "lblTestPointCaption2";
            lblTestPointCaption2.Size = new Size(17, 15);
            lblTestPointCaption2.TabIndex = 20;
            lblTestPointCaption2.Text = "Z:";
            // 
            // gbMagnets
            // 
            gbMagnets.Controls.Add(btnLockUpperBond);
            gbMagnets.Controls.Add(pbOut7);
            gbMagnets.Controls.Add(btnLockLowerBond);
            gbMagnets.Controls.Add(pbOut6);
            gbMagnets.Location = new Point(173, 22);
            gbMagnets.Name = "gbMagnets";
            gbMagnets.Size = new Size(118, 82);
            gbMagnets.TabIndex = 28;
            gbMagnets.TabStop = false;
            gbMagnets.Text = "Магниты";
            // 
            // btnLockUpperBond
            // 
            btnLockUpperBond.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnLockUpperBond.Location = new Point(6, 18);
            btnLockUpperBond.Name = "btnLockUpperBond";
            btnLockUpperBond.Size = new Size(65, 25);
            btnLockUpperBond.TabIndex = 14;
            btnLockUpperBond.Text = "Магнит 1";
            btnLockUpperBond.UseVisualStyleBackColor = true;
            btnLockUpperBond.Click += btnLockUpperBond_Click;
            // 
            // pbOut7
            // 
            pbOut7.BorderStyle = BorderStyle.FixedSingle;
            pbOut7.Location = new Point(77, 50);
            pbOut7.Name = "pbOut7";
            pbOut7.Size = new Size(26, 26);
            pbOut7.TabIndex = 26;
            pbOut7.TabStop = false;
            // 
            // btnLockLowerBond
            // 
            btnLockLowerBond.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnLockLowerBond.Location = new Point(6, 51);
            btnLockLowerBond.Name = "btnLockLowerBond";
            btnLockLowerBond.Size = new Size(65, 25);
            btnLockLowerBond.TabIndex = 22;
            btnLockLowerBond.Text = "Магнит 2";
            btnLockLowerBond.UseVisualStyleBackColor = true;
            btnLockLowerBond.Click += btnLockLowerBond_Click;
            // 
            // pbOut6
            // 
            pbOut6.BorderStyle = BorderStyle.FixedSingle;
            pbOut6.Location = new Point(77, 18);
            pbOut6.Name = "pbOut6";
            pbOut6.Size = new Size(26, 26);
            pbOut6.TabIndex = 26;
            pbOut6.TabStop = false;
            // 
            // gpForceData
            // 
            gpForceData.Controls.Add(rtbTestValues);
            gpForceData.Controls.Add(lblTestResult);
            gpForceData.Controls.Add(lblTestValues);
            gpForceData.Controls.Add(tbTestResult);
            gpForceData.Location = new Point(299, 22);
            gpForceData.Name = "gpForceData";
            gpForceData.Size = new Size(161, 225);
            gpForceData.TabIndex = 27;
            gpForceData.TabStop = false;
            gpForceData.Text = "Данные";
            // 
            // rtbTestValues
            // 
            rtbTestValues.Font = new Font("Segoe UI", 9F);
            rtbTestValues.Location = new Point(6, 83);
            rtbTestValues.Name = "rtbTestValues";
            rtbTestValues.ReadOnly = true;
            rtbTestValues.Size = new Size(149, 129);
            rtbTestValues.TabIndex = 3;
            rtbTestValues.Text = "";
            // 
            // lblTestResult
            // 
            lblTestResult.AutoSize = true;
            lblTestResult.Location = new Point(6, 21);
            lblTestResult.Name = "lblTestResult";
            lblTestResult.Size = new Size(99, 15);
            lblTestResult.TabIndex = 18;
            lblTestResult.Text = "Макс. усилие, г:";
            // 
            // lblTestValues
            // 
            lblTestValues.AutoSize = true;
            lblTestValues.Location = new Point(6, 65);
            lblTestValues.Name = "lblTestValues";
            lblTestValues.Size = new Size(122, 15);
            lblTestValues.TabIndex = 18;
            lblTestValues.Text = "Показания датчика:";
            // 
            // tbTestResult
            // 
            tbTestResult.Font = new Font("Segoe UI", 9F);
            tbTestResult.Location = new Point(6, 39);
            tbTestResult.Name = "tbTestResult";
            tbTestResult.ReadOnly = true;
            tbTestResult.Size = new Size(99, 23);
            tbTestResult.TabIndex = 17;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(lblReferenceLoad);
            groupBox1.Controls.Add(btnTare);
            groupBox1.Controls.Add(btnCalibrate);
            groupBox1.Controls.Add(cmbReferenceWeights);
            groupBox1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox1.Location = new Point(61, 527);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(362, 85);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Весы/сенсор";
            // 
            // lblReferenceLoad
            // 
            lblReferenceLoad.AutoSize = true;
            lblReferenceLoad.Font = new Font("Segoe UI", 9F);
            lblReferenceLoad.Location = new Point(118, 22);
            lblReferenceLoad.Name = "lblReferenceLoad";
            lblReferenceLoad.Size = new Size(87, 15);
            lblReferenceLoad.TabIndex = 12;
            lblReferenceLoad.Text = "Вес эталона, г:";
            // 
            // btnTare
            // 
            btnTare.Font = new Font("Segoe UI", 9F);
            btnTare.Location = new Point(6, 22);
            btnTare.Name = "btnTare";
            btnTare.Size = new Size(105, 44);
            btnTare.TabIndex = 4;
            btnTare.Text = "Тарирование";
            btnTare.UseVisualStyleBackColor = true;
            btnTare.Click += btnTare_Click;
            // 
            // btnCalibrate
            // 
            btnCalibrate.Font = new Font("Segoe UI", 9F);
            btnCalibrate.Location = new Point(229, 22);
            btnCalibrate.Name = "btnCalibrate";
            btnCalibrate.Size = new Size(122, 41);
            btnCalibrate.TabIndex = 11;
            btnCalibrate.Text = "Калибровка";
            btnCalibrate.UseVisualStyleBackColor = true;
            btnCalibrate.Click += btnCalibrate_Click;
            // 
            // cmbReferenceWeights
            // 
            cmbReferenceWeights.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbReferenceWeights.Font = new Font("Segoe UI", 9F);
            cmbReferenceWeights.FormattingEnabled = true;
            cmbReferenceWeights.Items.AddRange(new object[] { "5", "10", "15", "20", "25", "50" });
            cmbReferenceWeights.Location = new Point(118, 40);
            cmbReferenceWeights.Name = "cmbReferenceWeights";
            cmbReferenceWeights.Size = new Size(105, 23);
            cmbReferenceWeights.TabIndex = 13;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.Location = new Point(164, 6);
            label1.Name = "label1";
            label1.Size = new Size(144, 25);
            label1.TabIndex = 20;
            label1.Text = "Рабочий цикл";
            label1.Click += label1_Click;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(groupBox5);
            groupBox3.Controls.Add(gbForceBound);
            groupBox3.Controls.Add(gbTestType);
            groupBox3.Controls.Add(gbTestVelocity);
            groupBox3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox3.Location = new Point(3, 34);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(466, 160);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "Настройка теста";
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(nudBreakDelay);
            groupBox5.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox5.Location = new Point(311, 81);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(149, 68);
            groupBox5.TabIndex = 28;
            groupBox5.TabStop = false;
            groupBox5.Text = "Задержка после разрыва, c:";
            // 
            // nudBreakDelay
            // 
            nudBreakDelay.DecimalPlaces = 2;
            nudBreakDelay.Font = new Font("Segoe UI", 9F);
            nudBreakDelay.Increment = new decimal(new int[] { 25, 0, 0, 131072 });
            nudBreakDelay.Location = new Point(6, 37);
            nudBreakDelay.Minimum = new decimal(new int[] { 5, 0, 0, 65536 });
            nudBreakDelay.Name = "nudBreakDelay";
            nudBreakDelay.Size = new Size(105, 23);
            nudBreakDelay.TabIndex = 2;
            nudBreakDelay.Value = new decimal(new int[] { 3, 0, 0, 0 });
            // 
            // gbForceBound
            // 
            gbForceBound.Controls.Add(cbBoundSet);
            gbForceBound.Controls.Add(nudLoadBound);
            gbForceBound.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            gbForceBound.Location = new Point(311, 22);
            gbForceBound.Name = "gbForceBound";
            gbForceBound.Size = new Size(149, 53);
            gbForceBound.TabIndex = 27;
            gbForceBound.TabStop = false;
            gbForceBound.Text = "Огр. усилие, г:";
            // 
            // cbBoundSet
            // 
            cbBoundSet.AutoSize = true;
            cbBoundSet.Location = new Point(13, 27);
            cbBoundSet.Name = "cbBoundSet";
            cbBoundSet.Size = new Size(15, 14);
            cbBoundSet.TabIndex = 4;
            cbBoundSet.UseVisualStyleBackColor = true;
            // 
            // nudLoadBound
            // 
            nudLoadBound.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            nudLoadBound.Increment = new decimal(new int[] { 50, 0, 0, 0 });
            nudLoadBound.Location = new Point(38, 22);
            nudLoadBound.Maximum = new decimal(new int[] { 5000, 0, 0, 0 });
            nudLoadBound.Name = "nudLoadBound";
            nudLoadBound.Size = new Size(105, 23);
            nudLoadBound.TabIndex = 9;
            nudLoadBound.Value = new decimal(new int[] { 300, 0, 0, 0 });
            // 
            // gbTestType
            // 
            gbTestType.Controls.Add(rbBreakTest);
            gbTestType.Controls.Add(rbStretchTest);
            gbTestType.Controls.Add(rbShearTest);
            gbTestType.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            gbTestType.Location = new Point(6, 22);
            gbTestType.Name = "gbTestType";
            gbTestType.Size = new Size(110, 127);
            gbTestType.TabIndex = 26;
            gbTestType.TabStop = false;
            gbTestType.Text = "Тип теста";
            // 
            // rbBreakTest
            // 
            rbBreakTest.AutoSize = true;
            rbBreakTest.Font = new Font("Segoe UI", 9F);
            rbBreakTest.Location = new Point(11, 22);
            rbBreakTest.Name = "rbBreakTest";
            rbBreakTest.Size = new Size(65, 19);
            rbBreakTest.TabIndex = 5;
            rbBreakTest.Text = "Разрыв";
            rbBreakTest.UseVisualStyleBackColor = true;
            rbBreakTest.CheckedChanged += rbBreakTest_CheckedChanged;
            // 
            // rbStretchTest
            // 
            rbStretchTest.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rbStretchTest.AutoSize = true;
            rbStretchTest.Font = new Font("Segoe UI", 9F);
            rbStretchTest.Location = new Point(10, 60);
            rbStretchTest.Name = "rbStretchTest";
            rbStretchTest.Size = new Size(90, 19);
            rbStretchTest.TabIndex = 6;
            rbStretchTest.Text = "Растяжение";
            rbStretchTest.UseVisualStyleBackColor = true;
            rbStretchTest.CheckedChanged += rbStretchTest_CheckedChanged;
            // 
            // rbShearTest
            // 
            rbShearTest.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rbShearTest.AutoSize = true;
            rbShearTest.Font = new Font("Segoe UI", 9F);
            rbShearTest.Location = new Point(10, 96);
            rbShearTest.Name = "rbShearTest";
            rbShearTest.Size = new Size(57, 19);
            rbShearTest.TabIndex = 7;
            rbShearTest.Text = "Сдвиг";
            rbShearTest.UseVisualStyleBackColor = true;
            rbShearTest.CheckedChanged += rbShearTest_CheckedChanged;
            // 
            // gbTestVelocity
            // 
            gbTestVelocity.Controls.Add(lblTestSpeed);
            gbTestVelocity.Controls.Add(btnSaveTestVelocity);
            gbTestVelocity.Controls.Add(nudTestSpeed);
            gbTestVelocity.Controls.Add(btnLoadTestVelocity);
            gbTestVelocity.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            gbTestVelocity.Location = new Point(122, 22);
            gbTestVelocity.Name = "gbTestVelocity";
            gbTestVelocity.Size = new Size(183, 127);
            gbTestVelocity.TabIndex = 25;
            gbTestVelocity.TabStop = false;
            gbTestVelocity.Text = "Скорость теста";
            // 
            // lblTestSpeed
            // 
            lblTestSpeed.AutoSize = true;
            lblTestSpeed.Font = new Font("Segoe UI", 9F);
            lblTestSpeed.Location = new Point(18, 19);
            lblTestSpeed.Name = "lblTestSpeed";
            lblTestSpeed.Size = new Size(149, 15);
            lblTestSpeed.TabIndex = 12;
            lblTestSpeed.Text = "Скорость при тесте, мк/с:";
            // 
            // btnSaveTestVelocity
            // 
            btnSaveTestVelocity.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnSaveTestVelocity.Location = new Point(8, 66);
            btnSaveTestVelocity.Name = "btnSaveTestVelocity";
            btnSaveTestVelocity.Size = new Size(81, 49);
            btnSaveTestVelocity.TabIndex = 21;
            btnSaveTestVelocity.Text = "Сохранить скорость теста";
            btnSaveTestVelocity.UseVisualStyleBackColor = true;
            btnSaveTestVelocity.Click += btnSaveTestVelocity_Click;
            // 
            // nudTestSpeed
            // 
            nudTestSpeed.Font = new Font("Segoe UI", 9F);
            nudTestSpeed.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            nudTestSpeed.Location = new Point(38, 37);
            nudTestSpeed.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nudTestSpeed.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            nudTestSpeed.Name = "nudTestSpeed";
            nudTestSpeed.Size = new Size(105, 23);
            nudTestSpeed.TabIndex = 9;
            nudTestSpeed.Value = new decimal(new int[] { 10, 0, 0, 0 });
            nudTestSpeed.ValueChanged += nudTestSpeed_ValueChanged;
            nudTestSpeed.Enter += nudTestSpeed_Enter;
            nudTestSpeed.KeyDown += nudTestSpeed_KeyDown;
            // 
            // btnLoadTestVelocity
            // 
            btnLoadTestVelocity.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnLoadTestVelocity.Location = new Point(95, 66);
            btnLoadTestVelocity.Name = "btnLoadTestVelocity";
            btnLoadTestVelocity.Size = new Size(79, 49);
            btnLoadTestVelocity.TabIndex = 21;
            btnLoadTestVelocity.Text = "Загрузить скорость теста";
            btnLoadTestVelocity.UseVisualStyleBackColor = true;
            btnLoadTestVelocity.Click += btnLoadTestVelocity_Click;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(btnSaveTests);
            groupBox4.Controls.Add(btnLoadTests);
            groupBox4.Controls.Add(cmbTests);
            groupBox4.Controls.Add(lblTestId);
            groupBox4.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox4.Location = new Point(61, 633);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(362, 75);
            groupBox4.TabIndex = 3;
            groupBox4.TabStop = false;
            groupBox4.Text = "Прошлые измерения";
            // 
            // btnSaveTests
            // 
            btnSaveTests.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnSaveTests.Location = new Point(89, 18);
            btnSaveTests.Name = "btnSaveTests";
            btnSaveTests.Size = new Size(75, 41);
            btnSaveTests.TabIndex = 23;
            btnSaveTests.Text = "Сохранить \r\nтесты";
            btnSaveTests.UseVisualStyleBackColor = true;
            btnSaveTests.Click += btnSaveTests_Click;
            // 
            // btnLoadTests
            // 
            btnLoadTests.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnLoadTests.Location = new Point(6, 18);
            btnLoadTests.Name = "btnLoadTests";
            btnLoadTests.Size = new Size(77, 41);
            btnLoadTests.TabIndex = 22;
            btnLoadTests.Text = "Загрузить \r\nтесты";
            btnLoadTests.UseVisualStyleBackColor = true;
            btnLoadTests.Click += btnLoadTests_Click;
            // 
            // cmbTests
            // 
            cmbTests.Font = new Font("Segoe UI", 9F);
            cmbTests.FormattingEnabled = true;
            cmbTests.Location = new Point(170, 37);
            cmbTests.Name = "cmbTests";
            cmbTests.Size = new Size(105, 23);
            cmbTests.TabIndex = 15;
            cmbTests.SelectedIndexChanged += cmbTests_SelectedIndexChanged;
            // 
            // lblTestId
            // 
            lblTestId.AutoSize = true;
            lblTestId.Font = new Font("Segoe UI", 9F);
            lblTestId.Location = new Point(170, 19);
            lblTestId.Name = "lblTestId";
            lblTestId.Size = new Size(85, 15);
            lblTestId.TabIndex = 16;
            lblTestId.Text = "№ Измерения";
            // 
            // btnOpenCam
            // 
            btnOpenCam.Location = new Point(135, 837);
            btnOpenCam.Name = "btnOpenCam";
            btnOpenCam.Size = new Size(87, 41);
            btnOpenCam.TabIndex = 0;
            btnOpenCam.Text = "Начать просмотр";
            btnOpenCam.UseVisualStyleBackColor = true;
            btnOpenCam.Visible = false;
            btnOpenCam.Click += btnOpenCam_Click;
            // 
            // cmbCams
            // 
            cmbCams.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCams.FormattingEnabled = true;
            cmbCams.Location = new Point(320, 859);
            cmbCams.Name = "cmbCams";
            cmbCams.Size = new Size(121, 23);
            cmbCams.TabIndex = 1;
            cmbCams.Visible = false;
            // 
            // btnResetErrors
            // 
            btnResetErrors.Location = new Point(42, 833);
            btnResetErrors.Name = "btnResetErrors";
            btnResetErrors.Size = new Size(87, 49);
            btnResetErrors.TabIndex = 0;
            btnResetErrors.Text = "Сброс ошибок";
            btnResetErrors.UseVisualStyleBackColor = true;
            btnResetErrors.Visible = false;
            btnResetErrors.Click += btnResetErrors_Click;
            // 
            // btnUpdateCamList
            // 
            btnUpdateCamList.Location = new Point(227, 833);
            btnUpdateCamList.Name = "btnUpdateCamList";
            btnUpdateCamList.Size = new Size(87, 53);
            btnUpdateCamList.TabIndex = 0;
            btnUpdateCamList.Text = "Обновить список камер";
            btnUpdateCamList.UseVisualStyleBackColor = true;
            btnUpdateCamList.Visible = false;
            btnUpdateCamList.Click += btnUpdateCamList_Click;
            // 
            // timerBasing
            // 
            timerBasing.Tick += BasingTimerTick;
            // 
            // outsTimer
            // 
            outsTimer.Enabled = true;
            outsTimer.Tick += outsTimer_Tick;
            // 
            // calibrationTimer
            // 
            calibrationTimer.Interval = 1000;
            calibrationTimer.Tick += calibrationTimer_Tick;
            // 
            // btnGain
            // 
            btnGain.Location = new Point(320, 833);
            btnGain.Name = "btnGain";
            btnGain.Size = new Size(121, 23);
            btnGain.TabIndex = 21;
            btnGain.Text = "Усиление камеры";
            btnGain.UseVisualStyleBackColor = true;
            btnGain.Click += btnGain_Click;
            // 
            // DutyCycleForm
            // 
            AutoScaleMode = AutoScaleMode.None;
            AutoSize = true;
            ClientSize = new Size(1062, 896);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(2);
            Name = "DutyCycleForm";
            Text = "MainForm";
            FormClosed += BondTestForm_FormClosed;
            Load += DutyCycleForm_Load;
            tableLayoutPanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            operatorPanel.ResumeLayout(false);
            operatorPanel.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox6.ResumeLayout(false);
            gpTestPoint.ResumeLayout(false);
            gpTestPoint.PerformLayout();
            gbMagnets.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbOut7).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbOut6).EndInit();
            gpForceData.ResumeLayout(false);
            gpForceData.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)nudBreakDelay).EndInit();
            gbForceBound.ResumeLayout(false);
            gbForceBound.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudLoadBound).EndInit();
            gbTestType.ResumeLayout(false);
            gbTestType.PerformLayout();
            gbTestVelocity.ResumeLayout(false);
            gbTestVelocity.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudTestSpeed).EndInit();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Timer testTimer;
        private ScottPlot.WinForms.FormsPlot testPlot;
        private TableLayoutPanel tableLayoutPanel1;
        private PictureBox pictureBox1;
        private RadioButton rbShearTest;
        private RadioButton rbStretchTest;
        private Button btnSaveTests;
        private RadioButton rbBreakTest;
        private Button btnLoadTests;
        private Button btnStartTest;
        private Button btnLoadTestVelocity;
        private NumericUpDown nudBreakDelay;
        private Button btnSaveTestVelocity;
        private RichTextBox rtbTestValues;
        private Button btnSetTestPoint;
        private Button btnTare;
        private Label lblTestPoint2;
        private Label lblTestPointCaption2;
        private Label lblTestPoint1;
        private Label lblTestPoint0;
        private Label lblTestPointCaption1;
        private NumericUpDown nudLoadBound;
        private Label lblTestPointCaption0;
        private NumericUpDown nudTestSpeed;
        private Label lblTestResult;
        private Button btnCalibrate;
        private TextBox tbTestResult;
        private Label lblReferenceLoad;
        private Label lblTestId;
        private Label lblTestSpeed;
        private ComboBox cmbTests;
        private ComboBox cmbReferenceWeights;
        private Button btnLockUpperBond;
        private Button btnHome;
        private System.Windows.Forms.Timer timerBasing;
        private ComboBox cmbCams;
        private Button btnOpenCam;
        private Button btnUpdateCamList;
        private Button btnMoveToStart;
        private GroupBox groupBox4;
        private GroupBox groupBox3;
        private GroupBox groupBox2;
        private GroupBox groupBox1;
        private Label lblTestValues;
        private Button btnResetErrors;
        private TextBox tbForceData;
        private Panel panel1;
        private Button btnLockLowerBond;
        private PictureBox pbOut6;
        private PictureBox pbOut7;
        private System.Windows.Forms.Timer outsTimer;
        private System.Windows.Forms.Timer calibrationTimer;
        private Panel operatorPanel;
        private GroupBox gbTestVelocity;
        private GroupBox groupBox5;
        private GroupBox gbForceBound;
        private GroupBox gbTestType;
        private GroupBox gbMagnets;
        private GroupBox gpForceData;
        private GroupBox gpTestPoint;
        private GroupBox groupBox6;
        private CheckBox cbBoundSet;
        private Label label1;
        private Button btnGain;
    }
}