public class Worker
{
  public enum WorkerType
  {
    None = 0,
    Human = 1//and more
  }

  public int ID { get; set; }
  public WorkerType Type { get; set; }
  public string Name { get; set; }
  public string Description { get; set; }
  
  //stats...
  public int Stat1 { get; set; }
  public int Stat2 { get; set; }
  public bool IsAlive { get; set; }
}