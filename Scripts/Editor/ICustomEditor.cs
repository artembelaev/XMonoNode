using UnityEditor;

namespace XMonoNodeEditor {
    public interface ICustomEditor<T> where T : class {
        T Target { get; set; }
        SerializedObject SerializedObject { get; set; }
    }
}
