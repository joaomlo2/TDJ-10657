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
        public bool Armed { get; set; }

        public Actor()
        {
            X = 40;
            Y = 40;
            Angle = 90;
            Armed = false;
        }
    }
}
