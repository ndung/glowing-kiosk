using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

// imports methods returning UDT structure from InnTechSSP.dll and converts
// UDT structure from unmanaged memory to managed structure 
// (P/Invoke doesn't support returning arrays by methods)

namespace SSPDllExample
{
    class Declare_udt
    {
        //import methods from InnTechSSP.dll returning UDT structure
        [DllImport("InnTechSSP.dll")]
        static extern UDTu Command(Declare.UDT cmd);
        [DllImport("InnTechSSP.dll")]
        static extern UDTu GetLastRxPacket();
        [DllImport("InnTechSSP.dll")]
        static extern UDTu GetLastTxPacket();

        //unmanaged UDT structure UDTu (byte array starts with offset 3 but can't be
        //declared as a member of UDTu structure because of P/Invoke signature
        //compatibility)
        [StructLayout(LayoutKind.Explicit, Size = 258)]
        struct UDTu
        {
            [FieldOffset(0)]
            public short rxStatus;
            [FieldOffset(2)]
            public byte datalen;
        }

        //method Command_ sends UDT to external method Command which returns UDTu,
        //converts UDTu to UDT and returns UDT
        public static Declare.UDT Command_(Declare.UDT cmd)
        {
            UDTu cpy = Command(cmd);
            Declare.UDT udt = UDTu_to_UDT(cpy);
            return udt;
        }

        //method GetLastRxPacket_ sends UDT to external method GetLastRxPacket which returns UDTu,
        //converts UDTu to UDT and returns UDT
        public static Declare.UDT GetLastRxPacket_()
        {
            UDTu cpy = GetLastRxPacket();
            Declare.UDT udt = UDTu_to_UDT(cpy);
            return udt;
        }

        //method GetLastTxPacket_ sends UDT to external method GetLastTxPacket which returns UDTu,
        //converts UDTu to UDT and returns UDT
        public static Declare.UDT GetLastTxPacket_()
        {
            UDTu cpy = GetLastTxPacket();

            Declare.UDT udt = UDTu_to_UDT(cpy);
            return udt;
        }

        // convert unmanaged UDTu to managed UDT
        static Declare.UDT UDTu_to_UDT(UDTu src)
        {
            Declare.UDT udt = new Declare.UDT();
            byte[] arrayu = new byte[258];
            //allocate 258 bytes of unmanaged memory with pointer bfr
            IntPtr bfr = Marshal.AllocHGlobal(258);

            //copy rxStatus and datalen from UDTu to UDT
            udt.rxStatus = src.rxStatus;
            udt.datalen = src.datalen;

            //copy UDTu to unmanaged memory starting at bfr
            Marshal.StructureToPtr(src, bfr, false);
            //copy 258 bytes starting from bfr to byte array arrayu
            Marshal.Copy(bfr, arrayu, 0, 258);
            //free unmanaged memory starting from bfr
            Marshal.FreeHGlobal(bfr);
            //copy 255 bytes from arrayu to array1 of UDT (starts with offset 3 in arrayu
            //as there is rxStatus in bytes 0 & 1 and datalen in byte 2
            Array.Copy(arrayu, 3, udt.array1, 0, udt.datalen);
 
            return udt;
        }
    }
}
