﻿using System.Collections;
using UnityEngine;

public class LazyCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Vector3 velocity = Vector3.zero;
    private float smoothTime = 0.3f;
    private float endPos = 10f;
    private float originSize;

    private void Awake()
    {
        if (!target)
        {
            target = GameObject.Find("Player").transform;
        }

        originSize = Camera.main.orthographicSize;
    }

    void Update()
    {
        UpdateCamera();
    }

    private void UpdateCamera()
    {
        //if (endPos == 0)
        //{
        //    try
        //    {
        //        GameObject[] gos = GameObject.FindGameObjectsWithTag("EndPosition");
        //        for(int i = 0; i< gos.Length; i++)
        //        {
        //            if (gos[i].activeSelf)
        //            {                        
        //                endPos = gos[i].transform.position.x - 6;
        //            }
        //        }
        //    }
        //    catch(NullReferenceException)
        //    {
        //        endPos = 10f;
        //    }
        //}
        Vector3 destination = new Vector3(Mathf.Clamp(target.position.x, -6, endPos), target.position.y + 0.5f, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, smoothTime);
    }

    public IEnumerator Shake(float amount, float duration)
    {
        float time = 0;
        while (time < duration)
        {
            transform.position += (Vector3)Random.insideUnitCircle * amount;
            time += Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator ZoomIn(float amount, float duration)
    {
        float time = 0;
        while (time < 1)
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, originSize / amount, time);
            time += Time.deltaTime;
            yield return null;
        }
        while (time < duration)
        {
            time += Time.deltaTime;
            yield return null;
        }
        time = 0;
        while (time < 1)
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, originSize, time);
            time += Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator ZoomOut(float amount, float duration)
    {
        float time = 0;
        while (time < 1)
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, originSize * amount, time);
            time += Time.deltaTime;
            yield return null;
        }
        while (time < duration)
        {
            time += Time.deltaTime;
            yield return null;
        }
        time = 0;
        while (time < 1)
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, originSize, time);
            time += Time.deltaTime;
            yield return null;
        }
    }

    public void ChangeTarget(Transform target)
    {
        this.target = target;
    }
}
