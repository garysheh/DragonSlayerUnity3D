using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethod
{
    private const float threshold = 0.1f;
    public static bool IsTargetInfront(this Transform transform, Transform target)
    {
        var vectorToTarget = target.position - transform.position;
        vectorToTarget.Normalize();

        float dotProduct = Vector3.Dot(transform.forward, vectorToTarget);

        return dotProduct >= threshold;
    }
}
