using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Pooler
{
    public class CubePooler : MonoSingleton<CubePooler>
    {
        [SerializeField] private GameObject cubePrefab;
        [SerializeField] private GameObject disappearingCubePrefab;
        [SerializeField] private int numInstances;
        [SerializeField] private int numDisappearingInstances;
        
        private List<GameObject> _cubes;
        private List<GameObject> _dispCubes;

        protected override void Awake()
        {
            base.Awake();
            
            _cubes = new List<GameObject>();
            _dispCubes = new List<GameObject>();
            
            for (var i = 0; i < numInstances; i++)
                _cubes.Add(InstantiateCube());
            
            for (var i = 0; i < numDisappearingInstances; i++)
                _cubes.Add(InstantiateDisappearingCube());
        }

        public GameObject GetCube()
        {
            foreach (var cube in _cubes)
            {
                if (!cube.activeSelf)
                {
                    cube.transform.localScale = Vector3.one;
                    cube.transform.rotation = Quaternion.identity;
                    return cube;
                }
            }
                
            var newCube = InstantiateCube();
            _cubes.Add(newCube);
            
            return newCube;
        } 
        public GameObject GetDisappearingCube()
        {
            for (var index = 0; index < _dispCubes.Count; index++)
            {
                var cube = _dispCubes[index];
                
                if (!cube.activeSelf)
                {
                    cube.transform.localScale = Vector3.one;
                    cube.transform.rotation = Quaternion.identity;
                    cube.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    return cube;
                }
            }
            
            var newCube = InstantiateDisappearingCube();
            _dispCubes.Add(newCube);
            return newCube;
        }
        
        private GameObject InstantiateCube()
        {
            var go = Instantiate(cubePrefab);
            go.SetActive(false);
            
            return go;
        }  
        
        private GameObject InstantiateDisappearingCube()
        {
            var go = Instantiate(disappearingCubePrefab);
            go.SetActive(false);
            
            return go;
        }

        public void Reset()
        {
            foreach (var c in _cubes)
                c.SetActive(false);
            
            foreach (var c in _dispCubes)
                c.SetActive(false);
        }
        
    }
}