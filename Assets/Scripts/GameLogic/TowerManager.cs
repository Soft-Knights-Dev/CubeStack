using System.Collections.Generic;
using Cubes;
using UnityEngine;


namespace GameLogic
{
    public class TowerManager
    {
        private readonly List<CubeInfo> _tower;
        public int NumCubes => _tower.Count;
        public CubeInfo LastCube => _tower[^1];
        
        public TowerManager() =>
            _tower = new List<CubeInfo>();
        
        public void MoveTowerDown()
        {
            foreach (var cube in _tower)
                cube.SetPosition(cube.CubeTransform + Vector3.down * 0.1f);
        }
        
        public void AddToTower(CubeInfo info)
        {
            if(!_tower.Contains(info))
                _tower.Add(info);
        }
        
        public void DeleteFromTower(CubeInfo info)
        {
            if (_tower.Contains(info))
                _tower.Remove(info);
        }

        public void Reset() =>
            _tower.Clear();
        
    }
}