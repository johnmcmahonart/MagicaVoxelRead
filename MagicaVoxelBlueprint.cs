using Kaitai;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MagicaVoxelRead
{
    public class MagicaVoxelBlueprint : ITileBlueprint
    {
        private MagicavoxelVox _voxelFromDisk;
        private string _filepath;
        private Dictionary<VoxelPosition, int> _voxels;
        public VoxelPosition Extents { get; }

        public List<IVoxel> GetEdge(Direction edge)
        {
            List<IVoxel> edgeData = new List<IVoxel>();
            switch (edge)
            {
                case Direction.North:
                    for (int i = 0; i < Extents.X+1; i++)
                    {
                        VoxelPosition position = new VoxelPosition(i, 0, 0);
                        try
                        {
                            int id = GetVoxel(position);
                            edgeData.Add(new Voxel(position, id));
                        }
                        catch 
                        {

                            
                        }
                    } 
                        
                    
                    
                    break;
                case Direction.South:
                    for (int i = 0; i < Extents.X + 1; i++)
                    {
                        VoxelPosition position = new VoxelPosition(i, Extents.Y, 0);
                        try
                        {
                            int id = GetVoxel(position);
                            edgeData.Add(new Voxel(position, id));
                        }
                        catch
                        {


                        }
                    }
                    break;
                case Direction.East:
                    for (int i = 0; i < Extents.Y + 1; i++)
                    {
                        VoxelPosition position = new VoxelPosition(Extents.X, i, 0);
                        try
                        {
                            int id = GetVoxel(position);
                            edgeData.Add(new Voxel(position, id));
                        }
                        catch
                        {


                        }
                    }
                    break;
                case Direction.West:
                    for (int i = 0; i < Extents.Y + 1; i++)
                    {
                        VoxelPosition position = new VoxelPosition(0, i, 0);
                        try
                        {
                            int id = GetVoxel(position);
                            edgeData.Add(new Voxel(position, id));
                        }
                        catch
                        {


                        }
                    }
                    break;
                case Direction.Top:
                    break;
                case Direction.Bottom:
                    break;
                default:
                    break;
            }
            return edgeData;
        }

        public int GetVoxel(VoxelPosition position)
        {
            try
            {
                int foundVoxel = _voxels[position];
                return foundVoxel;
            }
            catch 
            {
                return -1;
            
            }
        }

        public ITileBlueprint BuildVariant(Direction _direction)
        //build rotational variant by performing matrix rotation
        //only works if X and Y are equal
        
        //todo
        //support other matrix rotations, right now this only works for +90
        {
            List<IVoxel> voxels = new List<IVoxel>();
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
                        try
                        {
                            int topID = _voxels[topPosition];
                            voxels.Add(new Voxel(topPosition, topID));
                        }
                        catch
                        {
                            //int topID = -1;
                            //if the dictionary doesn't contain the position it means the location in the voxel is empty aka air
                            //we also don't need to add it to the blueprint
                        }

                        VoxelPosition rightPosition = new VoxelPosition(v, last, iz);
                        try
                        {
                            int rightID = _voxels[rightPosition];
                            voxels.Add(new Voxel(rightPosition, rightID));
                        }
                        catch
                        {
                            //do nothing
                        }
                        VoxelPosition bottomPosition = new VoxelPosition(last, last - offset, iz);
                        try
                        {
                            int bottomID = _voxels[bottomPosition];
                            voxels.Add(new Voxel(bottomPosition, bottomID));
                        }
                        catch
                        {
                        }
                        VoxelPosition leftPosition = new VoxelPosition(last - offset, first, iz);
                        try
                        {
                            int leftID = _voxels[leftPosition];
                            voxels.Add(new Voxel(leftPosition, leftID));
                        }
                        catch
                        {
                        }
                    }
                }
            }
            return new VoxelVariantBlueprint(voxels, Extents);
        }

        //fill with voxel data
        public void Fill()
        {
            //the first item in the main.childrenchunks that is of type Xyzi contains the actual voxel data
            MagicavoxelVox.Xyzi voxData = (MagicavoxelVox.Xyzi)_voxelFromDisk.Main.ChildrenChunks.First(item => item.ChunkId == Kaitai.MagicavoxelVox.ChunkType.Xyzi).ChunkContent;

            foreach (var voxel in voxData.Voxels)
            {
                VoxelPosition position = new VoxelPosition(voxel.X, voxel.Y, voxel.Z);
                _voxels.Add(position, voxel.ColorIndex);
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