using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing.Printing;

namespace HCP
{
    public partial class Recherche : Form
    {
        SqlConnection cn ;
        private FormWindowState previousWindowState = FormWindowState.Normal;
        private const float ScrollSpeedFactor = 0.2f;
        public Recherche()
        {
            InitializeComponent();
            InitializeConnection();
            InitializeCustomScrollbar();
            Filldg();
            FillCombo3(combo3, "SELECT nom_categorie, id_categorie FROM Categorie", "nom_categorie", "id_categorie");
            FillCombo2(combo2, "SELECT nom_affectation, id_affectation FROM Affectation", "nom_affectation", "id_affectation");
            FillCombo1(combo1, "SELECT nom_fonction, id_fonction FROM Fonction", "nom_fonction", "id_fonction");
            combo1.SelectedIndex = -1;
            combo2.SelectedIndex = -1;
            combo3.SelectedIndex = -1;
            check1.CheckedChanged += CheckBox_CheckedChanged;
            check2.CheckedChanged += CheckBox_CheckedChanged;
            check3.CheckedChanged += CheckBox_CheckedChanged;
            Txt1.TextChanged += Txt1_TextChanged;
            btn2.Click += btn2_Click;
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


        private void but1_Click(object sender, EventArgs e)
        {
            Dashboard newForm = new Dashboard();
            newForm.Show();
            this.Hide();
        }

        private void but2_Click(object sender, EventArgs e)
        {
            Personnel newForm = new Personnel();
            newForm.Show();
            this.Hide();
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

        private void but4_Click(object sender, EventArgs e)
        {
            Absence newForm = new Absence();
            newForm.Show();
            this.Hide();
        }

        private void but5_Click(object sender, EventArgs e)
        {
            Paiement newForm = new Paiement();
            newForm.Show();
            this.Hide();
        }



        private void Filldg()
        {
            try
            {
                cn.Open();
                string query = "SELECT CIN, nom + ' ' + prenom AS 'Nom Complet', C.nom_categorie AS 'Catégorie', F.nom_fonction AS 'Fonction', Af.nom_affectation AS 'Affectation', P.date_debut AS 'date debut', P.date_fin AS 'date fin', zone_supervision AS 'Zone supervision', rib FROM Personnel P INNER JOIN Categorie C ON P.id_categorie = C.id_categorie INNER JOIN Fonction F ON P.id_fonction = F.id_fonction INNER JOIN Affectation Af ON P.id_affectation = Af.id_affectation";
                SqlDataAdapter adapter = new SqlDataAdapter(query, cn);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dg.DataSource = dataTable;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private void FillCombo1(ComboBox comboBox, string query, string displayMember, string valueMember)
        {
            try
            {
                DataTable dataTable = new DataTable();
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, cn))
                {
                    adapter.Fill(dataTable);
                }
                comboBox.DataSource = dataTable;
                comboBox.DisplayMember = displayMember;
                comboBox.ValueMember = valueMember;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private void FillCombo2(ComboBox comboBox, string query, string displayMember, string valueMember)
        {
            try
            {
                DataTable dataTable = new DataTable();
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, cn))
                {
                    adapter.Fill(dataTable);
                }
                comboBox.DataSource = dataTable;
                comboBox.DisplayMember = displayMember;
                comboBox.ValueMember = valueMember;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }



        private void FillCombo3(ComboBox comboBox, string query, string displayMember, string valueMember)
        {
            try
            {
                DataTable dataTable = new DataTable();
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, cn))
                {
                    adapter.Fill(dataTable);
                }
                comboBox.DataSource = dataTable;
                comboBox.DisplayMember = displayMember;
                comboBox.ValueMember = valueMember;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }




        private void Txt1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Txt1.Text))
            {
                check1.Checked = false;
                check2.Checked = false;
                check3.Checked = false;
                combo1.SelectedIndex = -1;
                combo2.SelectedIndex = -1;
                combo3.SelectedIndex = -1;
            }
        }



        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (check1.Checked || check2.Checked || check3.Checked)
            {
                Txt1.TextChanged -= Txt1_TextChanged;
                Txt1.Text = string.Empty;
                Txt1.TextChanged += Txt1_TextChanged;
            }

            if (sender == check1 && check1.Checked)
            {
                check2.Checked = false;
                check3.Checked = false;
                combo2.SelectedIndex = -1;
                combo3.SelectedIndex = -1;
            }
            else if (sender == check2 && check2.Checked)
            {
                check1.Checked = false;
                check3.Checked = false;
                combo1.SelectedIndex = -1;
                combo3.SelectedIndex = -1;
            }
            else if (sender == check3 && check3.Checked)
            {
                check1.Checked = false;
                check2.Checked = false;
                combo1.SelectedIndex = -1;
                combo2.SelectedIndex = -1;
            }
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            string query = "SELECT CIN, nom + ' ' + prenom AS 'Nom Complet', C.nom_categorie AS 'Catégorie', F.nom_fonction AS 'Fonction', Af.nom_affectation AS 'Affectation',  P.date_debut AS 'date debut', P.date_fin AS 'date fin', zone_supervision AS 'Zone supervision', rib FROM Personnel P INNER JOIN Categorie C ON P.id_categorie = C.id_categorie INNER JOIN Fonction F ON P.id_fonction = F.id_fonction INNER JOIN Affectation Af ON P.id_affectation = Af.id_affectation WHERE ";

            if (!string.IsNullOrWhiteSpace(Txt1.Text))
            {
                query += "CIN = @cin";
            }
            else if (check1.Checked && combo1.SelectedIndex != -1)
            {
                query += "P.id_fonction = @id_fonction";
            }
            else if (check2.Checked && combo2.SelectedIndex != -1)
            {
                query += "P.id_affectation = @id_affectation";
            }
            else if (check3.Checked && combo3.SelectedIndex != -1)
            {
                query += "P.id_categorie = @id_categorie";
            }
            else
            {
                MessageBox.Show("Veuillez saisir un CIN ou sélectionner un critère de filtrage.");
                return;
            }

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    if (!string.IsNullOrWhiteSpace(Txt1.Text))
                    {
                        cmd.Parameters.AddWithValue("@cin", Txt1.Text);
                    }
                    else if (check1.Checked && combo1.SelectedIndex != -1)
                    {
                        cmd.Parameters.AddWithValue("@id_fonction", combo1.SelectedValue);
                    }
                    else if (check2.Checked && combo2.SelectedIndex != -1)
                    {
                        cmd.Parameters.AddWithValue("@id_affectation", combo2.SelectedValue);
                    }
                    else if (check3.Checked && combo3.SelectedIndex != -1)
                    {
                        cmd.Parameters.AddWithValue("@id_categorie", combo3.SelectedValue);
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dg.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            // Clear the txtCin textbox
            Txt1.Text = string.Empty;

            // Uncheck all checkboxes
            check1.Checked = false;
            check2.Checked = false;
            check3.Checked = false;

            // Clear the combobox selections
            combo1.SelectedIndex = -1;
            combo2.SelectedIndex = -1;
            combo3.SelectedIndex = -1;

            // Load all personnel data
            try
            {
                string query = "SELECT CIN, nom + ' ' + prenom AS 'Nom Complet', C.nom_categorie AS 'Catégorie', F.nom_fonction AS 'Fonction', Af.nom_affectation AS 'Affectation',  P.date_debut AS 'date debut', P.date_fin AS 'date fin', zone_supervision AS 'Zone supervision', rib FROM Personnel P INNER JOIN Categorie C ON P.id_categorie = C.id_categorie INNER JOIN Fonction F ON P.id_fonction = F.id_fonction INNER JOIN Affectation Af ON P.id_affectation = Af.id_affectation";
                SqlDataAdapter adapter = new SqlDataAdapter(query, cn);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dg.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
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

        private void Recherche_Load(object sender, EventArgs e)
        {
            dg.ClearSelection();
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            Rech_report report = new Rech_report();
            Rech_form frm = new Rech_form();
            frm.Viewer_rech.ReportSource = report;
            frm.ShowDialog();
            this.Cursor = Cursors.Default;

        }
    }
}