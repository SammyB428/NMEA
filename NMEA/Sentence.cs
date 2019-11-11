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
   public class Sentence
   {
      public const int CarriageReturn = 0x0D;
      public const int LineFeed = 0x0A;

      public string Data;

      public void Empty()
      {
         Data = "";
      }

      public NMEA.Boolean Boolean(int field_number)
      {
         if (field_number < 0)
         {
            return (NMEA.Boolean.Unknown);
         }

         string field_data = Field(field_number);

         if (field_data == "A")
         {
            return (NMEA.Boolean.True);
         }
         else if (field_data == "V")
         {
            return (NMEA.Boolean.False);
         }

         return (NMEA.Boolean.Unknown);
      }

      public byte ComputeChecksum()
      {
         byte checksum_value = 0;

         int string_length = Data.Length;
         int index = 1; // Skip over the $ at the begining of the sentence

         while (index < string_length &&
                Data[index] != '*' &&
                Data[index] != Sentence.CarriageReturn &&
                Data[index] != Sentence.LineFeed)
         {
            checksum_value ^= (byte) Data[index];
            index++;
         }

         return (checksum_value);
      }

      public NMEA.CommunicationsMode CommunicationsMode(int field_number)
      {
         if (field_number < 0)
         {
            return (NMEA.CommunicationsMode.Unknown);
         }

         string field_data = Field( field_number );

         if ( field_data == "d" )
         {
            return( NMEA.CommunicationsMode.F3E_G3E_SimplexTelephone );
         }
         else if ( field_data == "e" )
         {
            return( NMEA.CommunicationsMode.F3E_G3E_DuplexTelephone );
         }
         else if ( field_data == "m" )
         {
            return (NMEA.CommunicationsMode.J3E_Telephone);
         }
         else if ( field_data == "o" )
         {
            return (NMEA.CommunicationsMode.H3E_Telephone);
         }
         else if ( field_data == "q" )
         {
            return (NMEA.CommunicationsMode.F1B_J2B_FEC_NBDP_TelexTeleprinter);
         }
         else if ( field_data == "s" )
         {
            return (NMEA.CommunicationsMode.F1B_J2B_ARQ_NBDP_TelexTeleprinter);
         }
         else if ( field_data == "w" )
         {
            return (NMEA.CommunicationsMode.F1B_J2B_ReceiveOnlyTeleprinterDSC);
         }
         else if ( field_data == "x" )
         {
            return (NMEA.CommunicationsMode.A1A_MorseTapeRecorder);
         }
         else if ( field_data == "{" )
         {
            return (NMEA.CommunicationsMode.A1A_MorseKeyHeadset);
         }
         else if ( field_data == "|" )
         {
            return (NMEA.CommunicationsMode.F1C_F2C_F3C_FaxMachine);
         }

         return (NMEA.CommunicationsMode.Unknown);
      }

      public double Double(int field_number)
      {
         if (field_number < 0)
         {
            return (0.0D);
         }

         string field_data = Field(field_number);

         if (field_data.Length <= 0)
         {
            return (0.0D);
         }

         double return_value = 0.0D;

         if (double.TryParse(field_data, out return_value) == false)
         {
            return (0.0D);
         }

         return (return_value );
      }

      public NMEA.EastOrWest EastOrWest(int field_number)
      {
         if (field_number < 0)
         {
            return (NMEA.EastOrWest.Unknown);
         }

         string field_data = Field(field_number);

         if (field_data == "E")
         {
            return (NMEA.EastOrWest.East);
         }
         else if (field_data == "W")
         {
            return (NMEA.EastOrWest.West);
         }

         return (NMEA.EastOrWest.Unknown);
      }

      public int GetNumberOfDataFields()
      {
         int index = 1; // Skip over the $ at the begining of the sentence
         int current_field_number = 0;
         int string_length = 0;

         string_length = Data.Length;

         while (index < string_length)
         {
            if (Data[index] == '*')
            {
               return (current_field_number);
            }

            if (Data[index] == ',')
            {
               current_field_number++;
            }

            index++;
         }

         return (current_field_number);
      }

      public int Integer(int field_number)
      {
         if ( field_number < 0 )
         {
            return( 0 );
         }

         string integer_string = Field( field_number );

         if ( integer_string.Length <= 0 )
         {
            return( 0 );
         }

         int return_value = 0;

         if (int.TryParse(integer_string, out return_value) == false)
         {
            return( 0 );
         }

         return (return_value);
      }

      public int HexadecimalInteger(int field_number)
      {
         if (field_number < 0)
         {
            return (0);
         }

         string integer_string = Field(field_number);

         if (integer_string.Length <= 0)
         {
            return (0);
         }

         int return_value = 0;

         if (int.TryParse(integer_string, System.Globalization.NumberStyles.HexNumber, System.Globalization.NumberFormatInfo.InvariantInfo, out return_value) == false)
         {
            return (0);
         }

         return (return_value);
      }

      public NMEA.Boolean IsChecksumBad()
      {
         if (Data.Length == 0)
         {
            return (NMEA.Boolean.Unknown);
         }

         int checksum_field_number = ChecksumFieldNumber();

         if (checksum_field_number == (-1))
         {
            return (NMEA.Boolean.Unknown);
         }

         /*
         ** Checksums are optional, return TRUE if an existing checksum is known to be bad
         */

         string checksum_in_sentence = Field(checksum_field_number);

         if (checksum_in_sentence.Length == 0)
         {
            return (NMEA.Boolean.Unknown);
         }

         if (ComputeChecksum() != HexValue(checksum_in_sentence))
         {
            return (NMEA.Boolean.True);
         }

         return (NMEA.Boolean.False);
      }

      public NMEA.LeftOrRight LeftOrRight(int field_number)
      {
         if (field_number < 0)
         {
            return (NMEA.LeftOrRight.Unknown);
         }

         string field_data = Field(field_number);

         if (field_data == "L")
         {
            return (NMEA.LeftOrRight.Left);
         }
         else if (field_data == "R")
         {
            return (NMEA.LeftOrRight.Right);
         }

         return (NMEA.LeftOrRight.Unknown);
      }

      public NMEA.NorthOrSouth NorthOrSouth(int field_number)
      {
         if (field_number < 0)
         {
            return (NMEA.NorthOrSouth.Unknown);
         }

         string field_data = Field(field_number);

         if (field_data == "N")
         {
            return (NMEA.NorthOrSouth.North);
         }
         else if (field_data == "S")
         {
            return (NMEA.NorthOrSouth.South);
         }

         return (NMEA.NorthOrSouth.Unknown);
      }

      public NMEA.Reference Reference(int field_number)
      {
         if (field_number < 0)
         {
            return (NMEA.Reference.Unknown);
         }

         string field_data = Field(field_number);

         if (field_data == "B")
         {
            return (NMEA.Reference.BottomTrackingLog);
         }
         else if (field_data == "M")
         {
            return (NMEA.Reference.ManuallyEntered);
         }
         else if (field_data == "W")
         {
            return (NMEA.Reference.WaterReferenced);
         }
         else if (field_data == "R")
         {
            return (NMEA.Reference.RadarTrackingOfFixedTarget);
         }
         else if (field_data == "P")
         {
            return (NMEA.Reference.PositioningSystemGroundReference);
         }

         return (NMEA.Reference.Unknown);
      }

      public System.DateTime DateTime(int date_field_number, int time_field_number)
      {
         // ddmmyy

         string temp_string = Field(date_field_number);

         if (temp_string.Length != 6)
         {
            // The original week zero https://en.wikipedia.org/wiki/Global_Positioning_System#Format
            return (Response.GPSEpoch);
         }

         int day = 0;

         if (int.TryParse(temp_string.Substring(0, 2), out day) == false)
         {
            // The original week zero https://en.wikipedia.org/wiki/Global_Positioning_System#Format
            return (Response.GPSEpoch);
         }

         if (day > 31 || day == 0 )
         {
            // The original week zero
            return (Response.GPSEpoch);
         }

         int month = 0;

         if (int.TryParse(temp_string.Substring(2, 2), out month) == false)
         {
            // The original week zero
            return (Response.GPSEpoch);
         }

         if (month > 12 || month == 0)
         {
            // The original week zero
            return (Response.GPSEpoch);
         }

         int year = 0;

         if (int.TryParse(temp_string.Substring(4, 2), out year) == false)
         {
            // The original week zero
            return (Response.GPSEpoch);
         }

         if (year >= 80) // First GPS bird was launched in 1980
            {
            year += 1900;
         }
         else
         {
            year += 2000;
         }

         System.DateTime time = Time(time_field_number);

         return (new System.DateTime(year, month, day, time.Hour, time.Minute, time.Second, time.Millisecond));
      }

      public System.TimeSpan Timespan(int field_number)
      {
         System.DateTime the_time = Time(field_number);

         return( new System.TimeSpan( 0, the_time.Hour, the_time.Minute, the_time.Second, the_time.Millisecond ) );
      }

      public System.DateTime Time(int field_number)
      {
         string temp_string = Field( field_number );

         if ( temp_string.Length >= 6 )
         {
            int hours = 0;

            if ( int.TryParse( temp_string.Substring( 0, 2 ), out hours ) == false )
            {
               // The original week zero
               return(Response.GPSEpoch);
            }

            int minutes = 0;

            if ( int.TryParse( temp_string.Substring( 2, 2 ), out minutes ) == false )
            {
               // The original week zero
               return(Response.GPSEpoch);
            }

            int seconds = 0;

            if ( int.TryParse( temp_string.Substring( 4, 2 ), out seconds ) == false )
            {
               return(Response.GPSEpoch);
            }

            int milliseconds = 0;

            // There could be a fractional part of a second here...

            temp_string = temp_string.Remove(0, 6);

            if (temp_string.Length > 2 && temp_string[ 0 ] == '.' )
            {
               temp_string = temp_string.Remove(0, 1);

               int temp_value = 0;

               if (int.TryParse(temp_string, out temp_value) == true)
               {
                  // Here's where it gets interesting. NMEA0183 doesn't dictate
                  // the number of digits after the decimal. This is a real problem
                  // because the TryParse above may produce a value of 1. OK, was
                  // that the result of parsing "1", "01" or "001"? They each have
                  // very different meanings.

                  if (temp_string.Length == 1)
                  {
                     milliseconds = temp_value * 100;
                  }
                  else if (temp_string.Length == 2)
                  {
                     milliseconds = temp_value * 10;
                  }
                  else if (temp_string.Length == 3)
                  {
                     milliseconds = temp_value;
                  }
                  else
                  {
                     milliseconds = 0;
                  }
               }
            }

            return (new System.DateTime(Response.GPSEpoch.Year, Response.GPSEpoch.Month, Response.GPSEpoch.Day, hours, minutes, seconds, milliseconds));
         }

         // The original week zero
         return(Response.GPSEpoch);
      }

      public NMEA.TransducerType TransducerType( int field_number )
      {
         if (field_number < 0)
         {
            return (NMEA.TransducerType.Unknown);
         }

         string field_data = Field(field_number);

         if (field_data == "A")
         {
            return (NMEA.TransducerType.AngularDisplacement);
         }
         else if (field_data == "D")
         {
            return (NMEA.TransducerType.LinearDisplacement);
         }
         else if (field_data == "C")
         {
            return (NMEA.TransducerType.Temperature);
         }
         else if (field_data == "F")
         {
            return (NMEA.TransducerType.Frequency );
         }
         else if (field_data == "N")
         {
            return (NMEA.TransducerType.Force);
         }
         else if (field_data == "P")
         {
            return (NMEA.TransducerType.Pressure);
         }
         else if (field_data == "R")
         {
            return (NMEA.TransducerType.FlowRate);
         }
         else if (field_data == "T")
         {
            return (NMEA.TransducerType.Tachometer);
         }
         else if (field_data == "H")
         {
            return (NMEA.TransducerType.Humidity);
         }
         else if (field_data == "V")
         {
            return (NMEA.TransducerType.Volume);
         }

         return (NMEA.TransducerType.Unknown);
      }

      public NMEA.FAAModeIndicator FAAMode(int field_number)
      {
         if (field_number < 0)
         {
            return (NMEA.FAAModeIndicator.Unknown);
         }

         string field_data = Field(field_number);

         if (field_data == "A")
         {
            return (NMEA.FAAModeIndicator.Autonomous);
         }
         else if (field_data == "D")
         {
            return (NMEA.FAAModeIndicator.Differential);
         }
         else if (field_data == "E")
         {
            return (NMEA.FAAModeIndicator.Estimated);
         }
         else if (field_data == "M")
         {
            return (NMEA.FAAModeIndicator.Manual);
         }
         else if (field_data == "S")
         {
            return (NMEA.FAAModeIndicator.Simulated);
         }
         else if (field_data == "N")
         {
            return (NMEA.FAAModeIndicator.NotValid);
         }
         else if (field_data == "V")
         {
            return (NMEA.FAAModeIndicator.Invalid);
         }

         return (NMEA.FAAModeIndicator.Unknown);
      }

      public int ChecksumFieldNumber()
      {
         int index = 1; // Skip over the $ at the begining of the sentence
         int current_field_number = 0;
         int string_length = 0;

         string_length = Data.Length;

         while (index < string_length)
         {
            if (Data[index] == '*')
            {
               return (current_field_number + 1);
            }

            if (Data[index] == ',')
            {
               current_field_number++;
            }

            index++;
         }

         return (-1);
      }

      public string Field( int desired_field_number )
      {
         if (desired_field_number < 0)
         {
            return("");
         }

         int index                = 1; // Skip over the $ at the begining of the sentence
         int current_field_number = 0;
         int string_length = Data.Length;

         while( current_field_number < desired_field_number && index < string_length )
         {
            if (Data[index] == ',' || Data[index] == '*')
            {
               current_field_number++;
            }

            index++;
         }

         System.Text.StringBuilder string_builder = new System.Text.StringBuilder(64);

         if ( current_field_number == desired_field_number )
         {
            while( index < string_length    &&
                   Data[index] != ',' &&
                   Data[index] != '*' &&
                   Data[index] != 0x00)
            {
               string_builder.Append(Data[index]);
               index++;
            }
         }

         return (string_builder.ToString());
      }

      public int HexValue(string hex_string)
      {
         if (hex_string.Length <= 0)
         {
            return (0);
         }

         int return_value = 0;

         if (int.TryParse(hex_string,
                          System.Globalization.NumberStyles.HexNumber,
                          System.Globalization.NumberFormatInfo.InvariantInfo,
                          out return_value) == false)
         {
            return (0);
         }

         return (return_value);
      }

      public bool IsGood()
      {
         if (string.IsNullOrEmpty(Data) == true)
         {
            return (false);
         }

         if (Data.Length < 3)
         {
            // Must begin with a $ and end with carriage return / line feed
            // That's three characters
            return (false);
         }

         if (Data[0] != '$')
         {
            return (false);
         }

         return (true);
      }
   }
}
