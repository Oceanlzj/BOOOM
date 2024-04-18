using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.BasicModule.Utility
{
  public class JsonWriter<T>
  {
    public static void Write(T obj, string Path)
    {
      using (StreamWriter sw = new StreamWriter(Application.persistentDataPath + '\\' + Path))
      {
        sw.WriteLine(JsonSerializer.Serialize(obj, typeof(T)));
        sw.Close();
        sw.Dispose();
      }
    }
  }
}
