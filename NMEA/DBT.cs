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
   public class DBT : Response
   {
      public double DepthFeet;
      public double DepthMeters;
      public double DepthFathoms;

      public DBT() : base("DBT")
      {
         Empty();
      }

      public override void Empty()
      {
         base.Empty();

         DepthFeet = 0.0D;
         DepthMeters = 0.0D;
         DepthFathoms = 0.0D;
      }

      public override bool Parse(Sentence sentence)
      {
         /*
         ** DBT - Depth below transducer
         **
         **        1   2 3   4 5   6 7
         **        |   | |   | |   | |
         ** $--DBT,x.x,f,x.x,M,x.x,F*hh<CR><LF>
         **
         ** Field Number: 
         **  1) Depth, feet
         **  2) f = feet
         **  3) Depth, meters
         **  4) M = meters
         **  5) Depth, Fathoms
         **  6) F = Fathoms
         **  7) Checksum
         */

         /*
         ** First we check the checksum...
         */

         if (sentence.IsChecksumBad() == Boolean.True)
         {
            Empty();
            return (false);
         }

         DepthFeet = sentence.Double(1);
         DepthMeters = sentence.Double(3);
         DepthFathoms = sentence.Double(5);

         return (true);
      }
   }
}
