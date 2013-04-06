using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SnapperSolver.Models
{
    /* [tc] #todo this can probably be refactored to a struct, could make solving faster */
    public class Block
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public BlockType Type;

        public Block Clone()
        {
            return new Block() {
                Row = this.Row,
                Column = this.Column,
                Type = this.Type
            };
        }
    }
}