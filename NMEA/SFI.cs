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
   public class FrequencyMode
   {
      public double Frequency;
      public NMEA.CommunicationsMode Mode;

      public FrequencyMode()
      {
      }

      public void Empty()
      {
         Frequency = 0.0D;
         Mode = CommunicationsMode.Unknown;
      }

      public void Parse(int field_number, Sentence sentence)
      {
         Frequency = sentence.Double(field_number);
         Mode = sentence.CommunicationsMode(field_number + 1);
      }
   }

   public class SFI : Response
   {
      public double TotalMessages;
      public double MessageNumber;
      public FrequencyMode[] Frequencies;

      public SFI() : base("SFI")
      {
         Frequencies = new FrequencyMode[6];
         Empty();
      }

      public override void Empty()
      {
         base.Empty();

         TotalMessages = 0.0D;
         MessageNumber = 0.0D;

         foreach( FrequencyMode f in Frequencies )
         {
            f.Empty();
         }
      }

      public override bool Parse(Sentence sentence)
      {
         /*
         ** SFI - Scanning Frequency Information
         **
         **        1   2   3      4                     x
         **        |   |   |      |                     |
         ** $--SFI,x.x,x.x,xxxxxx,c .......... xxxxxx,c*hh<CR><LF>
         **
         **  1) Total Number Of Messages
         **  2) Message Number
         **  3) Frequency 1
         **  4) Mode 1
         **  x) Checksum
         */

         /*
         ** First we check the checksum...
         */

         if (sentence.IsChecksumBad() == NMEA.Boolean.True)
         {
            Empty();
            return (false);
         }

         TotalMessages = sentence.Double(1);
         MessageNumber = sentence.Double(2);

         foreach (FrequencyMode f in Frequencies)
         {
            f.Empty();
         }

         int number_of_frequencies = ( sentence.GetNumberOfDataFields() - 2 ) / 2;
         int frequency_number = 0;

         int field_index = 0;

         while( frequency_number < number_of_frequencies )
         {
            field_index = 2 + (frequency_number * 2);

            Frequencies[frequency_number].Parse(field_index, sentence);

            frequency_number++;
         }

         return (true);
      }
   }
}
