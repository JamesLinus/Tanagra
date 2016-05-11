using System;
using System.Runtime.InteropServices;

namespace Vulkan
{
    unsafe public class PipelineColorBlendStateCreateInfo
    {
        internal Interop.PipelineColorBlendStateCreateInfo* NativePointer;
        
        public PipelineColorBlendStateCreateFlags Flags
        {
            get { return NativePointer->Flags; }
            set { NativePointer->Flags = value; }
        }
        
        public Bool32 LogicOpEnable
        {
            get { return NativePointer->LogicOpEnable; }
            set { NativePointer->LogicOpEnable = value; }
        }
        
        public LogicOp LogicOp
        {
            get { return NativePointer->LogicOp; }
            set { NativePointer->LogicOp = value; }
        }
        
        public UInt32 AttachmentCount
        {
            get { return NativePointer->AttachmentCount; }
            set { NativePointer->AttachmentCount = value; }
        }
        
        public PipelineColorBlendAttachmentState[] Attachments
        {
            get
            {
                var valueCount = NativePointer->AttachmentCount;
                var valueArray = new PipelineColorBlendAttachmentState[valueCount];
                var ptr = (PipelineColorBlendAttachmentState*)NativePointer->Attachments;
                for(var x = 0; x < valueCount; x++)
                    valueArray[x] = ptr[x];
                return valueArray;
            }
            set
            {
                var valueCount = value.Length;
                NativePointer->AttachmentCount = (uint)valueCount;
                NativePointer->Attachments = Marshal.AllocHGlobal(Marshal.SizeOf<PipelineColorBlendAttachmentState>() * valueCount);
                var ptr = (PipelineColorBlendAttachmentState*)NativePointer->Attachments;
                for(var x = 0; x < valueCount; x++)
                    ptr[x] = value[x];
            }
        }
        
        public Single BlendConstants
        {
            get { return NativePointer->BlendConstants; }
            set { NativePointer->BlendConstants = value; }
        }
        
        public PipelineColorBlendStateCreateInfo()
        {
            NativePointer = (Interop.PipelineColorBlendStateCreateInfo*)Interop.Structure.Allocate(typeof(Interop.PipelineColorBlendStateCreateInfo));
            NativePointer->SType = StructureType.PipelineColorBlendStateCreateInfo;
        }
    }
}
