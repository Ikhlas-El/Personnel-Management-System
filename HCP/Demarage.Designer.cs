
namespace HCP
{
    partial class Demarage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Demarage));
            this.progbar1 = new Bunifu.Framework.UI.BunifuCircleProgressbar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pic1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pic1)).BeginInit();
            this.SuspendLayout();
            // 
            // progbar1
            // 
            this.progbar1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.progbar1.animated = false;
            this.progbar1.animationIterval = 5;
            this.progbar1.animationSpeed = 300;
            this.progbar1.BackColor = System.Drawing.Color.Transparent;
            this.progbar1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("progbar1.BackgroundImage")));
            this.progbar1.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F);
            this.progbar1.ForeColor = System.Drawing.Color.Transparent;
            this.progbar1.LabelVisible = true;
            this.progbar1.LineProgressThickness = 20;
            this.progbar1.LineThickness = 16;
            this.progbar1.Location = new System.Drawing.Point(474, 180);
            this.progbar1.Margin = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this.progbar1.MaxValue = 100;
            this.progbar1.Name = "progbar1";
            this.progbar1.ProgressBackColor = System.Drawing.Color.Gainsboro;
            this.progbar1.ProgressColor = System.Drawing.Color.Maroon;
            this.progbar1.Size = new System.Drawing.Size(334, 334);
            this.progbar1.TabIndex = 0;
            this.progbar1.Value = 20;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 20;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pic1
            // 
            this.pic1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pic1.BackColor = System.Drawing.Color.Transparent;
            this.pic1.Image = ((System.Drawing.Image)(resources.GetObject("pic1.Image")));
            this.pic1.Location = new System.Drawing.Point(515, 275);
            this.pic1.Name = "pic1";
            this.pic1.Size = new System.Drawing.Size(253, 137);
            this.pic1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic1.TabIndex = 1;
            this.pic1.TabStop = false;
            // 
            // Demarage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1300, 720);
            this.Controls.Add(this.pic1);
            this.Controls.Add(this.progbar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Demarage";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.pic1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Bunifu.Framework.UI.BunifuCircleProgressbar progbar1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pic1;
    }
}

