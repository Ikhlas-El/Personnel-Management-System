using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace HCP
{
    public partial class Dashboard : Form
    {
        SqlConnection cn ;
        private FormWindowState previousWindowState = FormWindowState.Normal;

        public Dashboard()
        {
            InitializeComponent();
            InitializeConnection();
            this.Resize += new EventHandler(time_Resize);
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

        private void Dashboard_Load(object sender, EventArgs e)
        {
            timer1.Start();
            PersonnelTotale();
            PersonnelAbsent();
            PersonnelPresent();
        }

        private void pic2_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

        private void but2_Click(object sender, EventArgs e)
        {
            Personnel newForm = new Personnel();
            newForm.Show();
            this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbldate.Text = DateTime.Now.ToLongDateString();
            lbltime.Text = DateTime.Now.ToLongTimeString();
        }

        private void time_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                pandate.Location = new System.Drawing.Point(319, 27);
            }
            else
            {
                pandate.Location = new System.Drawing.Point(630, 110);
            }
        }

        private void but3_Click(object sender, EventArgs e)
        {
            Recherche newForm = new Recherche();
            newForm.Show();
            this.Hide();
        }

        private void but4_Click(object sender, EventArgs e)
        {
            Absence newForm = new Absence();
            newForm.Show();
            this.Hide();
        }

        private void but5_Click_1(object sender, EventArgs e)
        {
            Paiement newForm = new Paiement();
            newForm.Show();
            this.Hide();
        }

        private void PersonnelTotale()
        {
            try
            {
                cn.Open();
                string query = "SELECT COUNT(*) FROM Personnel";
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    int employeeCount = (int)cmd.ExecuteScalar();
                    lbltot.Text = employeeCount.ToString();
                    lbltot.Font = new System.Drawing.Font(lbltot.Font.FontFamily, 40);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving total employees: " + ex.Message);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        private void PersonnelAbsent()
        {
            try
            {
                cn.Open();

                // Count distinct personnel with at least one absence record for today
                string query = @"
            SELECT COUNT(DISTINCT A.id_personnel) AS AbsentCount
            FROM Absence A
            WHERE A.date_debut_absence <= GETDATE() AND A.date_fin_absence >= GETDATE()";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    int absentCount = (int)cmd.ExecuteScalar();
                    lblabs.Text = absentCount.ToString();
                    lblabs.Font = new System.Drawing.Font(lblabs.Font.FontFamily, 40);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving absent personnel: " + ex.Message);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        private void PersonnelPresent()
        {
            try
            {
                cn.Open();

                // Count distinct personnel who do not have any absence record for today
                string query = @"
            SELECT COUNT(DISTINCT P.id_personnel) AS PresentCount
            FROM Personnel P
            WHERE P.id_personnel NOT IN (
                SELECT DISTINCT A.id_personnel
                FROM Absence A
                WHERE A.date_debut_absence <= GETDATE() AND A.date_fin_absence >= GETDATE()
            )";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    int presentCount = (int)cmd.ExecuteScalar();
                    lblpre.Text = presentCount.ToString();
                    lblpre.Font = new System.Drawing.Font(lblpre.Font.FontFamily, 40);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving present personnel: " + ex.Message);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }
    }
}
