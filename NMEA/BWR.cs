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
   public class BWR : Response
   {
      public System.DateTime UTCTime;
      public double BearingTrue;
      public double BearingMagnetic;
      public double DistanceNauticalMiles;
      public string To;
      public LatLong Position;
      public FAAModeIndicator FAAMode;

      public BWR() : base("BWR")
      {
         Position = new LatLong();

         Empty();
      }

      public override void Empty()
      {
         base.Empty();

         UTCTime = Response.GPSEpoch;
         Position.Empty();
         BearingTrue = 0.0D;
         BearingMagnetic = 0.0D;
         DistanceNauticalMiles = 0.0D;
         To = string.Empty;
         FAAMode = FAAModeIndicator.Unknown;
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

         if (sentence.IsChecksumBad() == Boolean.True)
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
