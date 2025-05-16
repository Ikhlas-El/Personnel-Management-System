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

namespace HCP
{
    public partial class Absence : Form
    {
        SqlConnection cn;
        private FormWindowState previousWindowState = FormWindowState.Normal;
        private const float ScrollSpeedFactor = 0.2f;
        public Absence()
        {
            InitializeComponent();
            InitializeConnection();
            InitializeCustomScrollbar();
            Filldg();
            dg.SelectionChanged += dg_SelectionChanged;
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

        private void but3_Click(object sender, EventArgs e)
        {
            Recherche newForm = new Recherche();
            newForm.Show();
            this.Hide();
        }

        private void but5_Click(object sender, EventArgs e)
        {
            Paiement newForm = new Paiement();
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

        private void btn4_Click(object sender, EventArgs e)
        {
            Txt2.Text = string.Empty;
            Time1.Value = DateTime.Now;
            Time2.Value = DateTime.Now;
            RTxt3.Clear();
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            try
            {
                // Vérifier si les champs requis sont vides
                if (string.IsNullOrEmpty(Txt2.Text.Trim()) || string.IsNullOrEmpty(RTxt3.Text.Trim()))
                {
                    MessageBox.Show("Veuillez remplir tous les champs requis pour enregistrer l'absence.");
                    return;
                }

                // Ouvrir la connexion si elle n'est pas déjà ouverte
                if (cn.State != ConnectionState.Open)
                    cn.Open();

                // Vérifier si le personnel avec ce CIN existe
                string checkPersonnelQuery = "SELECT COUNT(*) FROM Personnel WHERE CIN = @CIN";
                using (SqlCommand checkCommand = new SqlCommand(checkPersonnelQuery, cn))
                {
                    checkCommand.Parameters.AddWithValue("@CIN", Txt2.Text);
                    int personnelCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                    if (personnelCount == 0)
                    {
                        MessageBox.Show("Aucun personnel trouvé avec ce CIN. Veuillez vérifier et réessayer.");
                        return; // Sortir de la méthode si le personnel n'existe pas
                    }
                }

                // Récupérer l'id_personnel correspondant au CIN fourni
                string getIdQuery = "SELECT id_personnel FROM Personnel WHERE CIN = @CIN";
                using (SqlCommand getIdCommand = new SqlCommand(getIdQuery, cn))
                {
                    getIdCommand.Parameters.AddWithValue("@CIN", Txt2.Text);
                    int idPersonnel = Convert.ToInt32(getIdCommand.ExecuteScalar());

                    // Insérer l'enregistrement d'absence avec l'id_personnel récupéré
                    string insertQuery = "INSERT INTO Absence (CIN, id_personnel, date_debut_absence, date_fin_absence, motif) " +
                                         "VALUES (@CIN, @id_personnel, @date_debut_absence, @date_fin_absence, @motif)";
                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, cn))
                    {
                        insertCommand.Parameters.AddWithValue("@CIN", Txt2.Text);
                        insertCommand.Parameters.AddWithValue("@id_personnel", idPersonnel);
                        insertCommand.Parameters.AddWithValue("@date_debut_absence", Time1.Value);
                        insertCommand.Parameters.AddWithValue("@date_fin_absence", Time2.Value);
                        insertCommand.Parameters.AddWithValue("@motif", RTxt3.Text);

                        // Exécuter la commande d'insertion
                        int rowsAffected = insertCommand.ExecuteNonQuery();

                        // Vérifier si l'insertion a réussi
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Absence enregistrée avec succès !");
                            // Effacer les contrôles après l'insertion réussie si nécessaire
                            Txt2.Clear();
                            Time1.Value = DateTime.Now;
                            Time2.Value = DateTime.Now;
                            RTxt3.Clear();

                            // Rafraîchir DataGridView pour refléter les changements
                            Filldg();
                        }
                        else
                        {
                            MessageBox.Show("Échec de l'enregistrement de l'absence.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'enregistrement de l'absence : " + ex.Message);
            }
            finally
            {
                // Fermer la connexion si elle est ouverte
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
            dg.CurrentCell = null;
        }


        private void Filldg()
        {
            try
            {
                if (cn.State != ConnectionState.Open)
                    cn.Open();

                string query = "SELECT A.CIN, P.nom + ' ' + P.prenom AS 'Nom complet', A.date_debut_absence AS 'Debut absence', A.date_fin_absence AS 'Fin absence', A.motif AS 'Motif' FROM Absence A INNER JOIN Personnel P ON A.id_personnel = P.id_personnel";
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
                // Close the connection if it's open
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            try
            {
                // Vérifier si le champ CIN est vide
                if (string.IsNullOrEmpty(Txt1.Text.Trim()))
                {
                    MessageBox.Show("Veuillez saisir un CIN pour effectuer la recherche.");
                    return; // Sortir de la méthode si le champ est vide
                }

                // Ouvrir la connexion si elle n'est pas déjà ouverte
                if (cn.State != ConnectionState.Open)
                    cn.Open();

                // Récupérer le CIN à rechercher dans la DataGridView
                string cinToSearch = Txt1.Text;

                // Vérifier si le CIN existe déjà dans la DataGridView
                string query = "SELECT A.CIN, P.nom + ' ' + P.prenom AS 'Nom complet', A.date_debut_absence AS 'Debut absence', A.date_fin_absence AS 'Fin absence', A.motif AS 'Motif' " +
                               "FROM Absence A INNER JOIN Personnel P ON A.id_personnel = P.id_personnel " +
                               "WHERE A.CIN = @CIN";

                using (SqlCommand command = new SqlCommand(query, cn))
                {
                    command.Parameters.AddWithValue("@CIN", cinToSearch);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dg.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la recherche : " + ex.Message);
            }
            finally
            {
                // Fermer la connexion si elle est ouverte
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            // Check if any row is selected
            if (dg.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner une ligne à modifier.");
                return; // Exit the method if no row is selected
            }

            // Validate all inputs before proceeding
            if (Time1.Value == null || Time2.Value == null || string.IsNullOrWhiteSpace(RTxt3.Text))
            {
                MessageBox.Show("Veuillez remplir toutes les informations avant de modifier.");
                return; // Exit the method if any input is missing
            }

            try
            {
                // Open the connection if it's not already open
                if (cn.State != ConnectionState.Open)
                    cn.Open();

                // Get the new values from the controls
                DateTime debutAbsence = Time1.Value;
                DateTime finAbsence = Time2.Value;
                string motif = RTxt3.Text;

                // Get the value of the CIN from the DataGridView
                string cin = dg.SelectedRows[0].Cells["CIN"].Value.ToString();

                // Check if the personnel exists with the provided CIN
                string checkPersonnelQuery = "SELECT COUNT(*) FROM Personnel WHERE CIN = @CIN";
                using (SqlCommand checkCommand = new SqlCommand(checkPersonnelQuery, cn))
                {
                    checkCommand.Parameters.AddWithValue("@CIN", cin);
                    int personnelCount = (int)checkCommand.ExecuteScalar();

                    if (personnelCount == 0)
                    {
                        MessageBox.Show("Aucun personnel trouvé avec ce CIN. Veuillez vérifier et réessayer.");
                        return; // Exit the method if personnel does not exist
                    }
                }

                // Update the data in the database
                string updateQuery = "UPDATE Absence SET date_debut_absence = @DebutAbsence, date_fin_absence = @FinAbsence, motif = @Motif WHERE CIN = @CIN";
                using (SqlCommand command = new SqlCommand(updateQuery, cn))
                {
                    command.Parameters.AddWithValue("@DebutAbsence", debutAbsence);
                    command.Parameters.AddWithValue("@FinAbsence", finAbsence);
                    command.Parameters.AddWithValue("@Motif", motif);
                    command.Parameters.AddWithValue("@CIN", cin);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Modification effectuée avec succès !");
                        // Refresh the DataGridView to reflect the changes
                        Filldg();
                        Txt2.Text = "";
                        Time1.Value = DateTime.Today;
                        Time2.Value = DateTime.Today;
                        RTxt3.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Aucune modification effectuée. Veuillez vérifier les valeurs et réessayer.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la modification : " + ex.Message);
            }
            finally
            {
                // Close the connection if it's open
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
            dg.CurrentCell = null;
        }




        private void dg_SelectionChanged(object sender, EventArgs e)
        {
            // Lorsqu'une nouvelle ligne est sélectionnée dans la DataGridView, remplir les contrôles avec les valeurs de cette ligne
            if (dg.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dg.SelectedRows[0];
                Txt2.Text = selectedRow.Cells["CIN"].Value.ToString();
                Time1.Value = Convert.ToDateTime(selectedRow.Cells["Debut absence"].Value);
                Time2.Value = Convert.ToDateTime(selectedRow.Cells["Fin absence"].Value);
                RTxt3.Text = selectedRow.Cells["Motif"].Value.ToString();
            }
            else
            {
                // No rows selected, reset the current cell
                dg.CurrentCell = null;
            }
        }

        private void Absence_Load(object sender, EventArgs e)
        {
            Txt2.Text = string.Empty;
            Time1.Value = DateTime.Now;
            Time2.Value = DateTime.Now;
            RTxt3.Clear();
            dg.CurrentCell = null;
        }

        private void Txt1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Txt1.Text))
            {
                // If the TextBox is empty, reload the DataGridView with all data
                Filldg();
            }
            else
            {
                // If the TextBox has text, perform a search
                btn6_Click(sender, e);
            }
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            // Check if all necessary input fields are filled
            if (Time1.Value != null && Time2.Value != null && !string.IsNullOrWhiteSpace(RTxt3.Text))
            {
                try
                {
                    // Open the connection if it's not already open
                    if (cn.State != ConnectionState.Open)
                        cn.Open();

                    // Get the values from the controls
                    DateTime debutAbsence = Time1.Value;
                    DateTime finAbsence = Time2.Value;
                    string motif = RTxt3.Text;

                    // Check if the personnel exists with the provided CIN
                    string cin = Txt2.Text.Trim(); // Assuming Txt2 is your TextBox for CIN input
                    string checkPersonnelQuery = "SELECT COUNT(*) FROM Personnel WHERE CIN = @CIN";
                    using (SqlCommand checkCommand = new SqlCommand(checkPersonnelQuery, cn))
                    {
                        checkCommand.Parameters.AddWithValue("@CIN", cin);
                        int personnelCount = (int)checkCommand.ExecuteScalar();

                        if (personnelCount == 0)
                        {
                            MessageBox.Show("Aucun personnel trouvé avec ce CIN. Veuillez vérifier et réessayer.");
                            return; // Exit the method if personnel does not exist
                        }
                    }

                    // Delete the record from the database
                    string deleteQuery = "DELETE FROM Absence WHERE date_debut_absence = @DebutAbsence AND date_fin_absence = @FinAbsence AND motif = @Motif";
                    using (SqlCommand command = new SqlCommand(deleteQuery, cn))
                    {
                        command.Parameters.AddWithValue("@DebutAbsence", debutAbsence);
                        command.Parameters.AddWithValue("@FinAbsence", finAbsence);
                        command.Parameters.AddWithValue("@Motif", motif);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Suppression effectuée avec succès !");
                            // Refresh the DataGridView to reflect the changes
                            Filldg();
                            Txt2.Text = "";
                            Time1.Value = DateTime.Today;
                            Time2.Value = DateTime.Today;
                            RTxt3.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("Aucune suppression effectuée. Veuillez vérifier les valeurs et réessayer.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur lors de la suppression : " + ex.Message);
                }
                finally
                {
                    // Close the connection if it's open
                    if (cn.State == ConnectionState.Open)
                        cn.Close();
                }
            }
            else
            {
                MessageBox.Show("Veuillez remplir toutes les informations avant de supprimer.");
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

        private void btn5_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            Abs_report report = new Abs_report();
            Abs_form frm = new Abs_form();
            frm.Viewer_abs.ReportSource = report;
            frm.ShowDialog();
            this.Cursor = Cursors.Default;
        }
    }
}
