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
   public class VWT : Response
   {
      public double CalculatedWindAngle;
      public LeftOrRight CalculatedDirection;
      public double CalculatedWindSpeedKnots;
      public double CalculatedWindSpeedMetersPerSecond;
      public double CalculatedWindSpeedKilometersPerHour;

      public VWT() : base("VWT")
      {
         Empty();
      }

      public override void Empty()
      {
         base.Empty();

         CalculatedWindAngle = 0.0D;
         CalculatedDirection = LeftOrRight.Unknown;
         CalculatedWindSpeedKnots = 0.0D;
         CalculatedWindSpeedMetersPerSecond = 0.0D;
         CalculatedWindSpeedKilometersPerHour = 0.0D;
      }

      public override bool Parse(Sentence sentence)
      {
         /*
         ** VWT - True Wind Speed and Angle
         **
         **        1   2 3   4 5   6 7   8 9
         **        |   | |   | |   | |   | |
         ** $--VWT,x.x,a,x.x,N,x.x,M,x.x,K*hh<CR><LF>
         **
         ** Field Number: 
         **  1) Calculated Wind Angle in relation to the boat, 0 to 180 degrees
         **  2) Left or Right
         **  3) Calculated wind speed in knots
         **  4) N = Knots
         **  5) Calculated wind speed in meters per second
         **  6) M = Meters per second
         **  7) Calculated wind speed in kilometers per hour
         **  8) K = Kilometers per hour
         **  9) Checksum
         */

         /*
         ** First we check the checksum...
         */

         if (sentence.IsChecksumBad() == NMEA.Boolean.True)
         {
            Empty();
            return (false);
         }

         CalculatedWindAngle = sentence.Double(1);
         CalculatedDirection = sentence.LeftOrRight(2);
         CalculatedWindSpeedKnots = sentence.Double(3);
         CalculatedWindSpeedMetersPerSecond = sentence.Double(5);
         CalculatedWindSpeedKilometersPerHour = sentence.Double(7);

         return (true);
      }
   }
}
