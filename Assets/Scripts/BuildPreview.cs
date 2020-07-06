using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPreview
{
    //private GameObject turretPreviewPrefab;
    private RaycastHit hit;
    private Ray ray;
    public Vector3 previewPositionOffset = new Vector3 (0f,0.85f, 0f);
    private Vector3 previewRotationOffset = new Vector3 (0f,-90f,0f);

    public void turretBuildPreview(GameObject turretPreviewPrefab)
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            turretPreviewPrefab.transform.position = hit.point + previewPositionOffset;
            turretPreviewPrefab.transform.rotation = Quaternion.Euler(previewRotationOffset);
        }
    }
}
