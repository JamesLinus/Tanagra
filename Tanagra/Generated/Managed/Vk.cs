using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Vulkan.Managed
{
    using static Unmanaged.VulkanBinding;
    
    public unsafe static class Vk
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="allocator">Optional</param>
        public static Instance CreateInstance(InstanceCreateInfo createInfo, AllocationCallbacks allocator = null)
        {
            var instance = new Instance();
            fixed(IntPtr* ptrInstance = &instance.NativePointer)
            {
                var result = vkCreateInstance(createInfo.NativePointer, (allocator != null) ? allocator.NativePointer : null, ptrInstance);
                if(result != Result.Success)
                    throw new VulkanCommandException(nameof(vkCreateInstance), result);
            }
            return instance;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance">ExternSync</param>
        /// <param name="allocator">Optional</param>
        public static void DestroyInstance(Instance instance, AllocationCallbacks allocator = null)
        {
            vkDestroyInstance((instance != null) ? instance.NativePointer : IntPtr.Zero, (allocator != null) ? allocator.NativePointer : null);
        }
        
        /// <summary>
        /// 
        /// </summary>
        public static List<PhysicalDevice> EnumeratePhysicalDevices(Instance instance)
        {
            UInt32 listLength;
            var result = vkEnumeratePhysicalDevices(instance.NativePointer, &listLength, null);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkEnumeratePhysicalDevices), result);
            
            var arrayPhysicalDevice = new IntPtr[listLength];
            fixed(IntPtr* resultPtr = &arrayPhysicalDevice[0])
                result = vkEnumeratePhysicalDevices(instance.NativePointer, &listLength, resultPtr);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkEnumeratePhysicalDevices), result);
            
            var list = new List<PhysicalDevice>();
            for(var x = 0; x < listLength; x++)
            {
                var item = new PhysicalDevice();
                item.NativePointer = arrayPhysicalDevice[x];
                list.Add(item);
            }
            
            return list;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public static IntPtr GetDeviceProcAddr(Device device, String name)
        {
            var result = vkGetDeviceProcAddr(device.NativePointer, name);
            return result;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance">Optional</param>
        public static IntPtr GetInstanceProcAddr(Instance instance, String name)
        {
            var result = vkGetInstanceProcAddr((instance != null) ? instance.NativePointer : IntPtr.Zero, name);
            return result;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public static PhysicalDeviceProperties GetPhysicalDeviceProperties(PhysicalDevice physicalDevice)
        {
            var properties = new PhysicalDeviceProperties();
            vkGetPhysicalDeviceProperties(physicalDevice.NativePointer, properties.NativePointer);
            return properties;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public static List<QueueFamilyProperties> GetPhysicalDeviceQueueFamilyProperties(PhysicalDevice physicalDevice)
        {
            UInt32 listLength;
            vkGetPhysicalDeviceQueueFamilyProperties(physicalDevice.NativePointer, &listLength, null);
            
            var arrayQueueFamilyProperties = new QueueFamilyProperties[listLength];
            fixed(QueueFamilyProperties* resultPtr = &arrayQueueFamilyProperties[0])
                vkGetPhysicalDeviceQueueFamilyProperties(physicalDevice.NativePointer, &listLength, resultPtr);
            
            var list = new List<QueueFamilyProperties>();
            for(var x = 0; x < listLength; x++)
            {
                list.Add(arrayQueueFamilyProperties[x]);
            }
            
            return list;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public static PhysicalDeviceMemoryProperties GetPhysicalDeviceMemoryProperties(PhysicalDevice physicalDevice)
        {
            var memoryProperties = new PhysicalDeviceMemoryProperties();
            vkGetPhysicalDeviceMemoryProperties(physicalDevice.NativePointer, memoryProperties.NativePointer);
            return memoryProperties;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public static PhysicalDeviceFeatures GetPhysicalDeviceFeatures(PhysicalDevice physicalDevice)
        {
            var features = new PhysicalDeviceFeatures();
            vkGetPhysicalDeviceFeatures(physicalDevice.NativePointer, &features);
            return features;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public static FormatProperties GetPhysicalDeviceFormatProperties(PhysicalDevice physicalDevice, Format format)
        {
            var formatProperties = new FormatProperties();
            vkGetPhysicalDeviceFormatProperties(physicalDevice.NativePointer, format, &formatProperties);
            return formatProperties;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="flags">Optional</param>
        public static ImageFormatProperties GetPhysicalDeviceImageFormatProperties(PhysicalDevice physicalDevice, Format format, ImageType type, ImageTiling tiling, ImageUsageFlags usage, ImageCreateFlags flags)
        {
            var imageFormatProperties = new ImageFormatProperties();
            var result = vkGetPhysicalDeviceImageFormatProperties(physicalDevice.NativePointer, format, type, tiling, usage, flags, &imageFormatProperties);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkGetPhysicalDeviceImageFormatProperties), result);
            return imageFormatProperties;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="allocator">Optional</param>
        public static Device CreateDevice(PhysicalDevice physicalDevice, DeviceCreateInfo createInfo, AllocationCallbacks allocator = null)
        {
            var device = new Device();
            fixed(IntPtr* ptrDevice = &device.NativePointer)
            {
                var result = vkCreateDevice(physicalDevice.NativePointer, createInfo.NativePointer, (allocator != null) ? allocator.NativePointer : null, ptrDevice);
                if(result != Result.Success)
                    throw new VulkanCommandException(nameof(vkCreateDevice), result);
            }
            return device;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="device">ExternSync</param>
        /// <param name="allocator">Optional</param>
        public static void DestroyDevice(Device device, AllocationCallbacks allocator = null)
        {
            vkDestroyDevice((device != null) ? device.NativePointer : IntPtr.Zero, (allocator != null) ? allocator.NativePointer : null);
        }
        
        /// <summary>
        /// 
        /// </summary>
        public static List<LayerProperties> EnumerateInstanceLayerProperties()
        {
            UInt32 listLength;
            var result = vkEnumerateInstanceLayerProperties(&listLength, null);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkEnumerateInstanceLayerProperties), result);
            
            var arrayLayerProperties = new Unmanaged.LayerProperties[listLength];
            fixed(Unmanaged.LayerProperties* resultPtr = &arrayLayerProperties[0])
                result = vkEnumerateInstanceLayerProperties(&listLength, resultPtr);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkEnumerateInstanceLayerProperties), result);
            
            var list = new List<LayerProperties>();
            for(var x = 0; x < listLength; x++)
            {
                var item = new LayerProperties();
                fixed(Unmanaged.LayerProperties* itemPtr = &arrayLayerProperties[x])
                    item.NativePointer = itemPtr;
                list.Add(item);
            }
            
            return list;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="layerName">Optional</param>
        public static List<ExtensionProperties> EnumerateInstanceExtensionProperties(String layerName)
        {
            UInt32 listLength;
            var result = vkEnumerateInstanceExtensionProperties(layerName, &listLength, null);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkEnumerateInstanceExtensionProperties), result);
            
            var arrayExtensionProperties = new Unmanaged.ExtensionProperties[listLength];
            fixed(Unmanaged.ExtensionProperties* resultPtr = &arrayExtensionProperties[0])
                result = vkEnumerateInstanceExtensionProperties(layerName, &listLength, resultPtr);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkEnumerateInstanceExtensionProperties), result);
            
            var list = new List<ExtensionProperties>();
            for(var x = 0; x < listLength; x++)
            {
                var item = new ExtensionProperties();
                fixed(Unmanaged.ExtensionProperties* itemPtr = &arrayExtensionProperties[x])
                    item.NativePointer = itemPtr;
                list.Add(item);
            }
            
            return list;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="physicalDevice">Optional</param>
        public static List<LayerProperties> EnumerateDeviceLayerProperties(PhysicalDevice physicalDevice)
        {
            UInt32 listLength;
            var result = vkEnumerateDeviceLayerProperties(physicalDevice.NativePointer, &listLength, null);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkEnumerateDeviceLayerProperties), result);
            
            var arrayLayerProperties = new Unmanaged.LayerProperties[listLength];
            fixed(Unmanaged.LayerProperties* resultPtr = &arrayLayerProperties[0])
                result = vkEnumerateDeviceLayerProperties(physicalDevice.NativePointer, &listLength, resultPtr);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkEnumerateDeviceLayerProperties), result);
            
            var list = new List<LayerProperties>();
            for(var x = 0; x < listLength; x++)
            {
                var item = new LayerProperties();
                fixed(Unmanaged.LayerProperties* itemPtr = &arrayLayerProperties[x])
                    item.NativePointer = itemPtr;
                list.Add(item);
            }
            
            return list;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="layerName">Optional</param>
        public static List<ExtensionProperties> EnumerateDeviceExtensionProperties(PhysicalDevice physicalDevice, String layerName)
        {
            UInt32 listLength;
            var result = vkEnumerateDeviceExtensionProperties(physicalDevice.NativePointer, layerName, &listLength, null);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkEnumerateDeviceExtensionProperties), result);
            
            var arrayExtensionProperties = new Unmanaged.ExtensionProperties[listLength];
            fixed(Unmanaged.ExtensionProperties* resultPtr = &arrayExtensionProperties[0])
                result = vkEnumerateDeviceExtensionProperties(physicalDevice.NativePointer, layerName, &listLength, resultPtr);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkEnumerateDeviceExtensionProperties), result);
            
            var list = new List<ExtensionProperties>();
            for(var x = 0; x < listLength; x++)
            {
                var item = new ExtensionProperties();
                fixed(Unmanaged.ExtensionProperties* itemPtr = &arrayExtensionProperties[x])
                    item.NativePointer = itemPtr;
                list.Add(item);
            }
            
            return list;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public static Queue GetDeviceQueue(Device device, UInt32 queueFamilyIndex, UInt32 queueIndex)
        {
            var queue = new Queue();
            fixed(IntPtr* ptrQueue = &queue.NativePointer)
            {
                vkGetDeviceQueue(device.NativePointer, queueFamilyIndex, queueIndex, ptrQueue);
            }
            return queue;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queue">ExternSync</param>
        /// <param name="fence">ExternSync</param>
        public static void QueueSubmit(Queue queue, List<SubmitInfo> submits, Fence fence)
        {
            var submitCount = (submits != null) ? (UInt32)submits.Count : 0;
            var _submitsPtr = (Unmanaged.SubmitInfo*)IntPtr.Zero;
            if(submitCount != 0)
            {
                var _submitsSize = Marshal.SizeOf(typeof(Unmanaged.SubmitInfo));
                _submitsPtr = (Unmanaged.SubmitInfo*)Marshal.AllocHGlobal((int)(_submitsSize * submitCount));
                for(var x = 0; x < submitCount; x++)
                    _submitsPtr[x] = *submits[x].NativePointer;
            }
            
            var result = vkQueueSubmit(queue.NativePointer, submitCount, _submitsPtr, (fence != null) ? fence.NativePointer : 0);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkQueueSubmit), result);
            Marshal.FreeHGlobal((IntPtr)_submitsPtr);
        }
        
        /// <summary>
        /// 
        /// </summary>
        public static void QueueWaitIdle(Queue queue)
        {
            var result = vkQueueWaitIdle(queue.NativePointer);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkQueueWaitIdle), result);
        }
        
        /// <summary>
        /// 
        /// </summary>
        public static void DeviceWaitIdle(Device device)
        {
            var result = vkDeviceWaitIdle(device.NativePointer);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkDeviceWaitIdle), result);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="allocator">Optional</param>
        public static DeviceMemory AllocateMemory(Device device, MemoryAllocateInfo allocateInfo, AllocationCallbacks allocator = null)
        {
            var memory = new DeviceMemory();
            fixed(UInt64* ptrDeviceMemory = &memory.NativePointer)
            {
                var result = vkAllocateMemory(device.NativePointer, allocateInfo.NativePointer, (allocator != null) ? allocator.NativePointer : null, ptrDeviceMemory);
                if(result != Result.Success)
                    throw new VulkanCommandException(nameof(vkAllocateMemory), result);
            }
            return memory;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="memory">ExternSync</param>
        /// <param name="allocator">Optional</param>
        public static void FreeMemory(Device device, DeviceMemory memory, AllocationCallbacks allocator = null)
        {
            vkFreeMemory(device.NativePointer, (memory != null) ? memory.NativePointer : 0, (allocator != null) ? allocator.NativePointer : null);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="memory">ExternSync</param>
        /// <param name="flags">Optional</param>
        public static IntPtr MapMemory(Device device, DeviceMemory memory, DeviceSize offset, DeviceSize size, MemoryMapFlags flags)
        {
            var data = new IntPtr();
            var result = vkMapMemory(device.NativePointer, memory.NativePointer, offset, size, flags, &data);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkMapMemory), result);
            return data;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="memory">ExternSync</param>
        public static void UnmapMemory(Device device, DeviceMemory memory)
        {
            vkUnmapMemory(device.NativePointer, memory.NativePointer);
        }
        
        /// <summary>
        /// 
        /// </summary>
        public static void FlushMappedMemoryRanges(Device device, List<MappedMemoryRange> memoryRanges)
        {
            var memoryRangeCount = (memoryRanges != null) ? (UInt32)memoryRanges.Count : 0;
            var _memoryRangesPtr = (Unmanaged.MappedMemoryRange*)IntPtr.Zero;
            if(memoryRangeCount != 0)
            {
                var _memoryRangesSize = Marshal.SizeOf(typeof(Unmanaged.MappedMemoryRange));
                _memoryRangesPtr = (Unmanaged.MappedMemoryRange*)Marshal.AllocHGlobal((int)(_memoryRangesSize * memoryRangeCount));
                for(var x = 0; x < memoryRangeCount; x++)
                    _memoryRangesPtr[x] = *memoryRanges[x].NativePointer;
            }
            
            var result = vkFlushMappedMemoryRanges(device.NativePointer, memoryRangeCount, _memoryRangesPtr);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkFlushMappedMemoryRanges), result);
            Marshal.FreeHGlobal((IntPtr)_memoryRangesPtr);
        }
        
        /// <summary>
        /// 
        /// </summary>
        public static void InvalidateMappedMemoryRanges(Device device, List<MappedMemoryRange> memoryRanges)
        {
            var memoryRangeCount = (memoryRanges != null) ? (UInt32)memoryRanges.Count : 0;
            var _memoryRangesPtr = (Unmanaged.MappedMemoryRange*)IntPtr.Zero;
            if(memoryRangeCount != 0)
            {
                var _memoryRangesSize = Marshal.SizeOf(typeof(Unmanaged.MappedMemoryRange));
                _memoryRangesPtr = (Unmanaged.MappedMemoryRange*)Marshal.AllocHGlobal((int)(_memoryRangesSize * memoryRangeCount));
                for(var x = 0; x < memoryRangeCount; x++)
                    _memoryRangesPtr[x] = *memoryRanges[x].NativePointer;
            }
            
            var result = vkInvalidateMappedMemoryRanges(device.NativePointer, memoryRangeCount, _memoryRangesPtr);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkInvalidateMappedMemoryRanges), result);
            Marshal.FreeHGlobal((IntPtr)_memoryRangesPtr);
        }
        
        /// <summary>
        /// 
        /// </summary>
        public static DeviceSize GetDeviceMemoryCommitment(Device device, DeviceMemory memory)
        {
            var committedMemoryInBytes = new DeviceSize();
            vkGetDeviceMemoryCommitment(device.NativePointer, memory.NativePointer, &committedMemoryInBytes);
            return committedMemoryInBytes;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public static MemoryRequirements GetBufferMemoryRequirements(Device device, Buffer buffer)
        {
            var memoryRequirements = new MemoryRequirements();
            vkGetBufferMemoryRequirements(device.NativePointer, buffer.NativePointer, &memoryRequirements);
            return memoryRequirements;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer">ExternSync</param>
        public static void BindBufferMemory(Device device, Buffer buffer, DeviceMemory memory, DeviceSize memoryOffset)
        {
            var result = vkBindBufferMemory(device.NativePointer, buffer.NativePointer, memory.NativePointer, memoryOffset);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkBindBufferMemory), result);
        }
        
        /// <summary>
        /// 
        /// </summary>
        public static MemoryRequirements GetImageMemoryRequirements(Device device, Image image)
        {
            var memoryRequirements = new MemoryRequirements();
            vkGetImageMemoryRequirements(device.NativePointer, image.NativePointer, &memoryRequirements);
            return memoryRequirements;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="image">ExternSync</param>
        public static void BindImageMemory(Device device, Image image, DeviceMemory memory, DeviceSize memoryOffset)
        {
            var result = vkBindImageMemory(device.NativePointer, image.NativePointer, memory.NativePointer, memoryOffset);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkBindImageMemory), result);
        }
        
        /// <summary>
        /// 
        /// </summary>
        public static List<SparseImageMemoryRequirements> GetImageSparseMemoryRequirements(Device device, Image image)
        {
            UInt32 listLength;
            vkGetImageSparseMemoryRequirements(device.NativePointer, image.NativePointer, &listLength, null);
            
            var arraySparseImageMemoryRequirements = new SparseImageMemoryRequirements[listLength];
            fixed(SparseImageMemoryRequirements* resultPtr = &arraySparseImageMemoryRequirements[0])
                vkGetImageSparseMemoryRequirements(device.NativePointer, image.NativePointer, &listLength, resultPtr);
            
            var list = new List<SparseImageMemoryRequirements>();
            for(var x = 0; x < listLength; x++)
            {
                list.Add(arraySparseImageMemoryRequirements[x]);
            }
            
            return list;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public static List<SparseImageFormatProperties> GetPhysicalDeviceSparseImageFormatProperties(PhysicalDevice physicalDevice, Format format, ImageType type, SampleCountFlags samples, ImageUsageFlags usage, ImageTiling tiling)
        {
            UInt32 listLength;
            vkGetPhysicalDeviceSparseImageFormatProperties(physicalDevice.NativePointer, format, type, samples, usage, tiling, &listLength, null);
            
            var arraySparseImageFormatProperties = new SparseImageFormatProperties[listLength];
            fixed(SparseImageFormatProperties* resultPtr = &arraySparseImageFormatProperties[0])
                vkGetPhysicalDeviceSparseImageFormatProperties(physicalDevice.NativePointer, format, type, samples, usage, tiling, &listLength, resultPtr);
            
            var list = new List<SparseImageFormatProperties>();
            for(var x = 0; x < listLength; x++)
            {
                list.Add(arraySparseImageFormatProperties[x]);
            }
            
            return list;
        }
        
        /// <summary>
        /// [<see cref="QueueFlags"/>: sparse_binding] 
        /// </summary>
        /// <param name="queue">ExternSync</param>
        /// <param name="fence">ExternSync</param>
        public static void QueueBindSparse(Queue queue, List<BindSparseInfo> bindInfo, Fence fence)
        {
            var bindInfoCount = (bindInfo != null) ? (UInt32)bindInfo.Count : 0;
            var _bindInfoPtr = (Unmanaged.BindSparseInfo*)IntPtr.Zero;
            if(bindInfoCount != 0)
            {
                var _bindInfoSize = Marshal.SizeOf(typeof(Unmanaged.BindSparseInfo));
                _bindInfoPtr = (Unmanaged.BindSparseInfo*)Marshal.AllocHGlobal((int)(_bindInfoSize * bindInfoCount));
                for(var x = 0; x < bindInfoCount; x++)
                    _bindInfoPtr[x] = *bindInfo[x].NativePointer;
            }
            
            var result = vkQueueBindSparse(queue.NativePointer, bindInfoCount, _bindInfoPtr, (fence != null) ? fence.NativePointer : 0);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkQueueBindSparse), result);
            Marshal.FreeHGlobal((IntPtr)_bindInfoPtr);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="allocator">Optional</param>
        public static Fence CreateFence(Device device, FenceCreateInfo createInfo, AllocationCallbacks allocator = null)
        {
            var fence = new Fence();
            fixed(UInt64* ptrFence = &fence.NativePointer)
            {
                var result = vkCreateFence(device.NativePointer, createInfo.NativePointer, (allocator != null) ? allocator.NativePointer : null, ptrFence);
                if(result != Result.Success)
                    throw new VulkanCommandException(nameof(vkCreateFence), result);
            }
            return fence;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fence">ExternSync</param>
        /// <param name="allocator">Optional</param>
        public static void DestroyFence(Device device, Fence fence, AllocationCallbacks allocator = null)
        {
            vkDestroyFence(device.NativePointer, (fence != null) ? fence.NativePointer : 0, (allocator != null) ? allocator.NativePointer : null);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fences">ExternSync</param>
        public static void ResetFences(Device device, List<Fence> fences)
        {
            var fenceCount = (fences != null) ? (UInt32)fences.Count : 0;
            var _fencesPtr = (UInt64*)IntPtr.Zero;
            if(fenceCount != 0)
            {
                var _fencesSize = Marshal.SizeOf(typeof(IntPtr));
                _fencesPtr = (UInt64*)Marshal.AllocHGlobal((int)(_fencesSize * fenceCount));
                for(var x = 0; x < fenceCount; x++)
                    _fencesPtr[x] = fences[x].NativePointer;
            }
            
            var result = vkResetFences(device.NativePointer, fenceCount, _fencesPtr);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkResetFences), result);
            Marshal.FreeHGlobal((IntPtr)_fencesPtr);
        }
        
        /// <summary>
        /// 
        /// </summary>
        public static void GetFenceStatus(Device device, Fence fence)
        {
            var result = vkGetFenceStatus(device.NativePointer, fence.NativePointer);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkGetFenceStatus), result);
        }
        
        /// <summary>
        /// 
        /// </summary>
        public static void WaitForFences(Device device, List<Fence> fences, Bool32 waitAll, UInt64 timeout)
        {
            var fenceCount = (fences != null) ? (UInt32)fences.Count : 0;
            var _fencesPtr = (UInt64*)IntPtr.Zero;
            if(fenceCount != 0)
            {
                var _fencesSize = Marshal.SizeOf(typeof(IntPtr));
                _fencesPtr = (UInt64*)Marshal.AllocHGlobal((int)(_fencesSize * fenceCount));
                for(var x = 0; x < fenceCount; x++)
                    _fencesPtr[x] = fences[x].NativePointer;
            }
            
            var result = vkWaitForFences(device.NativePointer, fenceCount, _fencesPtr, waitAll, timeout);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkWaitForFences), result);
            Marshal.FreeHGlobal((IntPtr)_fencesPtr);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="allocator">Optional</param>
        public static Semaphore CreateSemaphore(Device device, SemaphoreCreateInfo createInfo, AllocationCallbacks allocator = null)
        {
            var semaphore = new Semaphore();
            fixed(UInt64* ptrSemaphore = &semaphore.NativePointer)
            {
                var result = vkCreateSemaphore(device.NativePointer, createInfo.NativePointer, (allocator != null) ? allocator.NativePointer : null, ptrSemaphore);
                if(result != Result.Success)
                    throw new VulkanCommandException(nameof(vkCreateSemaphore), result);
            }
            return semaphore;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="semaphore">ExternSync</param>
        /// <param name="allocator">Optional</param>
        public static void DestroySemaphore(Device device, Semaphore semaphore, AllocationCallbacks allocator = null)
        {
            vkDestroySemaphore(device.NativePointer, (semaphore != null) ? semaphore.NativePointer : 0, (allocator != null) ? allocator.NativePointer : null);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="allocator">Optional</param>
        public static Event CreateEvent(Device device, EventCreateInfo createInfo, AllocationCallbacks allocator = null)
        {
            var @event = new Event();
            fixed(UInt64* ptrEvent = &@event.NativePointer)
            {
                var result = vkCreateEvent(device.NativePointer, createInfo.NativePointer, (allocator != null) ? allocator.NativePointer : null, ptrEvent);
                if(result != Result.Success)
                    throw new VulkanCommandException(nameof(vkCreateEvent), result);
            }
            return @event;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="@event">ExternSync</param>
        /// <param name="allocator">Optional</param>
        public static void DestroyEvent(Device device, Event @event, AllocationCallbacks allocator = null)
        {
            vkDestroyEvent(device.NativePointer, (@event != null) ? @event.NativePointer : 0, (allocator != null) ? allocator.NativePointer : null);
        }
        
        /// <summary>
        /// 
        /// </summary>
        public static void GetEventStatus(Device device, Event @event)
        {
            var result = vkGetEventStatus(device.NativePointer, @event.NativePointer);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkGetEventStatus), result);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="@event">ExternSync</param>
        public static void SetEvent(Device device, Event @event)
        {
            var result = vkSetEvent(device.NativePointer, @event.NativePointer);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkSetEvent), result);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="@event">ExternSync</param>
        public static void ResetEvent(Device device, Event @event)
        {
            var result = vkResetEvent(device.NativePointer, @event.NativePointer);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkResetEvent), result);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="allocator">Optional</param>
        public static QueryPool CreateQueryPool(Device device, QueryPoolCreateInfo createInfo, AllocationCallbacks allocator = null)
        {
            var queryPool = new QueryPool();
            fixed(UInt64* ptrQueryPool = &queryPool.NativePointer)
            {
                var result = vkCreateQueryPool(device.NativePointer, createInfo.NativePointer, (allocator != null) ? allocator.NativePointer : null, ptrQueryPool);
                if(result != Result.Success)
                    throw new VulkanCommandException(nameof(vkCreateQueryPool), result);
            }
            return queryPool;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryPool">ExternSync</param>
        /// <param name="allocator">Optional</param>
        public static void DestroyQueryPool(Device device, QueryPool queryPool, AllocationCallbacks allocator = null)
        {
            vkDestroyQueryPool(device.NativePointer, (queryPool != null) ? queryPool.NativePointer : 0, (allocator != null) ? allocator.NativePointer : null);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="flags">Optional</param>
        public static void GetQueryPoolResults(Device device, QueryPool queryPool, UInt32 firstQuery, UInt32 queryCount, List<IntPtr> data, DeviceSize stride, QueryResultFlags flags)
        {
            var dataSize = (data != null) ? (UInt32)data.Count : 0;
            var _dataPtr = (IntPtr*)IntPtr.Zero;
            if(dataSize != 0)
            {
                var _dataSize = Marshal.SizeOf(typeof(IntPtr));
                _dataPtr = (IntPtr*)Marshal.AllocHGlobal((int)(_dataSize * dataSize));
                for(var x = 0; x < dataSize; x++)
                    _dataPtr[x] = data[x];
            }
            
            var result = vkGetQueryPoolResults(device.NativePointer, queryPool.NativePointer, firstQuery, queryCount, dataSize, _dataPtr, stride, flags);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkGetQueryPoolResults), result);
            Marshal.FreeHGlobal((IntPtr)_dataPtr);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="allocator">Optional</param>
        public static Buffer CreateBuffer(Device device, BufferCreateInfo createInfo, AllocationCallbacks allocator = null)
        {
            var buffer = new Buffer();
            fixed(UInt64* ptrBuffer = &buffer.NativePointer)
            {
                var result = vkCreateBuffer(device.NativePointer, createInfo.NativePointer, (allocator != null) ? allocator.NativePointer : null, ptrBuffer);
                if(result != Result.Success)
                    throw new VulkanCommandException(nameof(vkCreateBuffer), result);
            }
            return buffer;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer">ExternSync</param>
        /// <param name="allocator">Optional</param>
        public static void DestroyBuffer(Device device, Buffer buffer, AllocationCallbacks allocator = null)
        {
            vkDestroyBuffer(device.NativePointer, (buffer != null) ? buffer.NativePointer : 0, (allocator != null) ? allocator.NativePointer : null);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="allocator">Optional</param>
        public static BufferView CreateBufferView(Device device, BufferViewCreateInfo createInfo, AllocationCallbacks allocator = null)
        {
            var view = new BufferView();
            fixed(UInt64* ptrBufferView = &view.NativePointer)
            {
                var result = vkCreateBufferView(device.NativePointer, createInfo.NativePointer, (allocator != null) ? allocator.NativePointer : null, ptrBufferView);
                if(result != Result.Success)
                    throw new VulkanCommandException(nameof(vkCreateBufferView), result);
            }
            return view;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bufferView">ExternSync</param>
        /// <param name="allocator">Optional</param>
        public static void DestroyBufferView(Device device, BufferView bufferView, AllocationCallbacks allocator = null)
        {
            vkDestroyBufferView(device.NativePointer, (bufferView != null) ? bufferView.NativePointer : 0, (allocator != null) ? allocator.NativePointer : null);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="allocator">Optional</param>
        public static Image CreateImage(Device device, ImageCreateInfo createInfo, AllocationCallbacks allocator = null)
        {
            var image = new Image();
            fixed(UInt64* ptrImage = &image.NativePointer)
            {
                var result = vkCreateImage(device.NativePointer, createInfo.NativePointer, (allocator != null) ? allocator.NativePointer : null, ptrImage);
                if(result != Result.Success)
                    throw new VulkanCommandException(nameof(vkCreateImage), result);
            }
            return image;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="image">ExternSync</param>
        /// <param name="allocator">Optional</param>
        public static void DestroyImage(Device device, Image image, AllocationCallbacks allocator = null)
        {
            vkDestroyImage(device.NativePointer, (image != null) ? image.NativePointer : 0, (allocator != null) ? allocator.NativePointer : null);
        }
        
        /// <summary>
        /// 
        /// </summary>
        public static SubresourceLayout GetImageSubresourceLayout(Device device, Image image, ImageSubresource subresource)
        {
            var layout = new SubresourceLayout();
            vkGetImageSubresourceLayout(device.NativePointer, image.NativePointer, &subresource, &layout);
            return layout;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="allocator">Optional</param>
        public static ImageView CreateImageView(Device device, ImageViewCreateInfo createInfo, AllocationCallbacks allocator = null)
        {
            var view = new ImageView();
            fixed(UInt64* ptrImageView = &view.NativePointer)
            {
                var result = vkCreateImageView(device.NativePointer, createInfo.NativePointer, (allocator != null) ? allocator.NativePointer : null, ptrImageView);
                if(result != Result.Success)
                    throw new VulkanCommandException(nameof(vkCreateImageView), result);
            }
            return view;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageView">ExternSync</param>
        /// <param name="allocator">Optional</param>
        public static void DestroyImageView(Device device, ImageView imageView, AllocationCallbacks allocator = null)
        {
            vkDestroyImageView(device.NativePointer, (imageView != null) ? imageView.NativePointer : 0, (allocator != null) ? allocator.NativePointer : null);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="allocator">Optional</param>
        public static ShaderModule CreateShaderModule(Device device, ShaderModuleCreateInfo createInfo, AllocationCallbacks allocator = null)
        {
            var shaderModule = new ShaderModule();
            fixed(UInt64* ptrShaderModule = &shaderModule.NativePointer)
            {
                var result = vkCreateShaderModule(device.NativePointer, createInfo.NativePointer, (allocator != null) ? allocator.NativePointer : null, ptrShaderModule);
                if(result != Result.Success)
                    throw new VulkanCommandException(nameof(vkCreateShaderModule), result);
            }
            return shaderModule;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="shaderModule">ExternSync</param>
        /// <param name="allocator">Optional</param>
        public static void DestroyShaderModule(Device device, ShaderModule shaderModule, AllocationCallbacks allocator = null)
        {
            vkDestroyShaderModule(device.NativePointer, (shaderModule != null) ? shaderModule.NativePointer : 0, (allocator != null) ? allocator.NativePointer : null);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="allocator">Optional</param>
        public static PipelineCache CreatePipelineCache(Device device, PipelineCacheCreateInfo createInfo, AllocationCallbacks allocator = null)
        {
            var pipelineCache = new PipelineCache();
            fixed(UInt64* ptrPipelineCache = &pipelineCache.NativePointer)
            {
                var result = vkCreatePipelineCache(device.NativePointer, createInfo.NativePointer, (allocator != null) ? allocator.NativePointer : null, ptrPipelineCache);
                if(result != Result.Success)
                    throw new VulkanCommandException(nameof(vkCreatePipelineCache), result);
            }
            return pipelineCache;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pipelineCache">ExternSync</param>
        /// <param name="allocator">Optional</param>
        public static void DestroyPipelineCache(Device device, PipelineCache pipelineCache, AllocationCallbacks allocator = null)
        {
            vkDestroyPipelineCache(device.NativePointer, (pipelineCache != null) ? pipelineCache.NativePointer : 0, (allocator != null) ? allocator.NativePointer : null);
        }
        
        /// <summary>
        /// 
        /// </summary>
        public static List<IntPtr> GetPipelineCacheData(Device device, PipelineCache pipelineCache)
        {
            UInt32 listLength;
            var result = vkGetPipelineCacheData(device.NativePointer, pipelineCache.NativePointer, &listLength, null);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkGetPipelineCacheData), result);
            
            var arrayIntPtr = new IntPtr[listLength];
            fixed(IntPtr* resultPtr = &arrayIntPtr[0])
                result = vkGetPipelineCacheData(device.NativePointer, pipelineCache.NativePointer, &listLength, resultPtr);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkGetPipelineCacheData), result);
            
            var list = new List<IntPtr>();
            for(var x = 0; x < listLength; x++)
            {
                list.Add(arrayIntPtr[x]);
            }
            
            return list;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dstCache">ExternSync</param>
        public static void MergePipelineCaches(Device device, PipelineCache dstCache, List<PipelineCache> srcCaches)
        {
            var srcCacheCount = (srcCaches != null) ? (UInt32)srcCaches.Count : 0;
            var _srcCachesPtr = (UInt64*)IntPtr.Zero;
            if(srcCacheCount != 0)
            {
                var _srcCachesSize = Marshal.SizeOf(typeof(IntPtr));
                _srcCachesPtr = (UInt64*)Marshal.AllocHGlobal((int)(_srcCachesSize * srcCacheCount));
                for(var x = 0; x < srcCacheCount; x++)
                    _srcCachesPtr[x] = srcCaches[x].NativePointer;
            }
            
            var result = vkMergePipelineCaches(device.NativePointer, dstCache.NativePointer, srcCacheCount, _srcCachesPtr);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkMergePipelineCaches), result);
            Marshal.FreeHGlobal((IntPtr)_srcCachesPtr);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pipelineCache">Optional</param>
        /// <param name="allocator">Optional</param>
        public static List<Pipeline> CreateGraphicsPipelines(Device device, PipelineCache pipelineCache, List<GraphicsPipelineCreateInfo> createInfos, AllocationCallbacks allocator = null)
        {
            var createInfoCount = (createInfos != null) ? (UInt32)createInfos.Count : 0;
            var _createInfosPtr = (Unmanaged.GraphicsPipelineCreateInfo*)IntPtr.Zero;
            if(createInfoCount != 0)
            {
                var _createInfosSize = Marshal.SizeOf(typeof(Unmanaged.GraphicsPipelineCreateInfo));
                _createInfosPtr = (Unmanaged.GraphicsPipelineCreateInfo*)Marshal.AllocHGlobal((int)(_createInfosSize * createInfoCount));
                for(var x = 0; x < createInfoCount; x++)
                    _createInfosPtr[x] = *createInfos[x].NativePointer;
            }
            
            var listLength = createInfoCount;
            Result result;
            
            var arrayPipeline = new UInt64[listLength];
            fixed(UInt64* resultPtr = &arrayPipeline[0])
                result = vkCreateGraphicsPipelines(device.NativePointer, (pipelineCache != null) ? pipelineCache.NativePointer : 0, listLength, _createInfosPtr, (allocator != null) ? allocator.NativePointer : null, resultPtr);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkCreateGraphicsPipelines), result);
            Marshal.FreeHGlobal((IntPtr)_createInfosPtr);
            
            var list = new List<Pipeline>();
            for(var x = 0; x < listLength; x++)
            {
                var item = new Pipeline();
                item.NativePointer = arrayPipeline[x];
                list.Add(item);
            }
            
            return list;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pipelineCache">Optional</param>
        /// <param name="allocator">Optional</param>
        public static List<Pipeline> CreateComputePipelines(Device device, PipelineCache pipelineCache, List<ComputePipelineCreateInfo> createInfos, AllocationCallbacks allocator = null)
        {
            var createInfoCount = (createInfos != null) ? (UInt32)createInfos.Count : 0;
            var _createInfosPtr = (Unmanaged.ComputePipelineCreateInfo*)IntPtr.Zero;
            if(createInfoCount != 0)
            {
                var _createInfosSize = Marshal.SizeOf(typeof(Unmanaged.ComputePipelineCreateInfo));
                _createInfosPtr = (Unmanaged.ComputePipelineCreateInfo*)Marshal.AllocHGlobal((int)(_createInfosSize * createInfoCount));
                for(var x = 0; x < createInfoCount; x++)
                    _createInfosPtr[x] = *createInfos[x].NativePointer;
            }
            
            var listLength = createInfoCount;
            Result result;
            
            var arrayPipeline = new UInt64[listLength];
            fixed(UInt64* resultPtr = &arrayPipeline[0])
                result = vkCreateComputePipelines(device.NativePointer, (pipelineCache != null) ? pipelineCache.NativePointer : 0, listLength, _createInfosPtr, (allocator != null) ? allocator.NativePointer : null, resultPtr);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkCreateComputePipelines), result);
            Marshal.FreeHGlobal((IntPtr)_createInfosPtr);
            
            var list = new List<Pipeline>();
            for(var x = 0; x < listLength; x++)
            {
                var item = new Pipeline();
                item.NativePointer = arrayPipeline[x];
                list.Add(item);
            }
            
            return list;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pipeline">ExternSync</param>
        /// <param name="allocator">Optional</param>
        public static void DestroyPipeline(Device device, Pipeline pipeline, AllocationCallbacks allocator = null)
        {
            vkDestroyPipeline(device.NativePointer, (pipeline != null) ? pipeline.NativePointer : 0, (allocator != null) ? allocator.NativePointer : null);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="allocator">Optional</param>
        public static PipelineLayout CreatePipelineLayout(Device device, PipelineLayoutCreateInfo createInfo, AllocationCallbacks allocator = null)
        {
            var pipelineLayout = new PipelineLayout();
            fixed(UInt64* ptrPipelineLayout = &pipelineLayout.NativePointer)
            {
                var result = vkCreatePipelineLayout(device.NativePointer, createInfo.NativePointer, (allocator != null) ? allocator.NativePointer : null, ptrPipelineLayout);
                if(result != Result.Success)
                    throw new VulkanCommandException(nameof(vkCreatePipelineLayout), result);
            }
            return pipelineLayout;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pipelineLayout">ExternSync</param>
        /// <param name="allocator">Optional</param>
        public static void DestroyPipelineLayout(Device device, PipelineLayout pipelineLayout, AllocationCallbacks allocator = null)
        {
            vkDestroyPipelineLayout(device.NativePointer, (pipelineLayout != null) ? pipelineLayout.NativePointer : 0, (allocator != null) ? allocator.NativePointer : null);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="allocator">Optional</param>
        public static Sampler CreateSampler(Device device, SamplerCreateInfo createInfo, AllocationCallbacks allocator = null)
        {
            var sampler = new Sampler();
            fixed(UInt64* ptrSampler = &sampler.NativePointer)
            {
                var result = vkCreateSampler(device.NativePointer, createInfo.NativePointer, (allocator != null) ? allocator.NativePointer : null, ptrSampler);
                if(result != Result.Success)
                    throw new VulkanCommandException(nameof(vkCreateSampler), result);
            }
            return sampler;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sampler">ExternSync</param>
        /// <param name="allocator">Optional</param>
        public static void DestroySampler(Device device, Sampler sampler, AllocationCallbacks allocator = null)
        {
            vkDestroySampler(device.NativePointer, (sampler != null) ? sampler.NativePointer : 0, (allocator != null) ? allocator.NativePointer : null);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="allocator">Optional</param>
        public static DescriptorSetLayout CreateDescriptorSetLayout(Device device, DescriptorSetLayoutCreateInfo createInfo, AllocationCallbacks allocator = null)
        {
            var setLayout = new DescriptorSetLayout();
            fixed(UInt64* ptrDescriptorSetLayout = &setLayout.NativePointer)
            {
                var result = vkCreateDescriptorSetLayout(device.NativePointer, createInfo.NativePointer, (allocator != null) ? allocator.NativePointer : null, ptrDescriptorSetLayout);
                if(result != Result.Success)
                    throw new VulkanCommandException(nameof(vkCreateDescriptorSetLayout), result);
            }
            return setLayout;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="descriptorSetLayout">ExternSync</param>
        /// <param name="allocator">Optional</param>
        public static void DestroyDescriptorSetLayout(Device device, DescriptorSetLayout descriptorSetLayout, AllocationCallbacks allocator = null)
        {
            vkDestroyDescriptorSetLayout(device.NativePointer, (descriptorSetLayout != null) ? descriptorSetLayout.NativePointer : 0, (allocator != null) ? allocator.NativePointer : null);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="allocator">Optional</param>
        public static DescriptorPool CreateDescriptorPool(Device device, DescriptorPoolCreateInfo createInfo, AllocationCallbacks allocator = null)
        {
            var descriptorPool = new DescriptorPool();
            fixed(UInt64* ptrDescriptorPool = &descriptorPool.NativePointer)
            {
                var result = vkCreateDescriptorPool(device.NativePointer, createInfo.NativePointer, (allocator != null) ? allocator.NativePointer : null, ptrDescriptorPool);
                if(result != Result.Success)
                    throw new VulkanCommandException(nameof(vkCreateDescriptorPool), result);
            }
            return descriptorPool;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="descriptorPool">ExternSync</param>
        /// <param name="allocator">Optional</param>
        public static void DestroyDescriptorPool(Device device, DescriptorPool descriptorPool, AllocationCallbacks allocator = null)
        {
            vkDestroyDescriptorPool(device.NativePointer, (descriptorPool != null) ? descriptorPool.NativePointer : 0, (allocator != null) ? allocator.NativePointer : null);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="descriptorPool">ExternSync</param>
        /// <param name="flags">Optional</param>
        public static void ResetDescriptorPool(Device device, DescriptorPool descriptorPool, DescriptorPoolResetFlags flags)
        {
            var result = vkResetDescriptorPool(device.NativePointer, descriptorPool.NativePointer, flags);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkResetDescriptorPool), result);
        }
        
        /// <summary>
        /// 
        /// </summary>
        public static List<DescriptorSet> AllocateDescriptorSets(Device device, DescriptorSetAllocateInfo allocateInfo)
        {
            var listLength = allocateInfo.NativePointer->DescriptorSetCount;
            Result result;
            
            var arrayDescriptorSet = new UInt64[listLength];
            fixed(UInt64* resultPtr = &arrayDescriptorSet[0])
                result = vkAllocateDescriptorSets(device.NativePointer, allocateInfo.NativePointer, resultPtr);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkAllocateDescriptorSets), result);
            
            var list = new List<DescriptorSet>();
            for(var x = 0; x < listLength; x++)
            {
                var item = new DescriptorSet();
                item.NativePointer = arrayDescriptorSet[x];
                list.Add(item);
            }
            
            return list;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="descriptorPool">ExternSync</param>
        /// <param name="descriptorSets">No Auto Validity</param>
        public static void FreeDescriptorSets(Device device, DescriptorPool descriptorPool, List<DescriptorSet> descriptorSets)
        {
            var descriptorSetCount = (descriptorSets != null) ? (UInt32)descriptorSets.Count : 0;
            var _descriptorSetsPtr = (UInt64*)IntPtr.Zero;
            if(descriptorSetCount != 0)
            {
                var _descriptorSetsSize = Marshal.SizeOf(typeof(IntPtr));
                _descriptorSetsPtr = (UInt64*)Marshal.AllocHGlobal((int)(_descriptorSetsSize * descriptorSetCount));
                for(var x = 0; x < descriptorSetCount; x++)
                    _descriptorSetsPtr[x] = descriptorSets[x].NativePointer;
            }
            
            var result = vkFreeDescriptorSets(device.NativePointer, descriptorPool.NativePointer, descriptorSetCount, _descriptorSetsPtr);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkFreeDescriptorSets), result);
            Marshal.FreeHGlobal((IntPtr)_descriptorSetsPtr);
        }
        
        /// <summary>
        /// 
        /// </summary>
        public static void UpdateDescriptorSets(Device device, List<WriteDescriptorSet> descriptorWrites, List<CopyDescriptorSet> descriptorCopies)
        {
            var descriptorWriteCount = (descriptorWrites != null) ? (UInt32)descriptorWrites.Count : 0;
            var _descriptorWritesPtr = (Unmanaged.WriteDescriptorSet*)IntPtr.Zero;
            if(descriptorWriteCount != 0)
            {
                var _descriptorWritesSize = Marshal.SizeOf(typeof(Unmanaged.WriteDescriptorSet));
                _descriptorWritesPtr = (Unmanaged.WriteDescriptorSet*)Marshal.AllocHGlobal((int)(_descriptorWritesSize * descriptorWriteCount));
                for(var x = 0; x < descriptorWriteCount; x++)
                    _descriptorWritesPtr[x] = *descriptorWrites[x].NativePointer;
            }
            
            var descriptorCopyCount = (descriptorCopies != null) ? (UInt32)descriptorCopies.Count : 0;
            var _descriptorCopiesPtr = (Unmanaged.CopyDescriptorSet*)IntPtr.Zero;
            if(descriptorCopyCount != 0)
            {
                var _descriptorCopiesSize = Marshal.SizeOf(typeof(Unmanaged.CopyDescriptorSet));
                _descriptorCopiesPtr = (Unmanaged.CopyDescriptorSet*)Marshal.AllocHGlobal((int)(_descriptorCopiesSize * descriptorCopyCount));
                for(var x = 0; x < descriptorCopyCount; x++)
                    _descriptorCopiesPtr[x] = *descriptorCopies[x].NativePointer;
            }
            
            vkUpdateDescriptorSets(device.NativePointer, descriptorWriteCount, _descriptorWritesPtr, descriptorCopyCount, _descriptorCopiesPtr);
            Marshal.FreeHGlobal((IntPtr)_descriptorWritesPtr);
            Marshal.FreeHGlobal((IntPtr)_descriptorCopiesPtr);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="allocator">Optional</param>
        public static Framebuffer CreateFramebuffer(Device device, FramebufferCreateInfo createInfo, AllocationCallbacks allocator = null)
        {
            var framebuffer = new Framebuffer();
            fixed(UInt64* ptrFramebuffer = &framebuffer.NativePointer)
            {
                var result = vkCreateFramebuffer(device.NativePointer, createInfo.NativePointer, (allocator != null) ? allocator.NativePointer : null, ptrFramebuffer);
                if(result != Result.Success)
                    throw new VulkanCommandException(nameof(vkCreateFramebuffer), result);
            }
            return framebuffer;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="framebuffer">ExternSync</param>
        /// <param name="allocator">Optional</param>
        public static void DestroyFramebuffer(Device device, Framebuffer framebuffer, AllocationCallbacks allocator = null)
        {
            vkDestroyFramebuffer(device.NativePointer, (framebuffer != null) ? framebuffer.NativePointer : 0, (allocator != null) ? allocator.NativePointer : null);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="allocator">Optional</param>
        public static RenderPass CreateRenderPass(Device device, RenderPassCreateInfo createInfo, AllocationCallbacks allocator = null)
        {
            var renderPass = new RenderPass();
            fixed(UInt64* ptrRenderPass = &renderPass.NativePointer)
            {
                var result = vkCreateRenderPass(device.NativePointer, createInfo.NativePointer, (allocator != null) ? allocator.NativePointer : null, ptrRenderPass);
                if(result != Result.Success)
                    throw new VulkanCommandException(nameof(vkCreateRenderPass), result);
            }
            return renderPass;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="renderPass">ExternSync</param>
        /// <param name="allocator">Optional</param>
        public static void DestroyRenderPass(Device device, RenderPass renderPass, AllocationCallbacks allocator = null)
        {
            vkDestroyRenderPass(device.NativePointer, (renderPass != null) ? renderPass.NativePointer : 0, (allocator != null) ? allocator.NativePointer : null);
        }
        
        /// <summary>
        /// 
        /// </summary>
        public static Extent2D GetRenderAreaGranularity(Device device, RenderPass renderPass)
        {
            var granularity = new Extent2D();
            vkGetRenderAreaGranularity(device.NativePointer, renderPass.NativePointer, &granularity);
            return granularity;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="allocator">Optional</param>
        public static CommandPool CreateCommandPool(Device device, CommandPoolCreateInfo createInfo, AllocationCallbacks allocator = null)
        {
            var commandPool = new CommandPool();
            fixed(UInt64* ptrCommandPool = &commandPool.NativePointer)
            {
                var result = vkCreateCommandPool(device.NativePointer, createInfo.NativePointer, (allocator != null) ? allocator.NativePointer : null, ptrCommandPool);
                if(result != Result.Success)
                    throw new VulkanCommandException(nameof(vkCreateCommandPool), result);
            }
            return commandPool;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandPool">ExternSync</param>
        /// <param name="allocator">Optional</param>
        public static void DestroyCommandPool(Device device, CommandPool commandPool, AllocationCallbacks allocator = null)
        {
            vkDestroyCommandPool(device.NativePointer, (commandPool != null) ? commandPool.NativePointer : 0, (allocator != null) ? allocator.NativePointer : null);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandPool">ExternSync</param>
        /// <param name="flags">Optional</param>
        public static void ResetCommandPool(Device device, CommandPool commandPool, CommandPoolResetFlags flags)
        {
            var result = vkResetCommandPool(device.NativePointer, commandPool.NativePointer, flags);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkResetCommandPool), result);
        }
        
        /// <summary>
        /// 
        /// </summary>
        public static List<CommandBuffer> AllocateCommandBuffers(Device device, CommandBufferAllocateInfo allocateInfo)
        {
            var listLength = allocateInfo.NativePointer->CommandBufferCount;
            Result result;
            
            var arrayCommandBuffer = new IntPtr[listLength];
            fixed(IntPtr* resultPtr = &arrayCommandBuffer[0])
                result = vkAllocateCommandBuffers(device.NativePointer, allocateInfo.NativePointer, resultPtr);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkAllocateCommandBuffers), result);
            
            var list = new List<CommandBuffer>();
            for(var x = 0; x < listLength; x++)
            {
                var item = new CommandBuffer();
                item.NativePointer = arrayCommandBuffer[x];
                list.Add(item);
            }
            
            return list;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandPool">ExternSync</param>
        /// <param name="commandBuffers">No Auto Validity</param>
        public static void FreeCommandBuffers(Device device, CommandPool commandPool, List<CommandBuffer> commandBuffers)
        {
            var commandBufferCount = (commandBuffers != null) ? (UInt32)commandBuffers.Count : 0;
            var _commandBuffersPtr = (IntPtr*)IntPtr.Zero;
            if(commandBufferCount != 0)
            {
                var _commandBuffersSize = Marshal.SizeOf(typeof(IntPtr));
                _commandBuffersPtr = (IntPtr*)Marshal.AllocHGlobal((int)(_commandBuffersSize * commandBufferCount));
                for(var x = 0; x < commandBufferCount; x++)
                    _commandBuffersPtr[x] = commandBuffers[x].NativePointer;
            }
            
            vkFreeCommandBuffers(device.NativePointer, commandPool.NativePointer, commandBufferCount, _commandBuffersPtr);
            Marshal.FreeHGlobal((IntPtr)_commandBuffersPtr);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandBuffer">ExternSync</param>
        public static void BeginCommandBuffer(CommandBuffer commandBuffer, CommandBufferBeginInfo beginInfo)
        {
            var result = vkBeginCommandBuffer(commandBuffer.NativePointer, beginInfo.NativePointer);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkBeginCommandBuffer), result);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandBuffer">ExternSync</param>
        public static void EndCommandBuffer(CommandBuffer commandBuffer)
        {
            var result = vkEndCommandBuffer(commandBuffer.NativePointer);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkEndCommandBuffer), result);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandBuffer">ExternSync</param>
        /// <param name="flags">Optional</param>
        public static void ResetCommandBuffer(CommandBuffer commandBuffer, CommandBufferResetFlags flags)
        {
            var result = vkResetCommandBuffer(commandBuffer.NativePointer, flags);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkResetCommandBuffer), result);
        }
        
        /// <summary>
        /// [<see cref="CommandBufferLevel"/>: primary, secondary] [<see cref="QueueFlags"/>: graphics, compute] [Render Pass: both] 
        /// </summary>
        /// <param name="commandBuffer">ExternSync</param>
        public static void CmdBindPipeline(CommandBuffer commandBuffer, PipelineBindPoint pipelineBindPoint, Pipeline pipeline)
        {
            vkCmdBindPipeline(commandBuffer.NativePointer, pipelineBindPoint, pipeline.NativePointer);
        }
        
        /// <summary>
        /// [<see cref="CommandBufferLevel"/>: primary, secondary] [<see cref="QueueFlags"/>: graphics] [Render Pass: both] 
        /// </summary>
        /// <param name="commandBuffer">ExternSync</param>
        public static void CmdSetViewport(CommandBuffer commandBuffer, UInt32 firstViewport, List<Viewport> viewports)
        {
            var viewportCount = (viewports != null) ? (UInt32)viewports.Count : 0;
            var _viewportsPtr = (Viewport*)IntPtr.Zero;
            if(viewportCount != 0)
            {
                var _viewportsSize = Marshal.SizeOf(typeof(Viewport));
                _viewportsPtr = (Viewport*)Marshal.AllocHGlobal((int)(_viewportsSize * viewportCount));
                for(var x = 0; x < viewportCount; x++)
                    _viewportsPtr[x] = viewports[x];
            }
            
            vkCmdSetViewport(commandBuffer.NativePointer, firstViewport, viewportCount, _viewportsPtr);
            Marshal.FreeHGlobal((IntPtr)_viewportsPtr);
        }
        
        /// <summary>
        /// [<see cref="CommandBufferLevel"/>: primary, secondary] [<see cref="QueueFlags"/>: graphics] [Render Pass: both] 
        /// </summary>
        /// <param name="commandBuffer">ExternSync</param>
        public static void CmdSetScissor(CommandBuffer commandBuffer, UInt32 firstScissor, List<Rect2D> scissors)
        {
            var scissorCount = (scissors != null) ? (UInt32)scissors.Count : 0;
            var _scissorsPtr = (Rect2D*)IntPtr.Zero;
            if(scissorCount != 0)
            {
                var _scissorsSize = Marshal.SizeOf(typeof(Rect2D));
                _scissorsPtr = (Rect2D*)Marshal.AllocHGlobal((int)(_scissorsSize * scissorCount));
                for(var x = 0; x < scissorCount; x++)
                    _scissorsPtr[x] = scissors[x];
            }
            
            vkCmdSetScissor(commandBuffer.NativePointer, firstScissor, scissorCount, _scissorsPtr);
            Marshal.FreeHGlobal((IntPtr)_scissorsPtr);
        }
        
        /// <summary>
        /// [<see cref="CommandBufferLevel"/>: primary, secondary] [<see cref="QueueFlags"/>: graphics] [Render Pass: both] 
        /// </summary>
        /// <param name="commandBuffer">ExternSync</param>
        public static void CmdSetLineWidth(CommandBuffer commandBuffer, Single lineWidth)
        {
            vkCmdSetLineWidth(commandBuffer.NativePointer, lineWidth);
        }
        
        /// <summary>
        /// [<see cref="CommandBufferLevel"/>: primary, secondary] [<see cref="QueueFlags"/>: graphics] [Render Pass: both] 
        /// </summary>
        /// <param name="commandBuffer">ExternSync</param>
        public static void CmdSetDepthBias(CommandBuffer commandBuffer, Single depthBiasConstantFactor, Single depthBiasClamp, Single depthBiasSlopeFactor)
        {
            vkCmdSetDepthBias(commandBuffer.NativePointer, depthBiasConstantFactor, depthBiasClamp, depthBiasSlopeFactor);
        }
        
        /// <summary>
        /// [<see cref="CommandBufferLevel"/>: primary, secondary] [<see cref="QueueFlags"/>: graphics] [Render Pass: both] 
        /// </summary>
        /// <param name="commandBuffer">ExternSync</param>
        public static void CmdSetBlendConstants(CommandBuffer commandBuffer, Single blendConstants)
        {
            vkCmdSetBlendConstants(commandBuffer.NativePointer, blendConstants);
        }
        
        /// <summary>
        /// [<see cref="CommandBufferLevel"/>: primary, secondary] [<see cref="QueueFlags"/>: graphics] [Render Pass: both] 
        /// </summary>
        /// <param name="commandBuffer">ExternSync</param>
        public static void CmdSetDepthBounds(CommandBuffer commandBuffer, Single minDepthBounds, Single maxDepthBounds)
        {
            vkCmdSetDepthBounds(commandBuffer.NativePointer, minDepthBounds, maxDepthBounds);
        }
        
        /// <summary>
        /// [<see cref="CommandBufferLevel"/>: primary, secondary] [<see cref="QueueFlags"/>: graphics] [Render Pass: both] 
        /// </summary>
        /// <param name="commandBuffer">ExternSync</param>
        public static void CmdSetStencilCompareMask(CommandBuffer commandBuffer, StencilFaceFlags faceMask, UInt32 compareMask)
        {
            vkCmdSetStencilCompareMask(commandBuffer.NativePointer, faceMask, compareMask);
        }
        
        /// <summary>
        /// [<see cref="CommandBufferLevel"/>: primary, secondary] [<see cref="QueueFlags"/>: graphics] [Render Pass: both] 
        /// </summary>
        /// <param name="commandBuffer">ExternSync</param>
        public static void CmdSetStencilWriteMask(CommandBuffer commandBuffer, StencilFaceFlags faceMask, UInt32 writeMask)
        {
            vkCmdSetStencilWriteMask(commandBuffer.NativePointer, faceMask, writeMask);
        }
        
        /// <summary>
        /// [<see cref="CommandBufferLevel"/>: primary, secondary] [<see cref="QueueFlags"/>: graphics] [Render Pass: both] 
        /// </summary>
        /// <param name="commandBuffer">ExternSync</param>
        public static void CmdSetStencilReference(CommandBuffer commandBuffer, StencilFaceFlags faceMask, UInt32 reference)
        {
            vkCmdSetStencilReference(commandBuffer.NativePointer, faceMask, reference);
        }
        
        /// <summary>
        /// [<see cref="CommandBufferLevel"/>: primary, secondary] [<see cref="QueueFlags"/>: graphics, compute] [Render Pass: both] 
        /// </summary>
        /// <param name="commandBuffer">ExternSync</param>
        public static void CmdBindDescriptorSets(CommandBuffer commandBuffer, PipelineBindPoint pipelineBindPoint, PipelineLayout layout, UInt32 firstSet, List<DescriptorSet> descriptorSets, List<UInt32> dynamicOffsets)
        {
            var descriptorSetCount = (descriptorSets != null) ? (UInt32)descriptorSets.Count : 0;
            var _descriptorSetsPtr = (UInt64*)IntPtr.Zero;
            if(descriptorSetCount != 0)
            {
                var _descriptorSetsSize = Marshal.SizeOf(typeof(IntPtr));
                _descriptorSetsPtr = (UInt64*)Marshal.AllocHGlobal((int)(_descriptorSetsSize * descriptorSetCount));
                for(var x = 0; x < descriptorSetCount; x++)
                    _descriptorSetsPtr[x] = descriptorSets[x].NativePointer;
            }
            
            var dynamicOffsetCount = (dynamicOffsets != null) ? (UInt32)dynamicOffsets.Count : 0;
            var _dynamicOffsetsPtr = (UInt32*)IntPtr.Zero;
            if(dynamicOffsetCount != 0)
            {
                var _dynamicOffsetsSize = Marshal.SizeOf(typeof(UInt32));
                _dynamicOffsetsPtr = (UInt32*)Marshal.AllocHGlobal((int)(_dynamicOffsetsSize * dynamicOffsetCount));
                for(var x = 0; x < dynamicOffsetCount; x++)
                    _dynamicOffsetsPtr[x] = dynamicOffsets[x];
            }
            
            vkCmdBindDescriptorSets(commandBuffer.NativePointer, pipelineBindPoint, layout.NativePointer, firstSet, descriptorSetCount, _descriptorSetsPtr, dynamicOffsetCount, _dynamicOffsetsPtr);
            Marshal.FreeHGlobal((IntPtr)_descriptorSetsPtr);
            Marshal.FreeHGlobal((IntPtr)_dynamicOffsetsPtr);
        }
        
        /// <summary>
        /// [<see cref="CommandBufferLevel"/>: primary, secondary] [<see cref="QueueFlags"/>: graphics] [Render Pass: both] 
        /// </summary>
        /// <param name="commandBuffer">ExternSync</param>
        public static void CmdBindIndexBuffer(CommandBuffer commandBuffer, Buffer buffer, DeviceSize offset, IndexType indexType)
        {
            vkCmdBindIndexBuffer(commandBuffer.NativePointer, buffer.NativePointer, offset, indexType);
        }
        
        /// <summary>
        /// [<see cref="CommandBufferLevel"/>: primary, secondary] [<see cref="QueueFlags"/>: graphics] [Render Pass: both] 
        /// </summary>
        /// <param name="commandBuffer">ExternSync</param>
        public static void CmdBindVertexBuffers(CommandBuffer commandBuffer, UInt32 firstBinding, List<Buffer> buffers, List<DeviceSize> offsets)
        {
            var bindingCount = (buffers != null) ? (UInt32)buffers.Count : 0;
            var _buffersPtr = (UInt64*)IntPtr.Zero;
            if(bindingCount != 0)
            {
                var _buffersSize = Marshal.SizeOf(typeof(IntPtr));
                _buffersPtr = (UInt64*)Marshal.AllocHGlobal((int)(_buffersSize * bindingCount));
                for(var x = 0; x < bindingCount; x++)
                    _buffersPtr[x] = buffers[x].NativePointer;
            }
            
            var _offsetsPtr = (DeviceSize*)IntPtr.Zero;
            if(bindingCount != 0)
            {
                var _offsetsSize = Marshal.SizeOf(typeof(DeviceSize));
                _offsetsPtr = (DeviceSize*)Marshal.AllocHGlobal((int)(_offsetsSize * bindingCount));
                for(var x = 0; x < bindingCount; x++)
                    _offsetsPtr[x] = offsets[x];
            }
            
            vkCmdBindVertexBuffers(commandBuffer.NativePointer, firstBinding, bindingCount, _buffersPtr, _offsetsPtr);
            Marshal.FreeHGlobal((IntPtr)_buffersPtr);
            Marshal.FreeHGlobal((IntPtr)_offsetsPtr);
        }
        
        /// <summary>
        /// [<see cref="CommandBufferLevel"/>: primary, secondary] [<see cref="QueueFlags"/>: graphics] [Render Pass: inside] 
        /// </summary>
        /// <param name="commandBuffer">ExternSync</param>
        public static void CmdDraw(CommandBuffer commandBuffer, UInt32 vertexCount, UInt32 instanceCount, UInt32 firstVertex, UInt32 firstInstance)
        {
            vkCmdDraw(commandBuffer.NativePointer, vertexCount, instanceCount, firstVertex, firstInstance);
        }
        
        /// <summary>
        /// [<see cref="CommandBufferLevel"/>: primary, secondary] [<see cref="QueueFlags"/>: graphics] [Render Pass: inside] 
        /// </summary>
        /// <param name="commandBuffer">ExternSync</param>
        public static void CmdDrawIndexed(CommandBuffer commandBuffer, UInt32 indexCount, UInt32 instanceCount, UInt32 firstIndex, Int32 vertexOffset, UInt32 firstInstance)
        {
            vkCmdDrawIndexed(commandBuffer.NativePointer, indexCount, instanceCount, firstIndex, vertexOffset, firstInstance);
        }
        
        /// <summary>
        /// [<see cref="CommandBufferLevel"/>: primary, secondary] [<see cref="QueueFlags"/>: graphics] [Render Pass: inside] 
        /// </summary>
        /// <param name="commandBuffer">ExternSync</param>
        public static void CmdDrawIndirect(CommandBuffer commandBuffer, Buffer buffer, DeviceSize offset, UInt32 drawCount, UInt32 stride)
        {
            vkCmdDrawIndirect(commandBuffer.NativePointer, buffer.NativePointer, offset, drawCount, stride);
        }
        
        /// <summary>
        /// [<see cref="CommandBufferLevel"/>: primary, secondary] [<see cref="QueueFlags"/>: graphics] [Render Pass: inside] 
        /// </summary>
        /// <param name="commandBuffer">ExternSync</param>
        public static void CmdDrawIndexedIndirect(CommandBuffer commandBuffer, Buffer buffer, DeviceSize offset, UInt32 drawCount, UInt32 stride)
        {
            vkCmdDrawIndexedIndirect(commandBuffer.NativePointer, buffer.NativePointer, offset, drawCount, stride);
        }
        
        /// <summary>
        /// [<see cref="CommandBufferLevel"/>: primary, secondary] [<see cref="QueueFlags"/>: compute] [Render Pass: outside] 
        /// </summary>
        /// <param name="commandBuffer">ExternSync</param>
        public static void CmdDispatch(CommandBuffer commandBuffer, UInt32 x, UInt32 y, UInt32 z)
        {
            vkCmdDispatch(commandBuffer.NativePointer, x, y, z);
        }
        
        /// <summary>
        /// [<see cref="CommandBufferLevel"/>: primary, secondary] [<see cref="QueueFlags"/>: compute] [Render Pass: outside] 
        /// </summary>
        /// <param name="commandBuffer">ExternSync</param>
        public static void CmdDispatchIndirect(CommandBuffer commandBuffer, Buffer buffer, DeviceSize offset)
        {
            vkCmdDispatchIndirect(commandBuffer.NativePointer, buffer.NativePointer, offset);
        }
        
        /// <summary>
        /// [<see cref="CommandBufferLevel"/>: primary, secondary] [<see cref="QueueFlags"/>: transfer, graphics, compute] [Render Pass: outside] 
        /// </summary>
        /// <param name="commandBuffer">ExternSync</param>
        public static void CmdCopyBuffer(CommandBuffer commandBuffer, Buffer srcBuffer, Buffer dstBuffer, List<BufferCopy> regions)
        {
            var regionCount = (regions != null) ? (UInt32)regions.Count : 0;
            var _regionsPtr = (BufferCopy*)IntPtr.Zero;
            if(regionCount != 0)
            {
                var _regionsSize = Marshal.SizeOf(typeof(BufferCopy));
                _regionsPtr = (BufferCopy*)Marshal.AllocHGlobal((int)(_regionsSize * regionCount));
                for(var x = 0; x < regionCount; x++)
                    _regionsPtr[x] = regions[x];
            }
            
            vkCmdCopyBuffer(commandBuffer.NativePointer, srcBuffer.NativePointer, dstBuffer.NativePointer, regionCount, _regionsPtr);
            Marshal.FreeHGlobal((IntPtr)_regionsPtr);
        }
        
        /// <summary>
        /// [<see cref="CommandBufferLevel"/>: primary, secondary] [<see cref="QueueFlags"/>: transfer, graphics, compute] [Render Pass: outside] 
        /// </summary>
        /// <param name="commandBuffer">ExternSync</param>
        public static void CmdCopyImage(CommandBuffer commandBuffer, Image srcImage, ImageLayout srcImageLayout, Image dstImage, ImageLayout dstImageLayout, List<ImageCopy> regions)
        {
            var regionCount = (regions != null) ? (UInt32)regions.Count : 0;
            var _regionsPtr = (ImageCopy*)IntPtr.Zero;
            if(regionCount != 0)
            {
                var _regionsSize = Marshal.SizeOf(typeof(ImageCopy));
                _regionsPtr = (ImageCopy*)Marshal.AllocHGlobal((int)(_regionsSize * regionCount));
                for(var x = 0; x < regionCount; x++)
                    _regionsPtr[x] = regions[x];
            }
            
            vkCmdCopyImage(commandBuffer.NativePointer, srcImage.NativePointer, srcImageLayout, dstImage.NativePointer, dstImageLayout, regionCount, _regionsPtr);
            Marshal.FreeHGlobal((IntPtr)_regionsPtr);
        }
        
        /// <summary>
        /// [<see cref="CommandBufferLevel"/>: primary, secondary] [<see cref="QueueFlags"/>: graphics] [Render Pass: outside] 
        /// </summary>
        /// <param name="commandBuffer">ExternSync</param>
        public static void CmdBlitImage(CommandBuffer commandBuffer, Image srcImage, ImageLayout srcImageLayout, Image dstImage, ImageLayout dstImageLayout, List<ImageBlit> regions, Filter filter)
        {
            var regionCount = (regions != null) ? (UInt32)regions.Count : 0;
            var _regionsPtr = (Unmanaged.ImageBlit*)IntPtr.Zero;
            if(regionCount != 0)
            {
                var _regionsSize = Marshal.SizeOf(typeof(Unmanaged.ImageBlit));
                _regionsPtr = (Unmanaged.ImageBlit*)Marshal.AllocHGlobal((int)(_regionsSize * regionCount));
                for(var x = 0; x < regionCount; x++)
                    _regionsPtr[x] = *regions[x].NativePointer;
            }
            
            vkCmdBlitImage(commandBuffer.NativePointer, srcImage.NativePointer, srcImageLayout, dstImage.NativePointer, dstImageLayout, regionCount, _regionsPtr, filter);
            Marshal.FreeHGlobal((IntPtr)_regionsPtr);
        }
        
        /// <summary>
        /// [<see cref="CommandBufferLevel"/>: primary, secondary] [<see cref="QueueFlags"/>: transfer, graphics, compute] [Render Pass: outside] 
        /// </summary>
        /// <param name="commandBuffer">ExternSync</param>
        public static void CmdCopyBufferToImage(CommandBuffer commandBuffer, Buffer srcBuffer, Image dstImage, ImageLayout dstImageLayout, List<BufferImageCopy> regions)
        {
            var regionCount = (regions != null) ? (UInt32)regions.Count : 0;
            var _regionsPtr = (BufferImageCopy*)IntPtr.Zero;
            if(regionCount != 0)
            {
                var _regionsSize = Marshal.SizeOf(typeof(BufferImageCopy));
                _regionsPtr = (BufferImageCopy*)Marshal.AllocHGlobal((int)(_regionsSize * regionCount));
                for(var x = 0; x < regionCount; x++)
                    _regionsPtr[x] = regions[x];
            }
            
            vkCmdCopyBufferToImage(commandBuffer.NativePointer, srcBuffer.NativePointer, dstImage.NativePointer, dstImageLayout, regionCount, _regionsPtr);
            Marshal.FreeHGlobal((IntPtr)_regionsPtr);
        }
        
        /// <summary>
        /// [<see cref="CommandBufferLevel"/>: primary, secondary] [<see cref="QueueFlags"/>: transfer, graphics, compute] [Render Pass: outside] 
        /// </summary>
        /// <param name="commandBuffer">ExternSync</param>
        public static void CmdCopyImageToBuffer(CommandBuffer commandBuffer, Image srcImage, ImageLayout srcImageLayout, Buffer dstBuffer, List<BufferImageCopy> regions)
        {
            var regionCount = (regions != null) ? (UInt32)regions.Count : 0;
            var _regionsPtr = (BufferImageCopy*)IntPtr.Zero;
            if(regionCount != 0)
            {
                var _regionsSize = Marshal.SizeOf(typeof(BufferImageCopy));
                _regionsPtr = (BufferImageCopy*)Marshal.AllocHGlobal((int)(_regionsSize * regionCount));
                for(var x = 0; x < regionCount; x++)
                    _regionsPtr[x] = regions[x];
            }
            
            vkCmdCopyImageToBuffer(commandBuffer.NativePointer, srcImage.NativePointer, srcImageLayout, dstBuffer.NativePointer, regionCount, _regionsPtr);
            Marshal.FreeHGlobal((IntPtr)_regionsPtr);
        }
        
        /// <summary>
        /// [<see cref="CommandBufferLevel"/>: primary, secondary] [<see cref="QueueFlags"/>: transfer, graphics, compute] [Render Pass: outside] 
        /// </summary>
        /// <param name="commandBuffer">ExternSync</param>
        public static void CmdUpdateBuffer(CommandBuffer commandBuffer, Buffer dstBuffer, DeviceSize dstOffset, List<Byte> data)
        {
            var dataSize = (data != null) ? (UInt32)data.Count : 0;
            var _dataPtr = (Byte*)IntPtr.Zero;
            if(dataSize != 0)
            {
                var _dataSize = Marshal.SizeOf(typeof(Byte));
                _dataPtr = (Byte*)Marshal.AllocHGlobal((int)(_dataSize * dataSize));
                for(var x = 0; x < dataSize; x++)
                    _dataPtr[x] = data[x];
            }
            
            vkCmdUpdateBuffer(commandBuffer.NativePointer, dstBuffer.NativePointer, dstOffset, dataSize, _dataPtr);
            Marshal.FreeHGlobal((IntPtr)_dataPtr);
        }
        
        /// <summary>
        /// [<see cref="CommandBufferLevel"/>: primary, secondary] [<see cref="QueueFlags"/>: graphics, compute] [Render Pass: outside] 
        /// </summary>
        /// <param name="commandBuffer">ExternSync</param>
        public static void CmdFillBuffer(CommandBuffer commandBuffer, Buffer dstBuffer, DeviceSize dstOffset, DeviceSize size, UInt32 data)
        {
            vkCmdFillBuffer(commandBuffer.NativePointer, dstBuffer.NativePointer, dstOffset, size, data);
        }
        
        /// <summary>
        /// [<see cref="CommandBufferLevel"/>: primary, secondary] [<see cref="QueueFlags"/>: graphics, compute] [Render Pass: outside] 
        /// </summary>
        /// <param name="commandBuffer">ExternSync</param>
        public static void CmdClearColorImage(CommandBuffer commandBuffer, Image image, ImageLayout imageLayout, ClearColorValue color, List<ImageSubresourceRange> ranges)
        {
            var rangeCount = (ranges != null) ? (UInt32)ranges.Count : 0;
            var _rangesPtr = (ImageSubresourceRange*)IntPtr.Zero;
            if(rangeCount != 0)
            {
                var _rangesSize = Marshal.SizeOf(typeof(ImageSubresourceRange));
                _rangesPtr = (ImageSubresourceRange*)Marshal.AllocHGlobal((int)(_rangesSize * rangeCount));
                for(var x = 0; x < rangeCount; x++)
                    _rangesPtr[x] = ranges[x];
            }
            
            vkCmdClearColorImage(commandBuffer.NativePointer, image.NativePointer, imageLayout, &color, rangeCount, _rangesPtr);
            Marshal.FreeHGlobal((IntPtr)_rangesPtr);
        }
        
        /// <summary>
        /// [<see cref="CommandBufferLevel"/>: primary, secondary] [<see cref="QueueFlags"/>: graphics] [Render Pass: outside] 
        /// </summary>
        /// <param name="commandBuffer">ExternSync</param>
        public static void CmdClearDepthStencilImage(CommandBuffer commandBuffer, Image image, ImageLayout imageLayout, ClearDepthStencilValue depthStencil, List<ImageSubresourceRange> ranges)
        {
            var rangeCount = (ranges != null) ? (UInt32)ranges.Count : 0;
            var _rangesPtr = (ImageSubresourceRange*)IntPtr.Zero;
            if(rangeCount != 0)
            {
                var _rangesSize = Marshal.SizeOf(typeof(ImageSubresourceRange));
                _rangesPtr = (ImageSubresourceRange*)Marshal.AllocHGlobal((int)(_rangesSize * rangeCount));
                for(var x = 0; x < rangeCount; x++)
                    _rangesPtr[x] = ranges[x];
            }
            
            vkCmdClearDepthStencilImage(commandBuffer.NativePointer, image.NativePointer, imageLayout, &depthStencil, rangeCount, _rangesPtr);
            Marshal.FreeHGlobal((IntPtr)_rangesPtr);
        }
        
        /// <summary>
        /// [<see cref="CommandBufferLevel"/>: primary, secondary] [<see cref="QueueFlags"/>: graphics] [Render Pass: inside] 
        /// </summary>
        /// <param name="commandBuffer">ExternSync</param>
        public static void CmdClearAttachments(CommandBuffer commandBuffer, List<ClearAttachment> attachments, List<ClearRect> rects)
        {
            var attachmentCount = (attachments != null) ? (UInt32)attachments.Count : 0;
            var _attachmentsPtr = (ClearAttachment*)IntPtr.Zero;
            if(attachmentCount != 0)
            {
                var _attachmentsSize = Marshal.SizeOf(typeof(ClearAttachment));
                _attachmentsPtr = (ClearAttachment*)Marshal.AllocHGlobal((int)(_attachmentsSize * attachmentCount));
                for(var x = 0; x < attachmentCount; x++)
                    _attachmentsPtr[x] = attachments[x];
            }
            
            var rectCount = (rects != null) ? (UInt32)rects.Count : 0;
            var _rectsPtr = (ClearRect*)IntPtr.Zero;
            if(rectCount != 0)
            {
                var _rectsSize = Marshal.SizeOf(typeof(ClearRect));
                _rectsPtr = (ClearRect*)Marshal.AllocHGlobal((int)(_rectsSize * rectCount));
                for(var x = 0; x < rectCount; x++)
                    _rectsPtr[x] = rects[x];
            }
            
            vkCmdClearAttachments(commandBuffer.NativePointer, attachmentCount, _attachmentsPtr, rectCount, _rectsPtr);
            Marshal.FreeHGlobal((IntPtr)_attachmentsPtr);
            Marshal.FreeHGlobal((IntPtr)_rectsPtr);
        }
        
        /// <summary>
        /// [<see cref="CommandBufferLevel"/>: primary, secondary] [<see cref="QueueFlags"/>: graphics] [Render Pass: outside] 
        /// </summary>
        /// <param name="commandBuffer">ExternSync</param>
        public static void CmdResolveImage(CommandBuffer commandBuffer, Image srcImage, ImageLayout srcImageLayout, Image dstImage, ImageLayout dstImageLayout, List<ImageResolve> regions)
        {
            var regionCount = (regions != null) ? (UInt32)regions.Count : 0;
            var _regionsPtr = (ImageResolve*)IntPtr.Zero;
            if(regionCount != 0)
            {
                var _regionsSize = Marshal.SizeOf(typeof(ImageResolve));
                _regionsPtr = (ImageResolve*)Marshal.AllocHGlobal((int)(_regionsSize * regionCount));
                for(var x = 0; x < regionCount; x++)
                    _regionsPtr[x] = regions[x];
            }
            
            vkCmdResolveImage(commandBuffer.NativePointer, srcImage.NativePointer, srcImageLayout, dstImage.NativePointer, dstImageLayout, regionCount, _regionsPtr);
            Marshal.FreeHGlobal((IntPtr)_regionsPtr);
        }
        
        /// <summary>
        /// [<see cref="CommandBufferLevel"/>: primary, secondary] [<see cref="QueueFlags"/>: graphics, compute] [Render Pass: outside] 
        /// </summary>
        /// <param name="commandBuffer">ExternSync</param>
        public static void CmdSetEvent(CommandBuffer commandBuffer, Event @event, PipelineStageFlags stageMask)
        {
            vkCmdSetEvent(commandBuffer.NativePointer, @event.NativePointer, stageMask);
        }
        
        /// <summary>
        /// [<see cref="CommandBufferLevel"/>: primary, secondary] [<see cref="QueueFlags"/>: graphics, compute] [Render Pass: outside] 
        /// </summary>
        /// <param name="commandBuffer">ExternSync</param>
        public static void CmdResetEvent(CommandBuffer commandBuffer, Event @event, PipelineStageFlags stageMask)
        {
            vkCmdResetEvent(commandBuffer.NativePointer, @event.NativePointer, stageMask);
        }
        
        /// <summary>
        /// [<see cref="CommandBufferLevel"/>: primary, secondary] [<see cref="QueueFlags"/>: graphics, compute] [Render Pass: both] 
        /// </summary>
        /// <param name="commandBuffer">ExternSync</param>
        public static void CmdWaitEvents(CommandBuffer commandBuffer, List<Event> events, PipelineStageFlags srcStageMask, PipelineStageFlags dstStageMask, List<MemoryBarrier> memoryBarriers, List<BufferMemoryBarrier> bufferMemoryBarriers, List<ImageMemoryBarrier> imageMemoryBarriers)
        {
            var eventCount = (events != null) ? (UInt32)events.Count : 0;
            var _eventsPtr = (UInt64*)IntPtr.Zero;
            if(eventCount != 0)
            {
                var _eventsSize = Marshal.SizeOf(typeof(IntPtr));
                _eventsPtr = (UInt64*)Marshal.AllocHGlobal((int)(_eventsSize * eventCount));
                for(var x = 0; x < eventCount; x++)
                    _eventsPtr[x] = events[x].NativePointer;
            }
            
            var memoryBarrierCount = (memoryBarriers != null) ? (UInt32)memoryBarriers.Count : 0;
            var _memoryBarriersPtr = (Unmanaged.MemoryBarrier*)IntPtr.Zero;
            if(memoryBarrierCount != 0)
            {
                var _memoryBarriersSize = Marshal.SizeOf(typeof(Unmanaged.MemoryBarrier));
                _memoryBarriersPtr = (Unmanaged.MemoryBarrier*)Marshal.AllocHGlobal((int)(_memoryBarriersSize * memoryBarrierCount));
                for(var x = 0; x < memoryBarrierCount; x++)
                    _memoryBarriersPtr[x] = *memoryBarriers[x].NativePointer;
            }
            
            var bufferMemoryBarrierCount = (bufferMemoryBarriers != null) ? (UInt32)bufferMemoryBarriers.Count : 0;
            var _bufferMemoryBarriersPtr = (Unmanaged.BufferMemoryBarrier*)IntPtr.Zero;
            if(bufferMemoryBarrierCount != 0)
            {
                var _bufferMemoryBarriersSize = Marshal.SizeOf(typeof(Unmanaged.BufferMemoryBarrier));
                _bufferMemoryBarriersPtr = (Unmanaged.BufferMemoryBarrier*)Marshal.AllocHGlobal((int)(_bufferMemoryBarriersSize * bufferMemoryBarrierCount));
                for(var x = 0; x < bufferMemoryBarrierCount; x++)
                    _bufferMemoryBarriersPtr[x] = *bufferMemoryBarriers[x].NativePointer;
            }
            
            var imageMemoryBarrierCount = (imageMemoryBarriers != null) ? (UInt32)imageMemoryBarriers.Count : 0;
            var _imageMemoryBarriersPtr = (Unmanaged.ImageMemoryBarrier*)IntPtr.Zero;
            if(imageMemoryBarrierCount != 0)
            {
                var _imageMemoryBarriersSize = Marshal.SizeOf(typeof(Unmanaged.ImageMemoryBarrier));
                _imageMemoryBarriersPtr = (Unmanaged.ImageMemoryBarrier*)Marshal.AllocHGlobal((int)(_imageMemoryBarriersSize * imageMemoryBarrierCount));
                for(var x = 0; x < imageMemoryBarrierCount; x++)
                    _imageMemoryBarriersPtr[x] = *imageMemoryBarriers[x].NativePointer;
            }
            
            vkCmdWaitEvents(commandBuffer.NativePointer, eventCount, _eventsPtr, srcStageMask, dstStageMask, memoryBarrierCount, _memoryBarriersPtr, bufferMemoryBarrierCount, _bufferMemoryBarriersPtr, imageMemoryBarrierCount, _imageMemoryBarriersPtr);
            Marshal.FreeHGlobal((IntPtr)_eventsPtr);
            Marshal.FreeHGlobal((IntPtr)_memoryBarriersPtr);
            Marshal.FreeHGlobal((IntPtr)_bufferMemoryBarriersPtr);
            Marshal.FreeHGlobal((IntPtr)_imageMemoryBarriersPtr);
        }
        
        /// <summary>
        /// [<see cref="CommandBufferLevel"/>: primary, secondary] [<see cref="QueueFlags"/>: transfer, graphics, compute] [Render Pass: both] 
        /// </summary>
        /// <param name="commandBuffer">ExternSync</param>
        /// <param name="dependencyFlags">Optional</param>
        public static void CmdPipelineBarrier(CommandBuffer commandBuffer, PipelineStageFlags srcStageMask, PipelineStageFlags dstStageMask, DependencyFlags dependencyFlags, List<MemoryBarrier> memoryBarriers, List<BufferMemoryBarrier> bufferMemoryBarriers, List<ImageMemoryBarrier> imageMemoryBarriers)
        {
            var memoryBarrierCount = (memoryBarriers != null) ? (UInt32)memoryBarriers.Count : 0;
            var _memoryBarriersPtr = (Unmanaged.MemoryBarrier*)IntPtr.Zero;
            if(memoryBarrierCount != 0)
            {
                var _memoryBarriersSize = Marshal.SizeOf(typeof(Unmanaged.MemoryBarrier));
                _memoryBarriersPtr = (Unmanaged.MemoryBarrier*)Marshal.AllocHGlobal((int)(_memoryBarriersSize * memoryBarrierCount));
                for(var x = 0; x < memoryBarrierCount; x++)
                    _memoryBarriersPtr[x] = *memoryBarriers[x].NativePointer;
            }
            
            var bufferMemoryBarrierCount = (bufferMemoryBarriers != null) ? (UInt32)bufferMemoryBarriers.Count : 0;
            var _bufferMemoryBarriersPtr = (Unmanaged.BufferMemoryBarrier*)IntPtr.Zero;
            if(bufferMemoryBarrierCount != 0)
            {
                var _bufferMemoryBarriersSize = Marshal.SizeOf(typeof(Unmanaged.BufferMemoryBarrier));
                _bufferMemoryBarriersPtr = (Unmanaged.BufferMemoryBarrier*)Marshal.AllocHGlobal((int)(_bufferMemoryBarriersSize * bufferMemoryBarrierCount));
                for(var x = 0; x < bufferMemoryBarrierCount; x++)
                    _bufferMemoryBarriersPtr[x] = *bufferMemoryBarriers[x].NativePointer;
            }
            
            var imageMemoryBarrierCount = (imageMemoryBarriers != null) ? (UInt32)imageMemoryBarriers.Count : 0;
            var _imageMemoryBarriersPtr = (Unmanaged.ImageMemoryBarrier*)IntPtr.Zero;
            if(imageMemoryBarrierCount != 0)
            {
                var _imageMemoryBarriersSize = Marshal.SizeOf(typeof(Unmanaged.ImageMemoryBarrier));
                _imageMemoryBarriersPtr = (Unmanaged.ImageMemoryBarrier*)Marshal.AllocHGlobal((int)(_imageMemoryBarriersSize * imageMemoryBarrierCount));
                for(var x = 0; x < imageMemoryBarrierCount; x++)
                    _imageMemoryBarriersPtr[x] = *imageMemoryBarriers[x].NativePointer;
            }
            
            vkCmdPipelineBarrier(commandBuffer.NativePointer, srcStageMask, dstStageMask, dependencyFlags, memoryBarrierCount, _memoryBarriersPtr, bufferMemoryBarrierCount, _bufferMemoryBarriersPtr, imageMemoryBarrierCount, _imageMemoryBarriersPtr);
            Marshal.FreeHGlobal((IntPtr)_memoryBarriersPtr);
            Marshal.FreeHGlobal((IntPtr)_bufferMemoryBarriersPtr);
            Marshal.FreeHGlobal((IntPtr)_imageMemoryBarriersPtr);
        }
        
        /// <summary>
        /// [<see cref="CommandBufferLevel"/>: primary, secondary] [<see cref="QueueFlags"/>: graphics, compute] [Render Pass: both] 
        /// </summary>
        /// <param name="commandBuffer">ExternSync</param>
        /// <param name="flags">Optional</param>
        public static void CmdBeginQuery(CommandBuffer commandBuffer, QueryPool queryPool, UInt32 query, QueryControlFlags flags)
        {
            vkCmdBeginQuery(commandBuffer.NativePointer, queryPool.NativePointer, query, flags);
        }
        
        /// <summary>
        /// [<see cref="CommandBufferLevel"/>: primary, secondary] [<see cref="QueueFlags"/>: graphics, compute] [Render Pass: both] 
        /// </summary>
        /// <param name="commandBuffer">ExternSync</param>
        public static void CmdEndQuery(CommandBuffer commandBuffer, QueryPool queryPool, UInt32 query)
        {
            vkCmdEndQuery(commandBuffer.NativePointer, queryPool.NativePointer, query);
        }
        
        /// <summary>
        /// [<see cref="CommandBufferLevel"/>: primary, secondary] [<see cref="QueueFlags"/>: graphics, compute] [Render Pass: outside] 
        /// </summary>
        /// <param name="commandBuffer">ExternSync</param>
        public static void CmdResetQueryPool(CommandBuffer commandBuffer, QueryPool queryPool, UInt32 firstQuery, UInt32 queryCount)
        {
            vkCmdResetQueryPool(commandBuffer.NativePointer, queryPool.NativePointer, firstQuery, queryCount);
        }
        
        /// <summary>
        /// [<see cref="CommandBufferLevel"/>: primary, secondary] [<see cref="QueueFlags"/>: graphics, compute] [Render Pass: both] 
        /// </summary>
        /// <param name="commandBuffer">ExternSync</param>
        public static void CmdWriteTimestamp(CommandBuffer commandBuffer, PipelineStageFlags pipelineStage, QueryPool queryPool, UInt32 query)
        {
            vkCmdWriteTimestamp(commandBuffer.NativePointer, pipelineStage, queryPool.NativePointer, query);
        }
        
        /// <summary>
        /// [<see cref="CommandBufferLevel"/>: primary, secondary] [<see cref="QueueFlags"/>: graphics, compute] [Render Pass: outside] 
        /// </summary>
        /// <param name="commandBuffer">ExternSync</param>
        /// <param name="flags">Optional</param>
        public static void CmdCopyQueryPoolResults(CommandBuffer commandBuffer, QueryPool queryPool, UInt32 firstQuery, UInt32 queryCount, Buffer dstBuffer, DeviceSize dstOffset, DeviceSize stride, QueryResultFlags flags)
        {
            vkCmdCopyQueryPoolResults(commandBuffer.NativePointer, queryPool.NativePointer, firstQuery, queryCount, dstBuffer.NativePointer, dstOffset, stride, flags);
        }
        
        /// <summary>
        /// [<see cref="CommandBufferLevel"/>: primary, secondary] [<see cref="QueueFlags"/>: graphics, compute] [Render Pass: both] 
        /// </summary>
        /// <param name="commandBuffer">ExternSync</param>
        public static void CmdPushConstants(CommandBuffer commandBuffer, PipelineLayout layout, ShaderStageFlags stageFlags, UInt32 offset, List<IntPtr> values)
        {
            var size = (values != null) ? (UInt32)values.Count : 0;
            var _valuesPtr = (IntPtr*)IntPtr.Zero;
            if(size != 0)
            {
                var _valuesSize = Marshal.SizeOf(typeof(IntPtr));
                _valuesPtr = (IntPtr*)Marshal.AllocHGlobal((int)(_valuesSize * size));
                for(var x = 0; x < size; x++)
                    _valuesPtr[x] = values[x];
            }
            
            vkCmdPushConstants(commandBuffer.NativePointer, layout.NativePointer, stageFlags, offset, size, _valuesPtr);
            Marshal.FreeHGlobal((IntPtr)_valuesPtr);
        }
        
        /// <summary>
        /// [<see cref="CommandBufferLevel"/>: primary] [<see cref="QueueFlags"/>: graphics] [Render Pass: outside] 
        /// </summary>
        /// <param name="commandBuffer">ExternSync</param>
        public static void CmdBeginRenderPass(CommandBuffer commandBuffer, RenderPassBeginInfo renderPassBegin, SubpassContents contents)
        {
            vkCmdBeginRenderPass(commandBuffer.NativePointer, renderPassBegin.NativePointer, contents);
        }
        
        /// <summary>
        /// [<see cref="CommandBufferLevel"/>: primary] [<see cref="QueueFlags"/>: graphics] [Render Pass: inside] 
        /// </summary>
        /// <param name="commandBuffer">ExternSync</param>
        public static void CmdNextSubpass(CommandBuffer commandBuffer, SubpassContents contents)
        {
            vkCmdNextSubpass(commandBuffer.NativePointer, contents);
        }
        
        /// <summary>
        /// [<see cref="CommandBufferLevel"/>: primary] [<see cref="QueueFlags"/>: graphics] [Render Pass: inside] 
        /// </summary>
        /// <param name="commandBuffer">ExternSync</param>
        public static void CmdEndRenderPass(CommandBuffer commandBuffer)
        {
            vkCmdEndRenderPass(commandBuffer.NativePointer);
        }
        
        /// <summary>
        /// [<see cref="CommandBufferLevel"/>: primary] [<see cref="QueueFlags"/>: transfer, graphics, compute] [Render Pass: both] 
        /// </summary>
        /// <param name="commandBuffer">ExternSync</param>
        public static void CmdExecuteCommands(CommandBuffer commandBuffer, List<CommandBuffer> commandBuffers)
        {
            var commandBufferCount = (commandBuffers != null) ? (UInt32)commandBuffers.Count : 0;
            var _commandBuffersPtr = (IntPtr*)IntPtr.Zero;
            if(commandBufferCount != 0)
            {
                var _commandBuffersSize = Marshal.SizeOf(typeof(IntPtr));
                _commandBuffersPtr = (IntPtr*)Marshal.AllocHGlobal((int)(_commandBuffersSize * commandBufferCount));
                for(var x = 0; x < commandBufferCount; x++)
                    _commandBuffersPtr[x] = commandBuffers[x].NativePointer;
            }
            
            vkCmdExecuteCommands(commandBuffer.NativePointer, commandBufferCount, _commandBuffersPtr);
            Marshal.FreeHGlobal((IntPtr)_commandBuffersPtr);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="allocator">Optional</param>
        public static SurfaceKHR CreateAndroidSurfaceKHR(Instance instance, AndroidSurfaceCreateInfoKHR createInfo, AllocationCallbacks allocator = null)
        {
            var surface = new SurfaceKHR();
            fixed(UInt64* ptrSurfaceKHR = &surface.NativePointer)
            {
                var result = vkCreateAndroidSurfaceKHR(instance.NativePointer, createInfo.NativePointer, (allocator != null) ? allocator.NativePointer : null, ptrSurfaceKHR);
                if(result != Result.Success)
                    throw new VulkanCommandException(nameof(vkCreateAndroidSurfaceKHR), result);
            }
            return surface;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public static List<DisplayPropertiesKHR> GetPhysicalDeviceDisplayPropertiesKHR(PhysicalDevice physicalDevice)
        {
            UInt32 listLength;
            var result = vkGetPhysicalDeviceDisplayPropertiesKHR(physicalDevice.NativePointer, &listLength, null);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkGetPhysicalDeviceDisplayPropertiesKHR), result);
            
            var arrayDisplayPropertiesKHR = new Unmanaged.DisplayPropertiesKHR[listLength];
            fixed(Unmanaged.DisplayPropertiesKHR* resultPtr = &arrayDisplayPropertiesKHR[0])
                result = vkGetPhysicalDeviceDisplayPropertiesKHR(physicalDevice.NativePointer, &listLength, resultPtr);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkGetPhysicalDeviceDisplayPropertiesKHR), result);
            
            var list = new List<DisplayPropertiesKHR>();
            for(var x = 0; x < listLength; x++)
            {
                var item = new DisplayPropertiesKHR();
                fixed(Unmanaged.DisplayPropertiesKHR* itemPtr = &arrayDisplayPropertiesKHR[x])
                    item.NativePointer = itemPtr;
                list.Add(item);
            }
            
            return list;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public static List<DisplayPlanePropertiesKHR> GetPhysicalDeviceDisplayPlanePropertiesKHR(PhysicalDevice physicalDevice)
        {
            UInt32 listLength;
            var result = vkGetPhysicalDeviceDisplayPlanePropertiesKHR(physicalDevice.NativePointer, &listLength, null);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkGetPhysicalDeviceDisplayPlanePropertiesKHR), result);
            
            var arrayDisplayPlanePropertiesKHR = new Unmanaged.DisplayPlanePropertiesKHR[listLength];
            fixed(Unmanaged.DisplayPlanePropertiesKHR* resultPtr = &arrayDisplayPlanePropertiesKHR[0])
                result = vkGetPhysicalDeviceDisplayPlanePropertiesKHR(physicalDevice.NativePointer, &listLength, resultPtr);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkGetPhysicalDeviceDisplayPlanePropertiesKHR), result);
            
            var list = new List<DisplayPlanePropertiesKHR>();
            for(var x = 0; x < listLength; x++)
            {
                var item = new DisplayPlanePropertiesKHR();
                fixed(Unmanaged.DisplayPlanePropertiesKHR* itemPtr = &arrayDisplayPlanePropertiesKHR[x])
                    item.NativePointer = itemPtr;
                list.Add(item);
            }
            
            return list;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public static List<DisplayKHR> GetDisplayPlaneSupportedDisplaysKHR(PhysicalDevice physicalDevice, UInt32 planeIndex)
        {
            UInt32 listLength;
            var result = vkGetDisplayPlaneSupportedDisplaysKHR(physicalDevice.NativePointer, planeIndex, &listLength, null);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkGetDisplayPlaneSupportedDisplaysKHR), result);
            
            var arrayDisplayKHR = new UInt64[listLength];
            fixed(UInt64* resultPtr = &arrayDisplayKHR[0])
                result = vkGetDisplayPlaneSupportedDisplaysKHR(physicalDevice.NativePointer, planeIndex, &listLength, resultPtr);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkGetDisplayPlaneSupportedDisplaysKHR), result);
            
            var list = new List<DisplayKHR>();
            for(var x = 0; x < listLength; x++)
            {
                var item = new DisplayKHR();
                item.NativePointer = arrayDisplayKHR[x];
                list.Add(item);
            }
            
            return list;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public static List<DisplayModePropertiesKHR> GetDisplayModePropertiesKHR(PhysicalDevice physicalDevice, DisplayKHR display)
        {
            UInt32 listLength;
            var result = vkGetDisplayModePropertiesKHR(physicalDevice.NativePointer, display.NativePointer, &listLength, null);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkGetDisplayModePropertiesKHR), result);
            
            var arrayDisplayModePropertiesKHR = new Unmanaged.DisplayModePropertiesKHR[listLength];
            fixed(Unmanaged.DisplayModePropertiesKHR* resultPtr = &arrayDisplayModePropertiesKHR[0])
                result = vkGetDisplayModePropertiesKHR(physicalDevice.NativePointer, display.NativePointer, &listLength, resultPtr);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkGetDisplayModePropertiesKHR), result);
            
            var list = new List<DisplayModePropertiesKHR>();
            for(var x = 0; x < listLength; x++)
            {
                var item = new DisplayModePropertiesKHR();
                fixed(Unmanaged.DisplayModePropertiesKHR* itemPtr = &arrayDisplayModePropertiesKHR[x])
                    item.NativePointer = itemPtr;
                list.Add(item);
            }
            
            return list;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="display">ExternSync</param>
        /// <param name="allocator">Optional</param>
        public static DisplayModeKHR CreateDisplayModeKHR(PhysicalDevice physicalDevice, DisplayKHR display, DisplayModeCreateInfoKHR createInfo, AllocationCallbacks allocator = null)
        {
            var mode = new DisplayModeKHR();
            fixed(UInt64* ptrDisplayModeKHR = &mode.NativePointer)
            {
                var result = vkCreateDisplayModeKHR(physicalDevice.NativePointer, display.NativePointer, createInfo.NativePointer, (allocator != null) ? allocator.NativePointer : null, ptrDisplayModeKHR);
                if(result != Result.Success)
                    throw new VulkanCommandException(nameof(vkCreateDisplayModeKHR), result);
            }
            return mode;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mode">ExternSync</param>
        public static DisplayPlaneCapabilitiesKHR GetDisplayPlaneCapabilitiesKHR(PhysicalDevice physicalDevice, DisplayModeKHR mode, UInt32 planeIndex)
        {
            var capabilities = new DisplayPlaneCapabilitiesKHR();
            var result = vkGetDisplayPlaneCapabilitiesKHR(physicalDevice.NativePointer, mode.NativePointer, planeIndex, &capabilities);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkGetDisplayPlaneCapabilitiesKHR), result);
            return capabilities;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="allocator">Optional</param>
        public static SurfaceKHR CreateDisplayPlaneSurfaceKHR(Instance instance, DisplaySurfaceCreateInfoKHR createInfo, AllocationCallbacks allocator = null)
        {
            var surface = new SurfaceKHR();
            fixed(UInt64* ptrSurfaceKHR = &surface.NativePointer)
            {
                var result = vkCreateDisplayPlaneSurfaceKHR(instance.NativePointer, createInfo.NativePointer, (allocator != null) ? allocator.NativePointer : null, ptrSurfaceKHR);
                if(result != Result.Success)
                    throw new VulkanCommandException(nameof(vkCreateDisplayPlaneSurfaceKHR), result);
            }
            return surface;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="allocator">Optional</param>
        public static List<SwapchainKHR> CreateSharedSwapchainsKHR(Device device, List<SwapchainCreateInfoKHR> createInfos, AllocationCallbacks allocator = null)
        {
            var swapchainCount = (createInfos != null) ? (UInt32)createInfos.Count : 0;
            var _createInfosPtr = (Unmanaged.SwapchainCreateInfoKHR*)IntPtr.Zero;
            if(swapchainCount != 0)
            {
                var _createInfosSize = Marshal.SizeOf(typeof(Unmanaged.SwapchainCreateInfoKHR));
                _createInfosPtr = (Unmanaged.SwapchainCreateInfoKHR*)Marshal.AllocHGlobal((int)(_createInfosSize * swapchainCount));
                for(var x = 0; x < swapchainCount; x++)
                    _createInfosPtr[x] = *createInfos[x].NativePointer;
            }
            
            var listLength = swapchainCount;
            Result result;
            
            var arraySwapchainKHR = new UInt64[listLength];
            fixed(UInt64* resultPtr = &arraySwapchainKHR[0])
                result = vkCreateSharedSwapchainsKHR(device.NativePointer, listLength, _createInfosPtr, (allocator != null) ? allocator.NativePointer : null, resultPtr);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkCreateSharedSwapchainsKHR), result);
            Marshal.FreeHGlobal((IntPtr)_createInfosPtr);
            
            var list = new List<SwapchainKHR>();
            for(var x = 0; x < listLength; x++)
            {
                var item = new SwapchainKHR();
                item.NativePointer = arraySwapchainKHR[x];
                list.Add(item);
            }
            
            return list;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="allocator">Optional</param>
        public static SurfaceKHR CreateMirSurfaceKHR(Instance instance, MirSurfaceCreateInfoKHR createInfo, AllocationCallbacks allocator = null)
        {
            var surface = new SurfaceKHR();
            fixed(UInt64* ptrSurfaceKHR = &surface.NativePointer)
            {
                var result = vkCreateMirSurfaceKHR(instance.NativePointer, createInfo.NativePointer, (allocator != null) ? allocator.NativePointer : null, ptrSurfaceKHR);
                if(result != Result.Success)
                    throw new VulkanCommandException(nameof(vkCreateMirSurfaceKHR), result);
            }
            return surface;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public static IntPtr GetPhysicalDeviceMirPresentationSupportKHR(PhysicalDevice physicalDevice, UInt32 queueFamilyIndex)
        {
            var connection = new IntPtr();
            vkGetPhysicalDeviceMirPresentationSupportKHR(physicalDevice.NativePointer, queueFamilyIndex, &connection);
            return connection;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="surface">ExternSync</param>
        /// <param name="allocator">Optional</param>
        public static void DestroySurfaceKHR(Instance instance, SurfaceKHR surface, AllocationCallbacks allocator = null)
        {
            vkDestroySurfaceKHR(instance.NativePointer, (surface != null) ? surface.NativePointer : 0, (allocator != null) ? allocator.NativePointer : null);
        }
        
        /// <summary>
        /// 
        /// </summary>
        public static Bool32 GetPhysicalDeviceSurfaceSupportKHR(PhysicalDevice physicalDevice, UInt32 queueFamilyIndex, SurfaceKHR surface)
        {
            var supported = new Bool32();
            var result = vkGetPhysicalDeviceSurfaceSupportKHR(physicalDevice.NativePointer, queueFamilyIndex, surface.NativePointer, &supported);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkGetPhysicalDeviceSurfaceSupportKHR), result);
            return supported;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public static SurfaceCapabilitiesKHR GetPhysicalDeviceSurfaceCapabilitiesKHR(PhysicalDevice physicalDevice, SurfaceKHR surface)
        {
            var surfaceCapabilities = new SurfaceCapabilitiesKHR();
            var result = vkGetPhysicalDeviceSurfaceCapabilitiesKHR(physicalDevice.NativePointer, surface.NativePointer, &surfaceCapabilities);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkGetPhysicalDeviceSurfaceCapabilitiesKHR), result);
            return surfaceCapabilities;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public static List<SurfaceFormatKHR> GetPhysicalDeviceSurfaceFormatsKHR(PhysicalDevice physicalDevice, SurfaceKHR surface)
        {
            UInt32 listLength;
            var result = vkGetPhysicalDeviceSurfaceFormatsKHR(physicalDevice.NativePointer, surface.NativePointer, &listLength, null);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkGetPhysicalDeviceSurfaceFormatsKHR), result);
            
            var arraySurfaceFormatKHR = new SurfaceFormatKHR[listLength];
            fixed(SurfaceFormatKHR* resultPtr = &arraySurfaceFormatKHR[0])
                result = vkGetPhysicalDeviceSurfaceFormatsKHR(physicalDevice.NativePointer, surface.NativePointer, &listLength, resultPtr);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkGetPhysicalDeviceSurfaceFormatsKHR), result);
            
            var list = new List<SurfaceFormatKHR>();
            for(var x = 0; x < listLength; x++)
            {
                list.Add(arraySurfaceFormatKHR[x]);
            }
            
            return list;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public static List<PresentModeKHR> GetPhysicalDeviceSurfacePresentModesKHR(PhysicalDevice physicalDevice, SurfaceKHR surface)
        {
            UInt32 listLength;
            var result = vkGetPhysicalDeviceSurfacePresentModesKHR(physicalDevice.NativePointer, surface.NativePointer, &listLength, null);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkGetPhysicalDeviceSurfacePresentModesKHR), result);
            
            var arrayPresentModeKHR = new PresentModeKHR[listLength];
            fixed(PresentModeKHR* resultPtr = &arrayPresentModeKHR[0])
                result = vkGetPhysicalDeviceSurfacePresentModesKHR(physicalDevice.NativePointer, surface.NativePointer, &listLength, resultPtr);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkGetPhysicalDeviceSurfacePresentModesKHR), result);
            
            var list = new List<PresentModeKHR>();
            for(var x = 0; x < listLength; x++)
            {
                list.Add(arrayPresentModeKHR[x]);
            }
            
            return list;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="allocator">Optional</param>
        public static SwapchainKHR CreateSwapchainKHR(Device device, SwapchainCreateInfoKHR createInfo, AllocationCallbacks allocator = null)
        {
            var swapchain = new SwapchainKHR();
            fixed(UInt64* ptrSwapchainKHR = &swapchain.NativePointer)
            {
                var result = vkCreateSwapchainKHR(device.NativePointer, createInfo.NativePointer, (allocator != null) ? allocator.NativePointer : null, ptrSwapchainKHR);
                if(result != Result.Success)
                    throw new VulkanCommandException(nameof(vkCreateSwapchainKHR), result);
            }
            return swapchain;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="swapchain">ExternSync</param>
        /// <param name="allocator">Optional</param>
        public static void DestroySwapchainKHR(Device device, SwapchainKHR swapchain, AllocationCallbacks allocator = null)
        {
            vkDestroySwapchainKHR(device.NativePointer, (swapchain != null) ? swapchain.NativePointer : 0, (allocator != null) ? allocator.NativePointer : null);
        }
        
        /// <summary>
        /// 
        /// </summary>
        public static List<Image> GetSwapchainImagesKHR(Device device, SwapchainKHR swapchain)
        {
            UInt32 listLength;
            var result = vkGetSwapchainImagesKHR(device.NativePointer, swapchain.NativePointer, &listLength, null);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkGetSwapchainImagesKHR), result);
            
            var arrayImage = new UInt64[listLength];
            fixed(UInt64* resultPtr = &arrayImage[0])
                result = vkGetSwapchainImagesKHR(device.NativePointer, swapchain.NativePointer, &listLength, resultPtr);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkGetSwapchainImagesKHR), result);
            
            var list = new List<Image>();
            for(var x = 0; x < listLength; x++)
            {
                var item = new Image();
                item.NativePointer = arrayImage[x];
                list.Add(item);
            }
            
            return list;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="swapchain">ExternSync</param>
        /// <param name="semaphore">ExternSync</param>
        /// <param name="fence">ExternSync</param>
        public static UInt32 AcquireNextImageKHR(Device device, SwapchainKHR swapchain, UInt64 timeout, Semaphore semaphore, Fence fence)
        {
            var imageIndex = new UInt32();
            var result = vkAcquireNextImageKHR(device.NativePointer, swapchain.NativePointer, timeout, (semaphore != null) ? semaphore.NativePointer : 0, (fence != null) ? fence.NativePointer : 0, &imageIndex);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkAcquireNextImageKHR), result);
            return imageIndex;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queue">ExternSync</param>
        public static void QueuePresentKHR(Queue queue, PresentInfoKHR presentInfo)
        {
            var result = vkQueuePresentKHR(queue.NativePointer, presentInfo.NativePointer);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkQueuePresentKHR), result);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="allocator">Optional</param>
        public static SurfaceKHR CreateWaylandSurfaceKHR(Instance instance, WaylandSurfaceCreateInfoKHR createInfo, AllocationCallbacks allocator = null)
        {
            var surface = new SurfaceKHR();
            fixed(UInt64* ptrSurfaceKHR = &surface.NativePointer)
            {
                var result = vkCreateWaylandSurfaceKHR(instance.NativePointer, createInfo.NativePointer, (allocator != null) ? allocator.NativePointer : null, ptrSurfaceKHR);
                if(result != Result.Success)
                    throw new VulkanCommandException(nameof(vkCreateWaylandSurfaceKHR), result);
            }
            return surface;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public static IntPtr GetPhysicalDeviceWaylandPresentationSupportKHR(PhysicalDevice physicalDevice, UInt32 queueFamilyIndex)
        {
            var display = new IntPtr();
            vkGetPhysicalDeviceWaylandPresentationSupportKHR(physicalDevice.NativePointer, queueFamilyIndex, &display);
            return display;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="allocator">Optional</param>
        public static SurfaceKHR CreateWin32SurfaceKHR(Instance instance, Win32SurfaceCreateInfoKHR createInfo, AllocationCallbacks allocator = null)
        {
            var surface = new SurfaceKHR();
            fixed(UInt64* ptrSurfaceKHR = &surface.NativePointer)
            {
                var result = vkCreateWin32SurfaceKHR(instance.NativePointer, createInfo.NativePointer, (allocator != null) ? allocator.NativePointer : null, ptrSurfaceKHR);
                if(result != Result.Success)
                    throw new VulkanCommandException(nameof(vkCreateWin32SurfaceKHR), result);
            }
            return surface;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public static Bool32 GetPhysicalDeviceWin32PresentationSupportKHR(PhysicalDevice physicalDevice, UInt32 queueFamilyIndex)
        {
            var result = vkGetPhysicalDeviceWin32PresentationSupportKHR(physicalDevice.NativePointer, queueFamilyIndex);
            return result;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="allocator">Optional</param>
        public static SurfaceKHR CreateXlibSurfaceKHR(Instance instance, XlibSurfaceCreateInfoKHR createInfo, AllocationCallbacks allocator = null)
        {
            var surface = new SurfaceKHR();
            fixed(UInt64* ptrSurfaceKHR = &surface.NativePointer)
            {
                var result = vkCreateXlibSurfaceKHR(instance.NativePointer, createInfo.NativePointer, (allocator != null) ? allocator.NativePointer : null, ptrSurfaceKHR);
                if(result != Result.Success)
                    throw new VulkanCommandException(nameof(vkCreateXlibSurfaceKHR), result);
            }
            return surface;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public static Bool32 GetPhysicalDeviceXlibPresentationSupportKHR(PhysicalDevice physicalDevice, UInt32 queueFamilyIndex, IntPtr dpy, IntPtr visualID)
        {
            var result = vkGetPhysicalDeviceXlibPresentationSupportKHR(physicalDevice.NativePointer, queueFamilyIndex, &dpy, visualID);
            return result;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="allocator">Optional</param>
        public static SurfaceKHR CreateXcbSurfaceKHR(Instance instance, XcbSurfaceCreateInfoKHR createInfo, AllocationCallbacks allocator = null)
        {
            var surface = new SurfaceKHR();
            fixed(UInt64* ptrSurfaceKHR = &surface.NativePointer)
            {
                var result = vkCreateXcbSurfaceKHR(instance.NativePointer, createInfo.NativePointer, (allocator != null) ? allocator.NativePointer : null, ptrSurfaceKHR);
                if(result != Result.Success)
                    throw new VulkanCommandException(nameof(vkCreateXcbSurfaceKHR), result);
            }
            return surface;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public static Bool32 GetPhysicalDeviceXcbPresentationSupportKHR(PhysicalDevice physicalDevice, UInt32 queueFamilyIndex, IntPtr connection, IntPtr visual_id)
        {
            var result = vkGetPhysicalDeviceXcbPresentationSupportKHR(physicalDevice.NativePointer, queueFamilyIndex, &connection, visual_id);
            return result;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="allocator">Optional</param>
        public static DebugReportCallbackEXT CreateDebugReportCallbackEXT(Instance instance, DebugReportCallbackCreateInfoEXT createInfo, AllocationCallbacks allocator = null)
        {
            var callback = new DebugReportCallbackEXT();
            fixed(UInt64* ptrDebugReportCallbackEXT = &callback.NativePointer)
            {
                var result = vkCreateDebugReportCallbackEXT(instance.NativePointer, createInfo.NativePointer, (allocator != null) ? allocator.NativePointer : null, ptrDebugReportCallbackEXT);
                if(result != Result.Success)
                    throw new VulkanCommandException(nameof(vkCreateDebugReportCallbackEXT), result);
            }
            return callback;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="callback">ExternSync</param>
        /// <param name="allocator">Optional</param>
        public static void DestroyDebugReportCallbackEXT(Instance instance, DebugReportCallbackEXT callback, AllocationCallbacks allocator = null)
        {
            vkDestroyDebugReportCallbackEXT(instance.NativePointer, callback.NativePointer, (allocator != null) ? allocator.NativePointer : null);
        }
        
        /// <summary>
        /// 
        /// </summary>
        public static void DebugReportMessageEXT(Instance instance, DebugReportFlagsEXT flags, DebugReportObjectTypeEXT objectType, UInt64 @object, UInt32 location, Int32 messageCode, String layerPrefix, String message)
        {
            vkDebugReportMessageEXT(instance.NativePointer, flags, objectType, @object, location, messageCode, layerPrefix, message);
        }
        
        /// <summary>
        /// 
        /// </summary>
        public static DebugMarkerObjectNameInfoEXT DebugMarkerSetObjectNameEXT(Device device)
        {
            var nameInfo = new DebugMarkerObjectNameInfoEXT();
            var result = vkDebugMarkerSetObjectNameEXT(device.NativePointer, nameInfo.NativePointer);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkDebugMarkerSetObjectNameEXT), result);
            return nameInfo;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public static DebugMarkerObjectTagInfoEXT DebugMarkerSetObjectTagEXT(Device device)
        {
            var tagInfo = new DebugMarkerObjectTagInfoEXT();
            var result = vkDebugMarkerSetObjectTagEXT(device.NativePointer, tagInfo.NativePointer);
            if(result != Result.Success)
                throw new VulkanCommandException(nameof(vkDebugMarkerSetObjectTagEXT), result);
            return tagInfo;
        }
        
        /// <summary>
        /// [<see cref="CommandBufferLevel"/>: primary, secondary] [<see cref="QueueFlags"/>: graphics, compute] [Render Pass: both] 
        /// </summary>
        public static DebugMarkerMarkerInfoEXT CmdDebugMarkerBeginEXT(CommandBuffer commandBuffer)
        {
            var markerInfo = new DebugMarkerMarkerInfoEXT();
            vkCmdDebugMarkerBeginEXT(commandBuffer.NativePointer, markerInfo.NativePointer);
            return markerInfo;
        }
        
        /// <summary>
        /// [<see cref="CommandBufferLevel"/>: primary, secondary] [<see cref="QueueFlags"/>: graphics, compute] [Render Pass: both] 
        /// </summary>
        public static void CmdDebugMarkerEndEXT(CommandBuffer commandBuffer)
        {
            vkCmdDebugMarkerEndEXT(commandBuffer.NativePointer);
        }
        
        /// <summary>
        /// [<see cref="CommandBufferLevel"/>: primary, secondary] [<see cref="QueueFlags"/>: graphics, compute] [Render Pass: both] 
        /// </summary>
        public static DebugMarkerMarkerInfoEXT CmdDebugMarkerInsertEXT(CommandBuffer commandBuffer)
        {
            var markerInfo = new DebugMarkerMarkerInfoEXT();
            vkCmdDebugMarkerInsertEXT(commandBuffer.NativePointer, markerInfo.NativePointer);
            return markerInfo;
        }
        
    }
}
