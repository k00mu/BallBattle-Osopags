//==================================================
//
//  Created by Khalish
//
//==================================================

using System;
using System.Collections.Generic;

namespace BallBattle.EventSystem
{
    /// <summary>
    /// To manage all game events
    /// </summary>
    public static class EventManager
    {
        private static readonly Dictionary<Type, Action<CustomEvent>> eventCollections = new Dictionary<Type, Action<CustomEvent>>();
        private static readonly Dictionary<Delegate, Action<CustomEvent>> eventLookups = new Dictionary<Delegate, Action<CustomEvent>>();



        //==================================================
        // Methods
        //==================================================
        /// <summary>
        /// Call this method in OnEnable() of the class that will listen to the event
        /// This is to register the event to the event manager
        /// </summary>
        /// <param name="_evt"></param>
        /// <typeparam name="T"></typeparam>
        public static void AddListener<T>(Action<T> _evt) where T : CustomEvent
        {
            if (!eventLookups.ContainsKey(_evt))
            {
                Action<CustomEvent> newAction = (e) => _evt((T)e);
                eventLookups[_evt] = newAction;

                if (eventCollections.TryGetValue(typeof(T), out Action<CustomEvent> existingAction))
                {
                    eventCollections[typeof(T)] = existingAction += newAction;
                }
                else
                {
                    eventCollections[typeof(T)] = newAction;
                }
            }
        }



        /// <summary>
        /// Call this method in OnDisable() of the class that will listen to the event
        /// This is to unregister the event from the event manager
        /// </summary>
        /// <param name="_evt"></param>
        /// <typeparam name="T"></typeparam>
        public static void RemoveListener<T>(Action<T> _evt) where T : CustomEvent
        {
            if (eventLookups.TryGetValue(_evt, out var action))
            {
                if (eventCollections.TryGetValue(typeof(T), out var existingAction))
                {
                    existingAction -= action;
                    if (existingAction == null)
                    {
                        eventCollections.Remove(typeof(T));
                    }
                    else
                    {
                        eventCollections[typeof(T)] = existingAction;
                    }
                }

                eventLookups.Remove(_evt);
            }
        }



        /// <summary>
        /// Call this method to broadcast the event
        /// This is to invoke the event from the event manager
        /// </summary>
        /// <param name="_evt"></param>
        public static void Broadcast(CustomEvent _evt)
        {
            if (eventCollections.TryGetValue(_evt.GetType(), out var action))
            {
                action.Invoke(_evt);
            }
        }
    }
}
