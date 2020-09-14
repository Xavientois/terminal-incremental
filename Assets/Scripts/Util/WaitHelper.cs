using UnityEngine;
using System.Collections;
using System;

public class WaitHelper : MonoBehaviour
{
    static public WaitHelper instance;

    private void Awake()
    {
        instance = this;
    }

    public static void WaitSeconds(float seconds, Action action)
    {
        instance.StartCoroutine(instance._wait(seconds, action));
    }

    IEnumerator _wait(float time, Action callback)
    {
        yield return new WaitForSeconds(time);
        callback();
    }
}