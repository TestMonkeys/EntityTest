// ****************************************************************
// License Info:
// Portions Copyright © 2002-2009 Charlie Poole or 
// Copyright © 2002-2004 James W. Newkirk, Michael C. Two,
// Alexei A. Vorontsov or Copyright © 2000-2002 Philip A. Craig 
// ****************************************************************

using System;
using System.Collections;
using System.Threading;

namespace TestMonkey.Assertion
{
    internal class AsyncSynchronizationContext : SynchronizationContext
    {
        private readonly AsyncOperationQueue _operations = new AsyncOperationQueue();
        private int _operationCount;

        public override void Send(SendOrPostCallback d, object state)
        {
            throw new InvalidOperationException("Sending to this synchronization context is not supported");
        }

        public override void Post(SendOrPostCallback d, object state)
        {
            _operations.Enqueue(new AsyncOperation(d, state));
        }

        public override void OperationStarted()
        {
            Interlocked.Increment(ref _operationCount);
            base.OperationStarted();
        }

        public override void OperationCompleted()
        {
            if (Interlocked.Decrement(ref _operationCount) == 0)
                _operations.MarkAsComplete();

            base.OperationCompleted();
        }

        public void WaitForPendingOperationsToComplete()
        {
            _operations.InvokeAll();
        }

        private class AsyncOperation
        {
            private readonly SendOrPostCallback _action;
            private readonly object _state;

            public AsyncOperation(SendOrPostCallback action, object state)
            {
                _action = action;
                _state = state;
            }

            public void Invoke()
            {
                _action(_state);
            }
        }

        private class AsyncOperationQueue
        {
            private readonly Queue _operations = Queue.Synchronized(new Queue());
            private readonly AutoResetEvent _operationsAvailable = new AutoResetEvent(false);
            private bool _run = true;

            public void Enqueue(AsyncOperation asyncOperation)
            {
                _operations.Enqueue(asyncOperation);
                _operationsAvailable.Set();
            }

            public void MarkAsComplete()
            {
                _run = false;
                _operationsAvailable.Set();
            }

            public void InvokeAll()
            {
                while (_run)
                {
                    InvokePendingOperations();
                    _operationsAvailable.WaitOne();
                }

                InvokePendingOperations();
            }

            private void InvokePendingOperations()
            {
                while (_operations.Count > 0)
                {
                    var operation = (AsyncOperation) _operations.Dequeue();
                    operation.Invoke();
                }
            }
        }
    }
}