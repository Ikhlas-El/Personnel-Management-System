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
    public partial class Receipt_form : Form
    {
        public Receipt_form()
        {
            InitializeComponent();
        }

        private void pic2_Click(object sender, EventArgs e)
        {
            Paiement newForm = new Paiement();
            newForm.Show();
            this.Hide();
        }

        private void Viewer_abs_Load(object sender, EventArgs e)
        {

        }
    }
}
