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
