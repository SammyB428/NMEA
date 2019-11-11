#region License
/*
** Author: Samuel R. Blackburn
** Internet: wfc@pobox.com
**
** "You can get credit for something or get it done, but not both."
** Dr. Richard Garwin
**
** A modified BSD License follows. This license is based on the
** 3-clause license ("New BSD License" or "Modified BSD License") found at
** http://en.wikipedia.org/wiki/BSD_license
** The difference is the dropping of the "advertising" clause. You do not,
** I repeat, DO NOT have to include this copyright notice in binary distributions.
**
** Copyright (c) 2011, Samuel R. Blackburn
** All rights reserved.
**
** Redistribution and use in source and binary forms, with or without
** modification, are permitted provided that the following conditions are met:
**     * Redistributions of source code must retain the above copyright
**       notice, this list of conditions and the following disclaimer.
**     * Neither the name of the <organization> nor the
**       names of its contributors may be used to endorse or promote products
**       derived from this software without specific prior written permission.
**
** THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
** ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
** WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
** DISCLAIMED. IN NO EVENT SHALL SAMUEL R. BLACKBURN BE LIABLE FOR ANY
** DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
** (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
** LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
** ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
** (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
** SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
**
*/
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
