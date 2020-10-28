using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace caffeKambodzaApplication
{
    public class Constants
    {
        //public const string DATABASECONNECTION = "D:/My Documents/Visual Studio 2010/Projects/caffeKambodzaApplication/caffeKambodzaApplication/Products Database/Products.mdb";
        //public const string DATABASECONNECTION2 = @"D:\DATA\ajd7878\Documents\Visual Studio 2010\Projects\caffeKambodzaApplication\caffeKambodzaApplication\Products Database\Products.mdb";
        public const string DATABASECONNECTION_PRODUCTS = "\\ApplicationData\\Products.mdb";
        public const string DATABASECONNECTION_OPTIONS = "\\ApplicationData\\SavedOptions.mdb";
        public const string DATABASECONNECTION_APP = "\\ApplicationData\\DatabaseApplication.mdb";
        public const string DATABASECONNECTION_HISTORY = "\\LoggerData\\HistoryDatabaseOutputRecipes.mdb";
        public const string DATABASECONNECTION_LOGGER = "\\LoggerData\\Logger.mdb";
        public const string DATABASECONNECTION_LOGGER_ARCHIVE = "\\LoggerData\\ArchiveLogFiles\\LoggerArchive";
        public const string DATABASECONNECTION_LOGGERNUMBER = "\\LoggerData\\LogNumberNodes.mdb";
        public const string QUERIESPATH = "\\ApplicationData\\Queries.xml";
        public const string TEXT = "Text";
        public const string BACKUPDATABASECONNECTION = "";
        public const string CHOOSEMEASURE = "Izaberite jediničnu meru";
        public const string CHOOSEMEASURE_STORE = "Izaberite jediničnu meru stavke šanka";
        public const string CHOOSEPRODUCT = "Izaberite proizvod";
        public const string CHOOSEPRODUCT_STORE = "Izaberite stavku šanka";
        public const string CHOOSECODE = "Izaberite šifru proizvoda";
        public const string CHOOSECODE_STORE = "Izaberite šifru stavke šanka";
        public const string REMOVEITEMREMARK = "Morate izabrati odredjeni proizvod";
        public const string REMARKPRODUCTANDPRICEANDMEASURE = "Morate uneti i naziv proizvoda i jediničnu cenu proizvoda i jediničnu meru proizvoda";
        public const string REMARKPRODUCTANDPRICE = "Morate uneti i naziv proizvoda i jediničnu cenu proizvoda";
        public const string REMARKPRODUCTANDMEASURE = "Morate uneti i naziv proizvoda i jediničnu meru proizvoda";
        public const string REMARKPRICEANDMEASURE = "Morate uneti i jediničnu cenu proizvoda i jediničnu meru proizvoda";
        public const string REMARKCODE = "Morate uneti šifru proizvoda";
        public const string REMARKPRODUCT = "Morate uneti naziv proizvoda";
        public const string REMARKAMOUNT = "Morate uneti količinu";
        public const string REMARKAMOUNTNOTNUMERIC = "Količina nije uneta u obliku broja. Najlepše vas molimo da količinu unesete u obliku broja (kao broj).";
        public const string REMARKAMOUNTNOTNUMERIC_NOTENTERED = "Količina nije uneta. Najlepše vas molimo da količinu unesete.";
        public const string REMARKAMOUNTNOTNUMERIC2 = "Stavka nije uneta. Najlepše vas molimo da količinu unesete u obliku broja (kao broj).";
        public const string REMARKAMOUNTNOTNUMERIC3TAB1 = "Jedinična cena nije uneta u obliku broja. Najlepše vas molimo da jedinična cena unesete u obliku broja (kao broj).";
        public const string REMARKPRICE = "Morate uneti jediničnu cenu proizvoda";
        public const string REMARKMEASURE = "Morate uneti jediničnu meru proizvoda";
        public const string REMARKENTERED = "Proizvod je već unet. Kliknite na dugme koje briše polja unetog proizvoda";
        public const string REMARKPROGRESSBAR = "Kreiranje izveštaja je u toku!";
        public const string REMARKPROGRESSBAREND = "Kreiranje izveštaja nije u toku";
        public const string REMARKDOWNTAB1_1 = "Morate uneti barem jednu stavku u izveštaj i izabrati datum";
        public const string REMARKDOWNTAB1_2 = "Morate uneti barem jednu stavku u izveštaj";
        public const string REMARKDOWNTAB1_3 = "Morate izabrati datum kreiranja izveštaja";
        //public const string REMARKPROGRESSBAR_PREPARATION1 = "Priprema podataka nije u toku";
        //public const string REMARKPROGRESSBAR_PREPARATION2 = "Priprema podataka je u toku !!!";
        //public const string REMARKPROGRESSBAR_PREPARATION3 = "Priprema podataka je je završena";
        //public const string REMARKPROGRESSBAR_WRITING1 = "Upis podataka nije u toku";
        //public const string REMARKPROGRESSBAR_WRITING2 = "Upis podataka je u toku !!!";
        public const string CODENOTENTERED = "Code product is not entered";
        public const string USEDCODEPRODUCT = " Postoji proizvod sa identičnom šifrom !!! Ova šifra proizvoda je u upotrebi za proizvod ";
        public const string USEDNAMEPRODUCT = " Naziv proizvoda : ";
        public const string USEDPRICE = " Jedinična cena : ";
        public const string NAMEPRODUCTEXISTS = " Postoji proizvod sa identičnim nazivom !!! Morate ili da uklonite proizvod sa identičnim nazivom ili da promenite naziv proizvoda koji unosite ";

        // information for message box
       // public const string NOTIFICATION_REPORTCREATED = "Izveštaj je kreiran.";
        public const string MUSTCLOSE = "Morate da zatvorite prozor sa natpisom IZVEŠTAJ JE KREIRAN";
        public const string NOTIFICATION_REPORTCREATEDBEGIN = "Izveštaj za datum ";
        public const string NOTIFICATION_REPORTCREATEDEND = " je kreiran.";
        public const string NOTIFICATION_REPORTSTATESTOREBEGIN = "Stanje magacina za datum ";
        public const string NOTIFICATION_REPORTSTATESTOREEND = " je kreirano ";
        public const string HEADER_REPORTCREATED = "IZVEŠTAJ JE KREIRAN";
        public const string REMARK_REPORTCREATED = "Morate da zatvorite prozor sa natpisom IZVEŠTAJ JE KREIRAN";


        public const string HEADER_CODEPRODUCT = "Šifra proizvoda";
        public const string HEADER_KINDOFPRODUCT = "Vrsta proizvoda";
        public const string HEADER_PRICE = "Jedinična cena";
        public const string HEADER_AMOUNT = "Količina";
        public const string HEADER_COSTITEM = "Vrednost stavke";
        public const string DEFAULTOPTION = "Koristi se podrazumevana vrednost";

        //default options for paths
        public const string DEFAULTDIRECTORIUM = @"D:\BarBooks";
        public const string DEFAULTDIRECTORIUM_STRING = "D:\\caffeKambodzaPodaci\\ExcelIzvestaji";
        public const string DEFAULTNAMEOFCREATEDREPORT = "Izvestaj_dd/mm/gggg";
        public const string DEFAULTREPORT = "Izvestaj";
        public const string DEFAULTEXTENSIONOFCREATEDREPORT = "XLSX";
        public const string DEFAULTDATABASEPATH = @"D:\caffeKambodzaPodaci\BazaPodataka";

        //default options for application
        public const string SOUNDON = "Zvuk je usključen";
        public const string SOUNDOFF = "Zvuk je isključen";
        public const string OPENFILE = "Fajl se otvara neposredno posle njegovog kreiranja";
        public const string NOTOPENFILE = "Fajl se ne otvara neposredno posle njegovog kreiranja";
        public const string YES = "Yes";
        public const string NO = "No";
        public const string DEFAULTNAMEOFCOMPANY = "caffe KAMBODŽA";
        public const string DEFAULTAUTHOR = "Saša Milošević";

        //tab2 enter store items constants
        public const string NOTCHOOSEDPRODUCT = "Nije upravo unet proizvod";
        public const string NOTCHOOSEDPRODUCT_DOWN = "Nije izabran proizvod";
        public const string NOTCHOOSEDPRODUCT_STORE = "Nije izabrana šifra ";
        public const string MUSTENTERSTOREITEMCODE = "Morate uneti šifru stavke šanka";
        public const string MUSTENTERSTOREITEMNAME = "Morate uneti naziv stavke šanka";
        public const string MUSTENTERSTOREITEMMEASURE = "Morate uneti jed meru stavke šanka";
        public const string MUSTENTERSTOREITEMGROUP = "Morate uneti grupu stavke šanka";
        public const string MUSTENTERSTOREITEMPRICE = "Morate uneti jediničnu cenu stavke";
        public const string MUSTENTERSOREITEM_AMOUNT = "Morate uneti količinu stavke";
        public const string STORECODEEXIST = "Uneli ste postojeću šifru!!!";
        public const string STORECODEEXIST2 = "Ista šifra, promenite je!!!";
        public const string MUSTCHOOSESTOREITEM = "Morate izabrati stavku šanka";
        public const string MUSTCHOOSESTOREGROUP = "Morate izabrati grupu stavki magacina";
        public const string MUSTENTERTHREEVALUE = "Morate izabrati stavku šanka i količine";
        public const string MUSTHAVERATIO = "Morate uneti proporciju stavke šanka i proizvoda";


        public const string NOMOREITEM = "No more store item!!!";
        public const string DENIED = "Denied!!!";

        public const string FILTERON = "  Filter je uključen !!!";
        public const string FILTER_COLUMN = "  Morate izabrati kolonu za filtriranje !!!";

        public const string CURRENCYDINAR = "din";


        public const string ENTERDATE_REPORT = "Mora uneti datum kreiranja izveštaja!!!";
        public const string tfWhyItemDeleted_INITIALTEXT = "Unesite razlog uklanjanja stavke";
        public const string tfCorrectionDelReasonStorehouse_INITIALTEXT =  "Unesite razlog korekcije/uklanjanja";
        public const string tfDeletionOutput_INITIALTEXT = "Unesi razlog brisanja stavke";
        public const string tfCorrectionOutput_INITIALTEXT = "Unesi razlog korekcije stavke";


        public const string HEADER_STORECODE = "Šifra stavke šanka";
        public const string HEADER_STORENAME = "Naziv stavke šanka";
        public const string HEADER_STOREGROUP = "Grupa stavke šanka";
        public const string HEADER_REALAMOUNT = "Količina stavke u magacinu (kg/l) ";
        public const string HEADER_REALPRICE = "Vrednost stavke šanka (din)";


        //history recipes types
        public const string PRODUCT = "Product";
        public const string STOREITEM = "StoreItem";
        public const string PRODUCTSTOREITEM = "ProductStoreItem";

        //Logger nodes statuses
        public const string INFORMATION = "INFORMATION";
        public const string MESSAGEBOX = "MESSAGE BOX";
        public const string EXCEPTION = "EXCEPTION";
        public const string EXCEPTION_EXCEL = "EXCEPTION EXCEL";
        public const string ERROR = "ERROR";
        public const string EXCELERROR = "EXCEL_ERROR";

        //waydisplay enums
        public const string KOM = "KOM";
        public const string LIT = "LIT";
        public const string KG = "KG";
     
    }
}
