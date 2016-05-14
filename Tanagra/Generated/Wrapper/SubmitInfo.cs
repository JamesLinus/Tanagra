using System;
using System.Runtime.InteropServices;

namespace Vulkan
{
    unsafe public class SubmitInfo
    {
        internal Interop.SubmitInfo* NativePointer;
        
        public Semaphore[] WaitSemaphores
        {
            get
            {
                var valueCount = NativePointer->WaitSemaphoreCount;
                var valueArray = new Semaphore[valueCount];
                var ptr = (UInt64*)NativePointer->WaitSemaphores;
                for(var x = 0; x < valueCount; x++)
                    valueArray[x] = new Semaphore { NativePointer = ptr[x] };
                return valueArray;
            }
            set
            {
                var valueCount = value.Length;
                NativePointer->WaitSemaphoreCount = (uint)valueCount;
                NativePointer->WaitSemaphores = Marshal.AllocHGlobal(Marshal.SizeOf<IntPtr>() * valueCount);
                var ptr = (IntPtr*)NativePointer->WaitSemaphores;
                for(var x = 0; x < valueCount; x++)
                    ptr[x] = (IntPtr)value[x].NativePointer;
            }
        }
        
        public PipelineStageFlags[] WaitDstStageMask
        {
            get
            {
                var valueCount = NativePointer->WaitSemaphoreCount;
                var valueArray = new PipelineStageFlags[valueCount];
                var ptr = (UInt32*)NativePointer->WaitDstStageMask;
                for(var x = 0; x < valueCount; x++)
                    valueArray[x] = (PipelineStageFlags)ptr[x];
                return valueArray;
            }
            set
            {
                var valueCount = value.Length;
                NativePointer->WaitSemaphoreCount = (uint)valueCount;
                NativePointer->WaitDstStageMask = Marshal.AllocHGlobal(Marshal.SizeOf<UInt32>() * valueCount);
                var ptr = (UInt32*)NativePointer->WaitDstStageMask;
                for(var x = 0; x < valueCount; x++)
                    ptr[x] = (UInt32)value[x];
            }
        }
        
        public CommandBuffer[] CommandBuffers
        {
            get
            {
                var valueCount = NativePointer->CommandBufferCount;
                var valueArray = new CommandBuffer[valueCount];
                var ptr = (IntPtr*)NativePointer->CommandBuffers;
                for(var x = 0; x < valueCount; x++)
                    valueArray[x] = new CommandBuffer { NativePointer = ptr[x] };
                return valueArray;
            }
            set
            {
                var valueCount = value.Length;
                NativePointer->CommandBufferCount = (uint)valueCount;
                NativePointer->CommandBuffers = Marshal.AllocHGlobal(Marshal.SizeOf<IntPtr>() * valueCount);
                var ptr = (IntPtr*)NativePointer->CommandBuffers;
                for(var x = 0; x < valueCount; x++)
                    ptr[x] = (IntPtr)value[x].NativePointer;
            }
        }
        
        public Semaphore[] SignalSemaphores
        {
            get
            {
                var valueCount = NativePointer->SignalSemaphoreCount;
                var valueArray = new Semaphore[valueCount];
                var ptr = (UInt64*)NativePointer->SignalSemaphores;
                for(var x = 0; x < valueCount; x++)
                    valueArray[x] = new Semaphore { NativePointer = ptr[x] };
                return valueArray;
            }
            set
            {
                var valueCount = value.Length;
                NativePointer->SignalSemaphoreCount = (uint)valueCount;
                NativePointer->SignalSemaphores = Marshal.AllocHGlobal(Marshal.SizeOf<IntPtr>() * valueCount);
                var ptr = (IntPtr*)NativePointer->SignalSemaphores;
                for(var x = 0; x < valueCount; x++)
                    ptr[x] = (IntPtr)value[x].NativePointer;
            }
        }
        
        public SubmitInfo()
        {
            NativePointer = (Interop.SubmitInfo*)Interop.Structure.Allocate(typeof(Interop.SubmitInfo));
            NativePointer->SType = StructureType.SubmitInfo;
        }
        
        public SubmitInfo(Semaphore[] WaitSemaphores, PipelineStageFlags[] WaitDstStageMask, CommandBuffer[] CommandBuffers, Semaphore[] SignalSemaphores) : this()
        {
            this.WaitSemaphores = WaitSemaphores;
            this.WaitDstStageMask = WaitDstStageMask;
            this.CommandBuffers = CommandBuffers;
            this.SignalSemaphores = SignalSemaphores;
        }
    }
}
