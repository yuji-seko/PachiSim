using System;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

namespace Framework.Message
{
    public abstract class MessageEventBase<T> : IDisposable where T : UnityEventBase, new()
    {
        readonly protected UnityEventBase m_event = null;

        protected T Event => ( T )m_event;

        protected MessageEventBase()
        {
            m_event = new T();
        }

        public void Dispose()
        {
            m_event.RemoveAllListeners();
        }

        void IDisposable.Dispose()
        {
            m_event.RemoveAllListeners();
        }

        public void AddTo( Component component )
        {
            DisposableExtensions.AddTo( this, component );
        }
    }

    public class MessageEvent : MessageEventBase<UnityEvent>
    {
        public void Invoke()
        {
            Event.Invoke();
        }

        public MessageEvent Subcrive( UnityAction action )
        {
            Event.AddListener( action );
            return this;
        }
    }

    public class MessageEvent<T> : MessageEventBase<UnityEvent<T>>
    {
        public void Invoke( T arg )
        {
            Event.Invoke( arg );
        }

        public MessageEvent<T> Subcrive( UnityAction<T> action )
        {
            Event.AddListener( action );
            return this;
        }
    }

    public class MessageEvent<T1, T2> : MessageEventBase<UnityEvent<T1, T2>>
    {
        public void Invoke( T1 arg1, T2 arg2 )
        {
            Event.Invoke( arg1, arg2 );
        }

        public MessageEvent<T1, T2> Subcrive( UnityAction<T1, T2> action )
        {
            Event.AddListener( action );
            return this;
        }
    }
}
