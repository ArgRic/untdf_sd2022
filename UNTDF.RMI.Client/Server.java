
import java.rmi.Naming;


public class Server {
    public static void main(String args[]) {
        try {
            
            CalcService calculadora = new CalcService();
            Naming.rebind(Config.ServiceName, calculadora);
            System.out.println("esperando invocaciones desde los clientes...");
        } catch (Exception e) {
            System.err.println("ha ocurrido una excepción: " + e.toString());
        }
    }
}