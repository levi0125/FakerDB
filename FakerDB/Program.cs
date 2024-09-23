// See https://aka.ms/new-console-template for more information

//Install-Package System.Data.SqlClient
//Install-Package "Faker.net"
using MyConexion;
using Faker;
using Bogus;
using System.Runtime.CompilerServices;
using System.Drawing;

namespace FakerDB
{
    class FillDB
    {
        public delegate string Metodo();
        Bogus.Faker fk = new Bogus.Faker("es");

        bool resultado;
        int indexOfNumberGen = 0;

        List<Object> rangesOfNumberGen = new List<Object>();
        List<Metodo> funciones = new List<Metodo>();
        List<string> dataTypes = new List<string>();
        List<int> largos = new List<int>();

        string[] Materias;
        string caracteresASeleccionar;
        string[] LettersConfig = {"m","M"};
        Metodo textoToRandom(string dato, object longitudObj) {
            //bool isMaxValue = false;
            //int longitud;
            dato = dato.Replace("'", "");
            Metodo m = delegate
            {
                return "__NO SE ENCONTRO EL DATO__";
            };
            //longitud = Convert.ToInt32(longitudObj.ToString().Replace("m",""));

            if (longitudObj.ToString().Contains("m"))
            {
                this.largos.Add(-1);
            }
            else
            {                
                this.largos.Add(Convert.ToInt32(longitudObj.ToString().Contains("M")?Convert.ToInt32(longitudObj.ToString().Replace("M","")):longitudObj));
            }

            switch (dato) {
                case "nombre_completo":
                    m = delegate {
                        return fk.Name.FullName();
                    };
                    break;
                case "nombre":
                    m = delegate {
                        return fk.Name.FirstName();
                    };
                    break;
                case "nombres":
                    m = delegate {
                        return fk.Name.FirstName() + " " + fk.Name.FirstName();
                    };
                    break;
                case "apellido":
                    m = delegate {
                        return fk.Name.LastName();
                    };
                    break;
                case "apellidos":
                    m = delegate {
                        return fk.Name.LastName() + " " + fk.Name.LastName();
                    };
                    break;
                case "punto_cardinal":
                    m = delegate {
                        return fk.Address.Direction();
                    };
                    break;
                case "direccion":
                    m = delegate {
                        return fk.Address.FullAddress();
                    };
                    break;
                case "ciudad":
                    m = delegate {
                        return fk.Address.City();
                    };
                    break;
                case "calle":
                    m = delegate {
                        return fk.Address.StreetName();
                    };
                    break;
                case "pais":
                    m = delegate {
                        return fk.Address.Country();
                    };
                    break;
                case "estado":
                    m = delegate { 
                        return fk.Address.State();

                    };
                    break;
                case "telefono":
                    m = delegate {
                        return fk.Phone.PhoneNumber();
                    };
                    break;
                case "dia":
                    m = delegate {
                        return fk.Date.Weekday();
                    };
                    break;
                case "fecha_anterior":
                    AddLongitud(longitudObj);
                    m = delegate {
                        int lim = Convert.ToInt32(getNumberRandomGen()[0]);
                        return AdaptarFecha(fk.Date.PastDateOnly(lim).ToString());
                    };
                    break;
                case "fecha_futura":
                    AddLongitud(longitudObj);
                    m = delegate {
                        int lim = Convert.ToInt32(getNumberRandomGen()[0]);
                        return AdaptarFecha(fk.Date.FutureDateOnly(lim).ToString());
                    };
                    break;
                case "numero":
                    AddLongitud(longitudObj);

                    m = delegate {
                        long limite;
                        string lim = this.rangesOfNumberGen[this.indexOfNumberGen].ToString();
                        if (lim.Contains("m"))
                        {
                            //es el limite pero en digito mas alto a sacar
                            limite = Convert.ToInt64(lim.Replace("m", ""));
                        }
                        else
                        {
                            limite = (long)(Math.Pow(10, Convert.ToInt64(lim)));
                        }

                        Console.WriteLine($"limite= {limite}({this.rangesOfNumberGen[this.indexOfNumberGen]})");
                        this.indexOfNumberGen += 1;
                        return (RandomNumber.Next(0, limite)).ToString();
                    };
                    break;
                case "empresa":
                    m = delegate
                    {
                        return fk.Company.CompanyName();
                        //fk.Company.GetType();
                    };
                    break;
                case "eslogan":
                    m = delegate
                    {
                        return fk.Company.CatchPhrase();
                    };
                    break;
                case "departamento":
                    m = delegate
                    {
                        return fk.Commerce.Department();
                    };
                    break;
                case "color":
                    m = delegate
                    {
                        return fk.Commerce.Color();
                    };
                    break;
                case "producto":
                    m = delegate
                    {
                        return fk.Commerce.Product();
                    };
                    break;
                case "material":
                    m = delegate
                    {
                        return fk.Commerce.ProductMaterial();
                    };
                    break;
                case "nombre_producto":
                    m = delegate
                    {
                        return fk.Commerce.ProductName();
                    };
                    break;
                //case "precio":
                //    this.rangesOfNumberGen.Add(longitudObj);
                //    m = delegate
                //    {
                //        long tope;
                //        string limite = rangesOfNumberGen[indexOfNumberGen].ToString();
                //        if (limite.Contains("m"))
                //        {
                //            tope = Convert.ToInt64(limite.Replace("m",""));
                //        }
                //        else
                //        {

                //        }
                //        return fk.Commerce.Price(0);
                //    };
                //    break;
                case "materia":
                    this.Materias = new string[] {
                    "Matematicas","Ingles","Ciencias","Historia","Literatura","Arte","Educacion FIsica","Qumica","Biologia","Fisica"
                    };
                    m = delegate
                    {
                        return fk.PickRandom(this.Materias);
                    };
                    break;
                case "letras":
                    AddLongitud(longitudObj);

                    m = delegate
                    {
                        char[] cad = initCadena(1).ToCharArray();
                        string res=genCad(cad);

                        return res;
                    };
                    break;
                case "letras_y_numeros":
                    AddLongitud(longitudObj);

                    m = delegate
                    {
                        string res;
                        res = initCadena(1);
                        res += initCadena(2);
                        char[] cad = res.ToCharArray();
                        return genCad(cad);
                    };
                    break;
                case "cadena_de_todo":
                    AddLongitud(longitudObj);

                    m = delegate
                    {
                        string res;
                        res = initCadena(1);
                        res += initCadena(2);
                        res += initCadena(3);
                        char[] cad = res.ToCharArray();
                        return  genCad(cad);
                    };
                    break;
            }

            
            //return null;

            if (m == null)
            {
                Console.WriteLine("NO SE ENCONTRO EL METODO: " + dato);
            }
            return m;
        }
        
        public FillDB(string db,string table,string campos,int repetitions, string servidor = null)
        {
            //db = "Practicas";
            if (buscarFunciones(campos))
            {
                string query = $"INSERT INTO {table} VALUES\n";
                for (int index = 0; index < repetitions; index++)
                {
                    query += "(";
                    int funcLarge = this.funciones.Count();
                    for (int indexFunc = 0; indexFunc < funcLarge; indexFunc++)
                    {
                        query += dataTypes[indexFunc] + executeFunction(indexFunc) + dataTypes[indexFunc];
                        if (indexFunc < (funcLarge - 1))
                        {
                            query += ",";
                        }
                    }
                    query += ")";
                    if (index < (repetitions - 1))
                    {
                        query += ",\n";
                    }
                    resetValues();
                }

                Console.WriteLine("query="+query);
                try
                {
                    ConexionSQL conexion = new ConexionSQL(db, servidor);
                    conexion.ejecutarQuery(query);

                    resultado = true;
                }
                catch (Exception ex)
                {
                    resultado = false;
                }
                
            }
            else
            {
                Console.WriteLine("ERROR AL BUSCAR LAS FUNCIONES");
                resultado = false;
            }
        }

        string AdaptarFecha(string fecha)
        {
            string[] newDate = fecha.Split("/");

            return $"{newDate[2]}-{newDate[1]}-{newDate[0]}";
        }

        string genCad(char[] cad)
        {

            object[] random = getNumberRandomGen();
            string newCad = "";
            //int selIndex;
            char charSelected;
            bool capitalize;

            //Console.WriteLine("LIMITE EN BUCLE:" + random[0]);
            for (int index = 0; index < (int)random[0]; index++)
            {
                //selIndex=RandomNumber.Next(0, this.caracteresASeleccionar.Length);
                charSelected = fk.PickRandom(cad);
                capitalize = RandomNumber.Next(0, 1) == 0;
                //Console.WriteLine("__caracter= " + charSelected);
                if (capitalize || random[1].ToString() == "M")
                {
                    try
                    {
                        charSelected = char.ToUpper(charSelected);
                    }
                    catch (Exception) { }
                }
                newCad += charSelected;
            }

            return newCad;

        }
        void AddLongitud(object longitudObj)
        {
            this.rangesOfNumberGen.Add(longitudObj);
        }
        string[] LimiteHasALetter(string limite, int index = 0)
        {
            for (index = index; index < this.LettersConfig.Length; index++)
            {
                if (limite.Contains(this.LettersConfig[index]))
                {
                    return [this.LettersConfig[index], $"{index}"];
                }
            }
            return ["", null];
        }

        object[] getNumberRandomGen()
        {
            int limite;
            object containsLetter = "";

            string lim = this.rangesOfNumberGen[this.indexOfNumberGen].ToString();
            //Console.WriteLine("LIM EN LISTA:" + lim);
            string letter = LimiteHasALetter(lim)[0];
            if (letter != "")
            {
                //es el limite pero en digito mas alto a sacar
                limite = Convert.ToInt32(lim.Replace(letter, ""));
                containsLetter = letter;
            }
            else
            {
                //el limite esta en digitos que puede contener
                //limite = (Math.Pow(10, Convert.ToInt64(lim)));
                limite = Convert.ToInt32(lim);
            }

            this.indexOfNumberGen += 1;
            return [limite, containsLetter];

            //return (RandomNumber.Next(0, limite)).ToString();

        }
        string initCadena(int tipo_inicio)
        {
            string cad = "";
            switch (tipo_inicio)
            {
                case 1:
                    cad = "abcdefghijllmnñopqrstuvwxyz";
                    break;
                case 2:
                    cad = "1234567890";
                    break;
                case 3:
                    cad = "¡!#$%&@<>~|()[]{}°¿?_+-/*=";
                    break;
            }
            return cad;
        }

        bool buscarFunciones(string campos)
        {
            string[] data = campos.Split(',');
            foreach (string dato in data)
            {
                

                if (!analizeCampo(dato))
                {
                    Console.WriteLine("ERROR EN :" + dato);
                    return false;
                }

            }
            return true;
        }

        public bool analizeCampo(string campo)
        {

            Console.WriteLine("campo analizar"+campo);

            object[] dato = getDataFromCampo(campo);

            //if (dato.Length < 2 || dato == null || dato[0] == null || dato[0] is not string || dato[1] == null)
            if(dato==null)
            {
                //los datos asignados en el campo erraron en el formato
                //por ejemplo, en el lugar de un int se envio un string
                Console.WriteLine("DATO VACIO");
                return false;
            }
            Metodo res= textoToRandom((string)dato[0], dato[1]);
            //if (res == null)
            //{
            //    return false; 
            //}
            this.funciones.Add(res);
            this.dataTypes.Add(getDataType(dato));
            return true;
        }

        public object[] getDataFromCampo(string campo,string contieneComillaMayor=null)
        {
            // va a permitir personalizar la generacion mediante varios identificadores, por ejemplo:
            //"'nombre(15)  apellido(30) numero(10m)'"
            //Que concatenaria un nombre, apellido y un numero de maximo 10(todavia no esta activo)

            // "'nombre_completo'(23)" <---Entrada
            // "'nombre_completo'(23"
            // "'nombre_completo',23"
            // ["'nombre_completo'"],["23"]
            // ["'nombre_completo'"],[23] <---Salida
            //Console.WriteLine("campo original:" + campo);

            campo = campo.Replace("  ", " ");
            campo = campo.Replace(" ", ";");

            campo = campo.Replace(")", "");
            campo = campo.Replace("(", ",");

            Console.WriteLine("campo:" + campo);
            object[] cmp = campo.Split(",");

            if (cmp.Length < 2)
            {
                //se recibieron menos datos de los esperados
                Console.WriteLine("CMP MENOR A LO ESPERADO");
                return null;
            }
            //string longitud = cmp[1].ToString();
            //if (longitud.IndexOf("m")==longitud.Length-1){
            //    Console.WriteLine("el mamximo es el valor maximo que puede tomar, no el largo de la cadena");
            //    cmp[1] = longitud.Replace("m","");

            //}

            try
            {
                //validaciones o adaptaciones
                string letter = LimiteHasALetter(cmp[1].ToString())[0];
                Console.WriteLine("letra colada:" + letter);
                string cad;

                if (letter != "") 
                {
                    cad = cmp[1].ToString().Replace(letter, "");
                }
                else
                {
                    cad = cmp[1].ToString();
                }
                Console.WriteLine("cad=" + cad);
                int numero=Convert.ToInt32(cad);

                Console.WriteLine("NUMERO:" + numero);                
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR EN LAS ADAPTACIONES");
                Console.WriteLine("----ERROR:" + ex.Message);
                return null;
            }
            //todo correcto
            //Object[] newobj= [cmp[0],Convert.ToInt32(cmp[1].ToString())];

            return cmp;
        }
        public string executeFunction(int indexFunc)
        {
            string res = this.funciones[indexFunc]();

            return res.Length > this.largos[indexFunc] && this.largos[indexFunc]>0 ? res.Substring(0, this.largos[indexFunc]) : res;
        }
        public void resetValues()
        {
            this.indexOfNumberGen = 0;
        }
        string getDireccion()
        {
            return $"{fk.Address.Direction}";
        }
        string getFullDireccion()
        {
            return $"{fk.Address.FullAddress()}";
        }
        string getDataType(object[] elemento)
        {
            if (elemento[0].ToString().Split("'").Length == 1)
            {
                //la cadena no se envio con comilla simple
                //en teoria es un numero
                return "";
            }
            return "'";
        }
    }
}

