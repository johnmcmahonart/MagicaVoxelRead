using System;
using System.Collections.Generic;
using System.Text;

namespace MagicaVoxelRead
{
    public class VoxelVariantBlueprint : ITileBlueprint
    {
        private VoxelType[,,] _voxelData;
        
        public VoxelVariantBlueprint(VoxelType[,,] _voxels, VoxelPosition _extents)
        {
            Extents = _extents;
            _voxelData = _voxels;
        }

        public VoxelPosition Extents { get; }

        public List<IVoxel> GetEdge(Direction edge)
        {
            throw new NotImplementedException();
        }

        public VoxelType GetVoxel(VoxelPosition _position)
        {
            return _voxelData[_position.X, _position.Y, _position.Z];
            
        }
    }
}
