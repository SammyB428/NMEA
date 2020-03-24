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
   public class VWR : Response
   {
      public double RelativeWindAngle;
      public LeftOrRight RelativeDirection;
      public double MeasuredWindSpeedKnots;
      public double MeasuredWindSpeedMetersPerSecond;
      public double MeasuredWindSpeedKilometersPerHour;

      public VWR() : base("VWR")
      {
         Empty();
      }

      public override void Empty()
      {
         base.Empty();

         RelativeWindAngle = 0.0D;
         RelativeDirection = LeftOrRight.Unknown;
         MeasuredWindSpeedKnots = 0.0D;
         MeasuredWindSpeedMetersPerSecond = 0.0D;
         MeasuredWindSpeedKilometersPerHour = 0.0D;
      }

      public override bool Parse(Sentence sentence)
      {
         /*
         ** VWR - Relative (apparent) Wind Speed and Angle
         **
         **        1   2 3   4 5   6 7   8 9
         **        |   | |   | |   | |   | |
         ** $--VWR,x.x,a,x.x,N,x.x,M,x.x,K*hh<CR><LF>
         **
         ** Field Number: 
         **  1) Measured Wind Angle in relation to the boat, 0 to 180 degrees
         **  2) Left or Right
         **  3) Measured wind speed in knots
         **  4) N = Knots
         **  5) Measured wind speed in meters per second
         **  6) M = Meters per second
         **  7) Measured wind speed in kilometers per hour
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

         RelativeWindAngle = sentence.Double(1);
         RelativeDirection = sentence.LeftOrRight(2);
         MeasuredWindSpeedKnots = sentence.Double(3);
         MeasuredWindSpeedMetersPerSecond = sentence.Double(5);
         MeasuredWindSpeedKilometersPerHour = sentence.Double(7);

         return (true);
      }
   }
}
