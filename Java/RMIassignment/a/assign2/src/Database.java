import java.sql.*;

public class Database  {

    static final String drivername
            = "com.mysql.jdbc.Driver"; // JDBC driver (a class name)

    static final String sysName = "zenit.senecac.on.ca";        // database server

    //btp400_151b45 : fqBE2447 (Winter 2015)
    static final String dbName  = "btp400_151b40";         // database name

    static final String userid =  "btp400_151b40";
    static final String password = "bqBN3886";

    private Connection conn;                          // database connnection


    public static void main(String args[]) {

        System.out.println("running a JDBC application...");

        Database test = new Database(); 	/* load the JDBC driver */

       // Statement statement = conn.createStatement();

        if ( test.testConnect( sysName, dbName ) ) {    /* test the connection */

           test.Test();
        }

        System.exit(0);
    }


    public Database() { // Step 1: load the JDBC driver

        try {
            Class.forName( drivername );  // load the Java class(i.e. JDBC driver) at run time
        }
        catch( ClassNotFoundException ec) { ec.printStackTrace();
            System.out.println( "MySQL JDBC driver not found!" );
            System.exit(1);
        }


        System.out.println("JDBC class found");
    }


    public boolean testConnect(String sys, String databaseName) {

        System.out.println( "Connecting to the MySQL server: " + sys + "...");

        try
        {
	  		/* Step 2: connect to the database
		     		   datasbase URL:
			   		   jdbc:<subprotocol>:<system url>/<collection name> */

            conn = DriverManager.getConnection( "jdbc:mysql:" +
                            "//" + sys +
                            "/"  + databaseName,
                    userid, password
            );
        }

        catch ( SQLException exc )  /* SQLException */
        {
            System.out.println( "connection failed with: " +
                    exc.getMessage() );
            return false;
        }

        System.out.println( "database connection-OK" );

        return true;
    }

    public void Test() {

        try {
            Statement statement = conn.createStatement();
            ResultSet rs = statement.executeQuery("SELECT naics FROM survey");
            while (rs.next()) {
                System.out.println(rs.getString("naics"));
            }
        } catch (SQLException e) {
            e.printStackTrace();
        }

    }

    public boolean testQueryAll() {

		/*  Step 3: Prepare the SQL statement object
	        Step 4: Execute the SQL statement object */

        // drop the table if it already exists!

        System.out.println( "dropping a table if it exists..." );

        try {
            Statement dropTable = conn.createStatement();

            dropTable.executeUpdate( "DROP TABLE " +
                    "survey" ); 	      // table name: Test
            dropTable.close();
        }
        catch( SQLException e ) { }

        System.out.println( "creating a database table..." );

        try {
            // SQL statements: create a table and insert data

            Statement stat = conn.createStatement();
            
            stat.executeUpdate( "CREATE TABLE survey (naics varchar(100), summary varchar(200), year2008 float, year2009 float,year2010 float,year2011 float,year2012 float )" );

            stat.executeUpdate( "INSERT INTO survey ( naics, summary, year2008,year2009,year2010,year2011,year2012) VALUES ('Computer systems design and related services [54151]', 'Operating revenue (dollars x 1,000,000) (5)',30440.1,32436.3,32285.1,34524.9,36038.7)" );
            stat.executeUpdate( "INSERT INTO survey ( naics, summary, year2008,year2009,year2010,year2011,year2012) VALUES( 'Software publishers [51121]', 'Operating revenue (dollars x 1,000,000) (5)', 5820.40,5398.5,5660.30,6417.80,6887.7 )" );
            stat.executeUpdate( "INSERT INTO survey ( naics, summary, year2008,year2009,year2010,year2011,year2012) VALUES( 'Software publishers [51121]', 'Salaries, wages and benefits (dollars x 1,000,000) (2)', 2832.60,2428.1,2723.6,2950.10,3192.10)" );
            stat.executeUpdate( "INSERT INTO survey ( naics, summary, year2008,year2009,year2010,year2011,year2012) VALUES( 'Software publishers [51121]', 'Operating year margin (percent) (3)', 6.5,9.1,9.4,6.6,8.8)" );

            //processing

            stat.executeUpdate( "INSERT INTO survey ( naics, summary, year2008,year2009,year2010 ,year2011 ,year2012 ) VALUES( 'Software publishers [51121]', 'Operating revenue (dollars x 1,000,000) (5)', 2692.7,2775.9,3104.5,3544.6,3670.9 )" );
            stat.executeUpdate( "INSERT INTO survey ( naics, summary, year2008,year2009,year2010 ,year2011 ,year2012 ) VALUES( 'Software publishers [51121]', 'Operating revenue (dollars x 1,000,000) (5)', 2391.7,2302,2791.7,2997.6,3142.9 )" );
            stat.executeUpdate( "INSERT INTO survey ( naics, summary, year2008,year2009,year2010 ,year2011 ,year2012 ) VALUES( 'Software publishers [51121]', 'Salaries, wages and benefits (dollars x 1,000,000) (2)', 930.5,968.1,1167.7,1277.5,1343.6))" );
            stat.executeUpdate( "INSERT INTO survey ( naics, summary, year2008,year2009,year2010 ,year2011 ,year2012 ) VALUES( 'Software publishers [51121]', 'Operating year margin (percent) (3)',  11.2,17.1,10.1,15.4,14.4" );

            //design

            stat.executeUpdate( "INSERT INTO survey ( naics, summary, year2008,year2009,year2010 ,year2011 ,year2012 ) VALUES( 'Software publishers [51121]', 'Operating revenue (dollars x 1,000,000) (5)', 30440.1,32436.3,32285.1,34524.9,36038.7 )" );
            stat.executeUpdate( "INSERT INTO survey ( naics, summary, year2008,year2009,year2010 ,year2011 ,year2012 ) VALUES( 'Software publishers [51121]', 'Operating revenue (dollars x 1,000,000) (5)', 27893.6,28794.5,28467,30488.9,31541.5" );
            stat.executeUpdate( "INSERT INTO survey ( naics, summary, year2008,year2009,year2010 ,year2011 ,year2012 ) VALUES( 'Software publishers [51121]', 'Salaries, wages and benefits (dollars x 1,000,000) (2)', 12692.2,12761.6,13010,14154.8,14709.9))" );
            stat.executeUpdate( "INSERT INTO survey ( naics, summary, year2008,year2009,year2010 ,year2011 ,year2012 ) VALUES( 'Software publishers [51121]', 'Operating year margin (percent) (3)',  8.4,11.2,11.8,11.7,12.5" );

            System.out.println( "querying the database table..." );

            // SQL statement: database query
            ResultSet rs = stat.executeQuery( "SELECT * FROM survey" );

            // an SQLException is thrown if the table name
            // is incorrect

	 	 	/* Step 5: Analyze(Navigate) the query result(the ResultSet) */

            System.out.println("\n******* The Result of the Query *******");

            while ( rs.next() ) {


                System.out.println( "/n****"+ rs +"*****/n" +
                                "NAICS: " + rs.getString(1) +
                                "Summary: " + rs.getString(2)+
                                "2008: " + rs.getString(3)+
                                "2009: " + rs.getString(4)+
                                "2010: " + rs.getString(5)+
                                "2011: " + rs.getString(6)+
                                "2012: "+rs.getString(7)
                );

            }

            rs.close();   // close the ResultSet object
            stat.close(); // close the Statement object
        }
        catch ( SQLException exc )
        {
            System.out.println( " query failed with: " + exc.getMessage() );
            return false;
        }
        finally { // always executed

            System.out.println( "\nclose the database connection..." );

            try { if (conn != null )
                conn.close(); // close the database connection
            }
            catch (SQLException se ) { se.printStackTrace(); }
        }

        System.out.println( "DONE" );

        return true;

    } // end testQueryAll

    public void getMetadata( ResultSet rs ) {

		/* Retrieve the metadata of a table */
        System.out.println( "metadata info:" );

        try {
            ResultSetMetaData rsmeta = rs.getMetaData();

            int columnCount = rsmeta.getColumnCount();

            System.out.println( "number of columns: " + columnCount );

            for (int i=1; i <= columnCount; i++)
                System.out.println( "column name: " + rsmeta.getColumnName(i) +
                        "\n data type: " + rsmeta.getColumnTypeName(i) );
        }
        catch( SQLException e ) { e.printStackTrace(); }
    }

}