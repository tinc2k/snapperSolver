using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SnapperSolver.Models
{
    /* [tc] #todo this can probably be refactored to a struct, could make solving slightly faster */
    public class Particle
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public ParticleDirection Direction { get; set; }
    }
}