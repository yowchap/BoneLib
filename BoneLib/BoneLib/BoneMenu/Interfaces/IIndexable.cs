using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoneLib.BoneMenu.Interfaces
{
    public interface IIndexable<T>
    {
        T value { get; set; }
        int index { get; set; }

        void NextIndex();
        void PreviousIndex();
    }
}
