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
   public class LCD : Response
   {
      public int GroupRepetitionInterval;
      public RatioAndPulse Master;
      public RatioAndPulse Secondary1;
      public RatioAndPulse Secondary2;
      public RatioAndPulse Secondary3;
      public RatioAndPulse Secondary4;
      public RatioAndPulse Secondary5;

      public LCD()
      {
         Master = new RatioAndPulse();
         Secondary1 = new RatioAndPulse();
         Secondary2 = new RatioAndPulse();
         Secondary3 = new RatioAndPulse();
         Secondary4 = new RatioAndPulse();
         Secondary5 = new RatioAndPulse();

         Mnemonic = "LCD";
      }

      public override void Empty()
      {
         base.Empty();

         GroupRepetitionInterval = 0;
         Master.Empty();
         Secondary1.Empty();
         Secondary2.Empty();
         Secondary3.Empty();
         Secondary4.Empty();
         Secondary5.Empty();

         Mnemonic = "LCD";
      }

      public override bool Parse(Sentence sentence)
      {
         /*
         ** LCD - Loran-C Signal Data
         **
         **        1    2   3   4   5   6   7   8   9   10  11  12  13  14
         **        |    |   |   |   |   |   |   |   |   |   |   |   |   |
         ** $--LCD,xxxx,xxx,xxx,xxx,xxx,xxx,xxx,xxx,xxx,xxx,xxx,xxx,xxx*hh<CR><LF>
         **
         **  1) Group Repetition Interval (GRI) Microseconds/10
         **  2) Master Relative SNR
         **  3) Master Relative ECD
         **  4) Time Difference 1 Microseconds
         **  5) Time Difference 1 Signal Status
         **  6) Time Difference 2 Microseconds
         **  7) Time Difference 2 Signal Status
         **  8) Time Difference 3 Microseconds
         **  9) Time Difference 3 Signal Status
         ** 10) Time Difference 4 Microseconds
         ** 11) Time Difference 4 Signal Status
         ** 12) Time Difference 5 Microseconds
         ** 13) Time Difference 5 Signal Status
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

         GroupRepetitionInterval = sentence.Integer(1);
         Master.Parse(2, sentence);
         Secondary1.Parse(4, sentence);
         Secondary2.Parse(6, sentence);
         Secondary3.Parse(8, sentence);
         Secondary4.Parse(10, sentence);
         Secondary5.Parse(12, sentence);

         return (true);
      }
   }
}
