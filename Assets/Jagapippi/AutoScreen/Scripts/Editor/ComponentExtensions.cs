﻿#if UNITY_EDITOR
using UnityEditor;

using UnityEngine;

namespace Jagapippi.AutoScreen
{
    public static class ComponentExtensions
    {
        public static bool IsInHierarchy(this Component component)
        {
            return component.gameObject.scene.IsValid();
        }

        public static bool IsInPrefabAsset(this Component component)
        {
            return AssetDatabase.Contains(component.transform.root.gameObject);
        }

        public static bool IsInPrefabStage(this Component component)
        {
            return UnityEditor.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage() != null && IsInHierarchy(component);
        }
    }
}
#endif