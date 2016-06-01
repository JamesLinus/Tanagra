﻿using System;

using SharpDX.Windows;

using Vulkan;                     // Core Vulkan classes
using Vulkan.Managed;             // A managed interface to Vulkan
using Vulkan.Managed.ObjectModel; // Extentions to object handles

namespace TanagraExample
{
    class Program
    {
        static void Main(string[] args)
        {
            /*for(int x = 0; x < 100000; x++)
            {
                var info = new SwapchainCreateInfoKHR();
                info.Surface = new SurfaceKHR();
                //info.QueueFamilyIndices = new UInt32[1000];
                info.Dispose();
            }

            GC.GetTotalMemory(true);*/

            //run();
            //ExampleBaseTest();

            var init = new VKInit();

            //var opt = new Tanagra.InstanceMgr.InitializeOptions();
            //opt.ForceUseDevice(x);
            // Indicate that we would like a graphics queue to be allocated
            //     This tells tanagra that it should look for a device that supports
            //     graphics rendering and allocate a single graphics queue
            //opt.AddQueue(QueueFlags.Graphics);

            //var mgr = new Tanagra.InstanceMgr();
            //mgr.Initialize(opt);

            Console.WriteLine("program complete");
            Console.ReadKey();
        }

        static void run()
        {
            var tri = new VKTriangle2();
            tri.Main(null);
        }

        static RenderForm form;

        static void ExampleBaseTest()
        {
            form = new RenderForm("Tanagra - Vulkan Sample");

            var ex = new ExampleBase();
            ex.initVulkan();
            ex.initSwapchain(form);
            ex.prepare();
        }
    }
}
