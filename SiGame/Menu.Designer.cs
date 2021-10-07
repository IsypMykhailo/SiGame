
namespace SiGame
{
    partial class Menu
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblCreate = new System.Windows.Forms.Label();
            this.lblJoin = new System.Windows.Forms.Label();
            this.lblCredits = new System.Windows.Forms.Label();
            this.lblExit = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Harlow Solid Italic", 72F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.DarkSeaGreen;
            this.lblTitle.Location = new System.Drawing.Point(171, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(465, 152);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "SiGame";
            // 
            // lblCreate
            // 
            this.lblCreate.Font = new System.Drawing.Font("Impact", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCreate.Location = new System.Drawing.Point(268, 181);
            this.lblCreate.Name = "lblCreate";
            this.lblCreate.Size = new System.Drawing.Size(270, 75);
            this.lblCreate.TabIndex = 1;
            this.lblCreate.Tag = "Create";
            this.lblCreate.Text = "Create";
            this.lblCreate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCreate.Click += new System.EventHandler(this.lblCreate_Click);
            this.lblCreate.MouseEnter += new System.EventHandler(this.Mouse_Enter);
            this.lblCreate.MouseLeave += new System.EventHandler(this.lblCreate_MouseLeave);
            // 
            // lblJoin
            // 
            this.lblJoin.Font = new System.Drawing.Font("Impact", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblJoin.Location = new System.Drawing.Point(268, 270);
            this.lblJoin.Name = "lblJoin";
            this.lblJoin.Size = new System.Drawing.Size(270, 75);
            this.lblJoin.TabIndex = 2;
            this.lblJoin.Tag = "Join";
            this.lblJoin.Text = "Join";
            this.lblJoin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblJoin.Click += new System.EventHandler(this.lblJoin_Click);
            this.lblJoin.MouseEnter += new System.EventHandler(this.Mouse_Enter);
            this.lblJoin.MouseLeave += new System.EventHandler(this.lblCreate_MouseLeave);
            // 
            // lblCredits
            // 
            this.lblCredits.AutoSize = true;
            this.lblCredits.Font = new System.Drawing.Font("Microsoft New Tai Lue", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCredits.Location = new System.Drawing.Point(632, 466);
            this.lblCredits.Name = "lblCredits";
            this.lblCredits.Size = new System.Drawing.Size(156, 52);
            this.lblCredits.TabIndex = 3;
            this.lblCredits.Text = "Credits";
            // 
            // lblExit
            // 
            this.lblExit.Font = new System.Drawing.Font("Impact", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblExit.Location = new System.Drawing.Point(268, 380);
            this.lblExit.Name = "lblExit";
            this.lblExit.Size = new System.Drawing.Size(270, 75);
            this.lblExit.TabIndex = 4;
            this.lblExit.Tag = "Exit";
            this.lblExit.Text = "Exit ";
            this.lblExit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblExit.Click += new System.EventHandler(this.lblExit_Click);
            this.lblExit.MouseEnter += new System.EventHandler(this.Mouse_Enter);
            this.lblExit.MouseLeave += new System.EventHandler(this.lblCreate_MouseLeave);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 527);
            this.Controls.Add(this.lblExit);
            this.Controls.Add(this.lblCredits);
            this.Controls.Add(this.lblJoin);
            this.Controls.Add(this.lblCreate);
            this.Controls.Add(this.lblTitle);
            this.Name = "Menu";
            this.Text = "Menu";
            this.Load += new System.EventHandler(this.Menu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblCreate;
        private System.Windows.Forms.Label lblJoin;
        private System.Windows.Forms.Label lblCredits;
        private System.Windows.Forms.Label lblExit;
    }
}