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
   public class GLC : Response
   {
      public int GroupRepetitionInterval;
      public LoranTimeDifference MasterTOA;
      public LoranTimeDifference TimeDifference1;
      public LoranTimeDifference TimeDifference2;
      public LoranTimeDifference TimeDifference3;
      public LoranTimeDifference TimeDifference4;
      public LoranTimeDifference TimeDifference5;

      public GLC()
      {
         Mnemonic = "GLC";
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

         Mnemonic = "GLC";
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

         Boolean checksum_is_bad = sentence.IsChecksumBad();

         if (checksum_is_bad == Boolean.True)
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
