﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using Autofac;
using Carving.Application;
using Carving.Domain.Core.Repositories;
using Carving.Domain.Repository.EF.Data;
using Carving.Domain.Repository.EF.Repository;

namespace Carving.Cmd
{
    class Program
    {
        static void Main(string[] args)
        {

            //获取当前进程对象
            Process cur = Process.GetCurrentProcess();

            PerformanceCounter curpcp = new PerformanceCounter("Process", "Working Set - Private", cur.ProcessName);
            PerformanceCounter curpc = new PerformanceCounter("Process", "Working Set", cur.ProcessName);
            PerformanceCounter curtime = new PerformanceCounter("Process", "% Processor Time", cur.ProcessName);

            //上次记录CPU的时间
            TimeSpan prevCpuTime = TimeSpan.Zero;
            //Sleep的时间间隔
            int interval = 1000;

            PerformanceCounter totalcpu = new PerformanceCounter("Processor", "% Processor Time", "_Total");

            SystemInfo sys = new SystemInfo();
            //const int KB_DIV = 1024;
            const int MB_DIV = 1024 * 1024;
            const int GB_DIV = 1024 * 1024 * 1024;
            while (true)
            {
                //第一种方法计算CPU使用率
                //当前时间
                TimeSpan curCpuTime = cur.TotalProcessorTime;
                //计算
                double value = (curCpuTime - prevCpuTime).TotalMilliseconds / interval / Environment.ProcessorCount * 100;
                prevCpuTime = curCpuTime;
                Console.WriteLine();
                Console.WriteLine("{0}:{1}  {2:N}KB CPU使用率：{3}", cur.ProcessName, "工作集(进程类)", cur.WorkingSet64 / 1024, value);//这个工作集只是在一开始初始化，后期不变
                Console.WriteLine("{0}:{1}  {2:N}KB CPU使用率：{3}", cur.ProcessName, "工作集        ", curpc.NextValue() / 1024, value);//这个工作集是动态更新的
                //第二种计算CPU使用率的方法
                Console.WriteLine("{0}:{1}  {2:N}KB CPU使用率：{3}%", cur.ProcessName, "私有工作集    ", curpcp.NextValue() / 1024, curtime.NextValue() / Environment.ProcessorCount);
                //Thread.Sleep(interval);

                //第一种方法获取系统CPU使用情况
                Console.WriteLine("\r系统CPU使用率：{0}%", totalcpu.NextValue());
                //Thread.Sleep(interval);

                //第二章方法获取系统CPU和内存使用情况
                Console.WriteLine("\r系统CPU使用率：{0}%，系统内存使用大小：{1}MB({2}GB)", sys.CpuLoad, (sys.PhysicalMemory - sys.MemoryAvailable) / MB_DIV, (sys.PhysicalMemory - sys.MemoryAvailable) / (double)GB_DIV);
                Thread.Sleep(interval);
            }

            //Console.ReadLine();

        }
        static void UsingProcess(string pname)
        {

            using (var pro = Process.GetProcessesByName(pname)[0])
            {

                //间隔时间（毫秒）

                int interval = 1000;

                //上次记录的CPU时间

                var prevCpuTime = TimeSpan.Zero;

                while (true)
                {

                    //当前时间

                    var curTime = pro.TotalProcessorTime;

                    //间隔时间内的CPU运行时间除以逻辑CPU数量

                    var value = (curTime - prevCpuTime).TotalMilliseconds / interval / Environment.ProcessorCount * 100;

                    prevCpuTime = curTime;

                    //输出

                    Console.WriteLine(value);

                    Thread.Sleep(interval);

                }

            }
        }
        static void UsingPerfmon(string pname)
        {

            //创建性能计数器

            using (var p1 = new PerformanceCounter("Process", "% Processor Time", pname))
            {

                while (true)
                {

                    //注意除以CPU数量

                    Console.WriteLine(p1.NextValue() / Environment.ProcessorCount);

                    Thread.Sleep(1000);

                }

            }

        }
    }
}
