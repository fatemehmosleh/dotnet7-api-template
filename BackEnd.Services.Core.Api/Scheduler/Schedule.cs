using Quartz;
using Quartz.Simpl;
using Quartz.Spi;
using BackEnd.Services.Core.Api.Context;
using BackEnd.Services.Core.Api.Facade.Abstraction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
namespace BackEnd.Services.Core.Api.Scheduler
{

    public class DemoJob : IJob
    {
        
        public readonly CoreContext _context;


        public DemoJob( CoreContext coreContext)
        {
            _context = coreContext;
        }

        public void logfile(DateTime time, string message)
        {
            string path = "\\var\\log\\BackEnd\\sample.txt"; // this line will use for server

            //string path = "C:\\log\\sample.txt";
            //using (StreamWriter writer = new StreamWriter(path, true))
            //{
            //    writer.WriteLine(message + "\t:" + time);
            //    writer.Close();
            //}
        }

        public  async Task Execute(IJobExecutionContext context)
        {
            

            await Task.CompletedTask;
        }
    }


    public class Task1 : IJobFactory
    {

        
        private readonly IServiceProvider _serviceProvider;
       
        public Task1(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
               
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return _serviceProvider.GetService<DemoJob>();
        }      

        public void ReturnJob(IJob job)
        {
            var disposable = job as IDisposable;
            disposable?.Dispose();
        }
        
    }
}
