using System;

namespace BoneLib
{
    public static class SafeActions
    {
        public static void InvokeActionSafe(Action action)
        {
            if (action == null) return;
            foreach (Delegate invoker in action.GetInvocationList())
            {
                try
                {
                    Action call = (Action)invoker;
                    call();
                }
                catch (Exception ex)
                {
                    ModConsole.Error("Exception while invoking hook callback!");
                    ModConsole.Error(ex.ToString());
                }
            }
        }

        public static void InvokeActionSafe<T>(Action<T> action, T param)
        {
            if (action == null) return;
            foreach (Delegate invoker in action.GetInvocationList())
            {
                try
                {
                    Action<T> call = (Action<T>)invoker;
                    call(param);
                }
                catch (Exception ex)
                {
                    ModConsole.Error("Exception while invoking hook callback!");
                    ModConsole.Error(ex.ToString());
                }
            }
        }

        public static void InvokeActionSafe<T1, T2>(Action<T1, T2> action, T1 param1, T2 param2)
        {
            if (action == null) return;
            foreach (Delegate invoker in action.GetInvocationList())
            {
                try
                {
                    Action<T1, T2> call = (Action<T1, T2>)invoker;
                    call(param1, param2);
                }
                catch (Exception ex)
                {
                    ModConsole.Error("Exception while invoking hook callback!");
                    ModConsole.Error(ex.ToString());
                }
            }
        }
    }
}
