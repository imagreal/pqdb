using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace PQDB.Primitives
{
    public struct Unit
    {
        private byte unit;

        public static bool operator ==(Unit a, Unit b)
        {
            return a.unit == b.unit;
        }

        public static bool operator !=(Unit a, Unit b)
        {
            return a.unit != b.unit;
        }

        public override int GetHashCode()
        {
            return unit;
        }

        public override bool Equals(object obj)
        {
            return this == (Unit)obj;
        }

        public Unit(byte u)
        {
            unit = u;
        }

        #region Quarter-Accessors

        // Lower-left
        public Quarter DexterQuarter
        {
            get { return (Quarter)GetQuarter(1); }
        }

        // Upper-right
        public Quarter SinisterQuarter
        {
            get { return (Quarter)GetQuarter(3); }
        }

        // Upper-left
        public Quarter InnerQuarter
        {
            get { return (Quarter)GetQuarter(0); }
        }

        // Lower-right
        public Quarter ExterQuarter
        {
            get { return (Quarter)GetQuarter(2); }
        }

        public Quarter[] Quarters
        {
            get
            {
                return new Quarter[]
                {
                    InnerQuarter,
                    DexterQuarter,
                    ExterQuarter,
                    SinisterQuarter
                };
            }
        }


        private static readonly int[] QuarterIndices = new int[] { 6, 7, 0, 5, 2, 3, 4, 1 };
        public byte GetQuarter(int index)
        {
            int u = unit;
            int d = (u >> QuarterIndices[index % 4 * 2]) & 1;
            int s = (u >> QuarterIndices[index % 4 * 2 + 1]) & 1;
            return (byte)(s * 2 + d); 
        }

        public void SetUnit(Quarter q0, Quarter q1, Quarter q2, Quarter q3)
        {
            SetUnit(new Quarter[] { q0, q1, q2, q3 });
        }
        public void SetUnit(Quarter[] quarters)
        {
            int unit = 0;
            for(int i=0;i<4;i++)
            {
                int v = (int)(quarters[i]);
                int d = v % 2;
                int s = v / 2;
                unit = unit |
                    (d << QuarterIndices[i % 4 * 2]) |
                    (s << QuarterIndices[i % 4 * 2 + 1]);        
            }
        }

        #endregion

        #region Half-Accessors
        public Half DextralHalf
        {
            get { return (Half)GetDextralHalf(); }
            set { SetUnit((byte)value, GetSinistralHalf()); }
        }
    
        public Half SinistralHalf
        {
            get { return (Half)GetSinistralHalf(); }
            set { SetUnit(GetDextralHalf(), (byte)value ); }
        }

        public byte GetDextralHalf()
        {
            int u = unit;
            int v =
                (u & 1) |
                (u >> 2 & 1) << 1 |
                (u >> 4 & 1) << 2 |
                (u >> 6 & 1) << 3;
            return (byte)v;
        }

        public byte GetSinistralHalf()
        {
            int u = unit;
            int v =
                (u >> 1 & 1) |
                (u >> 3 & 1) << 1 |
                (u >> 5 & 1) << 2 |
                (u >> 7 & 1) << 3;
            return (byte)v;
        }

        public void SetUnit(byte dextral, byte sinistral)
        {
            int d = dextral;
            int s = sinistral;
            int v =
                 (d & 1) |
                (d >> 1 & 1) << 2 |
                (d >> 2 & 1) << 4 |
                (d >> 3 & 1) << 6 |
                (s  & 1) << 1|
                (s >> 1 & 1) << 3 |
                (s >> 2 & 1) << 5 |
                (s >> 3 & 1) << 7;
            unit = (byte)v;
        }
        #endregion

    }
}
