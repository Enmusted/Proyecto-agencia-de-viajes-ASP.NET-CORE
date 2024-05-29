USE master; 
DROP DATABASE AgenciaV;
CREATE DATABASE AgenciaV;
USE AgenciaV;

CREATE TABLE Estado(
Estado varchar (13) Primary key
);

CREATE TABLE Usuario(
DNI varchar(8) PRIMARY KEY not null,
correo varchar(50) not null,
contraseña varchar(50) not null,
Nombres varchar(50) not null,
Apellidos varchar(50) not null,
Fecha_nacimiento date not null,
numero varchar(9),
Estado varchar (13)
FOREIGN KEY (Estado) REFERENCES Estado(Estado)

);

CREATE TABLE Vendedor(
Usuario varchar(30) not null PRIMARY KEY,
Nombres varchar(50) not null,
Apellidos varchar(50) not null,
contraseña varchar(50) not null,
Estado varchar (13)
FOREIGN KEY (Estado) REFERENCES Estado(Estado)
);

CREATE TABLE Tour(
ID_Producto INT IDENTITY(1,1) PRIMARY KEY,
Precio decimal not null,
Descripción varchar(250) not null,
Nombre_Tour varchar (50) not null,
Destinos varchar (50) not null,
Pais varchar(50) not null,
Fecha_inicio datetime not null,
Fecha_fin datetime not null,
DatosImagen VARBINARY(MAX),
Capacidad INT not null,
Estado varchar (13),
FOREIGN KEY (Estado) REFERENCES Estado(Estado),
);

Create table Aeropuert (
Id_aeropuerto INT IDENTITY(1,1) PRIMARY KEY,
Aeropuerto varchar(250) not null,
Provincia varchar(50) not null,
Pais varchar(50) not null,
Estado varchar (13)
FOREIGN KEY (Estado) REFERENCES Estado(Estado),
)

CREATE TABLE Aerolinea (
ID_Aerolinea INT IDENTITY(1,1) PRIMARY KEY,
Aerolinea varchar(50) not null,
Estado varchar (13)
FOREIGN KEY (Estado) REFERENCES Estado(Estado)
)

CREATE TABLE Vuelo(
ID_vuelo INT IDENTITY(1,1) PRIMARY KEY,
ID_Aerolinea int,
fecha_inicio datetime not null,
fecha_final datetime not null,
Id_aeropuerto_origen INT,
Id_aeropuerto_destino INT,
precio_v DECIMAL,
Estado varchar (13),
FOREIGN KEY (Estado) REFERENCES Estado(Estado),
FOREIGN KEY (Id_aeropuerto_origen) REFERENCES Aeropuert(Id_aeropuerto),
FOREIGN KEY (Id_aeropuerto_destino) REFERENCES Aeropuert(Id_aeropuerto),
FOREIGN KEY (ID_Aerolinea) REFERENCES Aerolinea(ID_Aerolinea),
);

CREATE TABLE ASIENTO(
ID_asiento INT IDENTITY(1,1) PRIMARY KEY,
Num_Asiento varchar(6),
Clase varchar(50),
disponibilidad  varchar(50),
precio_v DECIMAL,
ID_vuelo INT,
FOREIGN KEY (ID_vuelo) REFERENCES Vuelo(ID_vuelo)  
)

CREATE TABLE RegistroVuelo (
ID_Rvuelo INT IDENTITY(1,1) PRIMARY KEY,
Precio_t decimal,
ID_vuelo INT,
ID_asiento INT,
DNI varchar(8),
Estado varchar (13)
FOREIGN KEY (Estado) REFERENCES Estado(Estado),
FOREIGN KEY (ID_asiento) REFERENCES ASIENTO(ID_asiento),
FOREIGN KEY (ID_vuelo) REFERENCES Vuelo(ID_vuelo),
FOREIGN KEY (DNI) REFERENCES Usuario(DNI)
)

CREATE TABLE Hotel(
ID_Hotel INT IDENTITY(1,1) PRIMARY KEY,
Nombre varchar(50) not null,
Provincia varchar(50) not null,
Direccion varchar(150) not null,
Estado varchar (13),
FOREIGN KEY (Estado) REFERENCES Estado(Estado),

)

CREATE TABLE THabitacion(
ID_ProductoTH INT IDENTITY(1,1) PRIMARY KEY,
ID_Hotel INT,
TipoHabitacion varchar(15) not null,
Habitacion varchar(4) not null,
Precio decimal not null,
Cantidad_Personas int not null,
disponibilidad varchar (50),
FOREIGN KEY (ID_Hotel) REFERENCES Hotel(ID_Hotel)
)

CREATE TABLE ReservaHotel(
ID_PRH INT IDENTITY(1,1) PRIMARY KEY,
DNI varchar(8),
ID_Hotel INT, 
ID_ProductoTH INT,
Fecha_entrada datetime not null,
Fecha_salida datetime not null,
Total decimal not null,
Estado varchar (13)
FOREIGN KEY (DNI) REFERENCES Usuario(DNI),
FOREIGN KEY (Estado) REFERENCES Estado(Estado),
FOREIGN KEY (ID_Hotel) REFERENCES Hotel(ID_Hotel),
FOREIGN KEY (ID_ProductoTH) REFERENCES THabitacion(ID_ProductoTH)
);

CREATE TABLE Detallecompra (
Numero_venta INT IDENTITY(1,1) PRIMARY KEY,
DNI varchar(8), 
ID_PRH int,
ID_Rvuelo int,
ID_Producto int,
tarjeta_credito varchar(16) not null,
T_vencimiento varchar(5)not null,
CVV varchar(3) not null,
Fecha_compra datetime, 
total decimal,
FOREIGN KEY (DNI) REFERENCES Usuario(DNI),
FOREIGN KEY (ID_PRH) REFERENCES ReservaHotel(ID_PRH),
FOREIGN KEY (ID_Rvuelo) REFERENCES RegistroVuelo(ID_Rvuelo),
FOREIGN KEY (ID_Producto) REFERENCES Tour(ID_Producto)
);

CREATE TABLE Credenciales (
    Usuario varchar(50) not null,
    Contraseña varchar(255) not null,
	rol varchar(12) not null
    
);


INSERT INTO Credenciales (Usuario, Contraseña, rol) VALUES ('omar', '12345', 'adminitrador');

INSERT INTO Estado(Estado) VALUES ('HABILITADO');
INSERT INTO Estado(Estado) VALUES ('DESHABILITADO');


INSERT INTO Aeropuert (Aeropuerto, Provincia, Pais, Estado) VALUES 
    ('Aeropuerto Internacional Jorge Chávez', 'Lima', 'Perú', 'HABILITADO'),
    ('Aeropuerto Internacional de São Paulo-Guarulhos', 'São Paulo', 'Brasil', 'HABILITADO'),
    ('Aeropuerto Internacional José Martí', 'La Habana', 'Cuba', 'HABILITADO'),
    ('Aeropuerto Internacional Juan Santamaría', 'San José', 'Costa Rica', 'HABILITADO'),
    ('Aeropuerto Internacional Tocumen', 'Panamá', 'Panamá', 'HABILITADO'),
    ('Aeropuerto Internacional Ministro Pistarini', 'Buenos Aires', 'Argentina', 'HABILITADO'),
    ('Aeropuerto Internacional Simón Bolívar', 'Maiquetía', 'Venezuela', 'HABILITADO'),
    ('Aeropuerto Internacional Augusto C. Sandino', 'Managua', 'Nicaragua', 'HABILITADO'),
    ('Aeropuerto Internacional Benito Juárez', 'Ciudad de México', 'México', 'HABILITADO'),
    ('Aeropuerto Internacional Alfonso Bonilla Aragón', 'Cali', 'Colombia', 'HABILITADO'),
    ('Aeropuerto Internacional Silvio Pettirossi', 'Asunción', 'Paraguay', 'HABILITADO'),
    ('Aeropuerto Internacional El Salvador', 'San Salvador', 'El Salvador', 'HABILITADO'),
    ('Aeropuerto Internacional Juan Gualberto Gómez', 'Varadero', 'Cuba', 'HABILITADO'),
    ('Aeropuerto Internacional Rafael Núñez', 'Cartagena', 'Colombia', 'HABILITADO'),
    ('Aeropuerto Internacional de Cancún', 'Quintana Roo', 'México', 'HABILITADO'),
    ('Aeropuerto Internacional Arturo Merino Benítez', 'Santiago', 'Chile', 'HABILITADO'),
    ('Aeropuerto Internacional Jorge Wilstermann', 'Cochabamba', 'Bolivia', 'HABILITADO'),
    ('Aeropuerto Internacional Toncontín', 'Tegucigalpa', 'Honduras', 'HABILITADO'),
    ('Aeropuerto Internacional de Quito', 'Pichincha', 'Ecuador', 'HABILITADO'),
    ('Aeropuerto Internacional Matecaña', 'Pereira', 'Colombia', 'HABILITADO'),
    ('Aeropuerto Internacional José María Córdova', 'Medellín', 'Colombia', 'HABILITADO'),
    ('Aeropuerto Internacional Jorge Newbery', 'Buenos Aires', 'Argentina', 'HABILITADO'),
    ('Aeropuerto Internacional de Tijuana', 'Baja California', 'México', 'HABILITADO'),
    ('Aeropuerto Internacional Alejandro Velasco Astete', 'Cusco', 'Perú', 'HABILITADO'),
    ('Aeropuerto Internacional de Brasília', 'Distrito Federal', 'Brasil', 'HABILITADO'),
    ('Aeropuerto Internacional de Montevideo', 'Montevideo', 'Uruguay', 'HABILITADO'),
    ('Aeropuerto Internacional Juan Santamaría', 'San José', 'Costa Rica', 'HABILITADO'),
    ('Aeropuerto Internacional de Guatemala', 'Guatemala City', 'Guatemala', 'HABILITADO'),
    ('Aeropuerto Internacional de Mendoza', 'Mendoza', 'Argentina', 'HABILITADO'),
    ('Aeropuerto Internacional de Cali', 'Cali', 'Colombia', 'HABILITADO'),
    ('Aeropuerto Internacional de Curitiba', 'Paraná', 'Brasil', 'HABILITADO'),
    ('Aeropuerto Internacional de Monterrey', 'Nuevo León', 'México', 'HABILITADO'),
    ('Aeropuerto Internacional de Pisco', 'Ica', 'Perú', 'HABILITADO'),
    ('Aeropuerto Internacional General Mariano Escobedo', 'Monterrey', 'México', 'HABILITADO'),
    ('Aeropuerto Internacional de Tegucigalpa', 'Francisco Morazán', 'Honduras', 'HABILITADO'),
    ('Aeropuerto Internacional Simón Bolívar', 'Maiquetía', 'Venezuela', 'HABILITADO')



select*FROM ASIENTO;
