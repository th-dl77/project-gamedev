using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.Map
{
    public class FileTilemapReader : IMapReader
    {
        public IEnumerable<string> ReadLines(string filename)
        {
            using var stream = TitleContainer.OpenStream(filename);
            using var reader = new StreamReader(stream);
            while (!reader.EndOfStream)
            {
                yield return reader.ReadLine();
            }
        }
    }
}
