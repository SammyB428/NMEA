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

      public SFI()
      {
         Frequencies = new FrequencyMode[6];
         Mnemonic = "SFI";
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

         Mnemonic = "SFI";
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
