namespace MagicaVoxelRead
{
    public class Voxel : IVoxel
    {
        public int Data { get; }
        public VoxelPosition Position { get; }

        public Voxel(VoxelPosition _position,int _data)

        {
            Position = _position;
            Data = _data;
        }
    }
}