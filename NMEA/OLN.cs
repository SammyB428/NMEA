#region License
/*
Author: Samuel R. Blackburn
Internet: wfc@pobox.com

"You can get credit for something or get it done, but not both."
Dr. Richard Garwin

The MIT License (MIT)

Copyright (c) 1996-2019 Sam Blackburn

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

/* SPDX-License-Identifier: MIT */

#endregion

namespace NMEA
{
   public class OLN : Response
   {
      public OmegaPair Pair1;
      public OmegaPair Pair2;
      public OmegaPair Pair3;

      public OLN() : base("OLN")
      {
         Pair1 = new OmegaPair();
         Pair2 = new OmegaPair();
         Pair3 = new OmegaPair();
         Empty();
      }

      public override void Empty()
      {
         base.Empty();

         Pair1.Empty();
         Pair2.Empty();
         Pair3.Empty();
      }

      public override bool Parse(Sentence sentence)
      {
         /*
         ** OLN - Omega Lane Numbers
         **
         **        1          2          3          4
         **        |--------+ |--------+ |--------+ |
         ** $--OLN,aa,xxx,xxx,aa,xxx,xxx,aa,xxx,xxx*hh<CR><LF>
         **
         **  1) Omega Pair 1
         **  2) Omega Pair 1
         **  3) Omega Pair 1
         **  4) Checksum
         */

         /*
         ** First we check the checksum...
         */

         if (sentence.IsChecksumBad() == NMEA.Boolean.True)
         {
            Empty();
            return (false);
         }

         Pair1.Parse(1, sentence);
         Pair2.Parse(4, sentence);
         Pair3.Parse(7, sentence);

         return (true);
      }
   }
}
