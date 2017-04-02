//
// MSGTest/Types/Dir/DirTests.cs
//

using MSG.IO;
using MSG.Types.Dir;
using NUnit.Framework;
using System;

namespace MSGTest.Types.Dir
{
    public class Contents
    {
        public string S { get; set; }
        public int I { get; set; }

        public override bool Equals(object obj)
        {
            Contents o = (Contents) obj;
            return S == o.S && I == o.I;
        }

        public override int GetHashCode()
        {
            return S.GetHashCode() + I.GetHashCode();
        }
    }
}
