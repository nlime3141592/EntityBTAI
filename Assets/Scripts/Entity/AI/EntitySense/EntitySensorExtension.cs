using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnchordMetroidvania
{
    public static class EntitySensorExtension
    {
        public static List<EntityBase> FilterFromColliders(this List<EntityBase> entities, EntityBase origin, Collider2D[] colliders, bool bCanDetectSelf, params string[] tags)
        {
            bool contains = false;
            GameObject obj = null;
            EntityBase entity = null;

            for(int i = 0; i < colliders.Length; ++i)
            {
                obj = colliders[i].gameObject;
                contains = false;

                for(int j = 0; j < entities.Count && !contains; ++j)
                    contains = (obj == entities[j].gameObject);

                if(contains)
                    continue;
                else if(tags != null && !s_m_bCheckTag(obj, tags))
                    continue;
                else if(!bCanDetectSelf && obj == origin.gameObject)
                    continue;
                else if(obj.TryGetComponent<EntityBase>(out entity))
                    entities.Add(entity);
            }

            return entities;
        }

        public static List<EntityBase> SetTargetCount(this List<EntityBase> entities, int count)
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