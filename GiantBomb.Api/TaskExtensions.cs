using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiantBomb.Api
{
    public static class TaskExtensions
    {
        public static T WaitForResult<T>(this Task<T> task, int millisecondsToWait = 0)
        {
            task.Wait(millisecondsToWait);
            if (task.IsFaulted && task.Exception != null)
                throw task.Exception;

            return task.Result;
        }
    }
}
