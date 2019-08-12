import java.net.MalformedURLException;
import java.rmi.Naming;
import java.rmi.RemoteException;

/**
 * Created by Albert on 4/7/2015.
 */
public class Server {

    public static void main(String[] args) {

        String url = "rmi://localhost:6666/";

        System.out.println("Starting Server...");

        try {
            Implementation a = new Implementation("Hello");

            System.out.println("Binding remote objects to RMI registry");

            Naming.rebind("rmi://localhost:6666/albert", a);
        } catch (RemoteException e) {
            e.printStackTrace();
        } catch (MalformedURLException e) {
            e.printStackTrace();
        }

        System.out.println( "... bye bye" );
        System.out.println( "... the main thread is put into a wait state!!!" );
    }
}
