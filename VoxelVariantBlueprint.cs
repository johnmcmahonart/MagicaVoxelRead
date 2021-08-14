using System;
using System.Collections.Generic;
using System.Text;

namespace MagicaVoxelRead
{
    public class VoxelVariantBlueprint : ITileBlueprint
    {
        private List<IVoxel> _voxels;
        
        public VoxelVariantBlueprint(List<IVoxel> _voxels, VoxelPosition _extents)
        {
            Extents = _extents;
            this._voxels = _voxels;
        }

        public VoxelPosition Extents { get; }

        public List<IVoxel> GetEdge(Direction edge)
        {
            throw new NotImplementedException();
        }

        public IVoxel GetVoxel(VoxelPosition _position)
        {
            return _voxels[_position.Y + _voxels.Count * (_position.X + _voxels.Count * _position.Z)];

            
        }
    }
}
