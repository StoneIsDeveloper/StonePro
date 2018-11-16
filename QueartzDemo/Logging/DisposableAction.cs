using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueartzDemo.Logging
{
    internal class DisposableAction : IDisposable
    {
        private readonly Action _onDispose;

        public DisposableAction(Action onDispose = null)
        {
            _onDispose = onDispose;
        }

        public void Dispose()
        {
            _onDispose?.Invoke();
        }
    }
}
