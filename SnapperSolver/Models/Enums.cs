using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SnapperSolver.Models
{
    public enum BlockType
    {
        Null = 0,
        Red = 1,
        Green = 2,
        Orange = 3,
        Blue = 4
    }

    public enum ParticleDirection
    {
        Up = 0,
        Down = 1,
        Left = 2,
        Right = 3
    }
}