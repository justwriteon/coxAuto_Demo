using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleDataViewer.DataSource
{
    public enum VehicleDataStore
    {
       File_CSV =1, //To Upload CSV File and copy in file system
       File_XML =2, //To Upload XML File and copy in file system
        DB_CSV =3, // To Upload data in CSV File to Database
        DB_XML = 4 // To Upload data in XML File to Database
    }
}
