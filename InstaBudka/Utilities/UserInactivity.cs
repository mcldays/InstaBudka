﻿using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace YounGuard.Utilities
{
    public static class UserInactivity
    {
        private struct LASTINPUTINFO
        {
            public uint cbSize;
            public uint dwTime;
        }

        private static LASTINPUTINFO lastInPutNfo;

        static UserInactivity()
        {
            lastInPutNfo = new LASTINPUTINFO();
            lastInPutNfo.cbSize = (uint) Marshal.SizeOf(lastInPutNfo);
        }

        [DllImport("User32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        /// <summary>
        /// Idle time in ticks
        /// </summary>
        /// <returns></returns>
        public static uint GetIdleTickCount()
        {
            return ((uint) Environment.TickCount - GetLastInputTime());
        }

        /// <summary>
        /// Last input time in ticks
        /// </summary>
        /// <returns></returns>
        public static uint GetLastInputTime()
        {
            if (!GetLastInputInfo(ref lastInPutNfo))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            return lastInPutNfo.dwTime;
        }

        public static int GetSeconds()
        {
            return (int) (GetIdleTickCount() / 1000);
        }
    }
}