using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace LiftApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Floors floors = new Floors();           
            Lift lift = new Lift(floors);
            Manage manage = new Manage(floors, lift);

            Thread FloorsThread = new Thread(new ThreadStart(manage.AppearPeopleOnFloor));
            Thread LiftThread = new Thread(new ThreadStart(manage.MovingLift));
            Thread DisplayFunThread = new Thread(new ThreadStart(manage.DisplayFun));

            FloorsThread.IsBackground = true;
            LiftThread.IsBackground = true;
            DisplayFunThread.IsBackground = true;

            FloorsThread.Start();
            LiftThread.Start();
            DisplayFunThread.Start();

            Console.Read();            
        }       
    }
}
