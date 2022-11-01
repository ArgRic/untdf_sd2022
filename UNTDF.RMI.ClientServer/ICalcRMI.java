import java.rmi.*;

public interface ICalcRMI extends Remote {
    public int suma(int a, int b) throws RemoteException;

    public int resta(int a, int b) throws RemoteException;

    public int div(int a, int b) throws RemoteException;
    
    public int modulo(int a, int b) throws RemoteException;

    public int multi(int a, int b) throws RemoteException;
}
