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
   public class VWR : Response
   {
      public double RelativeWindAngle;
      public NMEA.LeftOrRight RelativeDirection;
      public double MeasuredWindSpeedKnots;
      public double MeasuredWindSpeedMetersPerSecond;
      public double MeasuredWindSpeedKilometersPerHour;

      public VWR()
      {
         Mnemonic = "VWR";
      }

      public override void Empty()
      {
         base.Empty();

         RelativeWindAngle = 0.0D;
         RelativeDirection = LeftOrRight.Unknown;
         MeasuredWindSpeedKnots = 0.0D;
         MeasuredWindSpeedMetersPerSecond = 0.0D;
         MeasuredWindSpeedKilometersPerHour = 0.0D;

         Mnemonic = "VWR";
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
