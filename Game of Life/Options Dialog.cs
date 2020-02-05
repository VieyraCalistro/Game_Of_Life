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
    public partial class Options_Dialog : Form
    {
        public Options_Dialog()
        {
            InitializeComponent();
        }

        public decimal OptionsTimer
        {
            get
            {
                return numericUpDown1.Value;
            }
            set
            {
                numericUpDown1.Value = value;
            }
        }

        public decimal OptionsUniverseWidth
        {
            get
            {
                return numericUpDown2.Value;
            }
            set
            {
                numericUpDown2.Value = value;
            }
        }

        public decimal OptionsUniverseHeight
        {
            get
            {
                return numericUpDown3.Value;
            }
            set
            {
                numericUpDown3.Value = value;
            }
        }
    }
}
