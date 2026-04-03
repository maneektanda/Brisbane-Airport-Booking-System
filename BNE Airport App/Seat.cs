using System;

namespace BNE_Airport_App
{
    /// <summary>
    /// Represents a seat on a plane. It contains data about the position of the seat as well as methods to view details,
    /// and increment the seat to the next position.
    /// 
    /// Responsibility: Represent a seat on a plane.
    /// </summary>
    public class Seat
    {
        // Fields to represent the seats location on a plane.

        /// <summary>
        /// The row position of the seat.
        /// </summary>
        private int row;

        /// <summary>
        /// The column position of the seat.
        /// </summary>
        private char column;

        /// <summary>
        /// A constructor for the seat.
        /// </summary>
        /// <param name="row">The row that the seat is in, a number from 1-10.</param>
        /// <param name="column">The column the seat is in, a letter from A-D.</param>
        public Seat(int row, char column)
        {
            this.row = row;
            this.column = column;
        }

        /// <summary>
        /// Overloading the increment operator to enable easy re-assignment of seats that are booked by a frequent flyer.
        /// The seats move along each row until they reach the "D" column, at which point the row is incremented and the
        /// column is reset to "A". The last seat in the plane is 10:D which is incremented back to the first seat, 1:A.
        /// </summary>
        /// <param name="seat">This is the seat to be incremented.</param>
        /// <returns>The next seat on the plane.</returns>
        public static Seat operator ++(Seat seat)
        {
            // Define some constants for readability.
            const int MIN_ROW = 1;
            const int MAX_ROW = 10;
            const char MIN_COLUMN = 'A';
            const char MAX_COLUMN = 'D';

            // If the seat is not the last one on that row, stay on that row and just move over one column.
            if (seat.column != MAX_COLUMN)
            {
                seat.column++;
                return seat;
            }

            // If the code gets to here, the seat is in the "D" column. If the seat isn't in the last row, then we can just
            // increment the row and reset the column to "A".
            else if (seat.row != MAX_ROW)
            {
                seat.column = MIN_COLUMN;
                seat.row++;
                return seat;
            }

            // If the code gets to here, we know the seat must be 10:D, so we set it back to 1:A.
            else
            {
                seat.column = MIN_COLUMN;
                seat.row = MIN_ROW;
                return seat;
            }
        }

        /// <summary>
        /// Creates a string version of the seat that is human readable.
        /// </summary>
        public string Value
        {
            get { return row.ToString() + ":" + column.ToString(); }
        }
    }
}