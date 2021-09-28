namespace WinFormsApp1
{
    public interface IModel
    {
        int Variance { get; }
    }

    class Model : IModel
    {
        public int Variance => 5;
    }
}
