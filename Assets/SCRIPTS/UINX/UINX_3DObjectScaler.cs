using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class UINX_3DObjectScaler : MonoBehaviour
{
    private void Update()
    {
        UpdateGraphic();
    }

    public void UpdateGraphic()
    {
        var rectt = transform.parent.GetComponent<RectTransform>();
        if (rectt != null)
        {
            transform.localScale = new Vector3(rectt.rect.width * transform.parent.localScale.x, rectt.rect.height * transform.parent.localScale.y, transform.localScale.z);
            transform.localPosition = new Vector3(0, 0, -transform.localScale.z * 0.51f);
        } else {
            Debug.Log("UINX_3DObjectScaler has no RectTransform!! <" + name + ">");
        }
    }
}
