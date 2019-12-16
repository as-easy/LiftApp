using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiftApp
{
    class People
    {
        public Floors.Floor CurrentFloor { get; set; }
        public Floors.Floor NeedFloor { get; set; }
     
        public People ()
        {           
            Random rnd = new Random();
            CurrentFloor = (Floors.Floor) rnd.Next((int)Floors.Floor.Start, (int)Floors.Floor.End+1);

            int[] temparr = new int[(int)Floors.Floor.End];
            int index = 0;

            foreach(int e in Enum.GetValues(typeof(Floors.Floor)))
            {
                if ((int) CurrentFloor == e) continue;
                temparr[index] = (int)e;
                index++;
            }
            NeedFloor = (Floors.Floor)temparr[rnd.Next(temparr.Length)]; 
        }
    }
}
