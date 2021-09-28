using System.Windows.Forms;

namespace WinFormsApp1
{
    public interface IView
    {

    }

    public partial class View : Form, IView
    {
        public View()
        {
            InitializeComponent();
        }
    }
}
