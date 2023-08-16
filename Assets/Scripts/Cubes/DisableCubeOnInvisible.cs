using GameLogic;
using UnityEngine;
using UnityEngine.Assertions;


namespace Cubes
{
    public class DisableCubeOnInvisible : MonoBehaviour
    {
        private CubeInfo _info;
        
        public void Configure(CubeInfo cubeInfo) =>
            _info = cubeInfo;
        
        private void OnBecameInvisible()
        {
            Assert.IsTrue(_info != null, "Cube info cannot be null");

            if (GameManager.Instance != null)
            {
                _info.Cube.SetActive(false);
                GameManager.Instance.RemoveCube(_info);
                transform.parent = null;
            }
        }
    }
}