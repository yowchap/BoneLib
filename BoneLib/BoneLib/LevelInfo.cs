using Il2CppSLZ.Marrow.Warehouse;

namespace BoneLib
{
    /// <summary>
    /// A <see cref="LevelCrate"/> wrapper for use with level loading events in <see cref="Hooking"/>.
    /// </summary>
    public struct LevelInfo
    {
        public string title;
        public string barcode;
        public LevelCrateReference levelReference;

        public LevelInfo(LevelCrateReference levelReference)
        {
            this.title = levelReference.Crate.Title;
            this.barcode = levelReference.Barcode;
            this.levelReference = levelReference;
        }

        public LevelInfo(LevelCrate level) : this(new LevelCrateReference(level.Barcode)) { }
    }
}
