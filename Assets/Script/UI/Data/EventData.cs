using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
namespace FrameWork.Data
{
    public class EventDataCollect
    {
        public List<EventJsonData> listEventData;

    }
    [System.Serializable]
    public class EventJsonData//선수들의 스텟 및 정보
    {
       public string eventName;
       public string effectText;
       public string eventGuide;

    }

    public class EventData : DataInterface
    {
        public EventDataCollect eventDataCollect;

        public async void AwaitFileRead(string filePath)
        {
            var fileTest = await ReadAllTextAsync(filePath);
            eventDataCollect = JsonConvert.DeserializeObject<EventDataCollect>(fileTest);

        }

        public void Init(string path)
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

        public List<EventJsonData> eventData()
        {
            // await UniTask.WaitUntil(() => characterInfoCollect != null);
            List<EventJsonData> listEventData = eventDataCollect.listEventData;
            return listEventData;
        }
    }
}
