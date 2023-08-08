using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;

namespace FrameWork.Data
{
    public class MapInformation
    {
        public MapInfoCollect characterCollect;
    }

    public class MapInfoCollect
    {
        public int playerSaveStageIndex;
        public int nodeSize;
        public List<int> nodeAdjacent;

    }
        

    public class MapData : DataInterface
    {
        // Start is called before the first frame update
        public async void AwaitFileRead(string filePath)
        {
            var fileTest = await ReadAllTextAsync(filePath);

        }

        public async void Init(string path)
        {
            AwaitFileRead(path);
        }

        public Task<string> ReadAllTextAsync(string filepath)
        {
            return Task.Factory.StartNew(() =>
            {
                return File.ReadAllText(filepath);
            });
        }
    }
}
