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
   public class BWR : Response
   {
      public System.DateTime UTCTime;
      public double BearingTrue;
      public double BearingMagnetic;
      public double DistanceNauticalMiles;
      public string To;
      public LatLong Position;
      public FAAModeIndicator FAAMode;

      public BWR()
      {
         UTCTime = new System.DateTime(1980, 1, 6);
         Position = new LatLong();
         Mnemonic = "BWR";
      }

      public override void Empty()
      {
         base.Empty();

         UTCTime = new System.DateTime(1980, 1, 6);
         Position.Empty();
         BearingTrue = 0.0D;
         BearingMagnetic = 0.0D;
         DistanceNauticalMiles = 0.0D;
         To = "";
         FAAMode = FAAModeIndicator.Unknown;

         Mnemonic = "BWR";
      }

      public override bool Parse(Sentence sentence)
      {
         /*
         ** BWR - Bearing and Distance to Waypoint - Rhumb Line
         ** Latitude, N/S, Longitude, E/W, UTC, Status
         **                                                       11
         **        1         2       3 4        5 6   7 8   9 10  | 12   13
         **        |         |       | |        | |   | |   | |   | |    |
         ** $--BWR,hhmmss.ss,llll.ll,a,yyyyy.yy,a,x.x,T,x.x,M,x.x,N,c--c*hh<CR><LF>
         **
         **  1) UTCTime
         **  2) Waypoint Latitude
         **  3) N = North, S = South
         **  4) Waypoint Longitude
         **  5) E = East, W = West
         **  6) Bearing, True
         **  7) T = True
         **  8) Bearing, Magnetic
         **  9) M = Magnetic
         ** 10) Nautical Miles
         ** 11) N = Nautical Miles
         ** 12) Waypoint ID
         ** 13) Checksum
         */

         /*
         ** First we check the checksum...
         */

         Boolean checksum_is_bad = sentence.IsChecksumBad();

         if (checksum_is_bad == Boolean.True)
         {
            Empty();
            return (false);
         }

         UTCTime = sentence.Time(1);
         Position.Parse(2, 3, 4, 5, sentence);
         BearingTrue = sentence.Double(6);
         BearingMagnetic = sentence.Double(8);
         DistanceNauticalMiles = sentence.Double(10);
         To = sentence.Field(12);

         int checksum_field_number = sentence.ChecksumFieldNumber();

         if (checksum_field_number == 14)
         {
            FAAMode = sentence.FAAMode(13);
         }
         else
         {
            FAAMode = FAAModeIndicator.Unknown;
         }

         return (true);
      }
   }
}
