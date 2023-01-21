using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class ShowBoxColliderGizmos : MonoBehaviour
{
    [SerializeField] private Color32 m_Color = new Color(1,1,1,1);
    private void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = m_Color;
        Gizmos.DrawCube(GetComponent<BoxCollider>().center,GetComponent<BoxCollider>().size);
    }
}
