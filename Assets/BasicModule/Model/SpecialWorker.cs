using Assets.BasicModule.Factory;
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
    public Request CurrentTask { get; set; }


    public SpecialWorker() { }

    public SpecialWorker(Worker worker)
    {
      object val;
      foreach (var item in worker.GetType().GetProperties())
      {
        if (item.CanWrite)
        {
          val = item.GetValue(worker);
          item.SetValue(this, val);
        }
      }

      BeginingTask = DataFactory.Instance().getInitTaskByWorkerID(ID);
      CurrentTask = BeginingTask;
    }

    public void NextRequest(bool IsYes)
    {
      CurrentTask = IsYes ?
        DataFactory.Instance().getTaskByID(CurrentTask.NextID_Yes)
        : DataFactory.Instance().getTaskByID(CurrentTask.NextID_No);
    }
  }
}