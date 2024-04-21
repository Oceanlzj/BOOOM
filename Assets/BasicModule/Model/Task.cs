using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.BasicModule.Model
{
  [Serializable]
  public class Task
  {
    public enum TaskType
    {
      None = 0,
      ExtraPortion = 1//and more ...
    }
    public int ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public TaskType Type { get; set; }
    public bool Triggered { get; set; }
    public bool Finished { get; set; }

    public Task NextTask { get; set; } = null;

  }
}
