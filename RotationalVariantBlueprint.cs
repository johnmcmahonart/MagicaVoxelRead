using System;
using System.Collections.Generic;
using System.Text;

namespace MagicaVoxelRead
{
    public class RotationalVariantBlueprint : ITileBlueprint
    {
        private List<IVoxel> _voxels = new List<IVoxel>() ;
        
        public RotationalVariantBlueprint(List<IVoxel> _voxels, VoxelPosition _extents, Direction _direction)
        {
            Extents = _extents;
            this._voxels = _voxels;
            _rotateMatrix(_direction);
        }

        private int _getIndex(VoxelPosition position)
        {
            return (position.Y + (position.X * Extents.X) + (Extents.X * Extents.Y * position.Z));
        }
        private void  _rotateMatrix(Direction _direction)
        //build rotational variant by performing matrix rotation
        //only works if X and Y are equal
        // https://stackoverflow.com/questions/42519/how-do-you-rotate-a-two-dimensional-array
        //todo
        //support other matrix rotations, right now this only works for +90
        {
            int size = Extents.X;
            IVoxel[] voxels = new IVoxel[size * size * size];
            int numLayers = (int)Math.Round((size / 2.0F), 0);

            //loop through each z layer and rotate each layer in 2d
            for (int iz = 0; iz < Extents.Z; iz++)
            {
                for (int iLayer = 0; iLayer < numLayers; iLayer++)
                {
                    int first = iLayer;
                    int last = size - first - 1;

                    for (int v = first; v < last; v++)
                    {
                        int offset = v - first;
                        VoxelPosition topPosition = new VoxelPosition(first, v, iz);
                        int topIndex = _getIndex(topPosition);

                        int topData = this._voxels[topIndex].Data;
                        voxels[topIndex] = new Voxel(topPosition, topData);

                        
                        VoxelPosition rightPosition = new VoxelPosition(v, last, iz);
                        int rightIndex = _getIndex(rightPosition);
                        int rightData = this._voxels[rightIndex].Data;
                        voxels[rightIndex] = new Voxel(rightPosition, rightData);

                        VoxelPosition bottomPosition = new VoxelPosition(last, last - offset, iz);
                        int bottomIndex = _getIndex(bottomPosition);
                        int bottomData = this._voxels[bottomIndex].Data;
                        voxels[bottomIndex] = new Voxel(bottomPosition, bottomData);

                        VoxelPosition leftPosition = new VoxelPosition(last - offset, first, iz);
                        int leftIndex = _getIndex(leftPosition);
                        int leftData = this._voxels[leftIndex].Data;
                        voxels[leftIndex] = new Voxel(leftPosition, leftData);

                        

                    }
                }
            }
            List<IVoxel>_voxels = new List<IVoxel>(voxels);
        }
        public VoxelPosition Extents { get; }

        public List<IVoxel> GetEdge(Direction edge)
        {
            throw new NotImplementedException();
        }

        public IVoxel GetVoxel(VoxelPosition _position)
        {
            return _voxels[_getIndex(_position)];

            
        }
    }
}
