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
   public class BOD : Response
   {
      public double BearingTrue;
      public double BearingMagnetic;
      public string To;
      public string From;

      public BOD() : base("BOD")
      {
         Empty();
      }

      public override void Empty()
      {
         base.Empty();

         BearingTrue = 0.0D;
         BearingMagnetic = 0.0D;
         To = string.Empty;
         From = string.Empty;
      }

      public override bool Parse(Sentence sentence)
      {
         /*
         ** BOD - Bearing - Origin Waypoint to Destination Waypoint
         **
         **        1   2 3   4 5    6    7
         **        |   | |   | |    |    |
         ** $--BOD,x.x,T,x.x,M,c--c,c--c*hh<CR><LF>
         **
         ** Field Number: 
         **  1) Bearing Degrees, TRUE
         **  2) T = True
         **  3) Bearing Degrees, Magnetic
         **  4) M = Magnetic
         **  5) TO Waypoint
         **  6) FROM Waypoint
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

         BearingTrue = sentence.Double(1);
         BearingMagnetic = sentence.Double(3);
         To = sentence.Field(5);
         From = sentence.Field(6);

         return (true);
      }
   }
}
