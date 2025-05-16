using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace HCP
{
    public partial class connexion : Form
    {
        SqlConnection cn;
        SqlCommand cmd;

        public connexion()
        {
            InitializeComponent();
            InitializeConnection();
        }

        private void InitializeConnection()
        {
            string mode = Properties.Settings.Default.Mode;

            if (mode == "SQL")
            {
                cn = new SqlConnection("Data Source=" + Properties.Settings.Default.Server +
                                       "; Initial Catalog=" + Properties.Settings.Default.Database +
                                       "; Integrated Security=False; User ID=" + Properties.Settings.Default.ID +
                                       "; Password=" + Properties.Settings.Default.Password + ";");
            }
            else
            {
                cn = new SqlConnection("Data Source=" + Properties.Settings.Default.Server +
                                       "; Initial Catalog=" + Properties.Settings.Default.Database +
                                       "; Integrated Security=True");
            }
        }

        private void Txtbox1_Enter(object sender, EventArgs e)
        {
            if (Txtbox1.Text == "Nom complet")
            {
                Txtbox1.Text = "";
                Txtbox1.ForeColor = Color.Black;
            }
        }

        private void Txtbox1_Leave(object sender, EventArgs e)
        {
            if (Txtbox1.Text == "")
            {
                Txtbox1.Text = "Nom complet";
                Txtbox1.ForeColor = Color.Gainsboro;
            }
        }

        private void Txtbox2_Enter(object sender, EventArgs e)
        {
            if (Txtbox2.Text == "Mot de passe")
            {
                Txtbox2.Text = "";
                Txtbox2.UseSystemPasswordChar = true;
                Txtbox2.ForeColor = Color.Black;
            }
        }

        private void Txtbox2_Leave(object sender, EventArgs e)
        {
            if (Txtbox2.Text == "")
            {
                Txtbox2.Text = "Mot de passe";
                Txtbox2.ForeColor = Color.Gainsboro;
            }
        }

        private void Check1_OnChange(object sender, EventArgs e)
        {
            Txtbox2.UseSystemPasswordChar = !Check1.Checked;
            lbl1.ForeColor = Check1.Checked ? Color.Orange : Color.Silver;
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Txtbox1.Text) || string.IsNullOrWhiteSpace(Txtbox2.Text))
                {
                    MessageBox.Show("Veuillez entrer votre nom complet et votre mot de passe.");
                    return;
                }

                cn.Open();
                string query = "SELECT * FROM Admin WHERE [nom_admin] + ' ' + [prenom_admin] = @nom AND [mot_de_passe] = @password";
                cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@nom", Txtbox1.Text);
                cmd.Parameters.AddWithValue("@password", Txtbox2.Text);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                int rowCount = ds.Tables[0].Rows.Count;

                if (rowCount == 0)
                {
                    MessageBox.Show("Nom ou mot de passe erroné.");
                    Txtbox1.Text = "";
                    Txtbox2.Text = "";
                }
                else
                {
                    Dashboard newForm = new Dashboard();
                    newForm.Show();
                    this.Hide();
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Erreur SQL : " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur est survenue : " + ex.Message);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        private void btn_config_Click(object sender, EventArgs e)
        {
            config_form newForm = new config_form();
            newForm.Show();
            this.Hide();
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
