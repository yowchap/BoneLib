using MelonLoader;
using System;
using System.Linq;
using System.Reflection;

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
                    string asm = invoker.GetMethodInfo().DeclaringType.Assembly.FullName;
                    MelonMod mod = MelonMod.RegisteredMelons.FirstOrDefault(i => i.MelonAssembly.Assembly.FullName == asm);

                    ModConsole.Error("Exception while invoking hook callback!");
                    mod.LoggerInstance.Error(ex.ToString());
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
                    string asm = invoker.GetMethodInfo().DeclaringType.Assembly.FullName;
                    MelonMod mod = MelonMod.RegisteredMelons.FirstOrDefault(i => i.MelonAssembly.Assembly.FullName == asm);

                    ModConsole.Error("Exception while invoking hook callback!");
                    mod.LoggerInstance.Error(ex.ToString());
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
                    string asm = invoker.GetMethodInfo().DeclaringType.Assembly.FullName;
                    MelonMod mod = MelonMod.RegisteredMelons.FirstOrDefault(i => i.MelonAssembly.Assembly.FullName == asm);

                    ModConsole.Error("Exception while invoking hook callback!");
                    mod.LoggerInstance.Error(ex.ToString());
                }
            }
        }

        public static void InvokeActionSafe<T1, T2, T3>(Action<T1, T2, T3> action, T1 param1, T2 param2, T3 param3)
        {
            if (action == null) return;
            foreach (Delegate invoker in action.GetInvocationList())
            {
                try
                {
                    Action<T1, T2, T3> call = (Action<T1, T2, T3>)invoker;
                    call(param1, param2, param3);
                }
                catch (Exception ex)
                {
                    string asm = invoker.GetMethodInfo().DeclaringType.Assembly.FullName;
                    MelonMod mod = MelonMod.RegisteredMelons.FirstOrDefault(i => i.MelonAssembly.Assembly.FullName == asm);

                    ModConsole.Error("Exception while invoking hook callback!");
                    mod.LoggerInstance.Error(ex.ToString());
                }
            }
        }

        public static void InvokeActionSafe<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action, T1 param1, T2 param2, T3 param3, T4 param4)
        {
            if (action == null) return;
            foreach (Delegate invoker in action.GetInvocationList())
            {
                try
                {
                    Action<T1, T2, T3, T4> call = (Action<T1, T2, T3, T4>)invoker;
                    call(param1, param2, param3, param4);
                }
                catch (Exception ex)
                {
                    string asm = invoker.GetMethodInfo().DeclaringType.Assembly.FullName;
                    MelonMod mod = MelonMod.RegisteredMelons.FirstOrDefault(i => i.MelonAssembly.Assembly.FullName == asm);

                    ModConsole.Error("Exception while invoking hook callback!");
                    mod?.LoggerInstance.Error(ex.ToString());
                }
            }
        }

        public static void InvokeActionSafe<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> action, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
        {
            if (action == null) return;
            foreach (Delegate invoker in action.GetInvocationList())
            {
                try
                {
                    Action<T1, T2, T3, T4, T5> call = (Action<T1, T2, T3, T4, T5>)invoker;
                    call(param1, param2, param3, param4, param5);
                }
                catch (Exception ex)
                {
                    string asm = invoker.GetMethodInfo().DeclaringType.Assembly.FullName;
                    MelonMod mod = MelonMod.RegisteredMelons.FirstOrDefault(i => i.MelonAssembly.Assembly.FullName == asm);

                    ModConsole.Error("Exception while invoking hook callback!");
                    mod?.LoggerInstance.Error(ex.ToString());
                }
            }
        }
    }
}
