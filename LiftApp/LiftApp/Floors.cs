using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace LiftApp
{
    class Floors  
    {
        public enum Floor  { Start, Two, Three, Four, End }

        public Dictionary<Floor,int> DeliveredPeople { get; set; }
        public Queue<People>[] QueuePeopleOnFloor { get; set; }        
        public int SumDeliveredPeople { get; set; }
        public int SumQueuePeopleOnFloor { get; set; }

        public Floors()
        {
            QueuePeopleOnFloor = new Queue<People>[(int)Floors.Floor.End + 1];
            DeliveredPeople = new Dictionary<Floors.Floor, int>();
            SumDeliveredPeople = 0;
            SumQueuePeopleOnFloor = 0;
            foreach (Floors.Floor e in Enum.GetValues(typeof(Floors.Floor)))
            {
                QueuePeopleOnFloor[(int)e] = new Queue<People>();
                DeliveredPeople.Add(e, 0);
            }
        }        
    }
}
