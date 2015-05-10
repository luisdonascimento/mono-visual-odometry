namespace VisualOdometry.UI
{
	partial class MainForm
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
            this.m_TopPanel = new System.Windows.Forms.Panel();
            this.m_ApplyButton = new System.Windows.Forms.Button();
            this.m_GroundTopTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.m_SkyBottomTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.m_MinDistanceTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.m_QualityLevelTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.m_BlockSizeTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.m_MaxFeatureCountTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_DrawFeaturesCheckBox = new System.Windows.Forms.CheckBox();
            this.m_BottomPanel = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Connect_btn = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.m_ObstaclesCheckBox = new System.Windows.Forms.CheckBox();
            this.m_SurfCheckbox = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.m_FramesPerSecondTextBox = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.m_Timer = new System.Windows.Forms.Timer(this.components);
            this.m_ImageBox = new Emgu.CV.UI.ImageBox();
            this.m_TopPanel.SuspendLayout();
            this.m_BottomPanel.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_ImageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // m_TopPanel
            // 
            this.m_TopPanel.Controls.Add(this.m_ApplyButton);
            this.m_TopPanel.Controls.Add(this.m_GroundTopTextBox);
            this.m_TopPanel.Controls.Add(this.label8);
            this.m_TopPanel.Controls.Add(this.m_SkyBottomTextBox);
            this.m_TopPanel.Controls.Add(this.label9);
            this.m_TopPanel.Controls.Add(this.m_MinDistanceTextBox);
            this.m_TopPanel.Controls.Add(this.label4);
            this.m_TopPanel.Controls.Add(this.m_QualityLevelTextBox);
            this.m_TopPanel.Controls.Add(this.label3);
            this.m_TopPanel.Controls.Add(this.m_BlockSizeTextBox);
            this.m_TopPanel.Controls.Add(this.label2);
            this.m_TopPanel.Controls.Add(this.m_MaxFeatureCountTextBox);
            this.m_TopPanel.Controls.Add(this.label1);
            this.m_TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_TopPanel.Location = new System.Drawing.Point(0, 0);
            this.m_TopPanel.Margin = new System.Windows.Forms.Padding(2);
            this.m_TopPanel.Name = "m_TopPanel";
            this.m_TopPanel.Size = new System.Drawing.Size(672, 67);
            this.m_TopPanel.TabIndex = 1;
            // 
            // m_ApplyButton
            // 
            this.m_ApplyButton.Location = new System.Drawing.Point(526, 31);
            this.m_ApplyButton.Margin = new System.Windows.Forms.Padding(2);
            this.m_ApplyButton.Name = "m_ApplyButton";
            this.m_ApplyButton.Size = new System.Drawing.Size(56, 19);
            this.m_ApplyButton.TabIndex = 4;
            this.m_ApplyButton.Text = "Apply";
            this.m_ApplyButton.UseVisualStyleBackColor = true;
            this.m_ApplyButton.Click += new System.EventHandler(this.OnApplyButtonClicked);
            // 
            // m_GroundTopTextBox
            // 
            this.m_GroundTopTextBox.Location = new System.Drawing.Point(440, 31);
            this.m_GroundTopTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.m_GroundTopTextBox.Name = "m_GroundTopTextBox";
            this.m_GroundTopTextBox.Size = new System.Drawing.Size(52, 20);
            this.m_GroundTopTextBox.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(358, 33);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "Ground Top:";
            // 
            // m_SkyBottomTextBox
            // 
            this.m_SkyBottomTextBox.Location = new System.Drawing.Point(440, 8);
            this.m_SkyBottomTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.m_SkyBottomTextBox.Name = "m_SkyBottomTextBox";
            this.m_SkyBottomTextBox.Size = new System.Drawing.Size(52, 20);
            this.m_SkyBottomTextBox.TabIndex = 10;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(358, 11);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 13);
            this.label9.TabIndex = 9;
            this.label9.Text = "Sky Bottom:";
            // 
            // m_MinDistanceTextBox
            // 
            this.m_MinDistanceTextBox.Location = new System.Drawing.Point(278, 31);
            this.m_MinDistanceTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.m_MinDistanceTextBox.Name = "m_MinDistanceTextBox";
            this.m_MinDistanceTextBox.Size = new System.Drawing.Size(52, 20);
            this.m_MinDistanceTextBox.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(196, 33);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Min Distance:";
            // 
            // m_QualityLevelTextBox
            // 
            this.m_QualityLevelTextBox.Location = new System.Drawing.Point(116, 31);
            this.m_QualityLevelTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.m_QualityLevelTextBox.Name = "m_QualityLevelTextBox";
            this.m_QualityLevelTextBox.Size = new System.Drawing.Size(52, 20);
            this.m_QualityLevelTextBox.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 33);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Quality Level:";
            // 
            // m_BlockSizeTextBox
            // 
            this.m_BlockSizeTextBox.Location = new System.Drawing.Point(278, 8);
            this.m_BlockSizeTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.m_BlockSizeTextBox.Name = "m_BlockSizeTextBox";
            this.m_BlockSizeTextBox.Size = new System.Drawing.Size(52, 20);
            this.m_BlockSizeTextBox.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(196, 11);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Block Size:";
            // 
            // m_MaxFeatureCountTextBox
            // 
            this.m_MaxFeatureCountTextBox.Location = new System.Drawing.Point(116, 8);
            this.m_MaxFeatureCountTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.m_MaxFeatureCountTextBox.Name = "m_MaxFeatureCountTextBox";
            this.m_MaxFeatureCountTextBox.Size = new System.Drawing.Size(52, 20);
            this.m_MaxFeatureCountTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Max Feature Count:";
            // 
            // m_DrawFeaturesCheckBox
            // 
            this.m_DrawFeaturesCheckBox.AutoSize = true;
            this.m_DrawFeaturesCheckBox.Checked = true;
            this.m_DrawFeaturesCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_DrawFeaturesCheckBox.Location = new System.Drawing.Point(5, 21);
            this.m_DrawFeaturesCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.m_DrawFeaturesCheckBox.Name = "m_DrawFeaturesCheckBox";
            this.m_DrawFeaturesCheckBox.Size = new System.Drawing.Size(98, 17);
            this.m_DrawFeaturesCheckBox.TabIndex = 13;
            this.m_DrawFeaturesCheckBox.Text = "Optic Flux Field";
            this.m_DrawFeaturesCheckBox.UseVisualStyleBackColor = true;
            // 
            // m_BottomPanel
            // 
            this.m_BottomPanel.Controls.Add(this.groupBox3);
            this.m_BottomPanel.Controls.Add(this.groupBox2);
            this.m_BottomPanel.Controls.Add(this.groupBox1);
            this.m_BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_BottomPanel.Location = new System.Drawing.Point(0, 418);
            this.m_BottomPanel.Margin = new System.Windows.Forms.Padding(2);
            this.m_BottomPanel.Name = "m_BottomPanel";
            this.m_BottomPanel.Size = new System.Drawing.Size(672, 113);
            this.m_BottomPanel.TabIndex = 2;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.Connect_btn);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox3.Location = new System.Drawing.Point(369, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 113);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Lego";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "label5";
            // 
            // Connect_btn
            // 
            this.Connect_btn.Location = new System.Drawing.Point(5, 18);
            this.Connect_btn.Name = "Connect_btn";
            this.Connect_btn.Size = new System.Drawing.Size(88, 19);
            this.Connect_btn.TabIndex = 15;
            this.Connect_btn.Text = "Connect LEGO";
            this.Connect_btn.UseVisualStyleBackColor = true;
            this.Connect_btn.Click += new System.EventHandler(this.Connect_btn_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.m_ObstaclesCheckBox);
            this.groupBox2.Controls.Add(this.m_SurfCheckbox);
            this.groupBox2.Controls.Add(this.m_DrawFeaturesCheckBox);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox2.Location = new System.Drawing.Point(152, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(217, 113);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "View";
            // 
            // m_ObstaclesCheckBox
            // 
            this.m_ObstaclesCheckBox.AutoSize = true;
            this.m_ObstaclesCheckBox.Checked = true;
            this.m_ObstaclesCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_ObstaclesCheckBox.Location = new System.Drawing.Point(5, 65);
            this.m_ObstaclesCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.m_ObstaclesCheckBox.Name = "m_ObstaclesCheckBox";
            this.m_ObstaclesCheckBox.Size = new System.Drawing.Size(73, 17);
            this.m_ObstaclesCheckBox.TabIndex = 17;
            this.m_ObstaclesCheckBox.Text = "Obstacles";
            this.m_ObstaclesCheckBox.UseVisualStyleBackColor = true;
            // 
            // m_SurfCheckbox
            // 
            this.m_SurfCheckbox.AutoSize = true;
            this.m_SurfCheckbox.Location = new System.Drawing.Point(5, 43);
            this.m_SurfCheckbox.Name = "m_SurfCheckbox";
            this.m_SurfCheckbox.Size = new System.Drawing.Size(55, 17);
            this.m_SurfCheckbox.TabIndex = 16;
            this.m_SurfCheckbox.Text = "SURF";
            this.m_SurfCheckbox.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.m_FramesPerSecondTextBox);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(152, 113);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Image Data";
            // 
            // m_FramesPerSecondTextBox
            // 
            this.m_FramesPerSecondTextBox.Location = new System.Drawing.Point(41, 17);
            this.m_FramesPerSecondTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.m_FramesPerSecondTextBox.Name = "m_FramesPerSecondTextBox";
            this.m_FramesPerSecondTextBox.ReadOnly = true;
            this.m_FramesPerSecondTextBox.Size = new System.Drawing.Size(52, 20);
            this.m_FramesPerSecondTextBox.TabIndex = 19;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(9, 19);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(24, 13);
            this.label16.TabIndex = 18;
            this.label16.Text = "fps:";
            // 
            // m_Timer
            // 
            this.m_Timer.Tick += new System.EventHandler(this.OnTimerTick);
            // 
            // m_ImageBox
            // 
            this.m_ImageBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_ImageBox.Location = new System.Drawing.Point(0, 67);
            this.m_ImageBox.Margin = new System.Windows.Forms.Padding(2);
            this.m_ImageBox.Name = "m_ImageBox";
            this.m_ImageBox.Size = new System.Drawing.Size(672, 351);
            this.m_ImageBox.TabIndex = 2;
            this.m_ImageBox.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 531);
            this.Controls.Add(this.m_ImageBox);
            this.Controls.Add(this.m_BottomPanel);
            this.Controls.Add(this.m_TopPanel);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Text = "Visual Odometry";
            this.m_TopPanel.ResumeLayout(false);
            this.m_TopPanel.PerformLayout();
            this.m_BottomPanel.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_ImageBox)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel m_TopPanel;
		private System.Windows.Forms.TextBox m_MinDistanceTextBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox m_QualityLevelTextBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox m_BlockSizeTextBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox m_MaxFeatureCountTextBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel m_BottomPanel;
        private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox m_GroundTopTextBox;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox m_SkyBottomTextBox;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Button m_ApplyButton;
        private System.Windows.Forms.Timer m_Timer;
        private System.Windows.Forms.CheckBox m_DrawFeaturesCheckBox;
		private System.Windows.Forms.GroupBox groupBox2;
        private Emgu.CV.UI.ImageBox m_ImageBox;
		private System.Windows.Forms.TextBox m_FramesPerSecondTextBox;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button Connect_btn;
        private System.Windows.Forms.CheckBox m_SurfCheckbox;
        private System.Windows.Forms.CheckBox m_ObstaclesCheckBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
	}
}

