using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatCameraCollision : MonoBehaviour
{
    [SerializeField] private float m_f_minDistance = 0.2f;
    [SerializeField] private float m_f_maxDistance = 2.9f;

    [SerializeField] private float m_f_smoothness = 5.0f;

    private Vector3 m_dollyDirection;
    private Vector3 m_dollyDirAdjusted;
    private float m_f_distance;

    private void Awake()
    {
        m_dollyDirection = transform.localPosition.normalized;
        m_f_distance = transform.localPosition.magnitude;
    }

    private void Update()
    {
        Vector3 desiredPos = transform.parent.TransformPoint(m_dollyDirection * m_f_maxDistance);
        RaycastHit hit;

        if(Physics.Linecast(transform.parent.position, desiredPos, out hit))
        {
            m_f_distance = Mathf.Clamp(hit.distance * 0.9f, m_f_minDistance, m_f_maxDistance);

        }
        else
        {
            m_f_distance = m_f_maxDistance;
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, m_dollyDirection * m_f_distance, Time.deltaTime * m_f_smoothness);
    }
}
