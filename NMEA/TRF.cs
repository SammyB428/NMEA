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

      public TRF()
      {
         Position = new LatLong();
         UTCTime = new System.DateTime(1980, 1, 6);
         Mnemonic = "TRF";
      }

      public override void Empty()
      {
         base.Empty();

         UTCTime = new System.DateTime(1980, 1, 6);
         IsDataValid = NMEA.Boolean.Unknown;
         Position.Empty();
         ElevationAngle = 0.0D;
         NumberOfIterations = 0.0D;
         NumberOfDopplerIntervals = 0.0D;
         UpdateDistanceNauticalMiles = 0.0D;
         SatelliteID = 0;

         Mnemonic = "TRF";
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
