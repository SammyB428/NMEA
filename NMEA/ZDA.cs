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
   public class ZDA : Response
   {
      public System.DateTime Time;
      public int LocalHourDeviation;
      public int LocalMinutesDeviation;

      public ZDA()
      {
         Time = new System.DateTime(1980, 1, 6);
         Mnemonic = "ZDA";
      }

      public override void Empty()
      {
         base.Empty();

         Time = new System.DateTime(1980, 1, 6);
         LocalHourDeviation = 0;
         LocalMinutesDeviation = 0;

         Mnemonic = "ZDA";
      }

      public override bool Parse(Sentence sentence)
      {
         /*
         ** ZDA - Time & Date
         ** UTC, day, month, year and local time zone
         **
         ** $--ZDA,hhmmss.ss,xx,xx,xxxx,xx,xx*hh<CR><LF>
         **        |         |  |  |    |  |
         **        |         |  |  |    |  +- Local zone minutes description, same sign as local hours
         **        |         |  |  |    +- Local zone description, 00 to +- 13 hours
         **        |         |  |  +- Year
         **        |         |  +- Month, 01 to 12
         **        |         +- Day, 01 to 31
         **        +- Universal Time Coordinated (UTC)
         */

         /*
         ** First we check the checksum...
         */

         if (sentence.IsChecksumBad() == NMEA.Boolean.True)
         {
            Empty();
            return (false);
         }

         Time = sentence.Time(1);
         int day = sentence.Integer(2);
         int month = sentence.Integer(3);
         int year = sentence.Integer(4);
         Time = new System.DateTime(year, month, day, Time.Hour, Time.Minute, Time.Second, Time.Millisecond);
         LocalHourDeviation = sentence.Integer(5);
         LocalMinutesDeviation = sentence.Integer(6);

         return (true);
      }
   }
}
