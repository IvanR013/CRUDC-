# CRUD Simple con ASP.NET 

## Descripción

Este proyecto implementa un CRUD utilizando el patrón repositorio, con una base de datos tipo SQL. La aplicación está estructurada en varias capas para mantener una separación clara de responsabilidades.

## Estructura del Proyecto

### Capas

1. **Controladores**: Manejan las solicitudes HTTP y responden con los resultados apropiados.
2. **Repositorios**: Contienen la lógica de acceso a datos y se comunican con la base de datos.
3. **Modelos**: Definen las entidades y sus propiedades.
4. **Contexto de Datos**: Configura la conexión a la base de datos y define los DbSets.

### Modelos

 **Usuario**: Representa un usuario en la base de datos siendo la entidad donde se realizan las operaciones CRUD.

 ## Endpoints

- **GET /api/Crud/READ**: Obtiene usuarios por una lista de IDs.
- **POST /api/Crud/CREATE**: Crea un nuevo usuario.
- **PUT /api/Crud/UPDATE**: Actualiza un usuario existente.
- **DELETE /api/Crud/DELETE**: Elimina un usuario por ID.

## Patrones de Diseño Utilizados

- **Patrón Repositorio**: Para la abstracción de la lógica de acceso a datos.
- **Inyección de Dependencias**: Para la gestión de dependencias y la configuración de servicios.
- **Separación de Responsabilidades**: Dividiendo la lógica en controladores, repositorios y modelos.

## Base de Datos

La base de datos utilizada es MySQL, configurada en el contexto de datos `AppDbContext` y conectada mediante una cadena de conexión especificada en el archivo de configuración.
