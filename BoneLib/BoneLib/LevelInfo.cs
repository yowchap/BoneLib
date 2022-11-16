using SLZ.Marrow.Warehouse;

namespace BoneLib
{
    /// <summary>
    /// A wrapper struct for <see cref="LevelCrate"/>
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
