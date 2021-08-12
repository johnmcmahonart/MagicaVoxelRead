
using System.Collections.Generic;
namespace MagicaVoxelRead
{
    public static class VoxelEdgeBuilder
    {

        public static List<IVoxel> BuildEdge(Direction _edge, ITileBlueprint _voxelData)
        {
            VoxelPosition position;
            VoxelType type;
            List<IVoxel> edgeData = new List<IVoxel>();
            switch (_edge)
            {
                case Direction.North:
                    for (int i = 0; i < _voxelData.Extents.X + 1; i++)
                    {
                        position = new VoxelPosition(i, 0, 0);
                        type = _voxelData.GetVoxel(position);
                        edgeData.Add(new Voxel(position, type));
                    }




                    break;
                case Direction.South:
                    for (int i = 0; i < _voxelData.Extents.X + 1; i++)
                    {
                        position = new VoxelPosition(i, _voxelData.Extents.Y, 0);
                        type = _voxelData.GetVoxel(position);
                        edgeData.Add(new Voxel(position, type));

                    }
                    break;
                case Direction.East:
                    for (int i = 0; i < _voxelData.Extents.Y + 1; i++)
                    {
                        position = new VoxelPosition(_voxelData.Extents.X, i, 0);
                        type = _voxelData.GetVoxel(position);
                        edgeData.Add(new Voxel(position, type));

                    }
                    break;
                case Direction.West:
                    for (int i = 0; i < _voxelData.Extents.Y + 1; i++)
                    {
                        position = new VoxelPosition(0, i, 0);
                        type = _voxelData.GetVoxel(position);
                        edgeData.Add(new Voxel(position, type));
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
    }
}