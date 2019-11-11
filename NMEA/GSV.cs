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
   public class GSV : Response
   {
      public int NumberOfSatellites;
      public SatelliteData[] SatellitesInView;

      public GSV() : base("GSV")
      {
         SatellitesInView = new SatelliteData[12];
         Empty();
      }

      public override void Empty()
      {
         base.Empty();

         NumberOfSatellites = 0;

         for (int index = 0; index < SatellitesInView.Length; index++)
         {
            SatellitesInView[index].Empty();
         }
      }

      public override bool Parse(Sentence sentence)
      {
         /*
         ** GSV - TRANSIT Position - Latitude/Longitude
         ** Location and time of TRANSIT fix at waypoint
         **
         **        1 2 3  4  5  6   7  8  9  10  11 12 13 14  15 16 17 18  19  20
         **        | | |  |  |  |   |  |  |  |   |  |  |  |   |  |  |  |   |   |
         ** $--GSV,x,x,xx,xx,xx,xxx,xx,xx,xx,xxx,xx,xx,xx,xxx,xx,xx,xx,xxx,xx,*hh<CR><LF>
         **
         **  1) Total number of messages, 1-3
         **  2) Message Number, 1-3
         **  3) Total number of satellites in view
         **  4) Satellite Number #1
         **  5) Elevation #1
         **  6) Azimuth, Degrees True #1
         **  7) SNR #1, NULL when not tracking
         **  8) Satellite Number #2
         **  9) Elevation #2
         ** 10) Azimuth, Degrees True #2
         ** 11) SNR #2, NULL when not tracking
         ** 12) Satellite Number #3
         ** 13) Elevation #3
         ** 14) Azimuth, Degrees True #3
         ** 15) SNR #3, NULL when not tracking
         ** 16) Satellite Number #4
         ** 17) Elevation #4
         ** 18) Azimuth, Degrees True #4
         ** 19) SNR #4, NULL when not tracking
         ** 20) Checksum
         */

         /*
         ** First we check the checksum...
         */

         if (sentence.IsChecksumBad() == Boolean.True)
         {
            return (false);
         }

         int message_number = sentence.Integer(2);

         NumberOfSatellites = sentence.Integer(3);

         int index = 0;

         while (index < 4)
         {
            //SatellitesInView[((message_number - 1) * 4) + index].Parse((index * 4) + 4, sentence);
            index++;
         }

         return (true);
      }
   }
}
