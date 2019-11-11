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
   public class RSA : Response
   {
      public double Starboard;
      public Boolean IsStarboardDataValid;
      public double Port;
      public Boolean IsPortDataValid;

      public RSA() : base("RSA")
      {
         Empty();
      }

      public override void Empty()
      {
         base.Empty();

         Starboard = 0.0D;
         IsStarboardDataValid = Boolean.Unknown;
         Port = 0.0D;
         IsPortDataValid = Boolean.Unknown;
      }

      public override bool Parse(Sentence sentence)
      {
         /*
         ** RSA - Rudder Sensor Angle
         **
         **        1   2 3   4 5
         **        |   | |   | |
         ** $--RSA,x.x,A,x.x,A*hh<CR><LF>
         **
         ** Field Number: 
         **  1) Starboard (or single) rudder sensor, "-" means Turn To Port
         **  2) Status, A means data is valid
         **  3) Port rudder sensor
         **  4) Status, A means data is valid
         **  5) Checksum
         */

         /*
         ** First we check the checksum...
         */

         if (sentence.IsChecksumBad() == NMEA.Boolean.True)
         {
            Empty();
            return (false);
         }

         Starboard = sentence.Double(1);
         IsStarboardDataValid = sentence.Boolean(2);
         Port = sentence.Double(3);
         IsPortDataValid = sentence.Boolean(4);

         return (true);
      }
   }
}
