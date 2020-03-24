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
   public class GLC : Response
   {
      public int GroupRepetitionInterval;
      public LoranTimeDifference MasterTOA;
      public LoranTimeDifference TimeDifference1;
      public LoranTimeDifference TimeDifference2;
      public LoranTimeDifference TimeDifference3;
      public LoranTimeDifference TimeDifference4;
      public LoranTimeDifference TimeDifference5;

      public GLC() : base("GLC")
      {
         MasterTOA = new LoranTimeDifference();
         TimeDifference1 = new LoranTimeDifference();
         TimeDifference2 = new LoranTimeDifference();
         TimeDifference3 = new LoranTimeDifference();
         TimeDifference4 = new LoranTimeDifference();
         TimeDifference5 = new LoranTimeDifference();
         Empty();
      }

      public override void Empty()
      {
         base.Empty();

         GroupRepetitionInterval = 0;
         MasterTOA.Empty();
         TimeDifference1.Empty();
         TimeDifference2.Empty();
         TimeDifference3.Empty();
         TimeDifference4.Empty();
         TimeDifference5.Empty();
      }

      public override bool Parse(Sentence sentence)
      {
         /*
         ** GLC - Geographic Position, Loran-C
         **                                           12    14
         **        1    2   3 4   5 6   7 8   9 10  11|   13|
         **        |    |   | |   | |   | |   | |   | |   | |
         ** $--GLC,xxxx,x.x,a,x.x,a,x.x,a.x,x,a,x.x,a,x.x,a*hh<CR><LF>
         **
         **  1) Group Repetition Interval (GRI) Microseconds/10
         **  2) Master TOA Microseconds
         **  3) Master TOA Signal Status
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

         if (sentence.IsChecksumBad() == Boolean.True)
         {
            Empty();
            return (false);
         }

         GroupRepetitionInterval = sentence.Integer(1);
         MasterTOA.Parse(2, sentence);
         TimeDifference1.Parse(4, sentence);
         TimeDifference2.Parse(6, sentence);
         TimeDifference3.Parse(8, sentence);
         TimeDifference4.Parse(10, sentence);
         TimeDifference5.Parse(12, sentence);

         return (true);
      }
   }
}
