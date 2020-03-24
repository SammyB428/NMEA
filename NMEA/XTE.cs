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
   public class XTE : Response
   {
      public Boolean IsLoranBlinkOK;
      public Boolean IsLoranCCycleLockOK;
      public double CrossTrackErrorMagnitude;
      public LeftOrRight DirectionToSteer;
      public string CrossTrackUnits;
      public FAAModeIndicator FAAMode;

      public XTE() : base("XTE")
      {
         Empty();
      }

      public override void Empty()
      {
         base.Empty();

         IsLoranBlinkOK = Boolean.Unknown;
         IsLoranCCycleLockOK = Boolean.Unknown;
         CrossTrackErrorMagnitude = 0.0D;
         DirectionToSteer = LeftOrRight.Unknown;
         CrossTrackUnits = string.Empty;
         FAAMode = FAAModeIndicator.Unknown;
      }

      public override bool Parse(Sentence sentence)
      {
         /*
         ** XTE - Cross-Track Error, Measured
         **
         **        1 2 3   4 5  6
         **        | | |   | |  |
         ** $--XTE,A,A,x.x,a,N,*hh<CR><LF>
         **
         **  1) Status
         **     V = LORAN-C Blink or SNR warning
         **     V = general warning flag or other navigation systems when a reliable
         **         fix is not available
         **  2) Status
         **     V = Loran-C Cycle Lock warning flag
         **     A = OK or not used
         **  3) Cross Track Error Magnitude
         **  4) Direction to steer, L or R
         **  5) Cross Track Units, N = Nautical Miles
         **  6) Checksum
         */

         /*
         ** First we check the checksum...
         */

         if (sentence.IsChecksumBad() == Boolean.True)
         {
            Empty();
            return (false);
         }

         IsLoranBlinkOK = sentence.Boolean(1);
         IsLoranCCycleLockOK = sentence.Boolean(2);
         CrossTrackErrorMagnitude = sentence.Double(3);
         DirectionToSteer = sentence.LeftOrRight(4);
         CrossTrackUnits = sentence.Field(5);

         int checksum_field_number = sentence.ChecksumFieldNumber();

         if (checksum_field_number == 7)
         {
            FAAMode = sentence.FAAMode(6);
         }
         else
         {
            FAAMode = FAAModeIndicator.Unknown;
         }

         return (true);
      }
   }
}
