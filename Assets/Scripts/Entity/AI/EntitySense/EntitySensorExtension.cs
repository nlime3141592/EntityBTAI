using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unchord
{
    public static class EntitySensorExtension
    {
        public static List<T> GetComponentsFromColliders<T>(this List<T> collection, Transform origin, Collider2D[] colliders, bool bCanDetectSelf, params string[] tags)
        where T : MonoBehaviour
        {
            bool contains = false;
            GameObject obj = null;
            T behaviour = null;

            collection.Clear(); // 넣을지 말지 고민하기

            for(int i = 0; i < colliders.Length; ++i)
            {
                obj = colliders[i].gameObject;
                contains = false;

                for(int j = 0; j < collection.Count && !contains; ++j)
                    contains = (obj == collection[j].gameObject);

                if(contains)
                    continue;
                else if(tags != null && !s_m_bCheckTag(obj, tags))
                    continue;
                else if(!bCanDetectSelf && obj == origin.gameObject)
                    continue;
                else if(obj.TryGetComponent<T>(out behaviour))
                    collection.Add(behaviour);
            }

            return collection;
        }

        public static List<Entity> FilterFromColliders(this List<Entity> entities, Entity origin, Collider2D[] colliders, bool bCanDetectSelf, List<Collider2D> ignores = null, params string[] tags)
        {
            bool contains = false;
            GameObject obj = null;
            Entity entity = null;

            entities.Clear(); // 넣을지 말지 고민하기.

            for(int i = 0; i < colliders.Length; ++i)
            {
                obj = colliders[i].gameObject;
                contains = false;

                if(ignores != null)
                    for(int j = 0; j < ignores.Count && !contains; ++j)
                        if(colliders[i] == ignores[j])
                            contains = true;

                for(int j = 0; j < entities.Count && !contains; ++j)
                    contains = (obj == entities[j].gameObject);

                if(contains)
                    continue;
                else if(tags != null && !s_m_bCheckTag(obj, tags))
                    continue;
                else if(!bCanDetectSelf && obj == origin.gameObject)
                    continue;
                else if(obj.TryGetComponent<Entity>(out entity))
                    entities.Add(entity);
            }

            return entities;
        }

        public static List<Entity> SetTargetCount(this List<Entity> entities, int count)
        {
            if(count < 1)
                count = 1;

            for(int i = entities.Count - 1; i >= count; --i)
                entities.RemoveAt(i);

            return entities;
        }

        private static bool s_m_bCheckTag(GameObject obj, string[] tags)
        {
            if(tags.Length == 0)
                return true;

            for(int i = 0; i < tags.Length; ++i)
                if(obj.tag == tags[i])
                    return true;

            return false;
        }
    }
}