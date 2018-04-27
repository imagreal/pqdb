using System;
using System.Collections.Generic;
using System.Text;

namespace PQDB.Primitives
{
    public enum Half : byte
    {
        Ora,  // 0000, Contradiction
        Xera, // 0001, NOR
        Mera, // 0010, Converse nonimplication
        Fera, // 0011, Primary negation
        Lera, // 0100, Material nonimplication 
        Gera, // 0101, Secondary negation
        Jera, // 0110, XOR, Exclusive disjunction
        Dera, // 0111, NAND
        Kera, // 1000, AND, Conjunction
        Era,  // 1001, XNOR, Biconditional
        Hera, // 1010, Secondary projection
        Cera, // 1011, Material implication
        Ira,  // 1100, Primary projection
        Bera, // 1101, Converse implication 
        Ara,  // 1110, OR, Disjunction
        Vera, // 1111, Tautology
    }

    

    
}
