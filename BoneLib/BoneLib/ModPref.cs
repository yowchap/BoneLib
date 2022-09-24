using MelonLoader;
using MelonLoader.Preferences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoneLib
{
    public class ModPref<T>
    {
        public MelonPreferences_Entry<T> entry { get; }

        
        public ModPref (MelonPreferences_Category category, string identifier, T default_value, string display_name = null, string description = null, bool is_hidden = false, bool dont_save_default = false, ValueValidator validator = null)
        {
            entry = category.CreateEntry(identifier, default_value, display_name, description, is_hidden, dont_save_default, validator);
        }

        public static implicit operator T(ModPref<T> m) => m.entry.Value;
    }
}
