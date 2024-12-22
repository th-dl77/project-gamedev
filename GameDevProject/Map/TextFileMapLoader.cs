namespace GameDevProject.Map
{
    public class TextFileMapLoader : IMapLoader
    {
        private const int MAP_HEIGHT = 50;
        private const int MAP_WIDTH = 50;
        private readonly IMapReader mapReader;

        public TextFileMapLoader(IMapReader mapReader)
        {
            this.mapReader = mapReader;
        }
        public string[,] Load(string filename)
        {
            string[,] tileMap = new string[MAP_WIDTH, MAP_HEIGHT];

            //read lines
            var lines = mapReader.ReadLines(filename);
            int y = 0;
            foreach (var line in lines)
            {
                string[] tiles = ParseLine(line);
                for (int x = 0; x < MAP_WIDTH; x++)
                {
                    tileMap[x, y] = x < tiles.Length ? tiles[x] : "0";
                }
                y++;
            }

            return tileMap;
        }
        private static string[] ParseLine(string line)
        {
            return line.Split(',');
        }
    }
}
