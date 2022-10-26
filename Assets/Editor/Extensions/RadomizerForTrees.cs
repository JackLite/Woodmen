using UnityEditor;
using UnityEngine;

namespace Woodman.Editor.Extensions
{
    public class RadomizerForTrees
    {
        [MenuItem("Woodman/Randomize Trees")]
        public static void Randomize()
        {
            if (Selection.count == 0)
            {
                Debug.LogError("You must select objects before randomize");
                return;
            }

            var r = Random.Range(0, Selection.count);
            for (int i = 0; i < Selection.count; i++)
            {
                    var oldRotation = Selection.transforms[i].rotation.eulerAngles;
                    Selection.transforms[i].rotation = Quaternion.Euler(
                        oldRotation.x,
                        Random.Range(-180f, 180f),
                        oldRotation.z
                    );
                EditorUtility.SetDirty(Selection.gameObjects[i]);
            }
        }
    }
}