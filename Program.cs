using CrystalQuartz.Owin;
using Owin;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace QuartzDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.New(x =>
            {
                x.StartAutomatically();
                x.UseAssemblyInfoForServiceInfo();
                x.Service<QuartzWrapper>(s => {
                    s.ConstructUsing(srv => new QuartzWrapper());
                    s.WhenStarted(async job => await job.Start());
                    s.WhenStopped(job => job.Stop());
                });
                x.SetServiceName("Quartz任务");
                x.SetDisplayName("Quartz任务");
                x.SetDescription("Quartz任务");
                x.EnableServiceRecovery(sr =>
                {
                    sr.RestartService(1);
                    sr.RestartService(2);
                });
            }).Run();
        }
    }
}
