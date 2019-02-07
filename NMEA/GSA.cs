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

      public GSA()
      {
         Satellites = new int[12];
         Mnemonic = "GSA";
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

         Mnemonic = "GSA";
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

         Boolean checksum_is_bad = sentence.IsChecksumBad();

         if (checksum_is_bad == Boolean.True)
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
