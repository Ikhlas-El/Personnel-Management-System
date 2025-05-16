using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace HCP
{
    public partial class Paiement : Form
    {
        SqlConnection cn;
        private const float ScrollSpeedFactor = 0.2f;
        private FormWindowState previousWindowState = FormWindowState.Normal;

        public Paiement()
        {
            InitializeComponent();
            InitializeConnection();
            InitializeCustomScrollbar();
            dg.CellClick += dg_CellClick;
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

        private void but4_Click(object sender, EventArgs e)
        {
            Absence newForm = new Absence();
            newForm.Show();
            this.Hide();
        }

        private void but3_Click(object sender, EventArgs e)
        {
            Recherche newForm = new Recherche();
            newForm.Show();
            this.Hide();
        }

        private void but2_Click(object sender, EventArgs e)
        {
            Personnel newForm = new Personnel();
            newForm.Show();
            this.Hide();
        }

        private void but1_Click(object sender, EventArgs e)
        {
            Dashboard newForm = new Dashboard();
            newForm.Show();
            this.Hide();
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

        private void Txt1_Enter(object sender, EventArgs e)
        {
            if (Txt1.Text == "Entrer le CIN")
            {
                Txt1.Text = "";
                Txt1.ForeColor = Color.Silver;
            }
        }

        private void Txt1_Leave(object sender, EventArgs e)
        {
            Txt1.ForeColor = Color.Black;

            if (Txt1.Text == "")
            {
                Txt1.Text = "Entrer le CIN";
                Txt1.ForeColor = Color.Silver;
            }
        }



        private void Filldg()
        {
            try
            {
                if (cn.State != ConnectionState.Open)
                    cn.Open();

                string query = "SELECT P.CIN AS [CIN], P.nom + ' ' + P.prenom AS [Nom complet], F.nom_fonction AS [Fonction], PA.date_paiement AS [Date paiement], PA.jours_travaille AS [Jours travaillés], PA.rib AS [RIB], PA.salaire_journalier AS [Salaire journalier], PA.montant_a_payer AS [Montant] FROM Paiement PA JOIN Personnel P ON PA.id_personnel = P.id_personnel JOIN Fonction F ON P.id_fonction = F.id_fonction ORDER BY PA.date_paiement DESC";

                using (SqlDataAdapter adapter = new SqlDataAdapter(query, cn))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dg.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        private void dg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dg.Rows[e.RowIndex];

                Txt1.Text = row.Cells["CIN"].Value.ToString();
                Txt2.Text = row.Cells["Nom complet"].Value.ToString();
                Txt3.Text = row.Cells["Salaire journalier"].Value.ToString();

                if (DateTime.TryParse(row.Cells["Date paiement"].Value.ToString(), out DateTime datePaiement))
                {
                    Time1.Value = datePaiement;
                }

                Txt4.Text = row.Cells["Jours travaillés"].Value.ToString();
                Txt5.Text = row.Cells["RIB"].Value.ToString();
                Txt6.Text = row.Cells["Montant"].Value.ToString();
            }
        }



        private void btn3_Click(object sender, EventArgs e)
        {
            Txt1.Text = string.Empty;
            Txt2.Text = string.Empty;
            Txt3.Text = string.Empty;
            Time1.Value = DateTime.Now;
            Txt4.Text = string.Empty;
            Txt5.Text = string.Empty;
            Txt6.Text = string.Empty;
        }

        private void Paiement_Load(object sender, EventArgs e)
        {
            Filldg();
            dg.ClearSelection();
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            string cin = Txt1.Text;
            if (string.IsNullOrWhiteSpace(cin) || cin == "Entrer le CIN")
            {
                MessageBox.Show("Veuillez entrer un CIN valide.");
                return;
            }

            try
            {
                if (cn.State != ConnectionState.Open)
                    cn.Open();

                using (SqlCommand cmd = new SqlCommand("GetPaiementDetailsByCIN", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CIN", cin);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Txt2.Text = reader["Nom complet"].ToString();
                            Txt3.Text = reader["Salaire journalier"].ToString();
                            if (DateTime.TryParse(reader["Date paiement"].ToString(), out DateTime datePaiement))
                            {
                                Time1.Value = datePaiement;
                            }
                            Txt4.Text = reader["Jours travaillés"].ToString();
                            Txt5.Text = reader["RIB"].ToString();
                            Txt6.Text = reader["Montant"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Aucun résultat trouvé pour le CIN spécifié.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }


        private void InitializeCustomScrollbar()
        {
            // Assuming dataGridView1 and guna2VScrollBar1 are already added to the form

            // Set DataGridView properties
            dg.ScrollBars = ScrollBars.None;

            // Set Guna ScrollBar properties
            scroll.Minimum = 0;
            scroll.Maximum = dg.RowCount - 1;
            scroll.SmallChange = 1;
            scroll.LargeChange = dg.DisplayedRowCount(false);

            // Handle Scroll event for Guna scroll bar
            scroll.Scroll += scroll_Scroll;

            // Handle DataGridView RowsAdded and RowsRemoved events to adjust scrollbar
            dg.RowsAdded += dg_RowsAdded;
            dg.RowsRemoved += dg_RowsRemoved;

            // Handle MouseWheel event for DataGridView
            dg.MouseWheel += dg_MouseWheel;
        }

        private void scroll_Scroll(object sender, ScrollEventArgs e)
        {
            dg.FirstDisplayedScrollingRowIndex = scroll.Value;
        }

        private void dg_Scroll(object sender, ScrollEventArgs e)
        {
            // Update the scroll bar value to match the DataGridView's first displayed row
            if (dg.FirstDisplayedScrollingRowIndex >= 0)
            {
                scroll.Value = dg.FirstDisplayedScrollingRowIndex;
            }
        }

        private void dg_MouseWheel(object sender, MouseEventArgs e)
        {
            // Determine the number of rows to scroll based on the mouse wheel delta
            int numberOfRowsToMove = (int)(e.Delta / SystemInformation.MouseWheelScrollLines * ScrollSpeedFactor);
            int newFirstDisplayedRowIndex = dg.FirstDisplayedScrollingRowIndex - numberOfRowsToMove;

            // Ensure the new index is within valid bounds
            if (newFirstDisplayedRowIndex < 0)
            {
                newFirstDisplayedRowIndex = 0;
            }
            else if (newFirstDisplayedRowIndex > dg.RowCount - dg.DisplayedRowCount(false))
            {
                newFirstDisplayedRowIndex = dg.RowCount - dg.DisplayedRowCount(false);
            }

            // Update the DataGridView and the custom scroll bar
            dg.FirstDisplayedScrollingRowIndex = newFirstDisplayedRowIndex;
            scroll.Value = newFirstDisplayedRowIndex;
        }

        private void dg_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            scroll.Maximum = dg.RowCount - 1;
            scroll.LargeChange = dg.DisplayedRowCount(false);
        }

        private void dg_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            scroll.Maximum = dg.RowCount - 1;
            scroll.LargeChange = dg.DisplayedRowCount(false);
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            Pai_report report = new Pai_report();
            Pai_form frm = new Pai_form();
            frm.Viewer_pai.ReportSource = report;
            frm.ShowDialog();
            this.Cursor = Cursors.Default;
        }


        private string GetCINFromTextBox()
        {
            
            return Txt1.Text.Trim();
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            string cin = GetCINFromTextBox();
            if (string.IsNullOrWhiteSpace(cin) || cin == "Entrer le CIN")
            {
                MessageBox.Show("Veuillez entrer un CIN valide.");
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            Receipt_report report = new Receipt_report();

            report.SetParameterValue("@CIN", cin);

            Receipt_form frm = new Receipt_form();
            frm.Viewer_receipt.ReportSource = report;
            frm.ShowDialog();
            this.Cursor = Cursors.Default;
        }
    }
    }
