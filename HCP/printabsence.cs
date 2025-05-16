using System;
using System.Data;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

namespace HCP
{
    public partial class printabsence : Form
    {
        private FormWindowState previousWindowState = FormWindowState.Normal;
        private DataTable reportData;

        public printabsence(DataTable dataTable)
        {
            InitializeComponent();
            reportData = dataTable;
            LoadReport(reportData);
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

        private void pic2_Click(object sender, EventArgs e)
        {
            Absence newForm = new Absence();
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

        private void LoadReport(DataTable dataTable)
        {
            try
            {
                repo_absence reportDocument = new repo_absence();
                reportDocument.Load("repo_absence.rpt");
                reportDocument.SetDataSource(dataTable);
                viewer.ReportSource = reportDocument;
                viewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading report: " + ex.Message);
            }
        }
    }
}
