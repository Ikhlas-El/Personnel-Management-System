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
    public partial class Rech_form : Form
    {
        public Rech_form()
        {
            InitializeComponent();
        }

        private void pic2_Click(object sender, EventArgs e)
        {
            Recherche newForm = new Recherche();
            newForm.Show();
            this.Hide();
        }
    }
}
