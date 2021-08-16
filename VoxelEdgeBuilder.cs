
using System.Collections.Generic;
namespace MagicaVoxelRead
{
    public static class VoxelEdgeBuilder
    {

        public static List<IVoxel> BuildEdge(Direction _edge, ITileBlueprint _voxelData)
        {
            VoxelPosition position;
            List<IVoxel> edgeData = new List<IVoxel>();
            switch (_edge)
            {
                case Direction.North:
                    for (int i = 0; i < _voxelData.Extents.X; i++)
                    {
                        position = new VoxelPosition(i, 0, 0);
                        MakeEdgeVoxel(_voxelData, position, edgeData, i);
                    }




                    break;
                case Direction.South:
                    for (int i = 0; i < _voxelData.Extents.X; i++)
                    {
                        position = new VoxelPosition(i, _voxelData.Extents.Y, 0);
                        MakeEdgeVoxel(_voxelData, position, edgeData, i);
                        

                    }
                    break;
                case Direction.East:
                    for (int i = 0; i < _voxelData.Extents.Y; i++)
                    {
                        position = new VoxelPosition(_voxelData.Extents.X, i, 0);
                        MakeEdgeVoxel(_voxelData, position, edgeData, i);
                        

                    }
                    break;
                case Direction.West:
                    for (int i = 0; i < _voxelData.Extents.Y; i++)
                    {
                        position = new VoxelPosition(0, i, 0);
                        MakeEdgeVoxel(_voxelData, position, edgeData, i);
                        
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
        public static List<IVoxel> MakeEdgeVoxel(ITileBlueprint _voxelData, VoxelPosition position, List<IVoxel> edgeData, int i)
        {
            IVoxel voxel = _voxelData.GetVoxel(position);
            edgeData.Add(new Voxel(position, voxel.Data));
            return edgeData;
        }
    }
}