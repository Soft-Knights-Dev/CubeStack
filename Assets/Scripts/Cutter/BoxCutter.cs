using Cubes;

namespace Cutter
{
    public class BoxCutter
    {
        public CuttingDeltas CutCubes(CubeInfo baseCube, CubeInfo cubeToCut) =>
            new (baseCube, cubeToCut);

        public CubeInfo PerformCut(CuttingDeltas deltas, CubeInfo baseCube, CubeInfo cubeToCut)
        {
            var c = cubeToCut.Color;
            var fallP = deltas.FallingCubePos;
            var fallS = deltas.SizeCutCube;            
            var towerP = deltas.TowerCubePos;
            var towerS = deltas.SizeTowerCube;
            
            new CubeInfo(fallS.x, fallS.z, c, fallP.x, fallP.z, true);
            return new CubeInfo( towerS.x, towerS.z, c, towerP.x, towerP.z);
        }
    }
}