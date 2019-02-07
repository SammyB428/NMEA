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
   public class LoranTimeDifference
   {
      public double Microseconds;
      public LoranSignalStatus SignalStatus;

      public LoranTimeDifference()
      {
      }

      public void Empty()
      {
         Microseconds = 0.0D;
         SignalStatus = LoranSignalStatus.Unknown;
      }

      public bool Parse( int first_field_number, Sentence sentence)
      {
         Microseconds = sentence.Double(first_field_number);

         string field_data = sentence.Field(first_field_number + 1);

         if (field_data == "B")
         {
            SignalStatus = LoranSignalStatus.BlinkWarning;
         }
         else if (field_data == "C")
         {
            SignalStatus = LoranSignalStatus.CycleWarning;
         }
         else if (field_data == "S")
         {
            SignalStatus = LoranSignalStatus.SignalToNoiseRatioWarning;
         }
         else if (field_data == "A")
         {
            SignalStatus = LoranSignalStatus.Valid;
         }
         else
         {
            SignalStatus = LoranSignalStatus.Unknown;
         }

         return (true);
      }
   }
}
