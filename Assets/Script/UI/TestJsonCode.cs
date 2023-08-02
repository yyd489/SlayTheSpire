using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

[System.Serializable]
public class CharacterInfoCollet
{
    public CharacterCollect characterCollectt;

}

[System.Serializable]
public class CharacterCollect
{
   public string name;
   public int hp;
   public int mp;
   public int attack;
    
}

public class TestJsonCode : MonoBehaviour
{
    [SerializeField]
    string asdasd;

    [SerializeField]
    CharacterInfoCollet characterInfoCollet;

    // Start is called before the first frame update
    void Start()
    {
        AwaitFileRead("Assets/DataJson/CharacterCollect.json");
    }

    private async void AwaitFileRead(string filePath)
    {
        string fileTest = await ReadAllTextAsync(filePath);

        characterInfoCollet = JsonConvert.DeserializeObject<CharacterInfoCollet>(fileTest);
    }
    Task<string> ReadAllTextAsync(string filepath)
    {
        return Task.Factory.StartNew(() =>
        {   
            // 텍스트 파일 불러올 시 한글 깨질 때, Encoind.Default 추가
            return File.ReadAllText(filepath);
        });
    }
}
