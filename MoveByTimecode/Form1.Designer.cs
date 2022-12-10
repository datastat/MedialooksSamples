namespace MedialooksMoveByTimecode
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelPreview1 = new System.Windows.Forms.Panel();
            this.panelPreview2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbPosition = new System.Windows.Forms.TrackBar();
            this.button2 = new System.Windows.Forms.Button();
            this.btnGoToTC = new System.Windows.Forms.Button();
            this.tbRequestTC = new System.Windows.Forms.TextBox();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnFromBeginning = new System.Windows.Forms.Button();
            this.btnFramePrev = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tbInfo = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbPosition)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panelPreview1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelPreview2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbInfo, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 450);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panelPreview1
            // 
            this.panelPreview1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPreview1.Location = new System.Drawing.Point(3, 3);
            this.panelPreview1.Name = "panelPreview1";
            this.panelPreview1.Size = new System.Drawing.Size(394, 219);
            this.panelPreview1.TabIndex = 0;
            // 
            // panelPreview2
            // 
            this.panelPreview2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPreview2.Location = new System.Drawing.Point(403, 3);
            this.panelPreview2.Name = "panelPreview2";
            this.panelPreview2.Size = new System.Drawing.Size(394, 219);
            this.panelPreview2.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tbPosition);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.btnGoToTC);
            this.panel1.Controls.Add(this.tbRequestTC);
            this.panel1.Controls.Add(this.hScrollBar1);
            this.panel1.Controls.Add(this.btnPause);
            this.panel1.Controls.Add(this.btnFromBeginning);
            this.panel1.Controls.Add(this.btnFramePrev);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 228);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(394, 219);
            this.panel1.TabIndex = 4;
            // 
            // tbPosition
            // 
            this.tbPosition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPosition.Location = new System.Drawing.Point(14, 34);
            this.tbPosition.Maximum = 10000;
            this.tbPosition.Name = "tbPosition";
            this.tbPosition.Size = new System.Drawing.Size(369, 45);
            this.tbPosition.TabIndex = 11;
            this.tbPosition.Scroll += new System.EventHandler(this.tbPosition_Scroll);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(3, 166);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 10;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnGoToTC
            // 
            this.btnGoToTC.Location = new System.Drawing.Point(109, 137);
            this.btnGoToTC.Name = "btnGoToTC";
            this.btnGoToTC.Size = new System.Drawing.Size(75, 23);
            this.btnGoToTC.TabIndex = 9;
            this.btnGoToTC.Text = "button2";
            this.btnGoToTC.UseVisualStyleBackColor = true;
            this.btnGoToTC.Click += new System.EventHandler(this.btnGoToTC_Click);
            // 
            // tbRequestTC
            // 
            this.tbRequestTC.Location = new System.Drawing.Point(3, 137);
            this.tbRequestTC.Name = "tbRequestTC";
            this.tbRequestTC.Size = new System.Drawing.Size(100, 23);
            this.tbRequestTC.TabIndex = 8;
            this.tbRequestTC.Text = "10:00:00:39";
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Location = new System.Drawing.Point(14, 11);
            this.hScrollBar1.Maximum = 1000;
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(369, 20);
            this.hScrollBar1.TabIndex = 7;
            this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar1_Scroll);
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(3, 108);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(75, 23);
            this.btnPause.TabIndex = 6;
            this.btnPause.Text = "pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnFromBeginning
            // 
            this.btnFromBeginning.Location = new System.Drawing.Point(3, 79);
            this.btnFromBeginning.Name = "btnFromBeginning";
            this.btnFromBeginning.Size = new System.Drawing.Size(75, 23);
            this.btnFromBeginning.TabIndex = 5;
            this.btnFromBeginning.Text = "beginning";
            this.btnFromBeginning.UseVisualStyleBackColor = true;
            // 
            // btnFramePrev
            // 
            this.btnFramePrev.Location = new System.Drawing.Point(82, 108);
            this.btnFramePrev.Name = "btnFramePrev";
            this.btnFramePrev.Size = new System.Drawing.Size(75, 23);
            this.btnFramePrev.TabIndex = 4;
            this.btnFramePrev.Text = "frame prev";
            this.btnFramePrev.UseVisualStyleBackColor = true;
            this.btnFramePrev.Click += new System.EventHandler(this.button_PreviousFrame);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(163, 108);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "frame next";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button_NextFrame);
            // 
            // tbInfo
            // 
            this.tbInfo.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbInfo.Location = new System.Drawing.Point(403, 228);
            this.tbInfo.Multiline = true;
            this.tbInfo.Name = "tbInfo";
            this.tbInfo.Size = new System.Drawing.Size(359, 131);
            this.tbInfo.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbPosition)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Panel panelPreview1;
        private Panel panelPreview2;
        private Panel panel1;
        private Button btnPause;
        private Button btnFromBeginning;
        private Button btnFramePrev;
        private Button button1;
        private TextBox tbInfo;
        private HScrollBar hScrollBar1;
        private TextBox tbRequestTC;
        private Button btnGoToTC;
        private Button button2;
        private TrackBar tbPosition;
    }
}