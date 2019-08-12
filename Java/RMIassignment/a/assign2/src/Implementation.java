import java.rmi.RemoteException;
import java.rmi.server.UnicastRemoteObject;
import java.sql.*;
import java.util.ArrayList;

/**
 * Created by Albert on 4/7/2015.
 */
public class Implementation extends UnicastRemoteObject implements RMI{

    private String name;

    private Database database;

    private Connection conn;

    static final String drivername
            = "com.mysql.jdbc.Driver"; // JDBC driver (a class name)

    static final String sysName = "zenit.senecac.on.ca";        // database server

    //btp400_151b45 : fqBE2447 (Winter 2015)
    static final String dbName  = "btp400_151b40";         // database name

    static final String userid =  "btp400_151b40";
    static final String password = "bqBN3886";

    public Implementation(String name) throws RemoteException {
        this.name = name;
        database = new Database();

        try
        {
	  		/* Step 2: connect to the database
		     		   datasbase URL:
			   		   jdbc:<subprotocol>:<system url>/<collection name> */

            conn = DriverManager.getConnection("jdbc:mysql:" +
                            "//" + sysName +
                            "/" + dbName,
                    userid, password
            );
        }

        catch ( SQLException exc )  /* SQLException */
        {
            System.out.println( "connection failed with: " +
                    exc.getMessage() );
        }
    }

    @Override
    public ArrayList<String> getNaics() throws RemoteException {

        ArrayList<String> naics = new ArrayList<String>();

        try {
            Statement statement = conn.createStatement();
            ResultSet rs = statement.executeQuery("SELECT naics FROM survey");
            while (rs.next()) {

                naics.add(rs.getString("naics"));
            }
        } catch (SQLException e) {
            e.printStackTrace();
        }

        return naics;
    }
    
    /*summary*/
    public ArrayList<String> getSummary() throws RemoteException {

        ArrayList<String> summary = new ArrayList<String>();

        try {
            Statement statement = conn.createStatement();
            ResultSet rs = statement.executeQuery("SELECT summary FROM survey");
            while (rs.next()) {

                summary.add(rs.getString("summary"));
            }
        } catch (SQLException e) {
            e.printStackTrace();
        }

        return summary;
    }

    @Override
    public String getName() throws RemoteException {
        return name;
    }
}
