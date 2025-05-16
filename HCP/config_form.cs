using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HCP
{
    public partial class config_form : Form
    {
        private FormWindowState previousWindowState = FormWindowState.Normal;

        public config_form()
        {
            InitializeComponent();
            Txt1.Text = Properties.Settings.Default.Server;
            Txt2.Text = Properties.Settings.Default.Database;
            if (Properties.Settings.Default.Mode == "SQL")
            {
                Rad2.Checked = true;
                Txt3.Text = Properties.Settings.Default.ID;
                Txt4.Text = Properties.Settings.Default.Password;
            }
            else
            {
                Rad1.Checked = true;
                Txt3.Clear();
                Txt4.Clear();
                Txt3.ReadOnly = true;
                Txt4.ReadOnly = true;
            }
        }

        private void pic2_Click(object sender, EventArgs e)
        {
            connexion newForm = new connexion();
            newForm.Show();
            this.Hide();
        }

        private void pic3_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                previousWindowState = this.WindowState;
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = previousWindowState;
            }
        }

        private void pic4_Click(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Minimized;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Server = Txt1.Text;
            Properties.Settings.Default.Database = Txt2.Text;
            Properties.Settings.Default.Mode = Rad2.Checked == true ? "SQL" : "Windows";
            Properties.Settings.Default.ID = Txt3.Text;
            Properties.Settings.Default.Password = Txt4.Text;
            Properties.Settings.Default.Save();

            TestDatabaseConnection();
        }

        private void Rad2_CheckedChanged(object sender, EventArgs e)
        {
            Txt3.ReadOnly = false;
            Txt4.ReadOnly = false;
        }

        private void Rad1_CheckedChanged(object sender, EventArgs e)
        {
            Txt3.Clear();
            Txt4.Clear();
            Txt3.ReadOnly = true;
            Txt4.ReadOnly = true;
        }

        private void Txt4_TextChanged(object sender, EventArgs e)
        {
            Txt4.UseSystemPasswordChar = true;
        }

        private void TestDatabaseConnection()
        {
            string connectionString;

            if (Properties.Settings.Default.Mode == "SQL")
            {
                connectionString = $"Data Source={Properties.Settings.Default.Server};" +
                                   $"Initial Catalog={Properties.Settings.Default.Database};" +
                                   $"Integrated Security=False;" +
                                   $"User ID={Properties.Settings.Default.ID};" +
                                   $"Password={Properties.Settings.Default.Password};";
            }
            else
            {
                connectionString = $"Data Source={Properties.Settings.Default.Server};" +
                                   $"Initial Catalog={Properties.Settings.Default.Database};" +
                                   $"Integrated Security=True;";
            }

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    cn.Open();
                    MessageBox.Show("Connexion réussie.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (SqlException ex)
            {
                string errorMessage;

                switch (ex.Number)
                {
                    case 53: // Network related or instance-specific error
                        errorMessage = "Erreur de réseau ou d'instance spécifique. Vérifiez le nom du serveur.";
                        break;
                    case 18456: // Login failed
                        errorMessage = "Échec de connexion. Vérifiez le nom d'utilisateur et le mot de passe.";
                        break;
                    default:
                        errorMessage = $"Erreur de connexion à la base de données : {ex.Message}";
                        break;
                }

                MessageBox.Show(errorMessage, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
