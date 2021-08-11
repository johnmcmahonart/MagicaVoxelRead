namespace MagicaVoxelRead
{
    public interface IVoxel
    {
        //visual ID = color index or enum int
        public int Vid { get; }

        public VoxelPosition Position { get; }
    }
}