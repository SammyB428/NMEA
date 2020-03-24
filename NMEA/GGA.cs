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
   public class GGA : Response
   {
      public System.DateTime UTCTime;
      public LatLong Position;
      public int GPSQuality;
      public int NumberOfSatellitesInUse;
      public double HorizontalDilutionOfPrecision;
      public double AntennaAltitudeMeters;
      public double GeoidalSeparationMeters;
      public double AgeOfDifferentialGPSDataSeconds;
      public int DifferentialReferenceStationID;

      public GGA() : base("GGA")
      {
         Position = new LatLong();
         Empty();
      }

      public override void Empty()
      {
         base.Empty();

         UTCTime = Response.GPSEpoch;
         Position.Empty();
         GPSQuality = 0;
         NumberOfSatellitesInUse = 0;
         HorizontalDilutionOfPrecision = 0.0D;
         AntennaAltitudeMeters = 0.0D;
         GeoidalSeparationMeters = 0.0D;
         AgeOfDifferentialGPSDataSeconds = 0.0D;
         DifferentialReferenceStationID = 0;
      }

      public override bool Parse(Sentence sentence)
      {
         /*
         ** GGA - Global Positioning System Fix Data
         ** Time, Position and fix related data fora GPS receiver.
         **
         **                                                      11
         **        1         2       3 4        5 6 7  8   9  10 |  12 13  14   15
         **        |         |       | |        | | |  |   |   | |   | |   |    |
         ** $--GGA,hhmmss.ss,llll.ll,a,yyyyy.yy,a,x,xx,x.x,x.x,M,x.x,M,x.x,xxxx*hh<CR><LF>
         **
         ** Field Number: 
         **  1) Universal Time Coordinated (UTC)
         **  2) Latitude
         **  3) N or S (North or South)
         **  4) Longitude
         **  5) E or W (East or West)
         **  6) GPS Quality Indicator,
         **     0 - fix not available,
         **     1 - GPS fix,
         **     2 - Differential GPS fix
         **  7) Number of satellites in view, 00 - 12
         **  8) Horizontal Dilution of precision
         **  9) Antenna Altitude above/below mean-sea-level (geoid) 
         ** 10) Units of antenna altitude, meters
         ** 11) Geoidal separation, the difference between the WGS-84 earth
         **     ellipsoid and mean-sea-level (geoid), "-" means mean-sea-level
         **     below ellipsoid
         ** 12) Units of geoidal separation, meters
         ** 13) Age of differential GPS data, time in seconds since last SC104
         **     type 1 or 9 update, null field when DGPS is not used
         ** 14) Differential reference station ID, 0000-1023
         ** 15) Checksum
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
         GPSQuality = sentence.Integer(6);
         NumberOfSatellitesInUse = sentence.Integer(7);
         HorizontalDilutionOfPrecision = sentence.Double(8);
         AntennaAltitudeMeters = sentence.Double(9);
         GeoidalSeparationMeters = sentence.Double(11);
         AgeOfDifferentialGPSDataSeconds = sentence.Double(13);
         DifferentialReferenceStationID = sentence.Integer(14);

         return (true);
      }
   }
}
