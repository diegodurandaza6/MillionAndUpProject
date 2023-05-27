Pre-requisitos para ejecuci�n:
- Proyecto desarrollado en Visual Studio 2022
- Framework utilizado dotNet 6

Configuraci�n para prueba en local (No se recomienda para uso en producci�n)
Cambio del modo de autenticaci�n con SQL Server Management Studio:
1. En el Explorador de objetos de SQL Server Management Studio (SSMS), haga clic con el bot�n derecho en el servidor y, despu�s, haga seleccione Propiedades.
2. En la p�gina Seguridad, bajo Autenticaci�n de servidor, seleccione el nuevo modo de autenticaci�n del servidor y seleccione Aceptar.
3. En el cuadro de di�logo de SQL Server Management Studio, seleccione Aceptar para aceptar el requisito de reiniciar SQL Server.
4. En el Explorador de objetos, haga clic con el bot�n derecho en el servidor y, despu�s, seleccione Reiniciar. Si el Agente SQL Server se est� ejecutando, tambi�n debe reiniciarse.

Pasos de la migraci�n:
1. Abrir la consola de administraci�n de paquetes de NuGet desde el men�:
	Tools->NuGet Package Manager->Package Manager Console
2. Ubicamos como Default project el proyecto de nombre:
	Properties.Ms.AdapterOutRepository
2. Ejecutar el comando:
	add-migration InitialMigration
3. posteriormente el comando:
	Update-Database

JSON request examples para obtención de token para posterior autorización de ejecución de metodos, los dos roles (admin y adviser) pueden ejecutar todos los metodos del API, sin embargo solo el rol (admin) puede ejecutar el metodo para actualizar precio y actualizar propiedad

- Get token admin user
{
  "userName": "dduran",
  "password": "Admin123*"
}

- Get token adviser user
{
  "userName": "dduran",
  "password": "Adviser123*"
}

JSON request examples para creación de propiedades y agregar imagen de la propiedad

- Create (Post)
{
  "name": "Club House MAU",
  "address": "Calle 123 # 45 - 67",
  "price": 10000000,
  "year": 2023,
  "idOwner": 1
}

- AddImage (Post)
{
  "idProperty": 1,
  "fileBase64": "iVBORw0KGgoAAAANSUhEUgAAAMgAAADICAMAAACahl6sAAAAdVBMVEUAAAD///9fX1+QkJD8/Py+vr4EBAQxMTH5+fkICAj29vZnZ2cNDQ309PTBwcGUlJRLS0sUFBTNzc0jIyM/Pz8dHR15eXnd3d0rKyuJiYnk5OTGxsbt7e1aWlqenp6xsbFwcHA7Ozu2trZERESCgoKjo6Pg4ODTZLLdAAAEZElEQVR4nO2ci3aiMBCGE40hAdSqtd6wta3d93/ETTKA160iniTs+b/d43psgXzMJBKYLGMAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAP5HZOgGtEKe/dtJ5NE7yXovAZvyHIyQTEa8F7odj1OHZPLOueqwiEFamd065WmHI0IByZMpN4gOizhmY+Ng/3RcZFcIwYkuisjy7/eUK867LKLt6+RD2b4x7KyI7eQ5Y29peghHJ0Ucr1Nhe4cQqrsiJiLzDzfi8o5HRL8VJhRWRPB60OqgyOdCUDgUP45L90T6/CoQCQZEYgMisQGRoNTz8nn97qbI0m6lPTf0N6SbdbjX5aBff3xTZPA1cx6SsVjueFErtF4JtapP8U2RDR/utaTZVyQiFsm2a3NxOK4/uCkyMhdhi5eIFEx+mLxabtyF7v0icuCuJ01+0Q6CNP0Ek0w675upkzVZ1R/f7iPGWwmu+lpG4iFZrzBNTG07G3R2CqG5wl9v3U7Ck4wO7WwyanFe3V35WoZoN6symgacfCyOprMNRQjB03HuRvA6Mv4CJCUdKyvceRWPiyh3ItY97x3l6HDycyPcbZI2IrS5EKOE+f2mlxQOzfLJWNUntVVq0eZinIcZvrKhyymyEC1EzNaKp2YPRVb+zKOOfp0etaNVRNThVqSYJrocRHxgEkB+u6Mr14r6ps8jIorCKYZcuR3N/Diw6mxlFIez+4iPphaFw70mnq8ge9fb2UaEVyJegQhEIAIRiEAEIhCBCEQgAhGIQAQiEIEIRCACEYhABCIQuQk9Y8/KJ4dnT3oeFynLtUVydBQfbOlJ2ROfWNGTVRMRj8UCNiL2uOr5EUmX1SG8iEgTEQqIOm1OGxF6rJp4FLFk9uxdeLRLLWELjD69OVBn35ZJ9bTUUuXulr5EXAWiZPmuoAqtp0XE1qHxYpd78mB1MWW+Si8GrZbP2dUqZ/4LNSWbv1eFQVVXEQ1EXLmaKscMtyjuZxao3FSy5LCKiupRGlSZDrioyjec0WgZqCJQu5qk15E4VAcJ1b+7ElsOyu2UU7GFszJUbSOV8WwXtDjPlTs1Sy1BJVKmj2cyWBlztYDVnMZsXQ2eTQqYN9X4rcT+UFoewqWuq5L6bWgtRJOIyI0qi9beJyxotb87tKSSUz3pKzcENR21uPpKglfHH9LAOs1+bJo0EbER3Nj/zII8/BXO/Y4Zi804dNxHLr4tzyMi1nEui9Hse9qkNr7YRxGBc2j1+uz+FT3ZJI5UuuS0aPfmqMUi6RPnVCuUKm72EacR0xqrf1BG5NzmJCKxhoQdt2x1xeI0Iu7XYwzJqcn+yj2WC5EoPc55WVDT6VrdTu/NN+DPPHSzHiH74+ZNoqw6T/nA9/3Qp2AvwfZFNW2itXpdyKTrTPpDyim+zmJb0non1Go7sbcxKfY5fRjlgPs71fnXyUf6Tn1cd1DjJInmkS0ubkg5fexm4wEAAAAAAAAAAAAAAAAAAAAAAAAAAAAA/IO/juwuEI4PNOkAAAAASUVORK5CYII=",
  "enabled": true,
  "mimeType": "LogoMAU.png"
}

* Manage Performance: https://localhost:7131/health-ui#/healthchecks