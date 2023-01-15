namespace UnchordMetroidvania
{
    public static class ObjectExtension_001
    {
        public static bool IsNull<T>(this System.Object obj)
        where T : class
        {
            if(object.ReferenceEquals(obj, null)) return true;
            if(obj is T) return (obj as T) == null;
            return false;
        }
    }
}