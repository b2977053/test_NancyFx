using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace test_NancyFx
{
    interface ICounter
    {
    }

    public class Counter: ICounter
    {
        public double cpuCounter { get; set; }
        public double memoryCounter { get; set; }

        public Counter()
        {
            /*
             * 程式範例－使用 C# 查詢 CPU 與記憶體使用狀況-黑暗執行緒
             * https://blog.darkthread.net/blog/performancecounter-c-sample/
             */
            PerformanceCounter cpu = new PerformanceCounter("Processor"
            , "% Processor Time"
            , "_Total");
            PerformanceCounter memory = new PerformanceCounter("Memory"
            , "% Committed Bytes in Use");

            Queue<float> cpuCounters = new Queue<float>();
            Queue<float> memoryCounters = new Queue<float>();
            float total_cpu = 0;
            float total_memory = 0;

            for (int i = 0; i < 10; i++)
            {
                float cpuCounter = cpu.NextValue();
                total_cpu += cpuCounter;
                float memoryCounter = memory.NextValue();
                total_memory += memoryCounter;

                cpuCounters.Enqueue(cpuCounter);
                memoryCounters.Enqueue(memoryCounter);
                Thread.Sleep(100);
            }

            cpuCounter = System.Math.Round(total_cpu / cpuCounters.Count * 10) / 10;
            memoryCounter = System.Math.Round(total_memory / memoryCounters.Count * 10) / 10;
        }
    }
}
