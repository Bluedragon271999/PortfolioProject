
package funnyselenium;
import java.util.List;
import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.chrome.ChromeDriver;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import com.google.maps.GeoApiContext;
import com.google.maps.GeocodingApi;
import com.google.maps.errors.ApiException;
import com.google.maps.model.GeocodingResult;
import java.io.IOException;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;
import java.sql.Statement;
import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;

public class FunnySelenium {
    public static WebDriver driver;
    public static void main(String[] args) throws SQLException {
        //stripData();
        compareTables();
    }
    //gets the current date for one variable
    private static String getDate(){
        DateTimeFormatter dtf = DateTimeFormatter.ofPattern("MM/dd/yyyy");
        LocalDate localDate = LocalDate.now();
        return  "update" + dtf.format(localDate).replaceAll("/","-");
    }
    private static void compareTables(){
        
    }
    private static void stripData() throws SQLException{
        //Code to strip data from table
        System.setProperty("webdriver.chrome.driver", "D:\\Selenium\\chromeDriver\\chromedriver.exe");
        System.out.println("Test Iniatiated");
        driver = new ChromeDriver();
        driver.get("https://li-public.fmcsa.dot.gov/LIVIEW/pkg_oos_process.prc_list?pv_vpath=LIVIEW&pv_show_all=N&pn_dotno=&pn_docket=&pv_legalname=&s_state=INUS");
        ArrayList<String> usdotID = new ArrayList();
        usdotID.add("");
        GoogleKey j = new GoogleKey();
        Connection con = DriverManager.getConnection(j.ConString);
        WebElement simpleTable = driver.findElement(By.xpath("/html/body/font/table[2]/tbody")); 
        // Get all rows 
        List<WebElement> rows = simpleTable.findElements(By.tagName("tr")); 
        //Variables to get data from arrays
        StringBuilder data = new StringBuilder();
        StringBuilder data2 = new StringBuilder();
        //Individual fields for SQL Table
        String ID = "";
        String LegalName = "";
        String Address = "";
        String OOSReason = "";
        String OOSDate = "";
        String Status = "";
        String Geolocation = "";
        String DBAName = "";
        //Variable used to manage the SQL being used
        String SQL = "";
        //Count to count the rows of data inserted
        int count = 0;
        //runs through the rows
        for (WebElement row : rows) { 
            List<WebElement> cols = row.findElements(By.tagName("td"));
            List<WebElement> cols2 = row.findElements(By.tagName("th"));
            //collects data from <td> columns
            for (WebElement col : cols) {
                data.append(col.getText() + "\t");
            }
            //collects data from <th> columns
            for (WebElement col2 : cols2) {
                data2.append(col2.getText() + "\t");
            }
            //splits the fields into an array for insertion into individual variables
            String[] array = data.toString().split("\t", 5);
            String[] array2 = data2.toString().replaceAll("\n", "").split("\t");
            //ignores row 1
            if(array.length > 1 && array2.length <=1){
                //separates data into separate variables and formats them properly
                ID = (array2[0].replaceAll("\t", "").replaceAll(" ", ""));
                String[] array3 = (array[0].replaceAll("\t", "").split("\n"));
                LegalName = array3[0].replaceAll("'", "''");
                //checks if a DBAName is present or not then reacts accordingly
                if(array3[1].equals(" ")){
                    DBAName = array3[1].replace(" ", "").replaceAll("'", "''");
                }
                else{
                    DBAName = array3[1].replaceAll("'", "''");
                }
                Address = (array[1].replaceAll("\t", "").replaceAll("\n", ""));
                OOSReason = (array[2].replaceAll("\t", "").replaceAll("\n", ""));
                OOSDate = (array[3].replaceAll("\t", "").replaceAll("\n", ""));
                Status = (array[4].replaceAll("\t", "").replaceAll("\n", ""));
                //geocodes the addresses
                //comment out so that credits are not wasted
                GeoApiContext context;
                GoogleKey k = new GoogleKey();
                try {
                    context = new GeoApiContext.Builder()
                            .apiKey(k.Key)
                            .build();
                    GeocodingResult[] results =  GeocodingApi.geocode(context, Address).await();
                    Gson gson = new GsonBuilder().setPrettyPrinting().create();
                    //checks to see if the geocoder gets a result.  If not then sets the Geolocation to -NA-
                    if(results.length < 1){
                        Geolocation = "-NA-";
                    }
                    else{
                        Geolocation = gson.toJson(results[0].geometry.location.lat) + ":" + gson.toJson(results[0].geometry.location.lng);
                    }
                } catch (ApiException | InterruptedException | IOException ex) {
                    System.out.println(ex.getMessage());
                    Geolocation = "-NA-";
                }
                String today = getDate();
                //If the usdot has been seen while outputing will update SQL instead of inserting
                if(usdotID.contains(ID)){
                    SQL = "UPDATE LiquidatedCompanys SET OOSReason = '" + OOSReason + "', OOSDate = '" + OOSDate + "', Status = '" + Status + "' WHERE USDOT = '" + ID + "'";
                }
                else{
                    //creates insert statement and addes ID to usdotID arrayList
                    SQL = "insert into LiquidatedCompanys values('" + ID + "', '" + LegalName + "', '" + DBAName + "', '" + Address + "', '" + OOSReason + "', '" + OOSDate + "', '" + Status + "', '" + Geolocation + "', '" + today + "')";
                    usdotID.add(ID);
                    count++;
                }
                //executes to database and prints sql to the console
                Statement stml = con.createStatement();
                stml.execute(SQL);
                System.out.print(SQL);
            }
            System.out.println();
            data = null;
            data2 = null;
            data = new StringBuilder();
            data2 = new StringBuilder();
        }
        //prints count so that the amount of inserted rows is visible
        System.out.println(count);
        driver.close();
    }
}
