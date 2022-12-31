using HarmonyLib;
using System;
using System.Runtime.InteropServices;
using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.InteropTypes;
using System.Threading;

namespace BoneLib.Nullables
{
    public class BoxedNullable<T> : Il2CppSystem.ValueType where T : unmanaged
    {
        private static readonly IntPtr classPtr;
        private static readonly IntPtr TClassPtr;
        private static readonly int valueSize;
        private static readonly int marshalSize;
        private static readonly int hasValueOffset;

        static BoxedNullable()
        {
            classPtr = Il2CppClassPointerStore<Il2CppSystem.Nullable<T>>.NativeClassPtr;
            TClassPtr = Il2CppClassPointerStore<T>.NativeClassPtr;

            uint align = 0;
            hasValueOffset = (int)IL2CPP.il2cpp_field_get_offset(IL2CPP.GetIl2CppField(classPtr, "hasValue"));
            valueSize = IL2CPP.il2cpp_class_value_size(TClassPtr, ref align);
            marshalSize = Marshal.SizeOf(typeof(T));
        }

        public IntPtr ValuePtr => IL2CPP.il2cpp_object_unbox(Pointer);

        public unsafe bool HasValue
        {
            get => (*(byte*)(Pointer + hasValueOffset)) != 0;
            set => (*(byte*)(Pointer + hasValueOffset)) = (value ? (byte)1 : (byte)0);
        }

        public unsafe T Value
        {
            get
            {
                if (marshalSize == valueSize)
                {
                    return *(T*)ValuePtr;
                }
                else if (valueSize == 1 && marshalSize > 0)// ie, bool
                {
                    T x = default;
                    *((byte*)&x) = *(byte*)ValuePtr;
                    return x;
                }

                throw new InvalidOperationException("Interop done goof?");
            }
            set
            {
                if (marshalSize == valueSize)
                {
                    *(T*)ValuePtr = value;
                }
                else if (valueSize == 1 && marshalSize > 0)
                {
                    *(byte*)ValuePtr = *((byte*)&value);
                }
                else
                {
                    throw new InvalidOperationException("Interop done goof?");
                }
            }
        }

        public unsafe BoxedNullable(T? nullable)
        {
            IntPtr obj = IL2CPP.il2cpp_object_new(classPtr);

            uint gcHandle = IL2CPP.il2cpp_gchandle_new(obj, false);
            AccessTools.Field(typeof(Il2CppObjectBase), "myGcHandle").SetValue(this, gcHandle);

            if (nullable.HasValue)
            {
                this.Value = nullable.Value;
                this.HasValue = true;
            }
            else
            {
                this.Value = default;
                this.HasValue = false;
            }
        }

        public unsafe BoxedNullable(T value)
        {
            IntPtr obj = IL2CPP.il2cpp_object_new(classPtr);
            IntPtr dataPtr = IL2CPP.il2cpp_object_unbox(obj);
            if (value is bool b)
            {
                Set(dataPtr, b ? (byte)1 : (byte)0);
            }
            else
            {
                Set(dataPtr, value);
            }

            *(byte*)(dataPtr + hasValueOffset) = 1;

            uint gcHandle = IL2CPP.il2cpp_gchandle_new(obj, false);

            AccessTools.Field(typeof(Il2CppObjectBase), "myGcHandle").SetValue(this, gcHandle);
        }

        private static unsafe void Set<U>(IntPtr tgt, U value) where U : unmanaged
        {
            *(U*)tgt = value;
        }

        public static implicit operator Il2CppSystem.Nullable<T>(BoxedNullable<T> me)
        {
            return new Il2CppSystem.Nullable<T>(me.Pointer);
        }
    }
}

namespace System.Runtime.CompilerServices
{
    /// <summary>
    /// Allows <see cref="BoneLib.Nullables.BoxedNullable{T}"/> to compile, with its <c>where T : unmanaged</c> type stipulation.
    /// </summary>
    public class IsUnmanagedAttribute
    {

    }
}