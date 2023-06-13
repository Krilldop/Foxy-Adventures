using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] Transform alvo;
    private Vector3 offset;
    void Start()
    {
        offset = transform.position - alvo.position;
    }

    void LateUpdate()
    {
        Vector3 novaPosicao = new Vector3(transform.position.x, transform.position.y, offset.z + alvo.position.z);
        transform.position = Vector3.Lerp(transform.position, novaPosicao, 10 * Time.deltaTime);
    }
}
