using System;
using System.Collections.Generic;
using System.Text;

namespace MagicaVoxelRead
{
    public class VoxelVariantBlueprint : ITileBlueprint
    {
        private Dictionary <VoxelPosition, int> _voxelData;
        public VoxelVariantBlueprint(List<IVoxel> _voxels, VoxelPosition _extents)
        {
            Extents = _extents;
            foreach (var item in _voxels )
            {
                _voxelData.Add(item.Position, item.Vid);
            }
        }

        public VoxelPosition Extents { get; }

        public List<IVoxel> GetEdge(Direction edge)
        {
            throw new NotImplementedException();
        }

        public int GetVoxel(VoxelPosition _position)
        {
            try
            {
                int foundVoxel = _voxelData[_position];
                return foundVoxel;
            }
            catch
            {
                return -1; //used to indicate voxel is empty aka air
            }
            
        }
    }
}
