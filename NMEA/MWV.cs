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
   public class MWV : Response
   {
      public double WindAngle;
      public string Reference;
      public double WindSpeed;
      public string WindSpeedUnits;
      public Boolean IsDataValid;

      public MWV() : base("MWV")
      {
         Empty();
      }

      public override void Empty()
      {
         base.Empty();

         WindAngle = 0.0D;
         Reference = string.Empty;
         WindSpeed = 0.0D;
         WindSpeedUnits = string.Empty;
         IsDataValid = Boolean.Unknown;
      }

      public override bool Parse(Sentence sentence)
      {
         /*
         ** MWV - Wind Speed and Angle
         **
         **        1   2 3   4 5
         **        |   | |   | |
         ** $--MWV,x.x,a,x.x,a*hh<CR><LF>
         **
         ** Field Number: 
         **  1) Wind Angle, 0 to 360 degrees
         **  2) Reference, R = Relative, T = True
         **  3) Wind Speed
         **  4) Wind Speed Units, K/M/N
         **  5) Status, A = Data Valid
         **  6) Checksum
         */

         /*
         ** First we check the checksum...
         */

         if (sentence.IsChecksumBad() == Boolean.True)
         {
            Empty();
            return (false);
         }

         WindAngle = sentence.Double(1);
         Reference = sentence.Field(2);
         WindSpeed = sentence.Double(3);
         WindSpeedUnits = sentence.Field(4);
         IsDataValid = sentence.Boolean(5);

         return (true);
      }
   }
}
