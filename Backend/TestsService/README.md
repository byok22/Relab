# TestsService Microservice

Este microservicio maneja la gestión de pruebas (`Tests`) y solicitudes de pruebas (`TestRequests`). Utiliza NATS como sistema de mensajería para la comunicación entre componentes.

## Tabla de Contenidos

- [Requisitos](#requisitos)
- [Configuración](#configuración)
- [Estructura del Proyecto](#estructura-del-proyecto)
- [Uso del Microservicio](#uso-del-microservicio)
  - [Mensajes Disponibles](#mensajes-disponibles)
  - [Ejemplos de Uso](#ejemplos-de-uso)
- [Logs y Manejo de Errores](#logs-y-manejo-de-errores)
- [Contribución](#contribución)
- [Licencia](#licencia)

## Requisitos

- [.NET Core 6.0 o superior](https://dotnet.microsoft.com/download)
- [NATS Server](https://nats.io/download/)

## Configuración

1. **Clonar el repositorio:**

   ```bash
   git clone https://tu-repositorio.git
   cd tu-repositorio

2. **Configurar NATS Server:**
El microservicio se comunica usando NATS. Asegúrate de tener un NATS server corriendo en tu entorno:
    nats-server

Por defecto, el microservicio se conecta al localhost:4222. Si tu NATS server está en otra dirección o puerto, asegúrate de actualizar la configuración en el código.

3. **Configurar variables de entorno (si aplica):**
    export NATS_URL="nats://localhost:4222"

## Estructura del Proyecto
    ├── Application
    │   └── UseCases
    │       ├── Tests
    │       └── TestRequests
    ├── Domain
    │   ├── Enums
    │   └── Services
    ├── Presentation
    │   ├── Interfaces
    │   ├── Messages
    │   │   ├── Test
    │   │   └── TestRequests
    │   └── Telemetry
    ├── Shared
    │   ├── Dtos
    │   └── Response
    └── TestsService


    -Application: Contiene los casos de uso del dominio.
    -Domain: Define los servicios y la lógica del dominio.
    -Presentation: Contiene los mensajes y la interfaz que se comunica con otros servicios o clientes.
    -Shared: Define los DTOs y respuestas compartidas entre diferentes capas.

## Uso del Microservicio

## Mensajes Disponibles

    El microservicio expone los siguientes mensajes a través de NATS:

- AddTest
        Tipo de request: TestDto
        Tipo de response: GenericResponse
        Añade un nuevo test.

- GetAllTest
        Tipo de request: Ninguno
        Tipo de response: List<TestDto>
        Recupera todos los tests.

    - GetAllTestByStatus
        Tipo de request: TestStatusEnum
        Tipo de response: List<TestDto>
        Recupera todos los tests filtrados por estatus.

    - PatchTest
        Tipo de request: TestDto
        Tipo de response: GenericResponse
        Actualiza un test existente.

    - AddTestRequests
        Tipo de request: TestRequestDto
        Tipo de response: GenericResponse
        Añade una nueva solicitud de prueba.

    - GetAllTestRequests
        Tipo de request: Ninguno
        Tipo de response: List<TestRequestDto>
        Recupera todas las solicitudes de prueba.

    - GetAllTestRequestsByStatus
        Tipo de request: TestRequestsRequest
        Tipo de response: List<TestRequestDto>
        Recupera todas las solicitudes de prueba filtradas por estatus.

## Ejemplos de Uso
    Publicar un mensaje

    Para publicar un mensaje en NATS, usa el método Publish del servicio IMsgService.

    var connection = _msgService.GetConnection();
    _msgService.Publish("AddTest", message);

## Suscribirse a un mensaje

    Para suscribirse a un mensaje y manejar la lógica cuando se reciba un mensaje:

        await _msgService.SubscribeAsync<TestDto, GenericResponse>("AddTest", async (test) =>
        {
            return await _useCase.Execute(test);
        });

## Logs y Manejo de Errores

    Los errores se registran utilizando ILogger. Cada mensaje implementa un manejo de errores básico dentro del bloque try-catch. Asegúrate de revisar los logs para cualquier excepción o problema.
    Contribución



    Si deseas contribuir al proyecto, sigue los siguientes pasos:

        Haz un fork del proyecto.
        Crea una nueva rama (git checkout -b feature/nueva-funcionalidad).
        Haz commit de tus cambios (git commit -am 'Añadir nueva funcionalidad').
        Haz push a la rama (git push origin feature/nueva-funcionalidad).
        Abre un Pull Request.

## Licencia

Este proyecto está licenciado bajo la Licencia MIT. Para más detalles, consulta el archivo LICENSE.






