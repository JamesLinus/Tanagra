﻿using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Vulkan
{
    using Managed;

    public static class DebugUtils
    {
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public unsafe delegate Bool32 DebugReportCallbackDelegate(DebugReportFlagsEXT flags, DebugReportObjectTypeEXT objectType, ulong @object, IntPtr location, int messageCode, string layerPrefix, string message, IntPtr userData);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal unsafe delegate Result CreateDebugReportCallbackEXT_Delegate(IntPtr instance, Unmanaged.DebugReportCallbackCreateInfoEXT* createInfo, Unmanaged.AllocationCallbacks* allocator, UInt64* callback);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal unsafe delegate Result DestroyDebugReportCallbackDelegate(IntPtr instance, UInt64 callback, Unmanaged.AllocationCallbacks* allocator);

        static DebugReportCallbackDelegate debugDelegate;
        static IntPtr callbackHolder;

        public static unsafe DebugReportCallbackEXT CreateDebugReportCallback(Instance instance, DebugReportCallbackDelegate callback)
        {
            var name = "vkCreateDebugReportCallbackEXT";
            var nameBytes = Encoding.ASCII.GetBytes(name);
            var procAddr = Vk.GetInstanceProcAddr(instance, name);
            if(procAddr == IntPtr.Zero)
                throw new NullReferenceException($"Didn't find InstanceProcAddr {nameBytes}");

            debugDelegate = callback;
            GC.KeepAlive(debugDelegate);

            callbackHolder = Marshal.GetFunctionPointerForDelegate(callback);
            GC.KeepAlive(callbackHolder);

            var createDelegate = (CreateDebugReportCallbackEXT_Delegate)Marshal.GetDelegateForFunctionPointer(procAddr, typeof(CreateDebugReportCallbackEXT_Delegate));
            var createInfo = new DebugReportCallbackCreateInfoEXT
            {
                Flags    = (DebugReportFlagsEXT)0x1F,//DebugReportFlagsEXT.Error | DebugReportFlagsEXT.Warning | DebugReportFlagsEXT.PerformanceWarning,
                Callback = callbackHolder,
            };

            var debugReportCallbackEXT = new DebugReportCallbackEXT();
            fixed (UInt64* ptr = &debugReportCallbackEXT.NativePointer)
            {
                var result = createDelegate(instance.NativePointer, createInfo.NativePointer, null, ptr);
                if(result != Result.Success)
                    throw new VulkanResultException("vkCreateDebugReportCallbackEXT", result);
            }

            createInfo.Dispose();
            return debugReportCallbackEXT;
        }

        public static unsafe void DestroyDebugReportCallback(Instance instance, DebugReportCallbackEXT debugReportCallbackEXT)
        {
            if(debugReportCallbackEXT.NativePointer != 0)
            {
                var name = "vkDestroyDebugReportCallbackEXT";
                var fnPointer = Vk.GetInstanceProcAddr(instance, name);
                if(fnPointer == IntPtr.Zero)
                    throw new NullReferenceException($"Didn't find InstanceProcAddr {name}");

                var destroyDebugReportCallback = (DestroyDebugReportCallbackDelegate)Marshal.GetDelegateForFunctionPointer(fnPointer, typeof(DestroyDebugReportCallbackDelegate));
                destroyDebugReportCallback(instance.NativePointer, debugReportCallbackEXT.NativePointer, null);
            }
        }
    }
}