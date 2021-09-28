using System.Runtime.InteropServices;

namespace ClassLibrary
{
    [Guid("CC9A9CBC-054A-4C9C-B559-CE39A5EA2742")]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ComVisible(true)]
    public class NewMethods
    {
        private readonly IModel model;

        public NewMethods()
        {
            model = new Model();
        }

        public NewMethods(IModel model)
        {
            this.model = model;
        }

        public int ReturnFive()
        {
            return model.GetFive();
        }
    }
}
