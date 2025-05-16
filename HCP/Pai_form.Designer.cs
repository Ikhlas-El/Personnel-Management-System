
namespace HCP
{
    partial class Pai_form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Pai_form));
            this.Viewer_pai = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.pic2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pic2)).BeginInit();
            this.SuspendLayout();
            // 
            // Viewer_pai
            // 
            this.Viewer_pai.ActiveViewIndex = -1;
            this.Viewer_pai.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Viewer_pai.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Viewer_pai.Cursor = System.Windows.Forms.Cursors.Default;
            this.Viewer_pai.Location = new System.Drawing.Point(31, 61);
            this.Viewer_pai.Name = "Viewer_pai";
            this.Viewer_pai.Size = new System.Drawing.Size(1801, 1021);
            this.Viewer_pai.TabIndex = 12;
            // 
            // pic2
            // 
            this.pic2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pic2.Image = ((System.Drawing.Image)(resources.GetObject("pic2.Image")));
            this.pic2.Location = new System.Drawing.Point(1815, 24);
            this.pic2.Name = "pic2";
            this.pic2.Size = new System.Drawing.Size(36, 31);
            this.pic2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pic2.TabIndex = 11;
            this.pic2.TabStop = false;
            this.pic2.Click += new System.EventHandler(this.pic2_Click);
            // 
            // Pai_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1883, 1106);
            this.Controls.Add(this.Viewer_pai);
            this.Controls.Add(this.pic2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Pai_form";
            this.Text = "Pai_form";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.pic2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public CrystalDecisions.Windows.Forms.CrystalReportViewer Viewer_pai;
        private System.Windows.Forms.PictureBox pic2;
    }
}