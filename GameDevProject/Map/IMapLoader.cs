namespace GameDevProject.Map
{
    public interface IMapLoader
    {
        string[,] Load(string filename);
    }
}
