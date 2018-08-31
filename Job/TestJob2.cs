using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuartzDemo
{
    public class TestJob2 : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss")} TestJob2");
            });
        }
    }
}
