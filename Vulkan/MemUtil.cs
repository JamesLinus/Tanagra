﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Vulkan.Managed
{
    public unsafe static class MemUtil
    {
        /*
        ## Marshal.AllocHGlobal
        ##
        ## This method exposes the Win32 LocalAlloc function from Kernel32.dll (on Windows).
        ##
        ## When AllocHGlobal calls LocalAlloc, it passes a LMEM_FIXED flag, which causes the 
        ## allocated memory to be locked in place. Also, the allocated memory is not zero-filled.
        ## 
        ## https://msdn.microsoft.com/en-us/library/s69bkh17(v=vs.100).aspx
        */

        // Hmm, we need `PointerMemory` for Add/RemoveMemoryPressure to work ...
        // also: concurrency?

#if DEBUG
        static readonly Dictionary<IntPtr, Int64> PointerMemory;

        public static Int32 AllocCount => PointerMemory.Count;
        public static Int64 TotalBytes => PointerMemory.Values.Sum();

        static MemUtil()
        {
            PointerMemory = new Dictionary<IntPtr, long>();
        }
#endif

        internal static IntPtr Alloc(Type type)
        {
            var size = Marshal.SizeOf(type);
            var ptr = Marshal.AllocHGlobal(size);
            var bptr = (byte*)ptr;
            for(var i = 0; i < size; i++)
                bptr[i] = 0;
#if DEBUG
            //Console.WriteLine($"[SALLOC] Allocated {size} bytes for {type.Name} ({AllocCount})");
            GC.AddMemoryPressure(size);
            PointerMemory.Add(ptr, size);
#endif
            return ptr;
        }

        internal static void Free(void* ptr)
        {
            Free((IntPtr)ptr);
        }

        internal static void Free(IntPtr ptr)
        {
#if DEBUG
            //Console.WriteLine($"[SALLOC] Deallocated structure bytes ({AllocCount})");
            GC.RemoveMemoryPressure(PointerMemory[ptr]);
            PointerMemory.Remove(ptr);
#endif
            Marshal.FreeHGlobal(ptr);
        }

        internal static IntPtr Clone(IntPtr src, Type type)
        {
            var size = Marshal.SizeOf(type);
            var ptr = Marshal.AllocHGlobal(size);
            Copy(src, ptr, size);
#if DEBUG
            //Console.WriteLine($"[SALLOC] Allocated {size} bytes for {type.Name} ({AllocCount})");
            GC.AddMemoryPressure(size);
            PointerMemory.Add(ptr, size);
#endif
            return ptr;
        }
        
        internal static void Register(void* ptr, Type type)
        {
            Register((IntPtr)ptr, type);
        }

        internal static void Register(IntPtr ptr, Type type)
        {
#if DEBUG
            if(PointerMemory.ContainsKey(ptr))
            {
                Console.WriteLine($"(MemUtil) Duplicate registration of a `{type.Name}` instance (not an error)");
            }
            else
            {
                var size = Marshal.SizeOf(type);
                Console.WriteLine($"(MemUtil) Registered tracked pointer to a `{type.Name}` instance");
                GC.AddMemoryPressure(size);
                PointerMemory.Add(ptr, size);
            }
#endif
        }

        internal static void Copy(IntPtr src, IntPtr dest, int size)
        {
            /*fixed (float* sourcePtr = &source[0, 0])
                System.Buffer.MemoryCopy(sourcePtr, (void*)destination, destinationSizeInBytes, sourceBytesToCopy);*/
            var data = new byte[size];
            Marshal.Copy(src, data, 0, size);
            Marshal.Copy(data, 0, dest, size);
        }

        internal static void Copy(IntPtr src, IntPtr dest, Type type)
        {
            Copy(src, dest, Marshal.SizeOf(type));
        }

        internal static void MarshalFixedSizeString(byte* dst, string src, int size)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(src);
            size = Math.Min(size - 1, bytes.Length);
            int i;
            for(i = 0; i < size; i++)
                dst[i] = bytes[i];
            dst[i] = 0;
        }

        /*internal static void ClearMemory(IntPtr ptr, ulong size)
	    {
            var bptr = (byte*)ptr;
            for (ulong i = 0; i < size; i++)
                bptr[i] = 0;
        }*/

        internal static void DumpStruct<T>(T structTarget)
        {
            var size = Marshal.SizeOf(typeof(T));
            var bytes = new byte[size];
            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.Copy(ptr, bytes, 0, size);
            Marshal.FreeHGlobal(ptr);
            Console.WriteLine(bytes.ToString());
        }
    }
}
