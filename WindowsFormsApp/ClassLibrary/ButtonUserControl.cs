using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassLibrary
{
    public partial class ButtonUserControl : UserControl
    {
        public ButtonUserControl()
        {
            InitializeComponent();
        }

        public event Action HeyWhatsThis;
        public event EventHandler SumoOrange;

        private void getMainButton_Click(object sender, EventArgs e)
        {
            HeyWhatsThis?.Invoke();
            SumoOrange?.Invoke(sender, e);
        }
    }
}
