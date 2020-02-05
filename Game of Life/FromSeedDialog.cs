using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_of_Life
{
    public partial class FromSeedDialog : Form
    {
        public FromSeedDialog()
        {
            InitializeComponent();
        }

        public decimal FromSeedDialogRandomize
        {
            get
            {
                return FromSeednumericUpDown1.Value;
            }
            set
            {
                FromSeednumericUpDown1.Value = value;
            }
        }

        private void Seedbutton1_Click(object sender, EventArgs e)
        {
            Random random = new Random();

            FromSeedDialogRandomize = random.Next(int.MinValue, int.MaxValue);
            
        }
    }
}
