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
   public class GTD : Response
   {
      public string TimeDifference1;
      public string TimeDifference2;
      public string TimeDifference3;
      public string TimeDifference4;
      public string TimeDifference5;

      public GTD()
      {
         Mnemonic = "GTD";
      }

      public override void Empty()
      {
         base.Empty();

         TimeDifference1 = "";
         TimeDifference2 = "";
         TimeDifference3 = "";
         TimeDifference4 = "";
         TimeDifference5 = "";

         Mnemonic = "GTD";
      }

      public override bool Parse(Sentence sentence)
      {
         /*
         ** GTD - Geographical Position, Loran-C TDs
         **
         **        1   2   3   4   5   6
         **        |   |   |   |   |   |
         ** $--GTD,x.x,x.x,x.x,x,x,x.x*hh<CR><LF>
         **
         **  1) Time Difference 1 Microseconds
         **  2) Time Difference 2 Microseconds
         **  3) Time Difference 3 Microseconds
         **  4) Time Difference 4 Microseconds
         **  5) Time Difference 5 Microseconds
         **  6) Checksum
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

         TimeDifference1 = sentence.Field(1);
         TimeDifference2 = sentence.Field(2);
         TimeDifference3 = sentence.Field(3);
         TimeDifference4 = sentence.Field(4);
         TimeDifference5 = sentence.Field(5);

         return (true);
      }
   }
}
