namespace MagicaVoxelRead
{
    public interface IVoxel
    {
        //visual ID = color index or -1 for empty
        int Data { get; }
        bool IsSolid { get; }
        void ToggleSolid();
        VoxelPosition Position { get; }
    }
}