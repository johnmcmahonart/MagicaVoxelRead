using Kaitai;
using System;
using System.Linq;
using System.Collections.Generic;
namespace MagicaVoxelRead
{
    public class MagicaVoxelBlueprint : ITileBlueprint
    {
        private MagicavoxelVox _voxelFromDisk;
        private string _filepath;
        private List<IVoxel> _voxels;
        public VoxelPosition Extents { get; }

        public IVoxel GetVoxel(VoxelPosition position)
        {
            return _voxels[position.Y + _voxels.Count * (position.X + _voxels.Count * position.Z)];
        }

        public ITileBlueprint BuildVariant(Direction _direction)
        //build rotational variant by performing matrix rotation
        //only works if X and Y are equal
        // https://stackoverflow.com/questions/42519/how-do-you-rotate-a-two-dimensional-array
        //todo
        //support other matrix rotations, right now this only works for +90
        {
            
            int size = Extents.X;
            IVoxel[] voxels = new IVoxel[size*size*size];
            int numLayers = (int)MathF.Round((size / 2), 0);

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
                        int topIndex = topPosition.Y + _voxels.Count * (topPosition.X + _voxels.Count * topPosition.Z);
                        
                        int topData = _voxels[topIndex].Data;
                        voxels[topIndex]=new Voxel(topPosition,topData);
                        
                        //new voxels start as empty, so we need to set it to solid if it is solid in the original
                        if (_voxels[topIndex].IsSolid)
                        {
                            voxels[topIndex].ToggleSolid();
                        }

                        VoxelPosition rightPosition = new VoxelPosition(v, last, iz);
                        int rightIndex = rightPosition.Y + _voxels.Count * (rightPosition.X + _voxels.Count * rightPosition.Z);
                        int rightData = _voxels[rightIndex].Data;
                        voxels[rightIndex] = new Voxel(rightPosition,rightData);

                        if (_voxels[rightIndex].IsSolid)
                        {
                            voxels[rightIndex].ToggleSolid();
                        }
                        VoxelPosition bottomPosition = new VoxelPosition(last, last - offset, iz);
                        int bottomIndex = bottomPosition.Y + _voxels.Count * (bottomPosition.X + _voxels.Count * bottomPosition.Z);
                        int bottomData = _voxels[bottomIndex].Data;
                        voxels[bottomIndex] = new Voxel(bottomPosition,bottomData);

                        if (_voxels[bottomIndex].IsSolid)
                        {
                            voxels[bottomIndex].ToggleSolid();
                        }
                        VoxelPosition leftPosition = new VoxelPosition(last - offset, first, iz);
                        int leftIndex = leftPosition.Y + _voxels.Count * (leftPosition.X + _voxels.Count * leftPosition.Z);
                        int leftData = _voxels[leftIndex].Data;
                        voxels[leftIndex] = new Voxel(leftPosition,leftData);
                        
                        if (_voxels[leftIndex].IsSolid)
                        {
                            voxels[leftIndex].ToggleSolid();
                        }

                    }
                }
            }
            return new VoxelVariantBlueprint(voxels.ToList(), Extents);
        }

        //fill with voxel data
        public void Load()
        {
            _voxels = new List<IVoxel>();
            //the first item in the main.childrenchunks that is of type Xyzi contains the actual voxel data
            MagicavoxelVox.Xyzi voxMagicaData = (MagicavoxelVox.Xyzi)_voxelFromDisk.Main.ChildrenChunks.First(item => item.ChunkId == Kaitai.MagicavoxelVox.ChunkType.Xyzi).ChunkContent;

            //magicavoxel voxel data is an unordered list, which only contains voxels that aren't empty
            //so we need to check if a voxel is assigned a colorindex and if it is, make it solid

            for (int z = 0; z < Extents.Z; z++)
            {
                for (int x = 0; x < Extents.X; x++)
                {
                    for (int y = 0; y < Extents.Y; y++)
                    {
                        int foundVoxel = (int)voxMagicaData.Voxels.First(item => item.X == x && item.Y == y && item.Z == z).ColorIndex;
                        _voxels.Add(new Voxel(new VoxelPosition(x,y,z),foundVoxel));
                        
                    
                    }
                }
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
        public void ZuptoY()
        //converts Z up used in MagicaVoxel to Y up used in other engines like godot
        
        {
            int i = 0;
            foreach (var item in _voxels)
            {
                _voxels[i] = new Voxel( new VoxelPosition(item.Position.X, item.Position.Z, item.Position.Y),item.Data);
                i++;
            }
        }
    
    
    }
}