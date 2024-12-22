using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.Map
{
    public interface IMapLoader
    {
        string[,] Load(string filename);
    }
}
