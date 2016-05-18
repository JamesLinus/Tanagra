using System;
using System.Runtime.InteropServices;

namespace Vulkan
{
    unsafe public class SparseImageMemoryBindInfo
    {
        internal Interop.SparseImageMemoryBindInfo* NativePointer;
        
        Image _Image;
        public Image Image
        {
            get { return _Image; }
            set { _Image = value; NativePointer->Image = value.NativePointer; }
        }
        
        public SparseImageMemoryBind[] Binds
        {
            get
            {
                if(NativePointer->Binds == IntPtr.Zero)
                    return null;
                var valueCount = NativePointer->BindCount;
                var valueArray = new SparseImageMemoryBind[valueCount];
                var ptr = (Interop.SparseImageMemoryBind*)NativePointer->Binds;
                for(var x = 0; x < valueCount; x++)
                    valueArray[x] = new SparseImageMemoryBind { NativePointer = &ptr[x] };
                
                return valueArray;
            }
            set
            {
                if(value != null)
                {
                    var valueCount = value.Length;
                    var typeSize = Marshal.SizeOf<Interop.SparseImageMemoryBind>() * valueCount;
                    if(NativePointer->Binds != IntPtr.Zero)
                        Marshal.ReAllocHGlobal(NativePointer->Binds, (IntPtr)typeSize);
                    
                    if(NativePointer->Binds == IntPtr.Zero)
                        NativePointer->Binds = Marshal.AllocHGlobal(typeSize);
                    
                    NativePointer->BindCount = (UInt32)valueCount;
                    var ptr = (Interop.SparseImageMemoryBind*)NativePointer->Binds;
                    for(var x = 0; x < valueCount; x++)
                        ptr[x] = *value[x].NativePointer;
                }
                else
                {
                    if(NativePointer->Binds != IntPtr.Zero)
                        Marshal.FreeHGlobal(NativePointer->Binds);
                    
                    NativePointer->Binds = IntPtr.Zero;
                    NativePointer->BindCount = 0;
                }
            }
        }
        
        public SparseImageMemoryBindInfo()
        {
            NativePointer = (Interop.SparseImageMemoryBindInfo*)MemoryUtils.Allocate(typeof(Interop.SparseImageMemoryBindInfo));
        }
        
        public SparseImageMemoryBindInfo(Image Image, SparseImageMemoryBind[] Binds) : this()
        {
            this.Image = Image;
            this.Binds = Binds;
        }
        
        public void Free()
        {
            MemoryUtils.Free((IntPtr)NativePointer);
            NativePointer = (Interop.SparseImageMemoryBindInfo*)IntPtr.Zero;
        }
    }
}
