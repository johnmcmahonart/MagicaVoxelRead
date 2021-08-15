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
            if (Data>0)
            {
                _isSolid = true;
            }
        }
        public bool IsSolid => _isSolid;
        public void ToggleSolid()
        {
            _isSolid = !_isSolid;
        }
    }
    
}