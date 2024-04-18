using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Text.Json;


namespace Assets.BasicModule.Utility
{
  //read json file to class xml?
  public class JsonReader<T>
  {


    public static T Read(string FilePath)
    {
      string JsonFile = "";
      using (StreamReader sr = new StreamReader(Application.dataPath + '\\' + FilePath))
      {
        JsonFile = sr.ReadToEnd();
        sr.Close();
        sr.Dispose();
      }
      return JsonSerializer.Deserialize<T>(JsonFile);
    }



  }
}
