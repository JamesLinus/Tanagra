using System;
using System.Runtime.InteropServices;

namespace Vulkan
{
    unsafe public class PipelineCacheCreateInfo
    {
        internal Interop.PipelineCacheCreateInfo* NativePointer;
        
        public PipelineCacheCreateFlags Flags
        {
            get { return NativePointer->Flags; }
            set { NativePointer->Flags = value; }
        }
        
        public UIntPtr InitialDataSize
        {
            get { return NativePointer->InitialDataSize; }
            set { NativePointer->InitialDataSize = value; }
        }
        
        public IntPtr[] InitialData
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }
        
        public PipelineCacheCreateInfo()
        {
            NativePointer = (Interop.PipelineCacheCreateInfo*)Interop.Structure.Allocate(typeof(Interop.PipelineCacheCreateInfo));
            NativePointer->SType = StructureType.PipelineCacheCreateInfo;
        }
    }
}
