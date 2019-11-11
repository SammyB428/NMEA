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
