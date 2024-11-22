using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace практика_9
{
    struct Student // Объявляет структуру Student, которая будет хранить данные студента.
    {
        public Student(string sername, string street, int house, int room) // Конструктор структуры, принимающий параметры для инициализации.
        {
            Sername = sername;
            Street = street;
            House = house;
            Room = room;
        }
        public string Sername { get; set; }//определение свойств
        public string Street { get; set; }
        public int House { get; set; }
        public int Room { get; set; }
    }
}
