using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace LiftApp
{
    class Manage
    {
        public Floors floors { get; set; }
        public Lift lift { get; set; }
        public const int _timerPeople = 3000;   //появление людей на этаже
        public const int _timerLift = 1000;     //таймер лифта
        public const int _timerDisplay = 1000;  //таймер вывода сообщений
        public const int CapacityLift = 4;      //Вместимость лифта

        public Manage(Floors floors, Lift lift)
        {
            this.floors = floors;
            this.lift = lift;
        }

        //Функция для вызова нового объекта People и занесения в очередь на конкретном этаже
        public void AppearPeopleOnFloor()       
        {
            do
            {
                People people = new People();
                floors.QueuePeopleOnFloor[(int)people.CurrentFloor].Enqueue(people);
                floors.SumQueuePeopleOnFloor++;
                Thread.Sleep(_timerPeople);
            } while (true);
        }

        //Функция движения лифта, ведение статистики ожидающих и доставленных людей
        public void MovingLift()
        {
            do
            {
                if (lift.CurrentFloorLift == lift.NeedFloorLift)
                {
                    if (lift.PeopleInLift.Count() != 0)
                    {

                        Thread.Sleep(_timerLift);
                        floors.DeliveredPeople[lift.PeopleInLift.Peek().NeedFloor]++;
                        floors.SumDeliveredPeople++;
                        lift.PeopleInLift.Dequeue();
                        if (lift.PeopleInLift.Count() != 0) //проверка есть ли еще в лифте люди, которым нужно выйти на этом этаже
                        {
                            lift.NeedFloorLift = lift.PeopleInLift.Peek().NeedFloor;
                            if (lift.NeedFloorLift == lift.CurrentFloorLift) continue;
                        }
                        else
                            LiftCallFun();
                    }
                    else
                        LiftCallFun();

                    while (lift.PeopleInLift.Count() != CapacityLift && floors.QueuePeopleOnFloor[(int)lift.CurrentFloorLift].Count != 0)
                    {
                        Thread.Sleep(_timerLift);
                        lift.PeopleInLift.Enqueue(floors.QueuePeopleOnFloor[(int)lift.CurrentFloorLift].Dequeue());
                        floors.SumQueuePeopleOnFloor--;                        
                    }

                    lift.NeedFloorLift = lift.PeopleInLift.Peek().NeedFloor;
                }
                else
                {
                    switch (lift.CurrentFloorLift < lift.NeedFloorLift)
                    {
                        case (true):
                            Thread.Sleep(_timerLift);
                            lift.CurrentFloorLift++;
                            break;
                        case (false):
                            Thread.Sleep(_timerLift);
                            lift.CurrentFloorLift--;
                            break;
                    }

                }
            } while (true);
        }

        //функция вызова лифта с этажа
        public void LiftCallFun()
        {
            foreach (Floors.Floor e in Enum.GetValues(typeof(Floors.Floor)))
            {
                if (floors.QueuePeopleOnFloor[(int)e].Count != 0)
                {
                    lift.NeedFloorLift = e;
                    break;
                }
            }
        }

        //Функция вывода сообщений
        public void DisplayFun()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("Текущее местоположение лифта: " + ((int)lift.CurrentFloorLift + 1));
                Console.WriteLine("Человеку в лифте надо на: " + ((lift.PeopleInLift.Count == 0) ? "" : ((int)lift.NeedFloorLift + 1).ToString()));
                Console.WriteLine("Количество человек в лифте: " + (lift.PeopleInLift.Count) + "\n");

                foreach (Floors.Floor e in Enum.GetValues(typeof(Floors.Floor)))
                {
                    Console.Write($"Количество человек на {(int)e + 1} этаже: " + floors.QueuePeopleOnFloor[(int)e].Count + "/" + floors.SumQueuePeopleOnFloor + "\t");
                    Console.Write($"Доставлено человек на {(int)e + 1} этаже: " + floors.DeliveredPeople[e] + "/" + floors.SumDeliveredPeople + "\n");
                }

                Thread.Sleep(_timerDisplay);
            } while (true);
        }

    }
}
