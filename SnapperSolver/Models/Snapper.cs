using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SnapperSolver.Models
{
    public class Game
    {
        private static ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public Matrix Matrix { get; set; }
        public List<Particle> Particles { get; set; }
        public Game (Matrix matrix)
        {
            Matrix = matrix;
            Particles = new List<Particle>();
        }
        public void Move(int row, int column)
        {
            _logger.Info("Move(" + row + "," + column + ") called, calculating...");
            HitBlock(row, column);
            while (Particles.Count > 0)
            {
                AdvanceTime();
            }
            //Matrix.Log();
        }
        public void MoveSequence(string sequence)
        {
            var moves = sequence.Split(' ');
            foreach (var move in moves)
            {
                if (move != "")
                    Move(Int16.Parse(move[0].ToString()), Int16.Parse(move[1].ToString()));
            }
        }
        private void HitBlock(int row, int column)
        {
            var block = Matrix.Blocks.First(b => b.Row == row && b.Column == column);   /*find block that should be hit*/
            block.Type = (BlockType)((int)block.Type - 1);  /*block changes type when hit*/
            if (block.Type == BlockType.Null)               /*block died?*/
            {
                Matrix.Blocks.Remove(block);                /*remove the block from the matrix*/
                /*create particles*/
                Particles.Add(new Particle() { Row = block.Row, Column = block.Column, Direction = ParticleDirection.Down });
                Particles.Add(new Particle() { Row = block.Row, Column = block.Column, Direction = ParticleDirection.Right });
                Particles.Add(new Particle() { Row = block.Row, Column = block.Column, Direction = ParticleDirection.Up });
                Particles.Add(new Particle() { Row = block.Row, Column = block.Column, Direction = ParticleDirection.Left });
            }
            //_logger.Info("Block " + row.ToString() + column.ToString() + "hit!");
            //Matrix.Log();
        }
        private void AdvanceTime()
        {
            foreach (var particle in Particles.ToList())
            {
                /*move the particle in one direction*/
                switch (particle.Direction)
                {
                    case ParticleDirection.Up:
                        particle.Row -= 1;
                        break;
                    case ParticleDirection.Down:
                        particle.Row += 1;
                        break;
                    case ParticleDirection.Right:
                        particle.Column += 1;
                        break;
                    case ParticleDirection.Left:
                        particle.Column -= 1;
                        break;
                }
                /*did the particle go out of borders?*/
                if ( particle.Row < 1 || particle.Row > Dimensions.NumberOfRows || particle.Column < 1 || particle.Column > Dimensions.NumberOfRows)
                {
                    Particles.Remove(particle);     /*particle is in outer space, kill it*/
                }
                /*did the particle hit a block?*/
                else if (Matrix.Blocks.Any(b => b.Row == particle.Row && b.Column == particle.Column))
                {
                    var row = particle.Row;
                    var column = particle.Column;
                    Particles.Remove(particle);     /*particle is destroyed*/
                    HitBlock(row, column);          /*run hit procedure*/
                }
            }
        }
    }

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
            {
                temp.Blocks.Add(block.Clone());
            }
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
            {
                sum += (int)block.Type;
            }
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
            {
                result += block.Row.ToString() + block.Column.ToString() + block.Type.ToString().ToLower()[0];
            }
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

    public class Block
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public BlockType Type;
        public Block Clone()
        {
            return new Block() { Row = this.Row, Column = this.Column, Type = this.Type };
        }
    }

    public class Particle
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public ParticleDirection Direction { get; set; }
    }

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

    public static class Dimensions
    {
        public const int NumberOfColumns = 5;
        public const int NumberOfRows = 6;
    }
}