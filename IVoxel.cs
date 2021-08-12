namespace MagicaVoxelRead
{
    public interface IVoxel
    {
        //visual ID = color index or enum int
        public VoxelType Type { get; }

        public VoxelPosition Position { get; }
    }
}