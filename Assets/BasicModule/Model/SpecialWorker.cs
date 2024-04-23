using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.BasicModule.Model
{
  [Serializable]
  public class SpecialWorker : Worker
  {
    [Category("Task"), Description("开始任务")]
    public Request BeginingTask { get; set; }

    [Category("Task"), Description("当前任务")]
    public Request CurrentTaskID { get; set; }


  }
}
