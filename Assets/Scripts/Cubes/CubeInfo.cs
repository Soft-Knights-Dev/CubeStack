using DG.Tweening;
using Pooler;
using UnityEngine;

namespace Cubes
{
    public class CubeInfo
    {
        public float SideA;
        public float SideB;
        private readonly GameObject _myCube;
        
        public Vector3 CubeTransform => _myCube.transform.position;
        public Color Color { get;}
        public GameObject Cube => _myCube;

        
        public CubeInfo(float sideA, float sideB, Color color, float posX, float posZ, bool disappear=false)
        {
            SideA = sideA;
            SideB = sideB;
            Color = color;

            _myCube = disappear ? CubePooler.Instance.GetDisappearingCube() : CubePooler.Instance.GetCube();
            _myCube.transform.localScale = new Vector3(sideA, 0.1f, sideB);
            
            _myCube.GetComponent<MeshRenderer>().material.color = color;
            _myCube.transform.position = new Vector3(posX, 0, posZ);
            _myCube.SetActive(true);
            _myCube.GetComponent<DisableCubeOnInvisible>().Configure(this);
        }

        public void SetPosition(Vector3 position) =>
            _myCube.transform.position = position;

        public void ScaleUp(float percetage)
        {
            SideA = Mathf.Clamp01(SideA + SideA * percetage);
            SideB = Mathf.Clamp01(SideB + SideB * percetage);

            _myCube.transform.DOScale(new Vector3(SideA, 0.1f, SideB), 0.3f);
        }
    }
}