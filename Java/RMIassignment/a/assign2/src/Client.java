import java.net.MalformedURLException;
import java.rmi.Naming;
import java.rmi.NotBoundException;
import java.rmi.RemoteException;
import java.util.ArrayList;
import java.util.Scanner;

/**
 * Created by Albert on 4/7/2015.
 */
public class Client {


    public static void main(String[] args) {

        ArrayList<String> naics = new ArrayList<String>();

        String url = "rmi://localhost:6666/";
        System.out.println("Running client application");
        System.out.println("these services have been registered to RMI registry");

        String line = "";
        Scanner keyboard = new Scanner(System.in);
        System.out.println("To retrieve data sets enter 'data'");
        System.out.println("To exit type 'exit'");

        while (true) {
            try {

                System.out.print(">> ");
                line = keyboard.nextLine();

                if (line.equals("exit")) {
                    System.out.println("Exiting RMI client...");
                    break;
                }

                if (line.equals("data")) {
                    System.out.println("Hellloo");

                    String[] names = Naming.list(url);

                    for (int i = 0; i < names.length; i++) {
                        System.out.println("..." + names[i]);
                    }

                    RMI a = (RMI) Naming.lookup(url + "albert");

                    naics = a.getNaics();

                    for (int i = 0; i < naics.size(); i++) {
                        System.out.println(naics.get(i));
                    }

                }


            } catch (RemoteException e) {
                System.out.println("Err");
                e.printStackTrace();
            } catch (MalformedURLException e) {
                System.out.println("Err");
                e.printStackTrace();
            } catch (NotBoundException e) {
                System.out.println("Err");
                e.printStackTrace();
            }
        }

       // System.out.println( "rmi client: THE END!" );

    }
}
