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

      public ALM() : base("ALM")
      {
         Empty();
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

         if (sentence.IsChecksumBad() == Boolean.True)
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
