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
            return _voxels[_getIndex(position)];
        }

        private int _getIndex(VoxelPosition position)
        {
            return (position.Z + (position.X * Extents.X)+(Extents.X*Extents.Z*position.Y));
        }

        //fill with voxel data
        public void Load()
        {
            _voxels = new List<IVoxel>();
            //the first item in the main.childrenchunks that is of type Xyzi contains the actual voxel data
            MagicavoxelVox.Xyzi voxMagicaData = (MagicavoxelVox.Xyzi)_voxelFromDisk.Main.ChildrenChunks.First(item => item.ChunkId == Kaitai.MagicavoxelVox.ChunkType.Xyzi).ChunkContent;

            
            for (int z = 0; z < Extents.Z; z++)
            {
                for (int x = 0; x < Extents.X; x++)
                {
                    for (int y = 0; y < Extents.Y; y++)
                    {
                        int foundVoxel = 0;
                        try
                        {
                            foundVoxel = (int)voxMagicaData.Voxels.First(item => item.X == x && item.Y == y && item.Z == z).ColorIndex;
                        }
                        catch
                        {
                            foundVoxel = -1;
                        }
                        _voxels.Add(new Voxel(new VoxelPosition(x,y,z),foundVoxel));
                        
                    
                    }
                }
            }
            //convert Z up to Y up coordinate space
            _ZuptoY();            
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
        private void _ZuptoY()
        //converts Z up used in MagicaVoxel to Y up used in other engines like godot
        //we need to swap the Y and Z position of the voxel, as well as change where in the list it is stored, so it matches _getindex
        {
            List<IVoxel> tempList = new List<IVoxel>(); ;
            
            foreach (var item in _voxels)
            {


                //VoxelPosition tpos = new VoxelPosition(item.Position.X, item.Position.Y, item.Position.Z);
                //int tempIndex = _getIndex(tpos);
                tempList.Add(new Voxel( new VoxelPosition(item.Position.X, item.Position.Z, item.Position.Y),item.Data));
                
            }
            this._voxels = tempList;
            
        }
    
    
    }
}