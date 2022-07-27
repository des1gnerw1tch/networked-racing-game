using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform target;

    private void Start()
    {
        target = Camera.main.transform;
    }
    
    private void Update()
    {
        transform.LookAt(target);
    }
}
