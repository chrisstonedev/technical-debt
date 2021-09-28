using ClassLibrary;

namespace WindowsFormsApp
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
        }

        public NewMethods Presenter { get; internal set; }
    }
}
