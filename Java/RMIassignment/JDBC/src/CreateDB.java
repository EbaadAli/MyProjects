import java.sql.*;
import java.io.*;

public class CreateDB {

  public static void main(String args[]){
	 System.out.println("Inside CreateDB");
	 
	 DBConnection.register("datatbase.properties");
  }
}
