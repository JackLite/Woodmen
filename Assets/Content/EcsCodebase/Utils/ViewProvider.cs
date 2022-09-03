using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Woodman.EcsCodebase.Utils
{
    public class ViewProvider : MonoBehaviour
    {
        [ContextMenu("Find References")]
        private void FindReferences()
        {
            var serializedFields = GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .Where(f => f.GetCustomAttribute<SerializeField>() != null);

            foreach (var field in serializedFields)
            {
                var val = FindObjectOfType(field.FieldType, true);
                if (val == null)
                    Debug.LogError($"Can't find component {field.FieldType} in scene");
                else
                    field.SetValue(this, val);
            }
        }
    }
}