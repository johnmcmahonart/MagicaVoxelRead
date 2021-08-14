namespace MagicaVoxelRead
{
    public class Voxel : IVoxel
    {
        private bool _isSolid = false;
        public int Data { get; }
        public VoxelPosition Position { get; }

        public Voxel(VoxelPosition _position, int _data)

        {
            Position = _position;
            Data = _data;
        }
        public bool IsSolid { get; }
        public void ToggleSolid()
        {
            _isSolid = !_isSolid;
        }
    }
    
}