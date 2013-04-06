using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SnapperSolver.Models
{
    public class Matrix
    {
        private static ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public List<Block> Blocks { get; set; }

        public Matrix()
        {
            Blocks = new List<Block>();
        }

        public Matrix Clone()
        {
            var temp = new Matrix();
            foreach (var block in Blocks)
                temp.Blocks.Add(block.Clone());
            return temp;
        }

        public void Log()
        {
            for (int i = 1; i <= Dimensions.NumberOfRows; i++)
            {
                string row = "\t";
                for (int j = 1; j <= Dimensions.NumberOfColumns; j++)
                {
                    var candidate = Blocks.FirstOrDefault(b => b.Row == i && b.Column == j);
                    if (candidate != null)
                        row += ((int)candidate.Type).ToString();
                    else
                        row += "0";
                }
                _logger.Info(row);
            }
        }

        public int Sum()
        {
            int sum = 0;
            foreach (var block in Blocks)
                sum += (int)block.Type;
            return sum;
        }

        public string ToString()
        {
            if (Dimensions.NumberOfRows > 9 || Dimensions.NumberOfColumns > 9)
                throw new NotImplementedException();
            /*idea: we could use ascii to store >9 in URLs*/
            var sorted_blocks = Blocks.OrderBy(b => b.Row).ThenBy(b => b.Column);
            var result = "";
            foreach (var block in sorted_blocks)
                result += block.Row.ToString() + block.Column.ToString() + block.Type.ToString().ToLower()[0];
            return result;
        }

        public static List<Block> FromString(string value)
        {
            if (Dimensions.NumberOfRows > 9 || Dimensions.NumberOfColumns > 9)
                throw new NotImplementedException();

            var results = new List<Block>();
            for (int i = 0; i < value.Length; i += 3)
            {
                var stringy_block = value.Substring(i, 3);
                results.Add(new Block()
                {
                    Row = Int16.Parse(stringy_block[0].ToString()),
                    Column = Int16.Parse(stringy_block[1].ToString()),
                    Type = BlockFromString(stringy_block[2])
                });
            }
            return results;
        }

        private static BlockType BlockFromString(char value)
        {
            switch (value)
            {
                case 'r':
                    return BlockType.Red;
                case 'o':
                    return BlockType.Orange;
                case 'g':
                    return BlockType.Green;
                case 'b':
                    return BlockType.Blue;
                default:
                    return BlockType.Null;
            }
        }

    }
}