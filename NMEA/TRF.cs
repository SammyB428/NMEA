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
   public class TRF : Response
   {
      public System.DateTime UTCTime;
      public Boolean IsDataValid;
      public LatLong Position;
      public double ElevationAngle;
      public double NumberOfIterations;
      public double NumberOfDopplerIntervals;
      public double UpdateDistanceNauticalMiles;
      public double SatelliteID;

      public TRF() : base("TRF")
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
         ElevationAngle = 0.0D;
         NumberOfIterations = 0.0D;
         NumberOfDopplerIntervals = 0.0D;
         UpdateDistanceNauticalMiles = 0.0D;
         SatelliteID = 0;
      }

      public override bool Parse(Sentence sentence)
      {
         /*
         ** TRF - TRANSIT Fix Data
         **                                                                    13
         **        1         2      3       4 5        6 7   8   9   10  11  12|
         **        |         |      |       | |        | |   |   |   |   |   | |
         ** $--TRF,hhmmss.ss,xxxxxx,llll.ll,a,yyyyy.yy,a,x.x,x.x,x.x,x.x,xxx,A*hh<CR><LF>
         **
         ** Field Number: 
         **  1) UTC Time
         **  2) Date, ddmmyy
         **  3) Latitude
         **  4) N or S
         **  5) Longitude
         **  6) E or W
         **  7) Elevation Angle
         **  8) Number of iterations
         **  9) Number of Doppler intervals
         ** 10) Update distance, nautical miles
         ** 11) Satellite ID
         ** 12) Data Validity
         ** 13) Checksum
         */

         /*
         ** First we check the checksum...
         */

         if (sentence.IsChecksumBad() == NMEA.Boolean.True)
         {
            Empty();
            return (false);
         }

         UTCTime = sentence.DateTime(2,1);
         Position.Parse(3, 4, 5, 6, sentence);
         ElevationAngle = sentence.Double(7);
         NumberOfIterations = sentence.Double(8);
         NumberOfDopplerIntervals = sentence.Double(9);
         UpdateDistanceNauticalMiles = sentence.Double(10);
         SatelliteID = sentence.Integer(11);
         IsDataValid = sentence.Boolean(12);

         return (true);
      }
   }
}
