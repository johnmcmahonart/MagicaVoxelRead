namespace MagicaVoxelRead
{
    public interface IVoxel
    {
        //visual ID = color index or -1 for empty
        public int Data { get; }
        public bool IsSolid { get; }
        public void ToggleSolid();
        public VoxelPosition Position { get; }
    }
}