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
   /// <summary>
   /// The ALM sentence contains GPS almanac data.
   /// </summary>
   public class ALM : Response
   {
      public int NumberOfMessages;
      public int MessageNumber;
      public int PRNNumber;
      public int WeekNumber;
      public int SVHealth;
      public int Eccentricity;
      public int AlmanacReferenceTime;
      public int InclinationAngle;
      public int RateOfRightAscension;
      public int RootOfSemiMajorAxis;
      public int ArgumentOfPerigee;
      public int LongitudeOfAscensionNode;
      public int MeanAnomaly;
      public int F0ClockParameter;
      public int F1ClockParameter;

      public ALM()
      {
         Mnemonic = "ALM";
      }

      public override void Empty()
      {
         base.Empty();

         NumberOfMessages = 0;
         MessageNumber = 0;
         PRNNumber = 0;
         WeekNumber = 0;
         SVHealth = 0;
         Eccentricity = 0;
         AlmanacReferenceTime = 0;
         InclinationAngle = 0;
         RateOfRightAscension = 0;
         RootOfSemiMajorAxis = 0;
         ArgumentOfPerigee = 0;
         LongitudeOfAscensionNode = 0;
         MeanAnomaly = 0;
         F0ClockParameter = 0;
         F1ClockParameter = 0;

         Mnemonic = "ALM";
      }

      public override bool Parse(Sentence sentence)
      {
         /*
         ** ALM - GPS Almanac Data
         **
         **        1   2   3  4   5  6    7  8    9    10     11     12     13     14  15   16
         **        |   |   |  |   |  |    |  |    |    |      |      |      |      |   |    |
         ** $--ALM,x.x,x.x,xx,x.x,hh,hhhh,hh,hhhh,hhhh,hhhhhh,hhhhhh,hhhhhh,hhhhhh,hhh,hhh,*hh<CR><LF>
         **
         **  1) Total number of messages
         **  2) Message Number
         **  3) Satellite PRN number (01 to 32)
         **  4) GPS Week Number
         **  5) SV health, bits 17-24 of each almanac page
         **  6) Eccentricity
         **  7) Almanac Reference Time
         **  8) Inclination Angle
         **  9) Rate of Right Ascension
         ** 10) Root of semi-major axis
         ** 11) Argument of perigee
         ** 12) Longitude of ascension node
         ** 13) Mean anomaly
         ** 14) F0 Clock Parameter
         ** 15) F1 Clock Parameter
         ** 16) Checksum
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

         NumberOfMessages = sentence.Integer( 1 );
         MessageNumber = sentence.Integer( 2 );
         PRNNumber = sentence.Integer( 3 );
         WeekNumber = sentence.Integer( 4 );
         SVHealth = sentence.HexadecimalInteger(5);
         Eccentricity = sentence.HexadecimalInteger(6);
         AlmanacReferenceTime = sentence.HexadecimalInteger(7);
         InclinationAngle = sentence.HexadecimalInteger(8);
         RateOfRightAscension = sentence.HexadecimalInteger(9);
         RootOfSemiMajorAxis = sentence.HexadecimalInteger(10);
         ArgumentOfPerigee = sentence.HexadecimalInteger(11);
         LongitudeOfAscensionNode = sentence.HexadecimalInteger(12);
         MeanAnomaly = sentence.HexadecimalInteger(13);
         F0ClockParameter = sentence.HexadecimalInteger(14);
         F1ClockParameter = sentence.HexadecimalInteger(15);

         return (true);
      }
   }
}
