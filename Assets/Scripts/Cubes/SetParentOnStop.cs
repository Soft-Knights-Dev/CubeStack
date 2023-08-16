using UnityEngine;

public class SetParentOnStop : MonoBehaviour
{
    private Rigidbody _rb;
    private SetParentOnStop _thisC;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _thisC = this;
    }

    private void Update()
    {
        if (_rb != null && _rb.velocity == Vector3.zero)
        {
            var tr = transform;
            var scale = tr.localScale;
            var hits = Physics.OverlapBox(tr.position, tr.localScale * 1.1f);

            if (hits.Length > 0)
            {
                var parentScale = hits[0].transform.localScale;
                transform.SetParent(hits[0].transform, true);
                transform.localScale = new Vector3(1 /parentScale.x, 1 /parentScale.y, 1 /parentScale.z );
                _thisC.enabled = false;
            }
        }
    }

    private void OnDisable()
    {
        _thisC.enabled = true;
    }
}
