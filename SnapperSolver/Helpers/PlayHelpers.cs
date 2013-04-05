using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using SnapperSolver.Models;

namespace SnapperSolver.Helpers
{
    public class PlayHelpers
    {
        public static string GetBlockTypeCssClass(BlockType type)
        {
            switch (type)
            {
                case BlockType.Red:
                    return "gameBlockRed";
                case BlockType.Green:
                    return "gameBlockGreen";
                case BlockType.Orange:
                    return "gameBlockOrange";
                case BlockType.Blue:
                    return "gameBlockBlue";
                default:
                    return "";
            }
        }
    }
}