namespace SIP_Implementation
{
    partial class AppWindow
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AppWindow));
            boxOpen = new OpenFileDialog();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            panel3 = new Panel();
            dgv = new DataGridView();
            clGroup = new DataGridViewTextBoxColumn();
            clLoop = new DataGridViewCheckBoxColumn();
            clName = new DataGridViewTextBoxColumn();
            clX = new DataGridViewTextBoxColumn();
            clY = new DataGridViewTextBoxColumn();
            clHeight = new DataGridViewTextBoxColumn();
            clWidth = new DataGridViewTextBoxColumn();
            clLength = new DataGridViewTextBoxColumn();
            panel4 = new Panel();
            listCols = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
            panel2 = new Panel();
            bLoad = new Button();
            tbYTolerance = new MaskedTextBox();
            lbFound = new Label();
            label2 = new Label();
            panel1 = new Panel();
            spriteBox = new PictureBox();
            tabPage2 = new TabPage();
            jsonOut = new TextBox();
            tabPage3 = new TabPage();
            xmlOut = new TextBox();
            tabPage4 = new TabPage();
            label1 = new Label();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgv).BeginInit();
            panel4.SuspendLayout();
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)spriteBox).BeginInit();
            tabPage2.SuspendLayout();
            tabPage3.SuspendLayout();
            tabPage4.SuspendLayout();
            SuspendLayout();
            // 
            // boxOpen
            // 
            boxOpen.Filter = "PNG images|*.png|GIF images|*.gif";
            boxOpen.OkRequiresInteraction = true;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1098, 634);
            tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(panel3);
            tabPage1.Controls.Add(panel1);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1090, 606);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Preview";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            panel3.Controls.Add(dgv);
            panel3.Controls.Add(panel4);
            panel3.Dock = DockStyle.Left;
            panel3.Location = new Point(3, 3);
            panel3.Name = "panel3";
            panel3.Size = new Size(636, 600);
            panel3.TabIndex = 9;
            // 
            // dgv
            // 
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv.Columns.AddRange(new DataGridViewColumn[] { clGroup, clLoop, clName, clX, clY, clHeight, clWidth, clLength });
            dgv.Dock = DockStyle.Fill;
            dgv.EditMode = DataGridViewEditMode.EditOnF2;
            dgv.Location = new Point(0, 266);
            dgv.Name = "dgv";
            dgv.Size = new Size(636, 334);
            dgv.TabIndex = 12;
            // 
            // clGroup
            // 
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            clGroup.DefaultCellStyle = dataGridViewCellStyle1;
            clGroup.HeaderText = "Group";
            clGroup.MaxInputLength = 50;
            clGroup.Name = "clGroup";
            // 
            // clLoop
            // 
            clLoop.HeaderText = "Loop?";
            clLoop.Name = "clLoop";
            clLoop.Resizable = DataGridViewTriState.False;
            clLoop.Width = 45;
            // 
            // clName
            // 
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            clName.DefaultCellStyle = dataGridViewCellStyle2;
            clName.HeaderText = "Frame Name";
            clName.MaxInputLength = 50;
            clName.Name = "clName";
            // 
            // clX
            // 
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            clX.DefaultCellStyle = dataGridViewCellStyle3;
            clX.HeaderText = "X Pos.";
            clX.Name = "clX";
            clX.ReadOnly = true;
            clX.Resizable = DataGridViewTriState.False;
            clX.Width = 65;
            // 
            // clY
            // 
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            clY.DefaultCellStyle = dataGridViewCellStyle4;
            clY.HeaderText = "Y Pos.";
            clY.Name = "clY";
            clY.ReadOnly = true;
            clY.Resizable = DataGridViewTriState.False;
            clY.Width = 65;
            // 
            // clHeight
            // 
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
            clHeight.DefaultCellStyle = dataGridViewCellStyle5;
            clHeight.HeaderText = "Height";
            clHeight.Name = "clHeight";
            clHeight.ReadOnly = true;
            clHeight.Resizable = DataGridViewTriState.False;
            clHeight.Width = 65;
            // 
            // clWidth
            // 
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
            clWidth.DefaultCellStyle = dataGridViewCellStyle6;
            clWidth.HeaderText = "Width";
            clWidth.Name = "clWidth";
            clWidth.ReadOnly = true;
            clWidth.Resizable = DataGridViewTriState.False;
            clWidth.Width = 65;
            // 
            // clLength
            // 
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleCenter;
            clLength.DefaultCellStyle = dataGridViewCellStyle7;
            clLength.HeaderText = "Duration";
            clLength.MaxInputLength = 5;
            clLength.Name = "clLength";
            clLength.Resizable = DataGridViewTriState.False;
            clLength.Width = 65;
            // 
            // panel4
            // 
            panel4.Controls.Add(listCols);
            panel4.Controls.Add(panel2);
            panel4.Dock = DockStyle.Top;
            panel4.Location = new Point(0, 0);
            panel4.Name = "panel4";
            panel4.Size = new Size(636, 266);
            panel4.TabIndex = 9;
            // 
            // listCols
            // 
            listCols.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3, columnHeader4 });
            listCols.Dock = DockStyle.Fill;
            listCols.FullRowSelect = true;
            listCols.GridLines = true;
            listCols.Location = new Point(0, 83);
            listCols.Name = "listCols";
            listCols.Size = new Size(636, 183);
            listCols.TabIndex = 9;
            listCols.UseCompatibleStateImageBehavior = false;
            listCols.View = View.Details;
            listCols.Visible = false;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "X Pos.";
            columnHeader1.Width = 70;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Y Pos.";
            columnHeader2.Width = 70;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Width";
            columnHeader3.Width = 70;
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "Height";
            columnHeader4.Width = 70;
            // 
            // panel2
            // 
            panel2.Controls.Add(bLoad);
            panel2.Controls.Add(tbYTolerance);
            panel2.Controls.Add(lbFound);
            panel2.Controls.Add(label2);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(636, 83);
            panel2.TabIndex = 10;
            // 
            // bLoad
            // 
            bLoad.Location = new Point(2, 15);
            bLoad.Name = "bLoad";
            bLoad.Size = new Size(78, 23);
            bLoad.TabIndex = 3;
            bLoad.Text = "Load image";
            bLoad.UseVisualStyleBackColor = true;
            // 
            // tbYTolerance
            // 
            tbYTolerance.Location = new Point(254, 16);
            tbYTolerance.Mask = "00000";
            tbYTolerance.Name = "tbYTolerance";
            tbYTolerance.PromptChar = ' ';
            tbYTolerance.Size = new Size(52, 23);
            tbYTolerance.TabIndex = 9;
            tbYTolerance.TextAlign = HorizontalAlignment.Center;
            tbYTolerance.ValidatingType = typeof(int);
            // 
            // lbFound
            // 
            lbFound.Location = new Point(1, 48);
            lbFound.Name = "lbFound";
            lbFound.Size = new Size(310, 23);
            lbFound.TabIndex = 6;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(153, 19);
            label2.Name = "label2";
            label2.Size = new Size(95, 15);
            label2.TabIndex = 8;
            label2.Text = "Y-axis Tolerance:";
            // 
            // panel1
            // 
            panel1.AutoScroll = true;
            panel1.BackgroundImage = (Image)resources.GetObject("panel1.BackgroundImage");
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(spriteBox);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(1084, 600);
            panel1.TabIndex = 7;
            // 
            // spriteBox
            // 
            spriteBox.Location = new Point(0, 0);
            spriteBox.Name = "spriteBox";
            spriteBox.Size = new Size(205, 176);
            spriteBox.TabIndex = 7;
            spriteBox.TabStop = false;
            spriteBox.Paint += SpriteBox_Paint;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(jsonOut);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1090, 606);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "JSON Output";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // jsonOut
            // 
            jsonOut.Dock = DockStyle.Fill;
            jsonOut.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            jsonOut.Location = new Point(3, 3);
            jsonOut.Multiline = true;
            jsonOut.Name = "jsonOut";
            jsonOut.ReadOnly = true;
            jsonOut.ScrollBars = ScrollBars.Vertical;
            jsonOut.Size = new Size(1084, 600);
            jsonOut.TabIndex = 0;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(xmlOut);
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(1090, 606);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "XML Output";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // xmlOut
            // 
            xmlOut.Dock = DockStyle.Fill;
            xmlOut.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            xmlOut.Location = new Point(0, 0);
            xmlOut.Multiline = true;
            xmlOut.Name = "xmlOut";
            xmlOut.ReadOnly = true;
            xmlOut.ScrollBars = ScrollBars.Vertical;
            xmlOut.Size = new Size(1090, 606);
            xmlOut.TabIndex = 1;
            // 
            // tabPage4
            // 
            tabPage4.Controls.Add(label1);
            tabPage4.Location = new Point(4, 24);
            tabPage4.Name = "tabPage4";
            tabPage4.Size = new Size(1090, 606);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "About";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.Location = new Point(19, 21);
            label1.Name = "label1";
            label1.Size = new Size(674, 418);
            label1.TabIndex = 0;
            label1.Text = resources.GetString("label1.Text");
            // 
            // AppWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1098, 634);
            Controls.Add(tabControl1);
            MinimumSize = new Size(717, 658);
            Name = "AppWindow";
            SizeGripStyle = SizeGripStyle.Hide;
            Text = "SIP Implementation Sample";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgv).EndInit();
            panel4.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)spriteBox).EndInit();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            tabPage4.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private OpenFileDialog boxOpen;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TextBox jsonOut;
        private TabPage tabPage3;
        private TextBox xmlOut;
        private TabPage tabPage4;
        private Label label1;
        private Panel panel1;
        private PictureBox spriteBox;
        private Panel panel3;
        private DataGridView dgv;
        private Panel panel4;
        private ListView listCols;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private Panel panel2;
        private Button bLoad;
        private MaskedTextBox tbYTolerance;
        private Label lbFound;
        private Label label2;
        private DataGridViewTextBoxColumn clGroup;
        private DataGridViewCheckBoxColumn clLoop;
        private DataGridViewTextBoxColumn clName;
        private DataGridViewTextBoxColumn clX;
        private DataGridViewTextBoxColumn clY;
        private DataGridViewTextBoxColumn clHeight;
        private DataGridViewTextBoxColumn clWidth;
        private DataGridViewTextBoxColumn clLength;
    }
}
