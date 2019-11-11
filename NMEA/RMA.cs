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
   public class RMA : Response
   {
      public NMEA.Boolean IsDataValid;
      public NMEA.LatLong Position;
      public double TimeDifferenceA;
      public double TimeDifferenceB;
      public double SpeedOverGroundKnots;
      public double TrackMadeGoodDegreesTrue;
      public double MagneticVariation;
      public NMEA.EastOrWest MagneticVariationDirection;
      public NMEA.FAAModeIndicator Mode;

      public RMA() : base("RMA")
      {
         Position = new LatLong();
         Empty();
      }

      public override void Empty()
      {
         base.Empty();

         IsDataValid = Boolean.Unknown;
         Position.Empty();
         TimeDifferenceA = 0.0D;
         TimeDifferenceB = 0.0D;
         SpeedOverGroundKnots = 0.0D;
         TrackMadeGoodDegreesTrue = 0.0D;
         MagneticVariation = 0.0D;
         MagneticVariationDirection = EastOrWest.Unknown;
         Mode = NMEA.FAAModeIndicator.Unknown;
      }

      public override bool Parse(Sentence sentence)
      {
         /*
         ** RMA - Recommended Minimum Navigation Information
         **                                                    12
         **        1 2       3 4        5 6   7   8   9   10  11|
         **        | |       | |        | |   |   |   |   |   | |
         ** $--RMA,A,llll.ll,a,yyyyy.yy,a,x.x,x.x,x.x,x.x,x.x,a*hh<CR><LF>
         **
         ** Field Number: 
         **  1) Blink Warning
         **  2) Latitude
         **  3) N or S
         **  4) Longitude
         **  5) E or W
         **  6) Time Difference A, uS
         **  7) Time Difference B, uS
         **  8) Speed Over Ground, Knots
         **  9) Track Made Good, degrees true
         ** 10) Magnetic Variation, degrees
         ** 11) E or W
         ** 12) Checksum
         */

         /*
         ** First we check the checksum...
         */

         if (sentence.IsChecksumBad() == Boolean.True)
         {
            Empty();
            return (false);
         }

         IsDataValid = sentence.Boolean(1);
         Position.Parse(2, 3, 4, 5, sentence);
         TimeDifferenceA = sentence.Double(6);
         TimeDifferenceB = sentence.Double(7);
         SpeedOverGroundKnots = sentence.Double(8);
         TrackMadeGoodDegreesTrue = sentence.Double(9);
         MagneticVariation = sentence.Double(10);
         MagneticVariationDirection = sentence.EastOrWest(11);

         int checksum_field_number = sentence.ChecksumFieldNumber();

         if (checksum_field_number == 13)
         {
            Mode = sentence.FAAMode(12);
         }
         else
         {
            Mode = FAAModeIndicator.Unknown;
         }

         return (true);
      }
   }
}
