
namespace ClassLibrary
{
    partial class ButtonUserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.getMainButton = new System.Windows.Forms.Button();
            this.getAltButton = new System.Windows.Forms.Button();
            this.postButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // getMainButton
            // 
            this.getMainButton.Location = new System.Drawing.Point(0, 0);
            this.getMainButton.Name = "getMainButton";
            this.getMainButton.Size = new System.Drawing.Size(95, 58);
            this.getMainButton.TabIndex = 0;
            this.getMainButton.Text = "GET main";
            this.getMainButton.UseVisualStyleBackColor = true;
            this.getMainButton.Click += new System.EventHandler(this.getMainButton_Click);
            // 
            // getAltButton
            // 
            this.getAltButton.Location = new System.Drawing.Point(101, 0);
            this.getAltButton.Name = "getAltButton";
            this.getAltButton.Size = new System.Drawing.Size(75, 58);
            this.getAltButton.TabIndex = 1;
            this.getAltButton.Text = "GET alt";
            this.getAltButton.UseVisualStyleBackColor = true;
            // 
            // postButton
            // 
            this.postButton.Location = new System.Drawing.Point(0, 64);
            this.postButton.Name = "postButton";
            this.postButton.Size = new System.Drawing.Size(176, 46);
            this.postButton.TabIndex = 2;
            this.postButton.Text = "POST";
            this.postButton.UseVisualStyleBackColor = true;
            // 
            // ButtonUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.postButton);
            this.Controls.Add(this.getAltButton);
            this.Controls.Add(this.getMainButton);
            this.Name = "ButtonUserControl";
            this.Size = new System.Drawing.Size(177, 112);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button getMainButton;
        private System.Windows.Forms.Button getAltButton;
        private System.Windows.Forms.Button postButton;
    }
}
