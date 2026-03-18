using System.Collections;
using UnityEngine;

public class RiseUpObject : MonoBehaviour
{
    [Header("Rise Settings")]
    public float riseDistance = 5f; 
    public float riseSpeed = 2f;    

    private Vector3 finalPosition;
    private Vector3 startPosition;

    void Awake()
    {
        finalPosition = transform.position;
      
        startPosition = finalPosition - new Vector3(0, riseDistance, 0);
    }

    void OnEnable()
    {
      
        transform.position = startPosition;
       
        StartCoroutine(RiseRoutine());
    }

    IEnumerator RiseRoutine()
    {
       
        while (Vector3.Distance(transform.position, finalPosition) > 0.01f)
        {
          
            transform.position = Vector3.MoveTowards(transform.position, finalPosition, riseSpeed * Time.deltaTime);
            yield return null;
        }

       
        transform.position = finalPosition;
    }
}