
namespace HCP
{
    partial class Receipt_form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Receipt_form));
            this.Viewer_receipt = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.pic2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pic2)).BeginInit();
            this.SuspendLayout();
            // 
            // Viewer_receipt
            // 
            this.Viewer_receipt.ActiveViewIndex = -1;
            this.Viewer_receipt.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Viewer_receipt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Viewer_receipt.Cursor = System.Windows.Forms.Cursors.Default;
            this.Viewer_receipt.Location = new System.Drawing.Point(20, 33);
            this.Viewer_receipt.Name = "Viewer_receipt";
            this.Viewer_receipt.Size = new System.Drawing.Size(1801, 1021);
            this.Viewer_receipt.TabIndex = 12;
            this.Viewer_receipt.Load += new System.EventHandler(this.Viewer_abs_Load);
            // 
            // pic2
            // 
            this.pic2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pic2.Image = ((System.Drawing.Image)(resources.GetObject("pic2.Image")));
            this.pic2.Location = new System.Drawing.Point(1804, -4);
            this.pic2.Name = "pic2";
            this.pic2.Size = new System.Drawing.Size(36, 31);
            this.pic2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pic2.TabIndex = 11;
            this.pic2.TabStop = false;
            this.pic2.Click += new System.EventHandler(this.pic2_Click);
            // 
            // Receipt_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1861, 1050);
            this.Controls.Add(this.Viewer_receipt);
            this.Controls.Add(this.pic2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Receipt_form";
            this.Text = "Receipt_form";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.pic2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public CrystalDecisions.Windows.Forms.CrystalReportViewer Viewer_receipt;
        private System.Windows.Forms.PictureBox pic2;
    }
}