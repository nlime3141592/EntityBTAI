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

        public static void GetComponents<T_Component>(this List<Collider2D> _colliders, in List<T_Component> _collection, List<string> _tags)
        where T_Component : UnityEngine.Component
        {
            int count = _colliders.Count;
            T_Component comp;

            for(int i = 0; i < count; ++i)
            {
                if(
                    _tags.Contains(_colliders[i].gameObject.tag) &&
                    _colliders[i].gameObject.TryGetComponent(out comp) &&
                    !_collection.Contains(comp)
                )
                {
                    _collection.Add(comp);
                }
            }
        }

        public static List<Collider2D> IgnoreCollider(this List<Collider2D> _colliders, Collider2D _col)
        {
            while(_colliders.Contains(_col))
                _colliders.Remove(_col);

            return _colliders;
        }

        public static List<Collider2D> IgnoreColliders(this List<Collider2D> _colliders, List<Collider2D> _cols)
        {
            for(int i = 0; i < _cols.Count; ++i)
                _colliders.IgnoreCollider(_cols[i]);

            return _colliders;
        }
    }
}