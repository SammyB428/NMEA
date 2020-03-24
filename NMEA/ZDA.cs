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
   public class ZDA : Response
   {
      public System.DateTime Time;
      public int LocalHourDeviation;
      public int LocalMinutesDeviation;

      public ZDA() : base("ZDA")
      {
         Empty();
      }

      public override void Empty()
      {
         base.Empty();

         Time = Response.GPSEpoch;
         LocalHourDeviation = 0;
         LocalMinutesDeviation = 0;
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
