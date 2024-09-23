using FakerDB;
string camposNecesitados;
string db = "Practicas";
string tabla = "alumnos";
int repeticiones = 20;

//debes llenar el array con minusculas y sin comerte letras o dejando espacios
//string camposNecesitados = [[ "nombre","20"],[ "apellido","15"],["ciudad","50"]];
//string camposNecesitados = "'nombre'(20),'apellido'(30),'materia'(20),numero(10),numero(15m),'letras_y_numeros'(10)";
//camposNecesitados = "'fecha_anterior'(10),'fecha_futura'(10)";


//matricula varchar(10),
//nombre varchar(30),
//apellidos varchar(30),
//fecha_nac date,
//direccion varchar(50),
//ciudad varchar(20),
//estado varchar(20),
//curso varchar(10),
//nivel int,
//clase varchar(2),
//faltasS1 int,
//faltasS2 int,
//faltasS3 int
camposNecesitados = "'letras_y_numeros'(10),'nombres'(30),'apellidos'(30),'fecha_anterior'(17),'direccion'(50),";
camposNecesitados += "'ciudad'(20),'estado'(20),'materia'(10),numero(3m),'letras'(1M),numero(26m),numero(26m),numero(26m)";
new FillDB(db,tabla, camposNecesitados, repeticiones);