using System;
using System.Collections.Generic;
using System.Text;
using static BigCubeSolver1025.Utils.Types;

namespace BigCubeSolver1025.Utils
{
    public class Rotation
    {
        public Direction rotatingSide;
        public int layerNum;
        public bool isWidemove;

        public Rotation(Direction rotatingSide, int layerNum, bool isWidemove)
        {
            this.rotatingSide = rotatingSide;
            this.layerNum = layerNum;
            this.isWidemove = isWidemove;
        }
    }
}
