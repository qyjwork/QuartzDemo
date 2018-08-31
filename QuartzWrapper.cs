using CrystalQuartz.Owin;
using Microsoft.Owin.Hosting;
using Owin;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuartzDemo
{
    public class QuartzWrapper
    {
        //定义调度器
        private IScheduler scheduler = null;
        private IDisposable webApp = null;
        private async Task InitJob()
        {
            //定义任务一
            IJobDetail jobDetail1 = JobBuilder.Create<TestJob1>().WithIdentity("job1", "group1").UsingJobData("name", "job1").WithDescription("this is job1").Build();
            ITrigger trigger1 = TriggerBuilder.Create().WithIdentity("trigger1", "group1").WithDescription("this  is  job1's Trigger").WithSimpleSchedule(it => it.WithIntervalInSeconds(3).RepeatForever()).Build();

            //定义任务二
            IJobDetail jobDetail2 = JobBuilder.Create<TestJob2>().WithIdentity("job2", "group1").Build();
            ITrigger trigger2 = TriggerBuilder.Create().WithIdentity("trigger2", "group1").WithSimpleSchedule(it => it.WithIntervalInSeconds(5).RepeatForever()).Build();

            //初始化调度器
            scheduler = new StdSchedulerFactory().GetScheduler().GetAwaiter().GetResult();

            //将任务添加到调度器
            await scheduler.ScheduleJob(jobDetail1, trigger1);
            await scheduler.ScheduleJob(jobDetail2, trigger2);
        }

        public async Task Start()
        {
            //初始化任务信息
            await this.InitJob();

            //定义监控页面
            Action<IAppBuilder> startup = app => app.UseCrystalQuartz(() => { return scheduler; });
            string listeningUrl = "http://127.0.0.1:9008/";
            webApp = WebApp.Start(listeningUrl, startup);
            Console.WriteLine("Server is started");
            Console.WriteLine("URL:" + listeningUrl);
            Console.WriteLine("Starting scheduler...");
            await scheduler.Start();
            Console.WriteLine("scheduler is Started");
        }
        public void Stop()
        {
            this.scheduler.Clear();
            this.scheduler.Shutdown(false);
        }
    }
}
