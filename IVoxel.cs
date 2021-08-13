namespace MagicaVoxelRead
{
    public interface IVoxel
    {
        //visual ID = color index or -1 for empty
        public int Data { get; }

        public VoxelPosition Position { get; }
    }
}