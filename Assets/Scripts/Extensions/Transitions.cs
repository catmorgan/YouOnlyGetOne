using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Transitions
{
    public static IEnumerator MoveTo(Transform transform, Vector3 position,
        bool local, float time, Action transitionEndCallback = null)
    {
        var startPosition = local ? transform.localPosition : transform.position;

        for (var elapsed = 0.0f; elapsed < time; elapsed += Time.deltaTime)
        {
            if (local)
                transform.localPosition = Vector3.Lerp(startPosition, position, elapsed / time);
            else
                transform.position = Vector3.Lerp(startPosition, position, elapsed / time);

            yield return null;
        }

        if (local)
            transform.localPosition = position;
        else
            transform.position = position;
        
        if (transitionEndCallback != null)
            transitionEndCallback();
    }

    public static IEnumerator RotateTo(Transform transform, Quaternion rotation,
        bool local, float time, Action transitionEndCallback = null)
    {
        var startRotation = local ? transform.localRotation : transform.rotation;

        for (var elapsed = 0.0f; elapsed < time; elapsed += Time.deltaTime)
        {
            if (local)
                transform.localRotation = Quaternion.Slerp(startRotation, rotation, elapsed / time);
            else
                transform.rotation = Quaternion.Slerp(startRotation, rotation, elapsed / time);

            yield return null;
        }

        if (local)
            transform.localRotation = rotation;
        else
            transform.rotation = rotation;
        
        if (transitionEndCallback != null)
            transitionEndCallback();
    }

    public static IEnumerator TransformTo(Transform transform, Vector3 position, Quaternion rotation,
        bool local, float time, Action transitionEndCallback = null)
    {
        var startPosition = local ? transform.localPosition : transform.position;
        var startRotation = local ? transform.localRotation : transform.rotation;

        for (var elapsed = 0.0f; elapsed < time; elapsed += Time.deltaTime)
        {
            if (local)
            {
                transform.localPosition = Vector3.Lerp(startPosition, position, elapsed / time);
                transform.localRotation = Quaternion.Slerp(startRotation, rotation, elapsed / time);
            }
            else
            {
                transform.position = Vector3.Lerp(startPosition, position, elapsed / time);
                transform.rotation = Quaternion.Slerp(startRotation, rotation, elapsed / time);
            }

            yield return null;
        }

        if (local)
        {
            transform.localPosition = position;
            transform.localRotation = rotation;
        }
        else
        {
            transform.position = position;
            transform.rotation = rotation;
        }


        if (transitionEndCallback != null)
        {
            transitionEndCallback();
        }
    }
}
