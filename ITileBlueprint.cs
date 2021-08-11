using System.Collections.Generic;

namespace MagicaVoxelRead
{
    public interface ITileBlueprint
    {
        public int GetVoxel(VoxelPosition position);

        public VoxelPosition Extents { get; }

        public List<IVoxel> GetEdge(Direction edge);
    }
}