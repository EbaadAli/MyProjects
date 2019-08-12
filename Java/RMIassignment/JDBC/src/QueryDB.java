/* Source: Horstmann
   Modified by Peter Liu (March 24, 2003) */

/* database system: MySQL server */

// MySQL script file name: Account.sql
// database name: accounts
// table name: Account

// command line: java QueryDB <account number>

// Question: What if the account number does not exist?
// source for valid account numbers: Account.sql

import java.sql.*;
import java.io.*;

public class QueryDB {

    static final String tableName = "Survey";

    public static void main(String args[])
    {
        System.out.println( "Inside CreateDB" );

        /* register the JDBC driver */
        /* read the database url, username and password from the properties file */

	    DBConnection.register( "database.properties" );


        /* connect to the database
           create the database
         */

        Connection conn = DBConnection.getConnection();

	    String naics = args[0];
	    String summary = args[1];
	    String year2008 = args[2];
	    String year2009 = args[3];
	    String year2010 = args[4];
	    String year2011 = args[5];
	    String year2012 = args[6];

        if ( conn != null ) {		/* test the connection */

	       try {
              	 runQueries( conn, naics );

	      	     conn.close();
           }
	       catch( SQLException e ) { e.printStackTrace(); }
        }

        System.exit(0);
    } // end main method


   public static void runQueries( Connection conn, String naic )
			          throws SQLException {


	   System.out.println( "NAICS:\n " + naic );

	   String showLine = "SELECT * FROM " +
			             tableName +
			             " WHERE naics= " +
			             naic ;

	   String sqlLine[] = {	"UPDATE " +
				            tableName +
				            " SET Balance = Balance + 100 " +
				            " WHERE Account_Number = " + accNum,

				            "UPDATE " +
				            tableName +
				            " SET Balance = Balance - 25 " +
				            " WHERE Account_Number = " + accNum
	   };

       Statement stat = conn.createStatement();

	   // show the beginning balance
 	   if ( stat.execute( showLine ) )      // SELECT
 	   										// true: a ResulSet is produced by
											// the SQL statement
											// false: there are NO MORE results
	   		showResultSet( stat );


	   for (int i=0; i < sqlLine.length; i++)

			stat.executeUpdate( sqlLine[i] );	// UPDATE, SET


	   if ( stat.execute( showLine ) )			// show the ending balance

	   		showResultSet( stat );

	   stat.close();
   }


   public static void showResultSet( Statement stat ) {

	  try {
			ResultSet rs = stat.getResultSet();

			int columnCount = rs.getMetaData().getColumnCount();

 			while ( rs.next() ) {

		   			for ( int i=1; i <= columnCount; i++ )

            				System.out.print( rs.getString(i) + " " );

            								/* getInt(i): INTEGER(SQL) => int(Java)
            								   getBigDecimal(i): DECIMAL(SQL) => BigDecimal(Java)
            								 */

                   	System.out.println();

			}
         	rs.close();
	 }
	 catch( SQLException e ) { e.printStackTrace(); }

   }

}

