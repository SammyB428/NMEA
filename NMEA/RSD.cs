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
   public class RSD : Response
   {
      public RadarData Data1;
      public RadarData Data2;
      public double CursorRangeFromOwnShip;
      public double CursorBearingDegreesClockwiseFromZero;
      public double RangeScale;
      public string RangeUnits;
      public Rotation DisplayRotation;

      public RSD() : base("RSD")
      {
         Data1 = new RadarData();
         Data2 = new RadarData();
         Empty();
      }

      public override void Empty()
      {
         base.Empty();

         Data1.Empty();
         Data2.Empty();
         CursorRangeFromOwnShip = 0.0D;
         CursorBearingDegreesClockwiseFromZero = 0.0D;
         RangeScale = 0.0D;
         RangeUnits = string.Empty;
         DisplayRotation = Rotation.Unknown;
      }

      public override bool Parse(Sentence sentence)
      {
         /*
         ** RSD - RADAR System Data
         **                                                        14
         **        1   2   3   4   5   6   7   8   9   10  11 12 13|
         **        |   |   |   |   |   |   |   |   |   |   |   | | |
         ** $--RSD,x.x,x.x,x.x,x.x,x.x,x.x,x.x,x.x,x.x,x.x,x.x,a,a*hh<CR><LF>
         **
         ** 14) Checksum
         */

         /*
         ** First we check the checksum...
         */

         if (sentence.IsChecksumBad() == NMEA.Boolean.True)
         {
            Empty();
            return (false);
         }

         Data1.Parse(1, sentence);
         Data2.Parse(5, sentence);
         CursorRangeFromOwnShip = sentence.Double(9);
         CursorBearingDegreesClockwiseFromZero = sentence.Double(10);
         RangeScale = sentence.Double(11);
         RangeUnits = sentence.Field(12);

         int temp_integer = sentence.Integer(13);

         switch (temp_integer)
         {
            case 'C':

               DisplayRotation = Rotation.CourseUp;
               break;

            case 'H':

               DisplayRotation = Rotation.HeadUp;
               break;

            case 'N':

               DisplayRotation = Rotation.NorthUp;
               break;

            default:

               DisplayRotation = Rotation.Unknown;
               break;
         }

         return (true);
      }
   }
}
