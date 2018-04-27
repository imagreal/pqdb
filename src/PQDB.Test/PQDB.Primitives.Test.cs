using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using PQDB.Primitives;
namespace PQDB.Test
{
    [TestClass]
    public class PQDB_Primitives_Test
    {
        [TestMethod]
        public void HalfStateTest()
        {
            foreach(var hVal in Enum.GetValues(typeof(Half)))
            {
                var h = (Half)hVal;
                Debug.WriteLine($"{h}\t{h.GetState()}");
            }
        }

        [TestMethod]
        public void UnitTest()
        {
            string ul = "\u25F8";
            string ur = "\u25F9";
            string ll = "\u25FA";
            string lr = "\u25FF";

            string[][] chars = new string[][] {
                new string[]{ lr, ur, ll, ul},
                new string[]{ ur, ul, lr, ll},
                new string[]{ ul, ll, ur, lr},
                new string[]{ ll, lr, ul, ur},
                };
            string[,] grids = new string[32, 32];
            for(int i=0; i<16; i++)
            {
                for(int j=0;j<16;j++)
                {
                    Unit u = new Unit(0);
                    u.SetUnit((byte)i, (byte)j);
                    grids[i * 2, j * 2] = chars[0][(byte)u.InnerQuarter];
                    grids[i * 2 + 1, j * 2] = chars[1][(byte)u.DexterQuarter];
                    grids[i * 2 + 1, j * 2 + 1] = chars[2][(byte)u.ExterQuarter];
                    grids[i * 2, j * 2 + 1] = chars[3][(byte)u.SinisterQuarter];
                }
            }

            for(int i=0;i<32;i++)
            {
                for(int j=0;j<32;j++)
                {
                    Debug.Write(grids[i, j]);
                }
                Debug.WriteLine("");
            }
        }
    }
}
