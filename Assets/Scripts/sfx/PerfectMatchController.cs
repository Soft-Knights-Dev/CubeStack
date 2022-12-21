using System.Collections.Generic;
using System.Linq;
using Cubes;
using DG.Tweening;
using UnityEngine;

public class PerfectMatchGOInfo
{
    public GameObject GO;
    public Material Material;
}

public class PerfectMatchController : MonoBehaviour
{
    [SerializeField] private GameObject perfectGameObject;
    private Transform _transform;
    private Material _myMaterial;
    private static readonly int Alpha = Shader.PropertyToID("_alpha");
    private Tweener _tween;
    private List<PerfectMatchGOInfo> _perfectGos;
    
    private void Awake()
    {
        _perfectGos = new List<PerfectMatchGOInfo>();

        for (var i = 0; i < 3; i++)
            AddEffectGO();
    }

    public void DoEffect(CubeInfo lastCube)
    {
        var goInfo = _perfectGos.FirstOrDefault(x => !x.GO.activeSelf) ?? AddEffectGO();

        goInfo.Material.SetFloat(Alpha, 1);
        goInfo.GO.transform.position = lastCube.CubeTransform + Vector3.down * 0.05f;
        goInfo.GO.transform.localScale = new Vector3(lastCube.SideA - 0.2f, lastCube.SideB - 0.2f, 1);
        goInfo.GO.SetActive(true);
        
        // Moving animation
        var to = new Vector3(lastCube.SideA + 0.2f, lastCube.SideB + 0.2f, 1);
        var t = goInfo.GO.transform.DOScale(to, 0.5f);
        t.SetEase(Ease.OutQuart);
        t.onComplete += () => goInfo.GO.SetActive(false);
        
        // Alpha animation
        var alpha = DOTween.To(
            () => goInfo.Material.GetFloat(Alpha), 
            value => goInfo.Material.SetFloat(Alpha, value), 
            0, 
            0.5f);
        
        alpha.SetEase(Ease.InExpo);
    }

    private PerfectMatchGOInfo AddEffectGO()
    {
        var go = Instantiate(perfectGameObject);
        go.SetActive(false);
        var mat = go.GetComponent<MeshRenderer>().material;
        var newInfo = new PerfectMatchGOInfo() {GO = go, Material = mat};
        _perfectGos.Add(newInfo);
        return newInfo;
    }
    
}
