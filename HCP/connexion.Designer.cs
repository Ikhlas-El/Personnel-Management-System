
namespace HCP
{
    partial class connexion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(connexion));
            this.Txtbox1 = new Guna.UI.WinForms.GunaTextBox();
            this.btn1 = new Bunifu.Framework.UI.BunifuThinButton2();
            this.Txtbox2 = new Guna.UI.WinForms.GunaTextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Check1 = new Bunifu.Framework.UI.BunifuCheckbox();
            this.lbl1 = new System.Windows.Forms.Label();
            this.btn_config = new Guna.UI.WinForms.GunaAdvenceButton();
            this.btn2 = new Bunifu.Framework.UI.BunifuThinButton2();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Txtbox1
            // 
            this.Txtbox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Txtbox1.BackColor = System.Drawing.Color.Transparent;
            this.Txtbox1.BaseColor = System.Drawing.Color.White;
            this.Txtbox1.BorderColor = System.Drawing.Color.DimGray;
            this.Txtbox1.BorderSize = 3;
            this.Txtbox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Txtbox1.FocusedBaseColor = System.Drawing.Color.White;
            this.Txtbox1.FocusedBorderColor = System.Drawing.Color.Maroon;
            this.Txtbox1.FocusedForeColor = System.Drawing.Color.Black;
            this.Txtbox1.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txtbox1.ForeColor = System.Drawing.Color.Gainsboro;
            this.Txtbox1.Location = new System.Drawing.Point(910, 297);
            this.Txtbox1.Name = "Txtbox1";
            this.Txtbox1.PasswordChar = '\0';
            this.Txtbox1.Radius = 20;
            this.Txtbox1.SelectedText = "";
            this.Txtbox1.Size = new System.Drawing.Size(502, 85);
            this.Txtbox1.TabIndex = 47;
            this.Txtbox1.Text = "Nom complet";
            this.Txtbox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Txtbox1.Enter += new System.EventHandler(this.Txtbox1_Enter);
            this.Txtbox1.Leave += new System.EventHandler(this.Txtbox1_Leave);
            // 
            // btn1
            // 
            this.btn1.ActiveBorderThickness = 1;
            this.btn1.ActiveCornerRadius = 20;
            this.btn1.ActiveFillColor = System.Drawing.Color.Maroon;
            this.btn1.ActiveForecolor = System.Drawing.Color.White;
            this.btn1.ActiveLineColor = System.Drawing.Color.Maroon;
            this.btn1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn1.BackColor = System.Drawing.Color.White;
            this.btn1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn1.BackgroundImage")));
            this.btn1.ButtonText = "Connecter";
            this.btn1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn1.Font = new System.Drawing.Font("Calibri", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn1.ForeColor = System.Drawing.Color.White;
            this.btn1.IdleBorderThickness = 3;
            this.btn1.IdleCornerRadius = 20;
            this.btn1.IdleFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(41)))), ((int)(((byte)(39)))));
            this.btn1.IdleForecolor = System.Drawing.Color.White;
            this.btn1.IdleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(41)))), ((int)(((byte)(39)))));
            this.btn1.Location = new System.Drawing.Point(1010, 571);
            this.btn1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(316, 79);
            this.btn1.TabIndex = 48;
            this.btn1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn1.Click += new System.EventHandler(this.btn1_Click);
            // 
            // Txtbox2
            // 
            this.Txtbox2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Txtbox2.BackColor = System.Drawing.Color.Transparent;
            this.Txtbox2.BaseColor = System.Drawing.Color.White;
            this.Txtbox2.BorderColor = System.Drawing.Color.DimGray;
            this.Txtbox2.BorderSize = 3;
            this.Txtbox2.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Txtbox2.FocusedBaseColor = System.Drawing.Color.White;
            this.Txtbox2.FocusedBorderColor = System.Drawing.Color.Maroon;
            this.Txtbox2.FocusedForeColor = System.Drawing.Color.Black;
            this.Txtbox2.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txtbox2.ForeColor = System.Drawing.Color.Gainsboro;
            this.Txtbox2.Location = new System.Drawing.Point(910, 419);
            this.Txtbox2.Name = "Txtbox2";
            this.Txtbox2.PasswordChar = '\0';
            this.Txtbox2.Radius = 20;
            this.Txtbox2.SelectedText = "";
            this.Txtbox2.Size = new System.Drawing.Size(502, 85);
            this.Txtbox2.TabIndex = 49;
            this.Txtbox2.Text = "Mot de passe";
            this.Txtbox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Txtbox2.Enter += new System.EventHandler(this.Txtbox2_Enter);
            this.Txtbox2.Leave += new System.EventHandler(this.Txtbox2_Leave);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1300, 720);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 50;
            this.pictureBox1.TabStop = false;
            // 
            // Check1
            // 
            this.Check1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Check1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(135)))), ((int)(((byte)(140)))));
            this.Check1.ChechedOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(135)))), ((int)(((byte)(140)))));
            this.Check1.Checked = false;
            this.Check1.CheckedOnColor = System.Drawing.Color.Orange;
            this.Check1.ForeColor = System.Drawing.Color.White;
            this.Check1.Location = new System.Drawing.Point(930, 528);
            this.Check1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Check1.Name = "Check1";
            this.Check1.Size = new System.Drawing.Size(20, 20);
            this.Check1.TabIndex = 45;
            this.Check1.OnChange += new System.EventHandler(this.Check1_OnChange);
            // 
            // lbl1
            // 
            this.lbl1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl1.AutoSize = true;
            this.lbl1.Font = new System.Drawing.Font("Calibri", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(135)))), ((int)(((byte)(140)))));
            this.lbl1.Location = new System.Drawing.Point(950, 528);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(151, 19);
            this.lbl1.TabIndex = 46;
            this.lbl1.Text = "Afficher Mot de passe";
            // 
            // btn_config
            // 
            this.btn_config.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_config.AnimationHoverSpeed = 0.07F;
            this.btn_config.AnimationSpeed = 0.03F;
            this.btn_config.BaseColor = System.Drawing.Color.White;
            this.btn_config.BorderColor = System.Drawing.Color.White;
            this.btn_config.CheckedBaseColor = System.Drawing.Color.White;
            this.btn_config.CheckedBorderColor = System.Drawing.Color.White;
            this.btn_config.CheckedForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(146)))), ((int)(((byte)(47)))));
            this.btn_config.CheckedImage = ((System.Drawing.Image)(resources.GetObject("btn_config.CheckedImage")));
            this.btn_config.CheckedLineColor = System.Drawing.Color.White;
            this.btn_config.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btn_config.FocusedColor = System.Drawing.Color.Empty;
            this.btn_config.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_config.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(135)))), ((int)(((byte)(140)))));
            this.btn_config.Image = ((System.Drawing.Image)(resources.GetObject("btn_config.Image")));
            this.btn_config.ImageSize = new System.Drawing.Size(30, 30);
            this.btn_config.LineColor = System.Drawing.Color.White;
            this.btn_config.Location = new System.Drawing.Point(1009, 483);
            this.btn_config.Name = "btn_config";
            this.btn_config.OnHoverBaseColor = System.Drawing.Color.White;
            this.btn_config.OnHoverBorderColor = System.Drawing.Color.White;
            this.btn_config.OnHoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(146)))), ((int)(((byte)(47)))));
            this.btn_config.OnHoverImage = ((System.Drawing.Image)(resources.GetObject("btn_config.OnHoverImage")));
            this.btn_config.OnHoverLineColor = System.Drawing.Color.White;
            this.btn_config.OnPressedColor = System.Drawing.Color.White;
            this.btn_config.Size = new System.Drawing.Size(319, 42);
            this.btn_config.TabIndex = 54;
            this.btn_config.Text = "Configuration du Serveur";
            this.btn_config.Click += new System.EventHandler(this.btn_config_Click);
            // 
            // btn2
            // 
            this.btn2.ActiveBorderThickness = 1;
            this.btn2.ActiveCornerRadius = 20;
            this.btn2.ActiveFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(146)))), ((int)(((byte)(47)))));
            this.btn2.ActiveForecolor = System.Drawing.Color.White;
            this.btn2.ActiveLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(146)))), ((int)(((byte)(47)))));
            this.btn2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn2.BackColor = System.Drawing.Color.White;
            this.btn2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn2.BackgroundImage")));
            this.btn2.ButtonText = "Quitter";
            this.btn2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn2.ForeColor = System.Drawing.Color.White;
            this.btn2.IdleBorderThickness = 2;
            this.btn2.IdleCornerRadius = 20;
            this.btn2.IdleFillColor = System.Drawing.Color.White;
            this.btn2.IdleForecolor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(41)))), ((int)(((byte)(39)))));
            this.btn2.IdleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(41)))), ((int)(((byte)(39)))));
            this.btn2.Location = new System.Drawing.Point(1120, 702);
            this.btn2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn2.Name = "btn2";
            this.btn2.Size = new System.Drawing.Size(109, 42);
            this.btn2.TabIndex = 55;
            this.btn2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn2.Click += new System.EventHandler(this.btn2_Click);
            // 
            // connexion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1300, 720);
            this.Controls.Add(this.btn2);
            this.Controls.Add(this.btn_config);
            this.Controls.Add(this.Txtbox1);
            this.Controls.Add(this.Check1);
            this.Controls.Add(this.btn1);
            this.Controls.Add(this.lbl1);
            this.Controls.Add(this.Txtbox2);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "connexion";
            this.Text = "connexion";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI.WinForms.GunaTextBox Txtbox1;
        private Bunifu.Framework.UI.BunifuThinButton2 btn1;
        private Guna.UI.WinForms.GunaTextBox Txtbox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Bunifu.Framework.UI.BunifuCheckbox Check1;
        private System.Windows.Forms.Label lbl1;
        private Guna.UI.WinForms.GunaAdvenceButton btn_config;
        private Bunifu.Framework.UI.BunifuThinButton2 btn2;
    }
}