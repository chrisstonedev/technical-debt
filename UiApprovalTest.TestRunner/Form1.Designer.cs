
using System;
using System.Windows.Forms;

namespace UiApprovalTest.TestRunner
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.Windows.Forms.Label windowTitleLabel;
            System.Windows.Forms.Panel leftPanel;
            System.Windows.Forms.Label expectedLabel;
            System.Windows.Forms.Label actualLabel;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.closeProgramButton = new System.Windows.Forms.Button();
            this.checkProgramIsOpenButton = new System.Windows.Forms.Button();
            this.windowTitleTextBox = new System.Windows.Forms.TextBox();
            this.runTestsButton = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.testResultsTabPage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.testResultsDataGridView = new System.Windows.Forms.DataGridView();
            this.classNameTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.testResultNameTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.testResultStatusTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.expectedPictureBox = new System.Windows.Forms.PictureBox();
            this.actualPictureBox = new System.Windows.Forms.PictureBox();
            this.errorDetailsTextBox = new System.Windows.Forms.TextBox();
            this.windowControlsTabPage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.listWindowControlsButton = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.WindowHandle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClassName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ParentHandle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.windowControlTextBox = new System.Windows.Forms.TextBox();
            this.approveNewChangeButton = new System.Windows.Forms.Button();
            windowTitleLabel = new System.Windows.Forms.Label();
            leftPanel = new System.Windows.Forms.Panel();
            expectedLabel = new System.Windows.Forms.Label();
            actualLabel = new System.Windows.Forms.Label();
            leftPanel.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.testResultsTabPage.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.testResultsDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.expectedPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.actualPictureBox)).BeginInit();
            this.windowControlsTabPage.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // windowTitleLabel
            // 
            windowTitleLabel.AutoSize = true;
            windowTitleLabel.Location = new System.Drawing.Point(12, 9);
            windowTitleLabel.Name = "windowTitleLabel";
            windowTitleLabel.Size = new System.Drawing.Size(74, 15);
            windowTitleLabel.TabIndex = 7;
            windowTitleLabel.Text = "Window title";
            // 
            // leftPanel
            // 
            leftPanel.Controls.Add(windowTitleLabel);
            leftPanel.Controls.Add(this.closeProgramButton);
            leftPanel.Controls.Add(this.checkProgramIsOpenButton);
            leftPanel.Controls.Add(this.windowTitleTextBox);
            leftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            leftPanel.Location = new System.Drawing.Point(0, 0);
            leftPanel.Name = "leftPanel";
            leftPanel.Size = new System.Drawing.Size(167, 505);
            leftPanel.TabIndex = 9;
            // 
            // closeProgramButton
            // 
            this.closeProgramButton.Location = new System.Drawing.Point(12, 90);
            this.closeProgramButton.Name = "closeProgramButton";
            this.closeProgramButton.Size = new System.Drawing.Size(144, 28);
            this.closeProgramButton.TabIndex = 2;
            this.closeProgramButton.Text = "Close the program";
            this.closeProgramButton.Click += new System.EventHandler(this.CloseProgramButton_Click);
            // 
            // checkProgramIsOpenButton
            // 
            this.checkProgramIsOpenButton.Location = new System.Drawing.Point(12, 56);
            this.checkProgramIsOpenButton.Name = "checkProgramIsOpenButton";
            this.checkProgramIsOpenButton.Size = new System.Drawing.Size(144, 28);
            this.checkProgramIsOpenButton.TabIndex = 3;
            this.checkProgramIsOpenButton.Text = "Check program is open";
            this.checkProgramIsOpenButton.Click += new System.EventHandler(this.CheckProgramIsOpenButton_Click);
            // 
            // windowTitleTextBox
            // 
            this.windowTitleTextBox.Location = new System.Drawing.Point(12, 27);
            this.windowTitleTextBox.Name = "windowTitleTextBox";
            this.windowTitleTextBox.Size = new System.Drawing.Size(144, 23);
            this.windowTitleTextBox.TabIndex = 6;
            this.windowTitleTextBox.Text = "Order Client";
            // 
            // expectedLabel
            // 
            expectedLabel.AutoSize = true;
            expectedLabel.Location = new System.Drawing.Point(228, 230);
            expectedLabel.Name = "expectedLabel";
            expectedLabel.Size = new System.Drawing.Size(58, 15);
            expectedLabel.TabIndex = 11;
            expectedLabel.Text = "Expected:";
            // 
            // actualLabel
            // 
            actualLabel.AutoSize = true;
            actualLabel.Location = new System.Drawing.Point(454, 230);
            actualLabel.Name = "actualLabel";
            actualLabel.Size = new System.Drawing.Size(44, 15);
            actualLabel.TabIndex = 12;
            actualLabel.Text = "Actual:";
            // 
            // runTestsButton
            // 
            this.runTestsButton.Location = new System.Drawing.Point(3, 3);
            this.runTestsButton.Name = "runTestsButton";
            this.runTestsButton.Size = new System.Drawing.Size(144, 28);
            this.runTestsButton.TabIndex = 0;
            this.runTestsButton.Text = "Run tests";
            this.runTestsButton.Click += new System.EventHandler(this.RunClientTestButton_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.testResultsTabPage);
            this.tabControl.Controls.Add(this.windowControlsTabPage);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(167, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(692, 505);
            this.tabControl.TabIndex = 8;
            // 
            // testResultsTabPage
            // 
            this.testResultsTabPage.Controls.Add(this.tableLayoutPanel1);
            this.testResultsTabPage.Location = new System.Drawing.Point(4, 24);
            this.testResultsTabPage.Name = "testResultsTabPage";
            this.testResultsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.testResultsTabPage.Size = new System.Drawing.Size(684, 477);
            this.testResultsTabPage.TabIndex = 1;
            this.testResultsTabPage.Text = "Test results";
            this.testResultsTabPage.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33332F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.Controls.Add(this.runTestsButton, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.testResultsDataGridView, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.expectedPictureBox, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.actualPictureBox, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.errorDetailsTextBox, 0, 2);
            this.tableLayoutPanel1.Controls.Add(expectedLabel, 1, 2);
            this.tableLayoutPanel1.Controls.Add(actualLabel, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.approveNewChangeButton, 1, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(678, 471);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // testResultsDataGridView
            // 
            this.testResultsDataGridView.AllowUserToAddRows = false;
            this.testResultsDataGridView.AllowUserToDeleteRows = false;
            this.testResultsDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.testResultsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.testResultsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.classNameTextBoxColumn,
            this.testResultNameTextBoxColumn,
            this.testResultStatusTextBoxColumn});
            this.tableLayoutPanel1.SetColumnSpan(this.testResultsDataGridView, 3);
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.testResultsDataGridView.DefaultCellStyle = dataGridViewCellStyle1;
            this.testResultsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.testResultsDataGridView.Location = new System.Drawing.Point(3, 37);
            this.testResultsDataGridView.Name = "testResultsDataGridView";
            this.testResultsDataGridView.ReadOnly = true;
            this.testResultsDataGridView.RowTemplate.Height = 25;
            this.testResultsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.testResultsDataGridView.Size = new System.Drawing.Size(672, 190);
            this.testResultsDataGridView.TabIndex = 7;
            this.testResultsDataGridView.SelectionChanged += new System.EventHandler(this.TestResultsDataGridView_SelectionChanged);
            // 
            // classNameTextBoxColumn
            // 
            this.classNameTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.classNameTextBoxColumn.HeaderText = "Class";
            this.classNameTextBoxColumn.Name = "classNameTextBoxColumn";
            this.classNameTextBoxColumn.ReadOnly = true;
            // 
            // testResultNameTextBoxColumn
            // 
            this.testResultNameTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.testResultNameTextBoxColumn.HeaderText = "Test";
            this.testResultNameTextBoxColumn.Name = "testResultNameTextBoxColumn";
            this.testResultNameTextBoxColumn.ReadOnly = true;
            // 
            // testResultStatusTextBoxColumn
            // 
            this.testResultStatusTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.testResultStatusTextBoxColumn.HeaderText = "Result";
            this.testResultStatusTextBoxColumn.Name = "testResultStatusTextBoxColumn";
            this.testResultStatusTextBoxColumn.ReadOnly = true;
            // 
            // expectedPictureBox
            // 
            this.expectedPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.expectedPictureBox.Location = new System.Drawing.Point(228, 248);
            this.expectedPictureBox.Name = "expectedPictureBox";
            this.expectedPictureBox.Size = new System.Drawing.Size(220, 190);
            this.expectedPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.expectedPictureBox.TabIndex = 9;
            this.expectedPictureBox.TabStop = false;
            // 
            // actualPictureBox
            // 
            this.actualPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.actualPictureBox.Location = new System.Drawing.Point(454, 248);
            this.actualPictureBox.Name = "actualPictureBox";
            this.actualPictureBox.Size = new System.Drawing.Size(221, 190);
            this.actualPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.actualPictureBox.TabIndex = 8;
            this.actualPictureBox.TabStop = false;
            // 
            // errorDetailsTextBox
            // 
            this.errorDetailsTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.errorDetailsTextBox.Location = new System.Drawing.Point(3, 233);
            this.errorDetailsTextBox.Multiline = true;
            this.errorDetailsTextBox.Name = "errorDetailsTextBox";
            this.errorDetailsTextBox.ReadOnly = true;
            this.tableLayoutPanel1.SetRowSpan(this.errorDetailsTextBox, 3);
            this.errorDetailsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.errorDetailsTextBox.Size = new System.Drawing.Size(219, 235);
            this.errorDetailsTextBox.TabIndex = 10;
            // 
            // windowControlsTabPage
            // 
            this.windowControlsTabPage.Controls.Add(this.tableLayoutPanel2);
            this.windowControlsTabPage.Location = new System.Drawing.Point(4, 24);
            this.windowControlsTabPage.Name = "windowControlsTabPage";
            this.windowControlsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.windowControlsTabPage.Size = new System.Drawing.Size(684, 477);
            this.windowControlsTabPage.TabIndex = 2;
            this.windowControlsTabPage.Text = "Window controls";
            this.windowControlsTabPage.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.listWindowControlsButton, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.dataGridView, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.windowControlTextBox, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(678, 471);
            this.tableLayoutPanel2.TabIndex = 10;
            // 
            // listWindowControlsButton
            // 
            this.listWindowControlsButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.listWindowControlsButton.Location = new System.Drawing.Point(3, 3);
            this.listWindowControlsButton.Name = "listWindowControlsButton";
            this.listWindowControlsButton.Size = new System.Drawing.Size(144, 28);
            this.listWindowControlsButton.TabIndex = 7;
            this.listWindowControlsButton.Text = "List window controls";
            this.listWindowControlsButton.Click += new System.EventHandler(this.ListWindowControlsButton_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.WindowHandle,
            this.ClassName,
            this.ParentHandle});
            this.tableLayoutPanel2.SetColumnSpan(this.dataGridView, 3);
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(3, 37);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowTemplate.Height = 25;
            this.dataGridView.Size = new System.Drawing.Size(672, 431);
            this.dataGridView.TabIndex = 6;
            // 
            // WindowHandle
            // 
            this.WindowHandle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.WindowHandle.HeaderText = "Window Handle";
            this.WindowHandle.Name = "WindowHandle";
            this.WindowHandle.ReadOnly = true;
            // 
            // ClassName
            // 
            this.ClassName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ClassName.HeaderText = "Class Name";
            this.ClassName.Name = "ClassName";
            this.ClassName.ReadOnly = true;
            // 
            // ParentHandle
            // 
            this.ParentHandle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ParentHandle.HeaderText = "Parent Handle";
            this.ParentHandle.Name = "ParentHandle";
            this.ParentHandle.ReadOnly = true;
            // 
            // windowControlTextBox
            // 
            this.windowControlTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.windowControlTextBox.Location = new System.Drawing.Point(153, 5);
            this.windowControlTextBox.Name = "windowControlTextBox";
            this.windowControlTextBox.Size = new System.Drawing.Size(100, 23);
            this.windowControlTextBox.TabIndex = 9;
            // 
            // approveNewChangeButton
            // 
            this.approveNewChangeButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel1.SetColumnSpan(this.approveNewChangeButton, 2);
            this.approveNewChangeButton.Location = new System.Drawing.Point(369, 444);
            this.approveNewChangeButton.Name = "approveNewChangeButton";
            this.approveNewChangeButton.Size = new System.Drawing.Size(165, 23);
            this.approveNewChangeButton.TabIndex = 13;
            this.approveNewChangeButton.Text = "Approve New Change";
            this.approveNewChangeButton.UseVisualStyleBackColor = true;
            this.approveNewChangeButton.Click += new System.EventHandler(this.ApproveNewChangeButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
            this.ClientSize = new System.Drawing.Size(859, 505);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(leftPanel);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Test Windows API";
            leftPanel.ResumeLayout(false);
            leftPanel.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.testResultsTabPage.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.testResultsDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.expectedPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.actualPictureBox)).EndInit();
            this.windowControlsTabPage.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

		}

        #endregion

        private Button closeProgramButton;
        private Button runTestsButton;
        private Button checkProgramIsOpenButton;
        private TextBox windowTitleTextBox;
        private Label windowTitleLabel;
        private TabControl tabControl;
        private TabPage testResultsTabPage;
        private TabPage windowControlsTabPage;
        private DataGridView dataGridView;
        private DataGridViewTextBoxColumn WindowHandle;
        private DataGridViewTextBoxColumn ClassName;
        private DataGridViewTextBoxColumn ParentHandle;
        private Button listWindowControlsButton;
        private DataGridView testResultsDataGridView;
        private TextBox windowControlTextBox;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private PictureBox expectedPictureBox;
        private PictureBox actualPictureBox;
        private TextBox errorDetailsTextBox;
        private DataGridViewTextBoxColumn classNameTextBoxColumn;
        private DataGridViewTextBoxColumn testResultNameTextBoxColumn;
        private DataGridViewTextBoxColumn testResultStatusTextBoxColumn;
        private Button approveNewChangeButton;
    }
}

