namespace ClassLibrary
{
    public interface IModel
    {
        int GetFive();
    }

    public class Model : IModel
    {
        public int GetFive()
        {
            return 5;
        }
    }
}
