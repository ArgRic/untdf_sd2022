import java.rmi.RemoteException;
import java.rmi.server.UnicastRemoteObject;

public class CalcService extends UnicastRemoteObject implements ICalcRMI{
 
    public CalcService() throws RemoteException {
        super();
    }

    @Override
    public int suma(int a, int b) throws RemoteException {
        System.out.println(String.format("retornando la suma [a=%s + b=%s]",a,b));
        return a+b;
    }

    @Override
    public int resta(int a, int b) throws RemoteException {
        System.out.println(String.format("retornando la resta [a=%s - b=%s]",a,b));
        return a-b;
    }
    
    @Override
    public int div(int a, int b) throws RemoteException {
        System.out.println(String.format("retornando la division entera [a=%s / b=%s]",a,b));
        return a/b;
    }

    @Override
    public int modulo(int a, int b) throws RemoteException {
        System.out.println(String.format("retornando el resto de [a=%s / b=%s]",a,b));
        return a%b;
    }

    @Override
    public int multi(int a, int b) throws RemoteException {
        System.out.println(String.format("retornando la multiplicacion entera [a=%s / b=%s]",a,b));
        return a*b;
    }

}