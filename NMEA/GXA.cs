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
   public class GXA : Response
   {
      public System.DateTime UTCTime;
      public LatLong Position;
      public string WaypointID;
      public int SatelliteNumber;

      public GXA() : base("GXA")
      {
         Position = new LatLong();
         Empty();
      }

      public override void Empty()
      {
         base.Empty();

         UTCTime = Response.GPSEpoch;
         Position.Empty();
         WaypointID = string.Empty;
         SatelliteNumber = 0;
      }

      public override bool Parse(Sentence sentence)
      {
         /*
         ** GXA - TRANSIT Position - Latitude/Longitude
         ** Location and time of TRANSIT fix at waypoint
         **
         **        1         2       3 4        5 6    7 8
         **        |         |       | |        | |    | |
         ** $--GXA,hhmmss.ss,llll.ll,a,yyyyy.yy,a,c--c,X*hh<CR><LF>
         **
         ** 1) UTC of position fix
         ** 2) Latitude
         ** 3) East or West
         ** 4) Longitude
         ** 5) North or South
         ** 6) Waypoint ID
         ** 7) Satelite number
         ** 8) Checksum
         */

         /*
         ** First we check the checksum...
         */

         if (sentence.IsChecksumBad() == NMEA.Boolean.True)
         {
            Empty();
            return (false);
         }

         UTCTime = sentence.Time(1);
         Position.Parse(2, 3, 4, 5, sentence);
         WaypointID = sentence.Field(6);
         SatelliteNumber = sentence.Integer(7);

         return (true);
      }
   }
}
