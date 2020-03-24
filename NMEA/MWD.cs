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
   public class MWD : Response
   {
      public double WindDirectionDegreesTrue;
      public double WindDirectionDegreesMagnetic;
      public double WindSpeedKnots;
      public double WindSpeedMetersPerSecond;

      public MWD() : base("MWD")
      {
         Empty();
      }

      public override void Empty()
      {
         base.Empty();

         WindDirectionDegreesTrue = 0.0D;
         WindDirectionDegreesMagnetic = 0.0D;
         WindSpeedKnots = 0.0D;
         WindSpeedMetersPerSecond = 0.0D;
      }

      public override bool Parse(Sentence sentence)
      {
         /*
         ** MWD - Wind Direction and Velocity, Surface
         **
         **        1   2 3   4 5   6 7   8 9
         **        |   | |   | |   | |   | |
         ** $--MWD,x.x,T,x.x,M,x.x,N,x.x,M*hh<CR><LF>
         **
         ** Field Number: 
         **  1) Wind direction, degrees True
         **  2) T = True
         **  3) Wind direction, degrees Magnetic
         **  4) M = Magnetic
         **  5) Wind Speed in Knots
         **  6) N = Knots
         **  7) Wind Speed in Meters per second
         **  8) M = Meters per second
         **  9) Checksum
         */

         /*
         ** First we check the checksum...
         */

         if (sentence.IsChecksumBad() == Boolean.True)
         {
            Empty();
            return (false);
         }

         WindDirectionDegreesTrue = sentence.Double(1);
         WindDirectionDegreesMagnetic = sentence.Double(3);
         WindSpeedKnots = sentence.Double(5);
         WindSpeedMetersPerSecond = sentence.Double(7);

         return (true);
      }
   }
}
