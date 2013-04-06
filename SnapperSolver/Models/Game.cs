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

        public Game(Matrix matrix)
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
            block.Type = (BlockType)((int)block.Type - 1);                              /*block changes type when hit*/
            if (block.Type == BlockType.Null)                                           /*block died?*/
            {
                Matrix.Blocks.Remove(block);                                            /*remove the block from the matrix*/
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
                if (particle.Row < 1 || particle.Row > Dimensions.NumberOfRows || particle.Column < 1 || particle.Column > Dimensions.NumberOfRows)
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
}