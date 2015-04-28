using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Clear_
{
    class Actor
    {
        public int X {get;set;}
        public int Y{get;set;}
        public float Angle { get; set; }

        public Actor()
        {
            X = 10;
            Y = 10;
            Angle = 90;
        }
    }
}
