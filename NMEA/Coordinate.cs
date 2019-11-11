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
   public abstract class Coordinate
   {
      private double m_Degrees;

      public Coordinate()
      {
         Degrees = 0.0D;
      }

      public Coordinate(double c)
      {
         Degrees = c;
      }

      public double Degrees
      {
         get
         {
            return (m_Degrees);
         }

         set
         {
            m_Degrees = value;
         }
      }
      
      public double DecimalDegrees()
      {
         double return_value = 0.0;

         int degrees = (int) System.Math.Floor( Degrees );
         int minutes = degrees % 100;

         double fractional_minutes = Degrees - (double) degrees;

         degrees -= minutes;
         degrees /= 100;

         return_value = (double) degrees;
         return_value += (double) ( (double) minutes / (double) 60.0D );
         return_value += (double) ( (double) fractional_minutes / (double) 60.0D );

         return( return_value );
      }
   }

   public class Latitude : Coordinate
   {
      private NMEA.NorthOrSouth m_Northing;

      public NMEA.NorthOrSouth Northing
      {
         get
         {
            return (m_Northing);
         }

         set
         {
            m_Northing = value;
         }
      }

      public Latitude()
      {
         Northing = NorthOrSouth.Unknown;
      }

      public Latitude( double c ) : base( c )
      {
         Northing = NorthOrSouth.Unknown;
      }

      public Latitude(double c, NorthOrSouth n) : base(c)
      {
         Northing = n;
      }

      public void Empty()
      {
         Degrees = 0.0D;
         Northing = NorthOrSouth.Unknown;
      }

      public bool Parse(int position_field_number, int north_or_south_field_number, Sentence sentence)
      {
         Degrees = sentence.Double(position_field_number);
         Northing = sentence.NorthOrSouth(north_or_south_field_number);

         if (Northing == NorthOrSouth.Unknown)
         {
            return (false);
         }

         return (true);
      }

      public double Decimal()
      {
         var return_value = DecimalDegrees();

         if (Northing == NorthOrSouth.South)
         {
            return (return_value * -1.0D);
         }

         return (return_value);
      }
   }

   public class Longitude : Coordinate
   {
      private EastOrWest m_Easting;

      public EastOrWest Easting
      {
         get
         {
            return (m_Easting);
         }

         set
         {
            m_Easting = value;
         }
      }

      public Longitude()
      {
         Easting = EastOrWest.Unknown;
      }

      public Longitude( double c ) : base( c )
      {
         Easting = EastOrWest.Unknown;
      }

      public Longitude( double c, EastOrWest e ) : base( c )
      {
         Easting = e;
      }

      public void Empty()
      {
         Degrees = 0.0D;
         Easting = EastOrWest.Unknown;
      }

      public bool Parse(int position_field_number, int east_or_west_field_number, Sentence sentence)
      {
         Degrees = sentence.Double(position_field_number);
         Easting = sentence.EastOrWest(east_or_west_field_number);

         if (Easting == EastOrWest.Unknown)
         {
            return (false);
         }

         return (true);
      }

      public double Decimal()
      {
         var return_value = DecimalDegrees();

         if (Easting == EastOrWest.West)
         {
            return (return_value * -1.0D);
         }

         return (return_value);
      }
   }

   public class LatLong
   {
      public Latitude Latitude;
      public Longitude Longitude;

      public LatLong()
      {
         Latitude = new Latitude();
         Longitude = new Longitude();
      }

      public void Empty()
      {
         Latitude.Empty();
         Longitude.Empty();
      }

      public bool Parse(int LatitudePostionFieldNumber, int NorthingFieldNumber, int LongitudePositionFieldNumber, int EastingFieldNumber, Sentence LineToParse )
      {
         if (Latitude.Parse(LatitudePostionFieldNumber, NorthingFieldNumber, LineToParse) == false)
         {
            Empty();
            return (false);
         }

         if (Longitude.Parse(LongitudePositionFieldNumber, EastingFieldNumber, LineToParse) == false)
         {
            Empty();
            return (false);
         }

         return (true);
      }
   }
}
