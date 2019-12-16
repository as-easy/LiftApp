using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace LiftApp
{
    class Lift
    {
        public Queue<People> PeopleInLift { get; set; }
        public Floors.Floor CurrentFloorLift { get; set; }
        public Floors.Floor NeedFloorLift { get; set; }
        public Floors floors { get; set; }

        public Lift(Floors floors)
        {
            PeopleInLift = new Queue<People>();
            CurrentFloorLift = Floors.Floor.Start;
            this.floors = floors;
        }
    }
}
