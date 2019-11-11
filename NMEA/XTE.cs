﻿#region License
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
