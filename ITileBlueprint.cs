using System.Collections.Generic;

namespace MagicaVoxelRead
{
    public interface ITileBlueprint
    {
        public VoxelType GetVoxel(VoxelPosition position);

        public VoxelPosition Extents { get; }

        
    }
}