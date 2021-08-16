using System.Collections.Generic;

namespace MagicaVoxelRead
{
    public interface ITileBlueprint
    {
        IVoxel GetVoxel(VoxelPosition position);

        VoxelPosition Extents  { get; }

        
    }
}