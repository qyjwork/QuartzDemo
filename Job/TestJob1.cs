using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuartzDemo
{
    //实现JJob接口  3.0之后该接口方法为async模式 
    public class TestJob1 : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Task.Factory.StartNew(() =>
            {
                var data = context.JobDetail.JobDataMap;

                Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss")} TestJob1");
                //以下输出可以输出任务的相信信息,包括名称等
                //Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss")} {Environment.NewLine}TestJob1 Name: {data.Get("name")} TestJob1 Description:{context.JobDetail.Description} {Environment.NewLine}Trigger: Name:{context.Trigger.Key.Name} Description:{context.Trigger.Description}");
            });
        }

    }
}
