namespace BoneLib
{
    public class DirtyProperty<T>
    {
        public bool isDirty = true;
        protected T value;

        public DirtyProperty(T initial) => value = initial;

        public delegate void DirtyHandler(T input);

        public void OnDirty(DirtyHandler handler)
        {
            if (isDirty)
                handler(this);
        }

        public static implicit operator DirtyProperty<T>(T value) => new DirtyProperty<T>(value); // Setter

        public static implicit operator T(DirtyProperty<T> prop) // Getter
        {
            prop.isDirty = false;
            return prop.value;
        }
    }
}