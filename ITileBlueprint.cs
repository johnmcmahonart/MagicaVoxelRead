using System.Collections.Generic;

namespace MagicaVoxelRead
{
    public interface ITileBlueprint
    {
        public IVoxel GetVoxel(VoxelPosition position);

        public VoxelPosition Extents { get; }

        
    }
}