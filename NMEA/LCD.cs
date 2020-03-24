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
   public class LCD : Response
   {
      public int GroupRepetitionInterval;
      public RatioAndPulse Master;
      public RatioAndPulse Secondary1;
      public RatioAndPulse Secondary2;
      public RatioAndPulse Secondary3;
      public RatioAndPulse Secondary4;
      public RatioAndPulse Secondary5;

      public LCD() : base("LCD")
      {
         Master = new RatioAndPulse();
         Secondary1 = new RatioAndPulse();
         Secondary2 = new RatioAndPulse();
         Secondary3 = new RatioAndPulse();
         Secondary4 = new RatioAndPulse();
         Secondary5 = new RatioAndPulse();
         Empty();
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
