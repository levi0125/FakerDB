using FakerDB;
string camposNecesitados;
string db = "BdPractica2";
string tabla = "empleados";
int repeticiones = 10;

var fill=new FillDB(db);

//fill.cx.Open();
//fill.makeInserts(tabla,
//    "'nada'(1),'nada'(1),numero(10m)"
//    ,repeticiones,false);
fill.alterTable(tabla, "emp", "no_dept", "numero(40m)", "true=true");