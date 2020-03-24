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
   public class FSI : Response
   {
      public double TransmittingFrequency;
      public double ReceivingFrequency;
      public NMEA.CommunicationsMode Mode;
      public int PowerLevel;

      public FSI() : base("FSI")
      {
         Empty();
      }

      public override void Empty()
      {
         base.Empty();

         TransmittingFrequency = 0.0D;
         ReceivingFrequency = 0.0D;
         Mode = CommunicationsMode.Unknown;
         PowerLevel = 0;
      }

      public override bool Parse(Sentence sentence)
      {
         /*
         ** FSI - Frequency Set Information
         **
         **        1      2      3 4 5
         **        |      |      | | |
         ** $--FSI,xxxxxx,xxxxxx,c,x*hh<CR><LF>
         **
         ** Field Number: 
         **  1) Transmitting Frequency
         **  2) Receiving Frequency
         **  3) Communications Mode
         **  4) Power Level
         **  5) Checksum
         */

         /*
         ** First we check the checksum...
         */

         if (sentence.IsChecksumBad() == Boolean.True)
         {
            Empty();
            return (false);
         }

         TransmittingFrequency = sentence.Double(1);
         ReceivingFrequency = sentence.Double(2);
         Mode = sentence.CommunicationsMode(3);
         PowerLevel = sentence.Integer(4);

         return (true);
      }
   }
}
