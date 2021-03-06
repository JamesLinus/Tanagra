using System;
using System.Runtime.InteropServices;

namespace Vulkan.Managed
{
    unsafe public class ComputePipelineCreateInfo : IDisposable
    {
        internal Unmanaged.ComputePipelineCreateInfo* NativePointer { get; private set; }
        
        /// <summary>
        /// Pipeline creation flags (Optional)
        /// </summary>
        public PipelineCreateFlags Flags
        {
            get { return NativePointer->Flags; }
            set { NativePointer->Flags = value; }
        }
        
        PipelineShaderStageCreateInfo _Stage;
        public PipelineShaderStageCreateInfo Stage
        {
            get { return _Stage; }
            set { _Stage = value; NativePointer->Stage = *value.NativePointer; }
        }
        
        PipelineLayout _Layout;
        /// <summary>
        /// Interface layout of the pipeline
        /// </summary>
        public PipelineLayout Layout
        {
            get { return _Layout; }
            set { _Layout = value; NativePointer->Layout = value.NativePointer; }
        }
        
        Pipeline _BasePipelineHandle;
        /// <summary>
        /// If VK_PIPELINE_CREATE_DERIVATIVE_BIT is set and this value is nonzero, it specifies the handle of the base pipeline this is a derivative of (Optional)
        /// </summary>
        public Pipeline BasePipelineHandle
        {
            get { return _BasePipelineHandle; }
            set { _BasePipelineHandle = value; NativePointer->BasePipelineHandle = value.NativePointer; }
        }
        
        /// <summary>
        /// If VK_PIPELINE_CREATE_DERIVATIVE_BIT is set and this value is not -1, it specifies an index into pCreateInfos of the base pipeline this is a derivative of
        /// </summary>
        public Int32 BasePipelineIndex
        {
            get { return NativePointer->BasePipelineIndex; }
            set { NativePointer->BasePipelineIndex = value; }
        }
        
        public ComputePipelineCreateInfo()
        {
            NativePointer = (Unmanaged.ComputePipelineCreateInfo*)MemUtil.Alloc(typeof(Unmanaged.ComputePipelineCreateInfo));
            NativePointer->SType = StructureType.ComputePipelineCreateInfo;
        }
        
        internal ComputePipelineCreateInfo(Unmanaged.ComputePipelineCreateInfo* ptr)
        {
            NativePointer = ptr;
            MemUtil.Register(NativePointer, typeof(Unmanaged.ComputePipelineCreateInfo));
        }
        
        /// <param name="Layout">Interface layout of the pipeline</param>
        /// <param name="BasePipelineIndex">If VK_PIPELINE_CREATE_DERIVATIVE_BIT is set and this value is not -1, it specifies an index into pCreateInfos of the base pipeline this is a derivative of</param>
        public ComputePipelineCreateInfo(PipelineShaderStageCreateInfo Stage, PipelineLayout Layout, Int32 BasePipelineIndex) : this()
        {
            this.Stage = Stage;
            this.Layout = Layout;
            this.BasePipelineIndex = BasePipelineIndex;
        }
        
        public void Dispose()
        {
            MemUtil.Free(NativePointer);
            NativePointer = null;
            GC.SuppressFinalize(this);
        }
        
        ~ComputePipelineCreateInfo()
        {
            if(NativePointer != null)
            {
                MemUtil.Free(NativePointer);
                NativePointer = null;
            }
        }
    }
}
