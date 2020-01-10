using Nancy;
using Nancy.Hosting.Self;
using System;

namespace test_NancyFx
{
    class Program
    {
        /*
         * NancyFx－打造小型 WebAPI 與 Microservice 的輕巧利器-黑暗執行緒
         * https://blog.darkthread.net/blog/nancyfx
         * 
         * Nancy.Hosting.Self
         * 
         * 開放授權  netsh http add urlacl url="http://+:9527/" user="Everyone"
         * 移除權限  netsh http delete urlacl url="http://+:9527/"
         */
        static void Main(string[] args)
        {
            using (var host = new NancyHost(
                    new Uri("http://localhost:9527"))) // http://127.0.0.1:9527/
            {
                try
                {
                    host.Start();
                    Console.WriteLine("Press any key to stop...");
                    Console.Read();
                    host.Stop();
                }
                catch (AutomaticUrlReservationCreationFailureException ex)
                {
                    Console.WriteLine("權限不足:" + ex.Message);
                    Console.WriteLine("Press any key to stop...");
                    Console.Read();

                }
            }
        }

        
    }

    public class GuidGeneratorModule : NancyModule
    {
        public GuidGeneratorModule()
        {
            Get("/", (p) =>
            {
                Counter counter = new Counter();
                return Response.AsJson(counter);
            });
        }
    }


}
