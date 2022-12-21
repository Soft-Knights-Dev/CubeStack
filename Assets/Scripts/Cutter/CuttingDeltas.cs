using Cubes;
using UnityEngine;
using Utils;

namespace Cutter
{
    public class CuttingDeltas
    {
        public readonly Vector3 CenterDifference;
        public readonly Vector3 CutDirection;
        public readonly Vector3 FallingCubePos;
        public readonly Vector3 TowerCubePos;
        public readonly Vector3 SizeCutCube;
        public readonly Vector3 SizeTowerCube;
        
        private readonly Vector3 _cutPosition;
        
        public CuttingDeltas(CubeInfo baseCube, CubeInfo cuttingCube)
        {
            var tBase = baseCube.CubeTransform;
            var tCut = cuttingCube.CubeTransform;
            var cDif = tCut - tBase;
            
            CenterDifference = cDif.AbsVector();
            CutDirection = new Vector3(
                cDif.x == 0 ? 0: Mathf.Sign(cDif.x),
                0,
                cDif.z == 0 ? 0: Mathf.Sign(cDif.z)
                );
            
            var totalSize = new Vector3(cuttingCube.SideA, 0, cuttingCube.SideB);
            var totalTowerCubeSize = new Vector3(baseCube.SideA, 0, baseCube.SideB);
            
            SizeTowerCube = totalSize - CenterDifference;
            SizeCutCube = totalSize - SizeTowerCube.MultiplyVector(CutDirection.AbsVector());
            
            _cutPosition = tBase + (totalTowerCubeSize / 2f).MultiplyVector(CutDirection);
            FallingCubePos = _cutPosition + (SizeCutCube / 2f).MultiplyVector(CutDirection);
            TowerCubePos = _cutPosition - (SizeTowerCube / 2f).MultiplyVector(CutDirection);
        }

        public void Print()
        {
            Debug.Log("-----------------------------------");
            Debug.Log($"Center difference: {CenterDifference.x}/{CenterDifference.y}/{CenterDifference.z}");
            Debug.Log($"CutDirection: {CutDirection.x}/{CutDirection.z}");
            Debug.Log($"CutPosition: {_cutPosition.x}/{_cutPosition.z}");
            Debug.Log($"SizeCut: {SizeCutCube.x}/{SizeCutCube.z}");
            Debug.Log($"SizeTower: {SizeTowerCube.x}/{SizeTowerCube.z}");
            Debug.Log($"CutPosition: {FallingCubePos.x}/{FallingCubePos.y}/{FallingCubePos.z}");
            Debug.Log($"TowerCubePosition: {TowerCubePos.x}/{TowerCubePos.y}/{TowerCubePos.z}");
            Debug.Log("-----------------------------------");
        }
    }
}