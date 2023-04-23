using System.Collections.Generic;
using UnityEngine;

namespace Unchord
{
    public static class ColliderExtension2D
    {
        public static void GetComponents<T_Component>(this List<Collider2D> _colliders, in List<T_Component> _collection)
        where T_Component : UnityEngine.Component
        {
            int count = _colliders.Count;
            T_Component comp;

            for(int i = 0; i < count; ++i)
            {
                comp = _colliders[i].gameObject.GetComponentInParent<T_Component>();

                if(!_collection.Contains(comp))
                    _collection.Add(comp);

                // if(_colliders[i].gameObject.TryGetComponent(out comp)) 
            }
        }
    }
}