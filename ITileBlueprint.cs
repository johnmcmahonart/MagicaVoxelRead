using System.Collections.Generic;

namespace MagicaVoxelRead
{
    public interface ITileBlueprint
    {
        public int GetVoxData(VoxelPosition position);

        public VoxelPosition Extents { get; }

        
    }
}