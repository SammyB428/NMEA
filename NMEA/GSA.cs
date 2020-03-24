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
   public class GSA : Response
   {
      public enum OperatingMode
      {
         Unknown = 0,
         Manual,
         Automatic
      };

      public enum FixMode
      {
         Unknown = 0,
         Unavailable,
         TwoDimensional,
         ThreeDimensional
      };

      public OperatingMode Operating;
      public FixMode Fix;
      public int [] Satellites;
      public double PositionDilutionOfPrecision; // aka PDOP;
      public double HorizontalDilutionOfPrecision; // aka HDOP
      public double VerticalDilutionOfPrecision; // aka VDOP;

      public GSA() : base("GSA")
      {
         Satellites = new int[12];
         Empty();
      }

      public override void Empty()
      {
         base.Empty();

         Operating = OperatingMode.Unknown;
         Fix = FixMode.Unknown;
         PositionDilutionOfPrecision = 0.0D;
         HorizontalDilutionOfPrecision = 0.0D;
         VerticalDilutionOfPrecision = 0.0D;

         for (int index = 0; index < Satellites.Length; index++)
         {
            Satellites[index] = 0;
         }
      }

      public override bool Parse(Sentence sentence)
      {
         /*
         ** GSA - GPS DOP and Active Satellites
         **
         **        1 2 3  4  5  6  7  8  9  10 11 12 13 14 15  16  17  18
         **        | | |  |  |  |  |  |  |  |  |  |  |  |  |   |   |   |
         ** $--GSA,a,x,xx,xx,xx,xx,xx,xx,xx,xx,xx,xx,xx,xx,x.x,x.x,x.x*hh<CR><LF>
         **
         ** Field Number: 
         **  1) Operating Mode, A = Automatic, M = Manual
         **  2) Fix Mode, 1 = Fix not available, 2 = 2D, 3 = 3D
         **  3) Satellite PRN #1
         **  4) Satellite PRN #2
         **  5) Satellite PRN #3
         **  6) Satellite PRN #4
         **  7) Satellite PRN #5
         **  8) Satellite PRN #6
         **  9) Satellite PRN #7
         ** 10) Satellite PRN #8
         ** 11) Satellite PRN #9
         ** 12) Satellite PRN #10
         ** 13) Satellite PRN #11
         ** 14) Satellite PRN #12
         ** 15) PDOP
         ** 16) HDOP
         ** 17) VDOP
         ** 18) Checksum
         */

         /*
         ** First we check the checksum...
         */

         if (sentence.IsChecksumBad() == Boolean.True)
         {
            Empty();
            return (false);
         }

         string field = sentence.Field(1);

         if (field == "A")
         {
            Operating = OperatingMode.Automatic;
         }
         else if (field == "M")
         {
            Operating = OperatingMode.Manual;
         }
         else
         {
            Operating = OperatingMode.Unknown;
         }

         int index = sentence.Integer(2);

         switch (index)
         {
            case 1:

               Fix = FixMode.Unavailable;
               break;

            case 2:

               Fix = FixMode.TwoDimensional;
               break;

            case 3:

               Fix = FixMode.ThreeDimensional;
               break;

            default:

               Fix = FixMode.Unknown;
               break;
         }

         index = 0;

         while (index < 12)
         {
            Satellites[index] = sentence.Integer(index + 3);
            index++;
         }

         PositionDilutionOfPrecision = sentence.Double(15);
         HorizontalDilutionOfPrecision = sentence.Double(16);
         VerticalDilutionOfPrecision = sentence.Double(17);

         return (true);
      }
   }
}
