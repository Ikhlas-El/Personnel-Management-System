using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HCP
{
    public partial class Demarage : Form
    {
        public Demarage()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                progbar1.Value += 1;
                if (progbar1.Value >= 100)
                {
                    timer1.Stop();
                    connexion newForm = new connexion();
                    newForm.Show();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                // Handle the exception, e.g., log it or show an error message.
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
