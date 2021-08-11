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
            var voxAData = MagicavoxelVox.FromFile("G:/Still images/Digital art - Original sources/OneDrive - John McMahon Art/Projects/Untitled aRPG/aRPG/v2 - Godot/VoxelSource/Tree10x10.vox");
            var voxBData = MagicavoxelVox.FromFile("G:/Still images/Digital art - Original sources/OneDrive - John McMahon Art/Projects/Untitled aRPG/aRPG/v2 - Godot/VoxelSource/GrasswDirt10x10.vox");
            MagicavoxelVox.Size sizeofA = (MagicavoxelVox.Size)voxAData.Main.ChildrenChunks.First(item => item.ChunkId == Kaitai.MagicavoxelVox.ChunkType.Size).ChunkContent;
            Console.WriteLine("X size is {0}", sizeofA.SizeX);

            /*
            for (int i = 0; i < voxAData.Main.ChildrenChunks.Where(item=> item.ChunkId==Size);)
            {
            }
        */
        }
    }
}