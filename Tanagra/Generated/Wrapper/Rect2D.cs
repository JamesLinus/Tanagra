using System;
using System.Runtime.InteropServices;

namespace Vulkan
{
    unsafe public class Rect2D
    {
        internal Interop.Rect2D* NativePointer;
        
        Offset2D _Offset;
        public Offset2D Offset
        {
            get { return _Offset; }
            set { _Offset = value; NativePointer->Offset = (IntPtr)value.NativePointer; }
        }
        
        Extent2D _Extent;
        public Extent2D Extent
        {
            get { return _Extent; }
            set { _Extent = value; NativePointer->Extent = (IntPtr)value.NativePointer; }
        }
        
        public Rect2D()
        {
            NativePointer = (Interop.Rect2D*)Interop.Structure.Allocate(typeof(Interop.Rect2D));
        }
    }
}