using Kaitai;
using System;
using System.Linq;

namespace MagicaVoxelRead
{
    internal class Program
    {
        private static object x2;

        private static void Main(string[] args)
        {
            string path = @"G:\Still images\Digital art - Original sources\OneDrive - John McMahon Art\Projects\Untitled aRPG\aRPG\v2 - Godot\Project Files\Art\Voxel\GrasswDirt10x10.vox";
            var voxAData = MagicavoxelVox.FromFile(path);
            //var voxBData = MagicavoxelVox.FromFile("G:/Still images/Digital art - Original sources/OneDrive - John McMahon Art/Projects/Untitled aRPG/aRPG/v2 - Godot/VoxelSource/GrasswDirt10x10.vox");
            MagicavoxelVox.Size sizeofA = (MagicavoxelVox.Size)voxAData.Main.ChildrenChunks.First(item => item.ChunkId == Kaitai.MagicavoxelVox.ChunkType.Size).ChunkContent;
            //Console.WriteLine("X size is {0}", sizeofA.SizeX);

            /*
            for (int i = 0; i < voxAData.Main.ChildrenChunks.Where(item=> item.ChunkId==Size);)
            {
            }
        */
            MagicaVoxelBlueprint bp = new MagicaVoxelBlueprint(path);
            bp.Load();
            for (int z = 0; z < bp.Extents.Z; z++)
            {
                for (int x = 0; x < bp.Extents.X; x++)
                {
                    for (int y = 0; y < bp.Extents.Y; y++)
                    {
                        Voxel vox = (Voxel)bp.GetVoxel(new VoxelPosition(x,y,z));
                        //Console.WriteLine("X:{0} Y:{2} Z:{3}", x, y, z);
                        //Console.WriteLine("XV:{0} YV:{2} ZV:{3}", vox.Position.X, vox.Position.Y, vox.Position.Z);
                    }
                }
            }
        }
    }
}