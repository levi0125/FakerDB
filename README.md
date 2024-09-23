Intraduccion:
	Este programa esta pensado para llenar tablas con datos generados con la libreria Bogus(Faker) de una
	forma facil, intuitiva y con rango a personalizacion sin tener que alterar demasiado entre inserciones.

	Cabe destacar que todavia no esta terminado pero si sirve con lo que ya tiene, sin embargo tengo planes 
	de continuar mejorandolo.

Funcionamiento:
	El programa funciona mediante el importe del modulo de namespace "FakerDB" y la posterior instanciacion
	de un objeto de la clase "FillDB". Eso se hace mediante los siguientes comandos:

	using FakerDB;

	new FillDB(...args...);

	La clase FillDB requiere de varios argumentos: 
		-db:
			recibe el nombre de la base de datos
		-table:
			recibe el nombre de la tabla a la que hara la insercion
		-campos:
			recibe el nombre de los generadores aleatorios que quieras usar, mediante el formato:
			"nombre_generador(longitud),'nombre_generador2(longitud)"
			cabe decir que para que funcione correctamente no deben haber espacios en ningun lugar de la
			cadena

			si el campo requiere comillas para su correcta insrcion a la tabla se debe encerrar el generador
			en la susodichas comillas, seguida de su longitud para no pasarse de espacio;

			esta longitud puede tener algunas configuraciones extras:
				*"m".- si pegada a la longitud le colocas la letra "m" y estas generando un numero entonces 
					lo que hara sera genrear un numero entre 0 y la longitud; en caso contrario:
					generara un numero entre 0 y la longitud en forma de digitos, por ejemplo:
					si se ingreso como longitud (6) entonces se formaria un numero entre 0 y 999999

				*"M".- si pegada a la longitud colocas la letra "M" y estas generando una una cadena mediante
					"letras"/"letras_y_numeros"/"cadena_de_todo_" estas indicando que quieres que 
					unicamente se generen mayusculas
		-repetitions:
			Numero registros que har� a la tabla
		-servidor:
			servidor al que se conectara; este es opcional ya que si no recibe nada se conectara al servidor con
				el nombre de la maquina


Generadores:

	nombres:
		nombre_completo
		nombre
		nombres
		apellido
		apellidos

	Direccion:
		punto_cardinal
		direccion
		ciudad
		calle
		pais
		estado

	Numeros:
		telefono
		numero

	Fechas:
		dia
		fecha_anterior
		fecha_futura

	Empresas:
		empresa
		eslogan
		depatamento

	Productos:
		color
		producto
		material
		nombre_producto

	cadenas:		
		letras
		letras_y_numeros
		cadena_de_todo

	otro:
		materia
