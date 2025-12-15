
create database GestionProductosBD;
GO

use GestionProductosBD;
go


create table usuarios (
    id int identity(1,1) primary key,
    nombre varchar(50) not null,
    apellido varchar(50) not null,
    usuario varchar(30) not null unique,
    password varchar(100) not null,
    email varchar(50) not null unique,
    telefono varchar (10) not null,
    fecha datetime not null default getdate()

);


create table proveedores (
    id int identity(1,1) primary key,
    proveedor varchar(50) not null unique
);


create table productos (
    id int identity(1,1) primary key,
    nombre varchar(50) not null,
    existencia int not null,
    estado varchar(8) not null,
    id_proveedor int not null ,

    constraint estado_producto
    check (estado in ('activo', 'inactivo')),

    constraint fk_productos_proveedores
    foreign key (id_proveedor)
    references proveedores(id)
);


create table opciones (
    id int identity(1,1) primary key,
    opcion varchar(30) not null,
    estado varchar(8) not null,
    id_producto int not null,

    constraint estado_opciones
    check (estado in ('activo', 'inactivo')),

    constraint fk_opciones_productos
    foreign key (id_producto)
    references productos(id)
    on delete cascade
);




--Datos de prueba. 

insert into usuarios (nombre, apellido, usuario, password, email, telefono)
values
('admin', 'sistema', 'admin', 'admin', 'admin@empresa.com', '88880000'),
('elon', 'musk', 'emusk', 'clave123', 'elon@gmail.com', '88880001'),
('mark', 'zuckerberg', 'mzuck', 'clave123', 'mark@gmail.com', '88880002'),
('bill', 'gates', 'bgates', 'clave123', 'bill@gmail.com', '88880003'),
('jeff', 'bezos', 'jbezos', 'clave123', 'jeff@gmail.com', '88880004'),
('steve', 'jobs', 'sjobs', 'clave123', 'steve@gmail.com', '88880005'),
('lionel', 'messi', 'lmessi', 'clave123', 'messi@gmail.com', '88880006'),
('cristiano', 'ronaldo', 'cronaldo', 'clave123', 'ronaldo@gmail.com', '88880007'),
('serena', 'williams', 'swilliams', 'clave123', 'serena@gmail.com', '88880008'),
('taylor', 'swift', 'tswift', 'clave123', 'taylor@gmail.com', '88880009');

insert into proveedores (proveedor)
values
('proveedor1'),
('proveedor2'),
('proveedor3'),
('proveedor4'),
('proveedor5');

insert into productos (nombre, existencia, estado, id_proveedor)
values
('vaso plastico', 150, 'activo', 1),
('plato ceramico', 80, 'activo', 2),
('taza cafe', 60, 'activo', 3),
('cuchara metal', 300, 'activo', 1),
('tenedor acero', 250, 'activo', 1),
('cuchillo cocina', 120, 'inactivo', 2),
('botella agua', 90, 'activo', 4),
('vaso termico', 40, 'activo', 3),
('servilleta papel', 500, 'activo', 5),
('envase plastico', 200, 'activo', 4);

insert into opciones (opcion, estado, id_producto)
values
('grande', 'activo', 1),
('mediano', 'activo', 1),
('pequeno', 'activo', 1),
('hondo', 'activo', 2),
('llano', 'activo', 2),
('ceramica', 'activo', 3),
('vidrio', 'activo', 3),
('500ml', 'activo', 7),
('1 litro', 'activo', 7),
('con tapa', 'activo', 8);

--Ver tabla de producto con opciones y sin opciones
select 
    p.id,
    p.nombre as producto,
    p.existencia,
    p.estado,
    o.opcion
from productos p
left join opciones o
    on p.id = o.id_producto;
