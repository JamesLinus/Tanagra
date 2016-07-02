using System;
using System.Runtime.InteropServices;

namespace Vulkan.Managed
{
    unsafe public class XlibSurfaceCreateInfoKHR : IDisposable
    {
        internal Unmanaged.XlibSurfaceCreateInfoKHR* NativePointer { get; private set; }
        
        /// <summary>
        /// Reserved (Optional)
        /// </summary>
        public XlibSurfaceCreateFlagsKHR Flags
        {
            get { return NativePointer->Flags; }
            set { NativePointer->Flags = value; }
        }
        
        public IntPtr Dpy
        {
            get { return NativePointer->Dpy; }
            set { NativePointer->Dpy = value; }
        }
        
        public IntPtr Window
        {
            get { return NativePointer->Window; }
            set { NativePointer->Window = value; }
        }
        
        public XlibSurfaceCreateInfoKHR()
        {
            NativePointer = (Unmanaged.XlibSurfaceCreateInfoKHR*)MemUtil.Alloc(typeof(Unmanaged.XlibSurfaceCreateInfoKHR));
            NativePointer->SType = StructureType.XlibSurfaceCreateInfoKHR;
        }
        
        internal XlibSurfaceCreateInfoKHR(Unmanaged.XlibSurfaceCreateInfoKHR* ptr)
        {
            NativePointer = ptr;
            MemUtil.Register(NativePointer, typeof(Unmanaged.XlibSurfaceCreateInfoKHR));
        }
        
        public XlibSurfaceCreateInfoKHR(IntPtr Dpy, IntPtr Window) : this()
        {
            this.Dpy = Dpy;
            this.Window = Window;
        }
        
        public void Dispose()
        {
            MemUtil.Free(NativePointer);
            NativePointer = null;
            GC.SuppressFinalize(this);
        }
        
        ~XlibSurfaceCreateInfoKHR()
        {
            if(NativePointer != null)
            {
                MemUtil.Free(NativePointer);
                NativePointer = null;
            }
        }
    }
}
