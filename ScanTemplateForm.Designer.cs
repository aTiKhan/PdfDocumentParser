﻿namespace Cliver.PdfDocumentParser
{
    partial class ScanTemplateForm
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
            this.bCancel = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.bApply = new System.Windows.Forms.Button();
            this.defaultBitmapPreprocessorClassDefinitions = new System.Windows.Forms.ComboBox();
            this.bAddClassDefinition = new System.Windows.Forms.Button();
            this.bitmapPreprocessorClassDefinition = new ICSharpCode.TextEditor.TextEditorControl();
            this.DeskewBlockMaxHeight = new System.Windows.Forms.NumericUpDown();
            this.PageRotation = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ScalingAnchor = new System.Windows.Forms.ComboBox();
            this.DeskewBlockMinGap = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.gDeskew = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.DeskewMarginColor = new System.Windows.Forms.Button();
            this.DeskewContourMaxCount = new System.Windows.Forms.NumericUpDown();
            this.DeskewAngleMaxDeviation = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.DeskewColumnOfBlocks = new System.Windows.Forms.RadioButton();
            this.DeskewSingleBlock = new System.Windows.Forms.RadioButton();
            this.Deskew = new System.Windows.Forms.CheckBox();
            this.DeskewStructuringElementX = new System.Windows.Forms.NumericUpDown();
            this.DeskewStructuringElementY = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DeskewBlockMaxHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeskewBlockMinGap)).BeginInit();
            this.gDeskew.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DeskewContourMaxCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeskewAngleMaxDeviation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeskewStructuringElementX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeskewStructuringElementY)).BeginInit();
            this.SuspendLayout();
            // 
            // bCancel
            // 
            this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bCancel.Location = new System.Drawing.Point(863, 3);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 50;
            this.bCancel.Text = "Close";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.bCancel);
            this.flowLayoutPanel1.Controls.Add(this.bApply);
            this.flowLayoutPanel1.Controls.Add(this.defaultBitmapPreprocessorClassDefinitions);
            this.flowLayoutPanel1.Controls.Add(this.bAddClassDefinition);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 375);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(941, 31);
            this.flowLayoutPanel1.TabIndex = 60;
            // 
            // bApply
            // 
            this.bApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bApply.Location = new System.Drawing.Point(782, 3);
            this.bApply.Name = "bApply";
            this.bApply.Size = new System.Drawing.Size(75, 23);
            this.bApply.TabIndex = 51;
            this.bApply.Text = "Apply";
            this.bApply.UseVisualStyleBackColor = true;
            this.bApply.Click += new System.EventHandler(this.bApply_Click);
            // 
            // defaultBitmapPreprocessorClassDefinitions
            // 
            this.defaultBitmapPreprocessorClassDefinitions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.defaultBitmapPreprocessorClassDefinitions.FormattingEnabled = true;
            this.defaultBitmapPreprocessorClassDefinitions.Items.AddRange(new object[] {
            "",
            "↻ 90°",
            "↻ 180°",
            "↺ 90°",
            "Auto"});
            this.defaultBitmapPreprocessorClassDefinitions.Location = new System.Drawing.Point(555, 3);
            this.defaultBitmapPreprocessorClassDefinitions.Name = "defaultBitmapPreprocessorClassDefinitions";
            this.defaultBitmapPreprocessorClassDefinitions.Size = new System.Drawing.Size(221, 21);
            this.defaultBitmapPreprocessorClassDefinitions.TabIndex = 85;
            this.defaultBitmapPreprocessorClassDefinitions.SelectedIndexChanged += new System.EventHandler(this.defaults_SelectedIndexChanged);
            // 
            // bAddClassDefinition
            // 
            this.bAddClassDefinition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bAddClassDefinition.Location = new System.Drawing.Point(515, 3);
            this.bAddClassDefinition.Name = "bAddClassDefinition";
            this.bAddClassDefinition.Size = new System.Drawing.Size(34, 23);
            this.bAddClassDefinition.TabIndex = 53;
            this.bAddClassDefinition.Text = "+";
            this.bAddClassDefinition.UseVisualStyleBackColor = true;
            // 
            // bitmapPreprocessorClassDefinition
            // 
            this.bitmapPreprocessorClassDefinition.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bitmapPreprocessorClassDefinition.AutoHideScrollbars = false;
            this.bitmapPreprocessorClassDefinition.Location = new System.Drawing.Point(1, 97);
            this.bitmapPreprocessorClassDefinition.Margin = new System.Windows.Forms.Padding(1);
            this.bitmapPreprocessorClassDefinition.Name = "bitmapPreprocessorClassDefinition";
            this.bitmapPreprocessorClassDefinition.ShowVRuler = false;
            this.bitmapPreprocessorClassDefinition.Size = new System.Drawing.Size(941, 277);
            this.bitmapPreprocessorClassDefinition.TabIndex = 78;
            // 
            // DeskewBlockMaxHeight
            // 
            this.DeskewBlockMaxHeight.Location = new System.Drawing.Point(602, 19);
            this.DeskewBlockMaxHeight.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.DeskewBlockMaxHeight.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.DeskewBlockMaxHeight.Name = "DeskewBlockMaxHeight";
            this.DeskewBlockMaxHeight.Size = new System.Drawing.Size(57, 20);
            this.DeskewBlockMaxHeight.TabIndex = 83;
            this.DeskewBlockMaxHeight.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // PageRotation
            // 
            this.PageRotation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PageRotation.FormattingEnabled = true;
            this.PageRotation.Items.AddRange(new object[] {
            "",
            "↻ 90°",
            "↻ 180°",
            "↺ 90°",
            "Auto"});
            this.PageRotation.Location = new System.Drawing.Point(88, 12);
            this.PageRotation.Name = "PageRotation";
            this.PageRotation.Size = new System.Drawing.Size(60, 21);
            this.PageRotation.TabIndex = 80;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(506, 25);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(94, 13);
            this.label14.TabIndex = 82;
            this.label14.Text = "Block Max Height:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 79;
            this.label1.Text = "Rotate Pages:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 87;
            this.label2.Text = "Scale By Anchor:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 13);
            this.label3.TabIndex = 88;
            this.label3.Text = "Preprocessing Code:";
            // 
            // ScalingAnchor
            // 
            this.ScalingAnchor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ScalingAnchor.FormattingEnabled = true;
            this.ScalingAnchor.Items.AddRange(new object[] {
            "",
            "↻ 90°",
            "↻ 180°",
            "↺ 90°",
            "Auto"});
            this.ScalingAnchor.Location = new System.Drawing.Point(104, 39);
            this.ScalingAnchor.Name = "ScalingAnchor";
            this.ScalingAnchor.Size = new System.Drawing.Size(44, 21);
            this.ScalingAnchor.TabIndex = 89;
            // 
            // DeskewBlockMinGap
            // 
            this.DeskewBlockMinGap.Location = new System.Drawing.Point(602, 41);
            this.DeskewBlockMinGap.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.DeskewBlockMinGap.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.DeskewBlockMinGap.Name = "DeskewBlockMinGap";
            this.DeskewBlockMinGap.Size = new System.Drawing.Size(57, 20);
            this.DeskewBlockMinGap.TabIndex = 96;
            this.DeskewBlockMinGap.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(506, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 95;
            this.label4.Text = "Block Min Gap:";
            // 
            // gDeskew
            // 
            this.gDeskew.Controls.Add(this.label11);
            this.gDeskew.Controls.Add(this.DeskewMarginColor);
            this.gDeskew.Controls.Add(this.DeskewContourMaxCount);
            this.gDeskew.Controls.Add(this.DeskewAngleMaxDeviation);
            this.gDeskew.Controls.Add(this.label8);
            this.gDeskew.Controls.Add(this.label9);
            this.gDeskew.Controls.Add(this.label5);
            this.gDeskew.Controls.Add(this.label6);
            this.gDeskew.Controls.Add(this.DeskewColumnOfBlocks);
            this.gDeskew.Controls.Add(this.DeskewSingleBlock);
            this.gDeskew.Controls.Add(this.Deskew);
            this.gDeskew.Controls.Add(this.DeskewStructuringElementX);
            this.gDeskew.Controls.Add(this.DeskewStructuringElementY);
            this.gDeskew.Controls.Add(this.label7);
            this.gDeskew.Controls.Add(this.label10);
            this.gDeskew.Controls.Add(this.DeskewBlockMaxHeight);
            this.gDeskew.Controls.Add(this.DeskewBlockMinGap);
            this.gDeskew.Controls.Add(this.label4);
            this.gDeskew.Controls.Add(this.label14);
            this.gDeskew.Location = new System.Drawing.Point(181, 14);
            this.gDeskew.Name = "gDeskew";
            this.gDeskew.Size = new System.Drawing.Size(747, 72);
            this.gDeskew.TabIndex = 99;
            this.gDeskew.TabStop = false;
            this.gDeskew.Text = "Deskew:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.SystemColors.Control;
            this.label11.Location = new System.Drawing.Point(674, 25);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(69, 13);
            this.label11.TabIndex = 113;
            this.label11.Text = "Margin Color:";
            // 
            // DeskewMarginColor
            // 
            this.DeskewMarginColor.BackColor = System.Drawing.Color.LightGreen;
            this.DeskewMarginColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeskewMarginColor.ForeColor = System.Drawing.SystemColors.Control;
            this.DeskewMarginColor.Location = new System.Drawing.Point(677, 41);
            this.DeskewMarginColor.Name = "DeskewMarginColor";
            this.DeskewMarginColor.Size = new System.Drawing.Size(61, 18);
            this.DeskewMarginColor.TabIndex = 114;
            this.DeskewMarginColor.UseVisualStyleBackColor = false;
            this.DeskewMarginColor.Click += new System.EventHandler(this.DeskewMarginColor_Click);
            // 
            // DeskewContourMaxCount
            // 
            this.DeskewContourMaxCount.Location = new System.Drawing.Point(302, 18);
            this.DeskewContourMaxCount.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.DeskewContourMaxCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.DeskewContourMaxCount.Name = "DeskewContourMaxCount";
            this.DeskewContourMaxCount.Size = new System.Drawing.Size(57, 20);
            this.DeskewContourMaxCount.TabIndex = 110;
            this.DeskewContourMaxCount.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // DeskewAngleMaxDeviation
            // 
            this.DeskewAngleMaxDeviation.DecimalPlaces = 3;
            this.DeskewAngleMaxDeviation.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.DeskewAngleMaxDeviation.Location = new System.Drawing.Point(302, 40);
            this.DeskewAngleMaxDeviation.Maximum = new decimal(new int[] {
            45,
            0,
            0,
            0});
            this.DeskewAngleMaxDeviation.Name = "DeskewAngleMaxDeviation";
            this.DeskewAngleMaxDeviation.Size = new System.Drawing.Size(57, 20);
            this.DeskewAngleMaxDeviation.TabIndex = 112;
            this.DeskewAngleMaxDeviation.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(189, 46);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(108, 13);
            this.label8.TabIndex = 111;
            this.label8.Text = "Angle Max Deviation:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(189, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(101, 13);
            this.label9.TabIndex = 109;
            this.label9.Text = "Contour Max Count:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(400, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 13);
            this.label5.TabIndex = 100;
            this.label5.Text = "Column Of Blocks";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(400, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 13);
            this.label6.TabIndex = 101;
            this.label6.Text = "Single Block";
            // 
            // DeskewColumnOfBlocks
            // 
            this.DeskewColumnOfBlocks.AutoSize = true;
            this.DeskewColumnOfBlocks.Location = new System.Drawing.Point(384, 47);
            this.DeskewColumnOfBlocks.Name = "DeskewColumnOfBlocks";
            this.DeskewColumnOfBlocks.Size = new System.Drawing.Size(14, 13);
            this.DeskewColumnOfBlocks.TabIndex = 104;
            this.DeskewColumnOfBlocks.UseVisualStyleBackColor = true;
            // 
            // DeskewSingleBlock
            // 
            this.DeskewSingleBlock.AutoSize = true;
            this.DeskewSingleBlock.Checked = true;
            this.DeskewSingleBlock.Location = new System.Drawing.Point(384, 24);
            this.DeskewSingleBlock.Name = "DeskewSingleBlock";
            this.DeskewSingleBlock.Size = new System.Drawing.Size(14, 13);
            this.DeskewSingleBlock.TabIndex = 103;
            this.DeskewSingleBlock.TabStop = true;
            this.DeskewSingleBlock.UseVisualStyleBackColor = true;
            // 
            // Deskew
            // 
            this.Deskew.AutoSize = true;
            this.Deskew.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Deskew.Location = new System.Drawing.Point(58, 0);
            this.Deskew.Name = "Deskew";
            this.Deskew.Size = new System.Drawing.Size(15, 14);
            this.Deskew.TabIndex = 102;
            this.Deskew.UseVisualStyleBackColor = true;
            // 
            // DeskewStructuringElementX
            // 
            this.DeskewStructuringElementX.Location = new System.Drawing.Point(137, 20);
            this.DeskewStructuringElementX.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.DeskewStructuringElementX.Name = "DeskewStructuringElementX";
            this.DeskewStructuringElementX.Size = new System.Drawing.Size(35, 20);
            this.DeskewStructuringElementX.TabIndex = 101;
            this.DeskewStructuringElementX.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // DeskewStructuringElementY
            // 
            this.DeskewStructuringElementY.Location = new System.Drawing.Point(137, 41);
            this.DeskewStructuringElementY.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.DeskewStructuringElementY.Name = "DeskewStructuringElementY";
            this.DeskewStructuringElementY.Size = new System.Drawing.Size(35, 20);
            this.DeskewStructuringElementY.TabIndex = 100;
            this.DeskewStructuringElementY.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 47);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(112, 13);
            this.label7.TabIndex = 99;
            this.label7.Text = "Structuring Element Y:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(19, 25);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(112, 13);
            this.label10.TabIndex = 97;
            this.label10.Text = "Structuring Element X:";
            // 
            // ScanTemplateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(941, 406);
            this.Controls.Add(this.gDeskew);
            this.Controls.Add(this.ScalingAnchor);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PageRotation);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bitmapPreprocessorClassDefinition);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "ScanTemplateForm";
            this.Text = "Scanned Document Settings";
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DeskewBlockMaxHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeskewBlockMinGap)).EndInit();
            this.gDeskew.ResumeLayout(false);
            this.gDeskew.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DeskewContourMaxCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeskewAngleMaxDeviation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeskewStructuringElementX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeskewStructuringElementY)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bApply;
        private ICSharpCode.TextEditor.TextEditorControl bitmapPreprocessorClassDefinition;
        private System.Windows.Forms.NumericUpDown DeskewBlockMaxHeight;
        private System.Windows.Forms.ComboBox PageRotation;
        private System.Windows.Forms.Button bAddClassDefinition;
        private System.Windows.Forms.ComboBox defaultBitmapPreprocessorClassDefinitions;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox ScalingAnchor;
        private System.Windows.Forms.NumericUpDown DeskewBlockMinGap;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox gDeskew;
        private System.Windows.Forms.NumericUpDown DeskewStructuringElementY;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown DeskewStructuringElementX;
        private System.Windows.Forms.CheckBox Deskew;
        private System.Windows.Forms.RadioButton DeskewSingleBlock;
        private System.Windows.Forms.RadioButton DeskewColumnOfBlocks;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown DeskewContourMaxCount;
        private System.Windows.Forms.NumericUpDown DeskewAngleMaxDeviation;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button DeskewMarginColor;
    }
}