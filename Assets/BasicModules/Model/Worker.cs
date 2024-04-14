using System.Collections;
using System.Collections.Generic;

public class Worker
{
  public enum WorkerType
  {
    Standard = 0
  }
  public int Id { get; set; }

  public WorkerType Type { get; set; }
  public string Name { get; set; }
  public string Description { get; set; }

  //Worker's more statics here ...

}



