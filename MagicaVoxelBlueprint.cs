using Kaitai;
using System;
using System.Linq;

namespace MagicaVoxelRead
{
    public class MagicaVoxelBlueprint : ITileBlueprint
    {
        private MagicavoxelVox _voxelFromDisk;
        private string _filepath;
        private VoxelType[,,] _voxels;
        public VoxelPosition Extents { get; }

        public VoxelType GetVoxel(VoxelPosition position)
        {
            return _voxels[position.X, position.Y, position.Z];
        }

        public ITileBlueprint BuildVariant(Direction _direction)
        //build rotational variant by performing matrix rotation
        //only works if X and Y are equal

        //todo
        //support other matrix rotations, right now this only works for +90
        {
            VoxelType[,,] voxels = new VoxelType[Extents.X, Extents.Y, Extents.Z];
            int size = Extents.X;
            int numLayers = (int)MathF.Round((size / 2), 0);

            //loop through each z layer and rotate each layer in 2d
            for (int iz = 0; iz < Extents.Z + 1; iz++)
            {
                for (int iLayer = 0; iLayer < numLayers; iLayer++)
                {
                    int first = iLayer;
                    int last = size - first - 1;

                    for (int v = first; v < last; v++)
                    {
                        int offset = v - first;
                        VoxelPosition topPosition = new VoxelPosition(first, v, iz);
                        VoxelType topType = _voxels[topPosition.X, topPosition.Y, topPosition.Y];
                        voxels[topPosition.X, topPosition.Y, topPosition.Z] = topType;

                        VoxelPosition rightPosition = new VoxelPosition(v, last, iz);
                        VoxelType rightType = _voxels[rightPosition.X, rightPosition.Y, rightPosition.Z];
                        voxels[rightPosition.X, rightPosition.Y, rightPosition.Z] = rightType;

                        VoxelPosition bottomPosition = new VoxelPosition(last, last - offset, iz);
                        VoxelType bottomType = _voxels[bottomPosition.X, bottomPosition.Y, bottomPosition.Z];
                        voxels[bottomPosition.X, bottomPosition.Y, bottomPosition.Z] = bottomType;

                        VoxelPosition leftPosition = new VoxelPosition(last - offset, first, iz);
                        VoxelType leftType = _voxels[leftPosition.X, leftPosition.Y, leftPosition.Z];
                        voxels[leftPosition.X, leftPosition.Y, leftPosition.Z] = leftType;
                    }
                }
            }
            return new VoxelVariantBlueprint(voxels, Extents);
        }

        //fill with voxel data
        public void Fill()
        {
            _voxels = new VoxelType[Extents.X, Extents.Y, Extents.Z];
            //the first item in the main.childrenchunks that is of type Xyzi contains the actual voxel data
            MagicavoxelVox.Xyzi voxData = (MagicavoxelVox.Xyzi)_voxelFromDisk.Main.ChildrenChunks.First(item => item.ChunkId == Kaitai.MagicavoxelVox.ChunkType.Xyzi).ChunkContent;

            //magicavoxel voxel data is an unordered list, which only contains voxels that aren't empty
            //first init array to air, then fill cells with info from voxel file

            for (int z = 0; z < Extents.Z + 1; z++)
            {
                for (int x = 0; x < Extents.X + 1; x++)
                {
                    for (int y = 0; y < Extents.Y + 1; y++)
                    {
                        _voxels[x, y, z] = VoxelType.Air;
                    }
                }
            }

            //fill non empty cells
            foreach (var item in voxData.Voxels)
            {
                //todo convert magicavoxel color index to appropriate voxel type
                _voxels[item.X, item.Y, item.Z] = VoxelType.Grass;
            }
        }

        public MagicaVoxelBlueprint(string _path)
        {
            //load magicavoxel file from disk

            _filepath = _path;
            _voxelFromDisk = MagicavoxelVox.FromFile(_filepath);
            MagicavoxelVox.Size size = (MagicavoxelVox.Size)_voxelFromDisk.Main.ChildrenChunks.First(item => item.ChunkId == Kaitai.MagicavoxelVox.ChunkType.Size).ChunkContent;

            //get maximum bounds of the tile in voxels
            Extents = new VoxelPosition(Convert.ToInt32(size.SizeX), Convert.ToInt32(size.SizeY), Convert.ToInt32(size.SizeZ));
        }
    }
}