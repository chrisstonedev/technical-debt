
using System;
using System.Windows.Forms;

namespace UiTestRunner
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.runTestsButton = new System.Windows.Forms.Button();
            this.closeProgramButton = new System.Windows.Forms.Button();
            this.checkProgramIsOpenButton = new System.Windows.Forms.Button();
            this.windowTitleTextBox = new System.Windows.Forms.TextBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.testResultsTabPage = new System.Windows.Forms.TabPage();
            this.testResultsDataGridView = new System.Windows.Forms.DataGridView();
            this.windowControlsTabPage = new System.Windows.Forms.TabPage();
            this.windowControlTextBox = new System.Windows.Forms.TextBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.WindowHandle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClassName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ParentHandle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.listWindowControlsButton = new System.Windows.Forms.Button();
            this.classNameTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.testResultNameTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.testResultStatusTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.testResultDataTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            windowTitleLabel = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.testResultsTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.testResultsDataGridView)).BeginInit();
            this.windowControlsTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // windowTitleLabel
            // 
            windowTitleLabel.AutoSize = true;
            windowTitleLabel.Location = new System.Drawing.Point(12, 12);
            windowTitleLabel.Name = "windowTitleLabel";
            windowTitleLabel.Size = new System.Drawing.Size(74, 15);
            windowTitleLabel.TabIndex = 7;
            windowTitleLabel.Text = "Window title";
            // 
            // runTestsButton
            // 
            this.runTestsButton.Location = new System.Drawing.Point(6, 6);
            this.runTestsButton.Name = "runTestsButton";
            this.runTestsButton.Size = new System.Drawing.Size(144, 28);
            this.runTestsButton.TabIndex = 0;
            this.runTestsButton.Text = "Run tests";
            this.runTestsButton.Click += new System.EventHandler(this.RunClientTestButton_Click);
            // 
            // closeProgramButton
            // 
            this.closeProgramButton.Location = new System.Drawing.Point(12, 93);
            this.closeProgramButton.Name = "closeProgramButton";
            this.closeProgramButton.Size = new System.Drawing.Size(144, 28);
            this.closeProgramButton.TabIndex = 2;
            this.closeProgramButton.Text = "Close the program";
            this.closeProgramButton.Click += new System.EventHandler(this.CloseProgramButton_Click);
            // 
            // checkProgramIsOpenButton
            // 
            this.checkProgramIsOpenButton.Location = new System.Drawing.Point(12, 59);
            this.checkProgramIsOpenButton.Name = "checkProgramIsOpenButton";
            this.checkProgramIsOpenButton.Size = new System.Drawing.Size(144, 28);
            this.checkProgramIsOpenButton.TabIndex = 3;
            this.checkProgramIsOpenButton.Text = "Check program is open";
            this.checkProgramIsOpenButton.Click += new System.EventHandler(this.CheckProgramIsOpenButton_Click);
            // 
            // windowTitleTextBox
            // 
            this.windowTitleTextBox.Location = new System.Drawing.Point(12, 30);
            this.windowTitleTextBox.Name = "windowTitleTextBox";
            this.windowTitleTextBox.Size = new System.Drawing.Size(144, 23);
            this.windowTitleTextBox.TabIndex = 6;
            this.windowTitleTextBox.Text = "Order Client";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.testResultsTabPage);
            this.tabControl.Controls.Add(this.windowControlsTabPage);
            this.tabControl.Location = new System.Drawing.Point(169, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(485, 284);
            this.tabControl.TabIndex = 8;
            // 
            // testResultsTabPage
            // 
            this.testResultsTabPage.Controls.Add(this.testResultsDataGridView);
            this.testResultsTabPage.Controls.Add(this.runTestsButton);
            this.testResultsTabPage.Location = new System.Drawing.Point(4, 24);
            this.testResultsTabPage.Name = "testResultsTabPage";
            this.testResultsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.testResultsTabPage.Size = new System.Drawing.Size(477, 256);
            this.testResultsTabPage.TabIndex = 1;
            this.testResultsTabPage.Text = "Test results";
            this.testResultsTabPage.UseVisualStyleBackColor = true;
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
            this.testResultStatusTextBoxColumn,
            this.testResultDataTextBoxColumn});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.testResultsDataGridView.DefaultCellStyle = dataGridViewCellStyle1;
            this.testResultsDataGridView.Location = new System.Drawing.Point(6, 40);
            this.testResultsDataGridView.Name = "testResultsDataGridView";
            this.testResultsDataGridView.ReadOnly = true;
            this.testResultsDataGridView.RowTemplate.Height = 25;
            this.testResultsDataGridView.Size = new System.Drawing.Size(465, 210);
            this.testResultsDataGridView.TabIndex = 7;
            // 
            // windowControlsTabPage
            // 
            this.windowControlsTabPage.Controls.Add(this.windowControlTextBox);
            this.windowControlsTabPage.Controls.Add(this.dataGridView);
            this.windowControlsTabPage.Controls.Add(this.listWindowControlsButton);
            this.windowControlsTabPage.Location = new System.Drawing.Point(4, 24);
            this.windowControlsTabPage.Name = "windowControlsTabPage";
            this.windowControlsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.windowControlsTabPage.Size = new System.Drawing.Size(477, 256);
            this.windowControlsTabPage.TabIndex = 2;
            this.windowControlsTabPage.Text = "Window controls";
            this.windowControlsTabPage.UseVisualStyleBackColor = true;
            // 
            // windowControlTextBox
            // 
            this.windowControlTextBox.Location = new System.Drawing.Point(167, 10);
            this.windowControlTextBox.Name = "windowControlTextBox";
            this.windowControlTextBox.Size = new System.Drawing.Size(100, 23);
            this.windowControlTextBox.TabIndex = 9;
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
            this.dataGridView.Location = new System.Drawing.Point(6, 40);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowTemplate.Height = 25;
            this.dataGridView.Size = new System.Drawing.Size(465, 210);
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
            // listWindowControlsButton
            // 
            this.listWindowControlsButton.Location = new System.Drawing.Point(6, 6);
            this.listWindowControlsButton.Name = "listWindowControlsButton";
            this.listWindowControlsButton.Size = new System.Drawing.Size(144, 28);
            this.listWindowControlsButton.TabIndex = 7;
            this.listWindowControlsButton.Text = "List window controls";
            this.listWindowControlsButton.Click += new System.EventHandler(this.ListWindowControlsButton_Click);
            // 
            // classNameTextBoxColumn
            // 
            this.classNameTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.classNameTextBoxColumn.HeaderText = "Class";
            this.classNameTextBoxColumn.Name = "classNameTextBoxColumn";
            this.classNameTextBoxColumn.ReadOnly = true;
            this.classNameTextBoxColumn.Width = 59;
            // 
            // testResultNameTextBoxColumn
            // 
            this.testResultNameTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.testResultNameTextBoxColumn.HeaderText = "Test";
            this.testResultNameTextBoxColumn.Name = "testResultNameTextBoxColumn";
            this.testResultNameTextBoxColumn.ReadOnly = true;
            this.testResultNameTextBoxColumn.Width = 52;
            // 
            // testResultStatusTextBoxColumn
            // 
            this.testResultStatusTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.testResultStatusTextBoxColumn.HeaderText = "Result";
            this.testResultStatusTextBoxColumn.Name = "testResultStatusTextBoxColumn";
            this.testResultStatusTextBoxColumn.ReadOnly = true;
            this.testResultStatusTextBoxColumn.Width = 64;
            // 
            // testResultDataTextBoxColumn
            // 
            this.testResultDataTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.testResultDataTextBoxColumn.HeaderText = "Data";
            this.testResultDataTextBoxColumn.Name = "testResultDataTextBoxColumn";
            this.testResultDataTextBoxColumn.ReadOnly = true;
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
            this.ClientSize = new System.Drawing.Size(666, 308);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(windowTitleLabel);
            this.Controls.Add(this.windowTitleTextBox);
            this.Controls.Add(this.checkProgramIsOpenButton);
            this.Controls.Add(this.closeProgramButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Test Windows API";
            this.tabControl.ResumeLayout(false);
            this.testResultsTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.testResultsDataGridView)).EndInit();
            this.windowControlsTabPage.ResumeLayout(false);
            this.windowControlsTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private DataGridViewTextBoxColumn classNameTextBoxColumn;
        private DataGridViewTextBoxColumn testResultNameTextBoxColumn;
        private DataGridViewTextBoxColumn testResultStatusTextBoxColumn;
        private DataGridViewTextBoxColumn testResultDataTextBoxColumn;
    }
}

