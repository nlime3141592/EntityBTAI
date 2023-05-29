namespace System
{
    public static partial class ObjectExtension
    {
        public static bool IsNull<T>(this Object obj)
        where T : class
        {
            if(object.ReferenceEquals(obj, null)) return true;
            if(obj is T) return (obj as T) == null;
            return false;
        }
    }
}