namespace MagicaVoxelRead
{
    public class Voxel : IVoxel
    {
        public VoxelType Type { get; }
        public VoxelPosition Position { get; }

        public Voxel(VoxelPosition _position,VoxelType _type)

        {
            Position = _position;
            Type = _type;
        }
    }
}