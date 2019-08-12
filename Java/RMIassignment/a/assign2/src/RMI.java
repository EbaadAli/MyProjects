import java.rmi.Remote;
import java.rmi.RemoteException;
import java.util.ArrayList;

/**
 * Created by Albert on 4/7/2015.
 */
public interface RMI extends Remote {

    public String getName() throws RemoteException;

    public ArrayList<String> getNaics() throws RemoteException;
    public ArrayList<String> getSummary() throws RemoteException;
    public ArrayList<double> get08() throws RemoteException;
    public ArrayList<double> get09() throws RemoteException;
    public ArrayList<double> get10() throws RemoteException;
    public ArrayList<double> get11() throws RemoteException;
    public ArrayList<double> get12() throws RemoteException;

}
