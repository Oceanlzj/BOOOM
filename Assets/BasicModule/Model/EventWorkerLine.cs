using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.BasicModule.Model
{
  public class EventWorkerLine
  {
    public int WorkerID { get; set; }
    public int EventID { get; set; }
    public List<string> Lines_NormalSan {  get; set; }
    public List<string> Lines_LowSan { get; set; }
  } 
}
