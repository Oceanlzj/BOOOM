﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.BasicModule.Model
{
  [Serializable]
  public class SpecialWorker : Worker
  {
    public List<Task> Tasks { get; set; }

  }
}