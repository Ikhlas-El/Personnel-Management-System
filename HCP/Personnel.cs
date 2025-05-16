using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HCP
{
    public partial class Personnel : Form
    {
        private string connectionString;
        private FormWindowState previousWindowState = FormWindowState.Normal;
        private const float ScrollSpeedFactor = 0.2f;

        public Personnel()
        {
            InitializeComponent();
            InitializeConnection();
            InitializeCustomScrollbar();
            InitializeDateTimePicker();
            Filldg();
            FillCombo(combo1, "SELECT nom_categorie, id_categorie FROM Categorie", "nom_categorie", "id_categorie");
            FillCombo(combo3, "SELECT nom_affectation, id_affectation FROM Affectation", "nom_affectation", "id_affectation");
            FillCombo(combo2, "SELECT nom_fonction, id_fonction FROM Fonction", "nom_fonction", "id_fonction");
            combo1.SelectedIndex = -1;
            combo2.SelectedIndex = -1;
            combo3.SelectedIndex = -1;

        }

        private void InitializeConnection()
        {
            string mode = Properties.Settings.Default.Mode;

            if (mode == "SQL")
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
        }



        private void InitializeDateTimePicker()
        {
            Time1.Value = new DateTime(2024, 9, 1);
            Time2.Value = new DateTime(2024, 9, 30);
        }




        private void pic2_Click_1(object sender, EventArgs e)
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

        private void but1_Click(object sender, EventArgs e)
        {
            Dashboard newForm = new Dashboard();
            newForm.Show();
            this.Hide();
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

        private void but5_Click(object sender, EventArgs e)
        {
            Paiement newForm = new Paiement();
            newForm.Show();
            this.Hide();
        }

        private void FillCombo(ComboBox comboBox, string query, string displayMember, string valueMember)
        {
            try
            {
                DataTable dataTable = new DataTable();
                using (SqlConnection cn = new SqlConnection(connectionString))
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

        private void Filldg()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = @"SELECT P.CIN, P.nom + ' ' + P.prenom AS [Nom complet], 
                                     F.nom_fonction AS [Fonction], C.nom_categorie AS [Categorie], 
                                     A.nom_affectation AS [Affectation] 
                                     FROM Personnel P 
                                     JOIN Fonction F ON P.id_fonction = F.id_fonction 
                                     JOIN Categorie C ON P.id_categorie = C.id_categorie 
                                     JOIN Affectation A ON P.id_affectation = A.id_affectation;";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, cn);
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

        private void btn4_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            string cin = Txt1.Text.Trim();
            if (string.IsNullOrEmpty(cin))
            {
                MessageBox.Show("Veuillez saisir un CIN.");
                return;
            }

            string query = @"
                SELECT P.nom, P.prenom, P.date_debut, P.date_fin, P.zone_supervision, P.rib, 
                       P.id_categorie, P.id_fonction, P.id_affectation
                FROM Personnel P
                WHERE P.CIN = @cin;";

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@cin", cin);
                    cn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Txt2.Text = reader["nom"].ToString();
                            Txt3.Text = reader["prenom"].ToString();
                            Time1.Value = Convert.ToDateTime(reader["date_debut"]);
                            Time2.Value = Convert.ToDateTime(reader["date_fin"]);
                            Txt4.Text = reader["zone_supervision"].ToString();
                            Txt5.Text = reader["rib"].ToString();

                            combo1.SelectedValue = reader["id_categorie"];
                            combo2.SelectedValue = reader["id_fonction"];
                            combo3.SelectedValue = reader["id_affectation"];
                        }
                        else
                        {
                            MessageBox.Show("Aucun personnel trouvé avec ce CIN.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la récupération des données : " + ex.Message);
            }
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            try
            {
                string cin = Txt1.Text.Trim();
                string nom = Txt2.Text.Trim();
                string prenom = Txt3.Text.Trim();
                DateTime dateDebut = Time1.Value;
                DateTime dateFin = Time2.Value;
                string zoneSupervision = Txt4.Text.Trim();
                string rib = Txt5.Text.Trim();

                // Check for null values in combo box SelectedValue
                int idCategorie = combo1.SelectedValue == null ? -1 : (int)combo1.SelectedValue;
                int idFonction = combo2.SelectedValue == null ? -1 : (int)combo2.SelectedValue;
                int idAffectation = combo3.SelectedValue == null ? -1 : (int)combo3.SelectedValue;

                if (string.IsNullOrEmpty(cin) || string.IsNullOrEmpty(nom) || string.IsNullOrEmpty(prenom) ||
                    string.IsNullOrEmpty(rib) || idCategorie == -1 || idFonction == -1 || idAffectation == -1)
                {
                    MessageBox.Show("Veuillez remplir tous les champs requis.");
                    return;
                }

                // Check if CIN already exists in the Personnel table
                if (IsCinExisting(cin))
                {
                    MessageBox.Show("Un personnel avec ce numéro de CIN existe déjà.");
                    return;
                }

                string query = @"
            INSERT INTO Personnel (CIN, nom, prenom, date_debut, date_fin, zone_supervision, rib, 
                                   id_categorie, id_fonction, id_affectation)
            VALUES (@cin, @nom, @prenom, @dateDebut, @dateFin, @zoneSupervision, @rib, @idCategorie, @idFonction, @idAffectation);";

                using (SqlConnection cn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@cin", cin);
                    cmd.Parameters.AddWithValue("@nom", nom);
                    cmd.Parameters.AddWithValue("@prenom", prenom);
                    cmd.Parameters.AddWithValue("@dateDebut", dateDebut);
                    cmd.Parameters.AddWithValue("@dateFin", dateFin);
                    cmd.Parameters.AddWithValue("@zoneSupervision", zoneSupervision);
                    cmd.Parameters.AddWithValue("@rib", rib);
                    cmd.Parameters.AddWithValue("@idCategorie", idCategorie);
                    cmd.Parameters.AddWithValue("@idFonction", idFonction);
                    cmd.Parameters.AddWithValue("@idAffectation", idAffectation);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Données insérées avec succès.");
                Filldg();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'insertion des données : " + ex.Message);
            }
        }



        private bool IsCinExisting(string cin)
        {
            string query = "SELECT COUNT(*) FROM Personnel WHERE CIN = @cin;";
            int count = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@cin", cin);

                    cn.Open();
                    count = (int)cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la vérification de l'existence du CIN : " + ex.Message);
            }

            return count > 0;
        }


        private void ClearFields()
        {
            Txt1.Text = string.Empty;
            Txt2.Text = string.Empty;
            Txt3.Text = string.Empty;
            combo1.SelectedIndex = -1;
            combo2.SelectedIndex = -1;
            combo3.SelectedIndex = -1;
            Time1.Value = new DateTime(2024, 9, 1);
            Time2.Value = new DateTime(2024, 9, 30);
            Txt4.Text = string.Empty;
            Txt5.Text = string.Empty;
        }

        private void pic2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pic3_Click_1(object sender, EventArgs e)
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

        private void pic4_Click_1(object sender, EventArgs e)
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

        private void dg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dg.Rows[e.RowIndex];

                Txt1.Text = row.Cells["CIN"].Value.ToString();
                Txt2.Text = row.Cells["Nom complet"].Value.ToString().Split(' ')[0]; // Assuming "Nom complet" is "nom prenom"
                Txt3.Text = row.Cells["Nom complet"].Value.ToString().Split(' ')[1]; // Assuming "Nom complet" is "nom prenom"

                // Retrieve more detailed information from the database
                string cin = Txt1.Text.Trim();
                if (!string.IsNullOrEmpty(cin))
                {
                    string query = @"
                        SELECT P.date_debut, P.date_fin, P.zone_supervision, P.rib, 
                               P.id_categorie, P.id_fonction, P.id_affectation
                        FROM Personnel P
                        WHERE P.CIN = @cin;";

                    try
                    {
                        using (SqlConnection cn = new SqlConnection(connectionString))
                        using (SqlCommand cmd = new SqlCommand(query, cn))
                        {
                            cmd.Parameters.AddWithValue("@cin", cin);
                            cn.Open();

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    Time1.Value = Convert.ToDateTime(reader["date_debut"]);
                                    Time2.Value = Convert.ToDateTime(reader["date_fin"]);
                                    Txt4.Text = reader["zone_supervision"].ToString();
                                    Txt5.Text = reader["rib"].ToString();

                                    combo1.SelectedValue = reader["id_categorie"];
                                    combo2.SelectedValue = reader["id_fonction"];
                                    combo3.SelectedValue = reader["id_affectation"];
                                }
                                else
                                {
                                    MessageBox.Show("Aucun personnel trouvé avec ce CIN.");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erreur lors de la récupération des données : " + ex.Message);
                    }
                }
            }
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            // Check if all required fields are filled
            if (string.IsNullOrEmpty(Txt1.Text) || string.IsNullOrEmpty(Txt2.Text) || string.IsNullOrEmpty(Txt3.Text) ||
                combo1.SelectedIndex == -1 || combo2.SelectedIndex == -1 || combo3.SelectedIndex == -1 ||
                string.IsNullOrEmpty(Txt5.Text))
            {
                MessageBox.Show("Veuillez remplir tous les champs.");
                return;
            }

            // Confirm modification
            var confirmResult = MessageBox.Show("Êtes-vous sûr de vouloir modifier ces informations ?", "Confirmation", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                // Prepare update query
                string query = @"
            UPDATE Personnel
            SET nom = @nom, prenom = @prenom, date_debut = @dateDebut, date_fin = @dateFin,
                zone_supervision = @zoneSupervision, rib = @rib, id_categorie = @idCategorie,
                id_fonction = @idFonction, id_affectation = @idAffectation
            WHERE CIN = @cin";

                try
                {
                    using (SqlConnection cn = new SqlConnection(connectionString))
                    using (SqlCommand cmd = new SqlCommand(query, cn))
                    {
                        cmd.Parameters.AddWithValue("@cin", Txt1.Text.Trim());
                        cmd.Parameters.AddWithValue("@nom", Txt2.Text.Trim());
                        cmd.Parameters.AddWithValue("@prenom", Txt3.Text.Trim());
                        cmd.Parameters.AddWithValue("@dateDebut", Time1.Value);
                        cmd.Parameters.AddWithValue("@dateFin", Time2.Value);
                        cmd.Parameters.AddWithValue("@zoneSupervision", Txt4.Text.Trim());
                        cmd.Parameters.AddWithValue("@rib", Txt5.Text.Trim());
                        cmd.Parameters.AddWithValue("@idCategorie", combo1.SelectedValue);
                        cmd.Parameters.AddWithValue("@idFonction", combo2.SelectedValue);
                        cmd.Parameters.AddWithValue("@idAffectation", combo3.SelectedValue);

                        cn.Open();
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Informations modifiées avec succès.");
                    Filldg(); // Refresh DataGridView
                    ClearFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur lors de la modification des données : " + ex.Message);
                }
            }
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            string cin = Txt1.Text.Trim();

            if (string.IsNullOrEmpty(cin))
            {
                MessageBox.Show("Veuillez saisir un CIN pour supprimer.");
                return;
            }

            // Confirm deletion with the user
            var confirmResult = MessageBox.Show("Êtes-vous sûr de vouloir supprimer cet enregistrement ?", "Confirmation de suppression", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                string query = @"
            DELETE FROM Personnel
            WHERE CIN = @cin;";

                try
                {
                    using (SqlConnection cn = new SqlConnection(connectionString))
                    using (SqlCommand cmd = new SqlCommand(query, cn))
                    {
                        cmd.Parameters.AddWithValue("@cin", cin);

                        cn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Enregistrement supprimé avec succès.");
                            Filldg(); // Refresh DataGridView after deletion
                            ClearFields(); // Clear input fields
                        }
                        else
                        {
                            MessageBox.Show("Aucun enregistrement trouvé avec ce CIN.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur lors de la suppression : " + ex.Message);
                }
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

        private void Personnel_Load(object sender, EventArgs e)
        {
            dg.ClearSelection();
        }
    }
}
