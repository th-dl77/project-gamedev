using System.Collections.Generic;

namespace GameDevProject.Map
{
    public interface IMapReader
    {
        IEnumerable<string> ReadLines(string filename);
    }
}
