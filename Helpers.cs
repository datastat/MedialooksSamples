using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedialooksMoveByTimecode
{
    public class Helpers
    {

        public static void SetControlProp<T>(T control, Action<T> action, SynchronizationContext syncCtx = null) where T : Control
        {
            if (control == null || control.IsDisposed)
            {
                return;
            }

            if (syncCtx != null)
            {
                syncCtx.Post(o => action.Invoke(control), null);
                return;
            }

            if (control.InvokeRequired)
            {
                try
                {
                    // control could be disposed and that would cause an exception (for example, on exiting)
                    // that is why try/catch HACK is here
                    control.Invoke(action, new object[] { control });
                }
                catch (System.ObjectDisposedException)
                {
                    // we will ignore this exception
                }
            }
            else
            {
                action.Invoke(control);
            }
        }
    }
}
