import java.rmi.Naming;

public class Client {
    public static void main(String[] args) {
        if (Config.OverrideSecurity)
        {
            System.setProperty("java.security.policy", "./inseguridad.policy");
        }
        else {
            System.setProperty("java.security.policy", "client.policy");
        }
        
        System.setSecurityManager(new SecurityManager());
        try {
        String url = "//localhost:1099/" + Config.ServiceName;
        System.out.println("obteniendo el objeto calculadora...");
        ICalcRMI calculadoraStub = (ICalcRMI)Naming.lookup(url);
       int suma = calculadoraStub.suma(3, 4);
       System.out.println("suma: " + suma);
        } catch (Exception e) {
        System.err.println(e.toString());
        }
       }
       
}
