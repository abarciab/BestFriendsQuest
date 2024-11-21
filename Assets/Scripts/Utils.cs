using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static void SetDirty(UnityEngine.Object obj)
    {
#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(obj);
#endif
    }

    public static string Vec2String(Vector2 v) => v.x + ":" + v.y;
}
