using System;

namespace MagicaVoxelRead
{
    //simple class to store postion in 3d space

    public class VoxelPosition
    {
        public int X { get; }
        public int Y { get; }
        public int Z { get; }

        public VoxelPosition(int _x, int _y, int _z)
        {
            if (_x < 0)
                throw new Exception("x can't be less than 0");
            if (_y < 0)
                throw new Exception("y can't be less than 0");
            if (_z < 0)
                throw new Exception("z can't be less than 0");
            X = _x;
            Y = _y;
            z = _z;
        }
    }
}