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
   public class RMC : Response
   {
      public System.DateTime UTCTime;
      public Boolean IsDataValid;
      public LatLong Position;
      public FAAModeIndicator Mode;
      public double SpeedOverGroundKnots;
      public double TrackMadeGoodDegreesTrue;
      public double MagneticVariation;
      public EastOrWest MagneticVariationDirection;

      public RMC() : base("RMC")
      {
         Position = new LatLong();
         Empty();
      }

      public override void Empty()
      {
         base.Empty();

         UTCTime = Response.GPSEpoch;
         IsDataValid = NMEA.Boolean.Unknown;
         Position.Empty();
         Mode = FAAModeIndicator.Unknown;
         SpeedOverGroundKnots = 0.0D;
         TrackMadeGoodDegreesTrue = 0.0D;
         MagneticVariationDirection = EastOrWest.Unknown;
      }

      public override bool Parse(Sentence sentence)
      {
         /*
         ** RMC - Recommended Minimum Navigation Information
         **                                                            12
         **        1         2 3       4 5        6 7   8   9    10  11|
         **        |         | |       | |        | |   |   |    |   | |
         ** $--RMC,hhmmss.ss,A,llll.ll,a,yyyyy.yy,a,x.x,x.x,xxxx,x.x,a*hh<CR><LF>
         **
         ** Field Number: 
         **  1) UTC Time
         **  2) Status, V = Navigation receiver warning
         **  3) Latitude
         **  4) N or S
         **  5) Longitude
         **  6) E or W
         **  7) Speed over ground, knots
         **  8) Track made good, degrees true
         **  9) Date, ddmmyy
         ** 10) Magnetic Variation, degrees
         ** 11) E or W
         ** 12) Checksum
         */

         /*
         ** First we check the checksum...
         */

         if (sentence.IsChecksumBad() == NMEA.Boolean.True)
         {
            Empty();
            return (false);
         }

         UTCTime                    = sentence.DateTime( 9, 1 );
         IsDataValid                = sentence.Boolean( 2 );
         Position.Parse( 3, 4, 5, 6, sentence );
         SpeedOverGroundKnots       = sentence.Double( 7 );
         TrackMadeGoodDegreesTrue   = sentence.Double( 8 );
         MagneticVariation          = sentence.Double( 10 );
         MagneticVariationDirection = sentence.EastOrWest( 11 );

         int checksum_field_number = sentence.ChecksumFieldNumber();

         if ( checksum_field_number == 13 )
         {
            Mode = sentence.FAAMode( 12 );
         }
         else
         {
            Mode = FAAModeIndicator.Unknown;
         }

         return (true);
      }
   }
}
