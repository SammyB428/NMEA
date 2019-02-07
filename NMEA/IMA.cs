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
   public class IMA : Response
   {
      public string VesselName;
      public string Callsign;
      public LatLong Position;
      public double HeadingDegreesTrue;
      public double HeadingDegreesMagnetic;
      public double SpeedKnots;

      public IMA()
      {
         Mnemonic = "IMA";
      }

      public override void Empty()
      {
         base.Empty();

         VesselName = "";
         Callsign = "";
         Position.Empty();
         HeadingDegreesTrue = 0.0D;
         HeadingDegreesMagnetic = 0.0D;
         SpeedKnots = 0.0D;

         Mnemonic = "IMA";
      }

      public override bool Parse(Sentence sentence)
      {
         /*
         ** IMA - Vessel Identification
         **                                                              11    13
         **        1            2       3       4 5        6 7   8 9   10|   12|
         **        |            |       |       | |        | |   | |   | |   | |
         ** $--IMA,aaaaaaaaaaaa,aaaxxxx,llll.ll,a,yyyyy.yy,a,x.x,T,x.x,M,x.x,N*hh<CR><LF>
         **
         **  1) Twelve character vessel name
         **  2) Radio Call Sign
         **  3) Latitude
         **  4) North/South
         **  5) Longitude
         **  6) East/West
         **  7) Heading, degrees true
         **  8) T = True
         **  9) Heading, degrees magnetic
         ** 10) M = Magnetic
         ** 11) Speed
         ** 12) N = Knots
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

         VesselName = sentence.Field(1);
         Callsign = sentence.Field(2);
         Position.Parse(3, 4, 5, 6, sentence);
         HeadingDegreesTrue = sentence.Double(7);
         HeadingDegreesMagnetic = sentence.Double(9);
         SpeedKnots = sentence.Double(11);

         return (true);
      }
   }
}
