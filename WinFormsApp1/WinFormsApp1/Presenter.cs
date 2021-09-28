namespace WinFormsApp1
{
    public class Presenter
    {
        private IModel model;
        private IView view;

        public Presenter(IModel model, IView view)
        {
            this.model = model;
            this.view = view;
        }

        public int Test()
        {
            return model.Variance;
        }
    }
}
