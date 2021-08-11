namespace MagicaVoxelRead
{
    public class Voxel : IVoxel
    {
        public int Vid { get; }
        public VoxelPosition Position { get; }

        public Voxel(VoxelPosition _position,int _vid)

        {
            Position = _position;
            Vid = _vid;
        }
    }
}