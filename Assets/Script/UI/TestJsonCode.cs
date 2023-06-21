using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
public class TestJsonCode : MonoBehaviour
{
    [SerializeField]
    string asdasd;
    // Start is called before the first frame update
    void Start()
    {
        //var aa = ReadAllTextAsync("Assets/DataJson/equipInfo.json");

        AwaitFileRead("Assets/DataJson/equipInfo.json");

        
    }

    private static async void AwaitFileRead(string filePath)
    {
        string fileTest = await ReadAllTextAsync(filePath);
        Debug.Log(fileTest);
    }
    static Task<string> ReadAllTextAsync(string filepath)
    {
        return Task.Factory.StartNew(() =>
        {   
            // 텍스트 파일 불러올 시 한글 깨질 때, Encoind.Default 추가
            return File.ReadAllText(filepath);
        });
    }
}
