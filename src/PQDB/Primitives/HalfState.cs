using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace PQDB.Primitives
{
    public enum HalfState : byte
    {
        Unity,
        Homogeneity,
        Equality,
        Oddity,
    }

    public static class HalfStateHelper
    {
        static Dictionary<HalfState, IReadOnlyList<Half>> States = null;
        public static IReadOnlyDictionary<HalfState, IReadOnlyList<Half>> GetAllStates()
        {
            if(StateTable == null)
            {

                BuildStateTable();
            }
            if(States == null)
            {
                States = StateTable.Select((v, x) => new { V = (HalfState)v, X = (Half)x })
                    .GroupBy(p => p.V)
                    .ToDictionary
                    (
                    g => g.Key,
                    g => g.OrderBy(h => h).ToList() as IReadOnlyList<Half>
                    );                
            }
            return States;
        }


        static byte[] StateTable = null;
        public static HalfState GetState(this Half h)
        {
            if(StateTable == null)
            {
                BuildStateTable();
            }
            return (HalfState)StateTable[(byte)h];
        }

        static void BuildStateTable()
        {
            StateTable = new byte[16];
            for (byte i = 0; i < 16; i++)
            {
                StateTable[i] = GetStateDepth((Half)i);
            }
        }
        static byte GetStateDepth(this Half h)
        {
            if (h == Half.Ora || h == Half.Vera ) return 0;
            return (byte)(GetStateDepth(h.Degenerate()) + 1);
        }


        static byte[] DegenerationTable = null;
        public static Half Degenerate(this Half h)
        {
            if(DegenerationTable == null)
            {
                BuildDegenerationTable();
            }
            return (Half)DegenerationTable[((byte)h)%16];
        }

        static void BuildDegenerationTable()
        {
            DegenerationTable = new byte[16];
            for(int i=0;i<16;i++)
            {
                int v0 = i % 2;
                int v1 = i / 2 % 2;
                int v2 = i / 4 % 2;
                int v3 = i / 8 % 2;
                int rot = v2 * 8 + v1 * 4 + v0 * 2 + v3;
                DegenerationTable[i] = (byte)(rot ^ i);
            }
        }

    }
}
