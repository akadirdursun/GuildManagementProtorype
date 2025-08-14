using UnityEngine;

namespace AdventurerVillage.CustomScriptableObjectSystem
{
    public static class CustomScriptableObjectInitializer
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        public static void OnGameStart()
        {
#if UNITY_EDITOR
            //unity does not automatically load all assets in the resources. Might be good to look for better solution later
            Resources.LoadAll<ScriptableObject>(string.Empty);
#endif
            var customScriptableObjectArray =
                Resources.FindObjectsOfTypeAll(typeof(CustomScriptableObject)) as CustomScriptableObject[];
            if (customScriptableObjectArray == null)
            {
                Debug.LogError("Custom Scriptable Object Array is null!");
                return;
            }

            foreach (var customScriptableObject in customScriptableObjectArray)
            {
                customScriptableObject.OnGameStarted();
            }
        }
    }
}