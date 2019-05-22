using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.Events;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var main = new Driver();
            var link = "https://www.airasia.com/";
            var origin = "Yog";
            var destination = "Sin";
            main.Load(link);
            main.GetOrigin(origin);
            main.GetDestination(destination);
            main.GetDateSingle();
            main.GetDeptArrTime();
            main.ScreenShoot();
            main.StopDrv();
        }

    }
}