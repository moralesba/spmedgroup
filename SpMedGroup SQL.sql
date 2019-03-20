use SpMedGroup

create table tipo_usuario (
	id_tipousuario int identity primary key,
	nome varchar (205) not null 
);

create table especialidade (
	id_especialidade int identity primary key,
	nome varchar (205) not null
);

create table clinica (
	id_clinica int identity primary key,
	nome varchar (205) not null,
	endereco varchar (205),
	telefone varchar (205)
);

create table usuario (
	id_usuario int identity primary key,
	nome varchar (205),
	email varchar (205),
	senha varchar (205),
	tipo_usuario int foreign key references tipo_usuario (id_tipousuario)
);

create table medico (
	id_medico int identity primary key,
	nome varchar (205),
	crm varchar (205),
	id_clinica int foreign key references clinica (id_clinica),
	id_especialidade int foreign key references especialidade (id_especialidade),
	id_usuario int foreign key references usuario (id_usuario)
);

create table prontuario (
	id_prontuario int identity primary key,
	rg_paciente char (10),
	cpf_paciente char (11),
	end_paciente varchar (205), 
	dtnasc_paciente date,
	cel_paciente char (14)
);

create table consulta (
	id_consulta int identity primary key,
	dt_consulta date,
	id_medico int foreign key references medico (id_medico),
	id_prontuario int foreign key references prontuario (id_prontuario),
	descrição varchar (205),
	situação varchar (205)
);

insert into tipo_usuario (nome)
values ('Administrador'), ('Médico'), ('Paciente');

insert into especialidade (nome)
values ('Endocrinologista'), ('Oftalmo'), ('Cardiologista'), ('Ginecologista'), ('Neurologista'), ('Obstetra'), ('Pediatra'), ('Psiquiatra');

insert into clinica (nome, endereco, telefone) 
values ('SP Medical Group', 'São Paulo', '27839947');

insert into usuario (nome, email, senha, tipo_usuario)
values  ('Mariana', 'mariana@gmail.com', '123456', 3),
		('Alexandre', 'alexandre@gmail.com', '123456', 3),
		('Fernando', 'fernando@gmail.com', '123456', 3),
		('Henrique', 'henrique @gmail.com', '123456', 3),
		('Helena Strada', 'helena@gmail.com', '123456', 2),
		('Roberto Possarle', 'roberto@gmail.com', '123456', 2),
		('Ricardo Lemos', 'ricardo@gmail.com', '123456', 2);

insert into medico (nome, crm, id_clinica, id_especialidade, id_usuario)
values  ('Helena Strada', '78526', 1, 3, 5),
		('Roberto Possarle', '81527', 1, 7, 6),
		('Ricardo Lemos', '87625', 1, 1, 7);

insert into prontuario (rg_paciente, cpf_paciente, end_paciente, dtnasc_paciente, cel_paciente)
values  ('389278498', '67283927398', 'Rua Goias, 67', '08/10/1991', '(11) 982382673'),
		('482948289', '72873872809', 'Rua Alagoas, 990', '23/06/1970', '(11) 981039827'),
		('301937840', '84762791710', 'Rua Piaui, 420', '15/01/1983', '(11) 982967726'),
		('326478291', '54725497387', 'Rua Pietro, 52', '20/12/1967', '(11) 998276473');

insert into consulta (dt_consulta, id_medico, id_prontuario, descrição, situação)
values	 ('07/02/2019 11:00', 3, 3, '-' ,'Realizada'),
		 ('20/01/2019 15:00', 2, 1, '-' ,'Realizada'),
		 ('06/01/2018 10:00', 2, 2, '-' ,'Cancelada'),
		 ('07/02/2019 11:00', 4, 3, '-' ,'Realizada'),
		 ('08/02/2019 15:00', 2, 1, '-' ,'Agendada'),
		 ('09/02/2019 11:00', 4, 4, '-' ,'Cancelada'),
		 ('06/01/2018 10:00', 3, 2, '-' ,'Agendada');

select * from consulta
select * from medico
select * from usuario;
select * from tipo_usuario;

select * from usuario inner join tipo_usuario
on usuario.tipo_usuario = tipo_usuario.id_tipousuario

select m.nome, t.nome from medico m inner join usuario u
on m.id_usuario = u.id

alter table clinica add Endereço varchar(50), Telefone char(14);
alter table medico add id_usuario int foreign key references usuario(id_usuario)
delete from prontuario where id_prontuario > '11'
update medico set id_usuario = 9 where id_medico = 1
update medico set id_usuario = 10 where id_medico = 2
update medico set id_usuario = 11 where id_medico = 3
update clinica set endereco = 'São Paulo' where id = 2
update clinica set nome = 'SP Medical Group' where id = 2
update clinica set telefone = '27839947' where id = 2

select count(*) from usuario

select * from clinica
select * from usuario 
select * from tipo_usuario
select * from medico
select * from especialidade 
select * from consulta
select * from prontuario

insert into usuario (nome, email, senha, tipo_usuario)
values ('Admnistrador', 'adm@gmail.com', 'adm123456', 1);

insert into usuario (nome, email, senha, tipo_usuario)
values ('Guilherme Moreno', 'guilherme@gmail.com', '123456', 2);

insert into medico (nome, crm, id_clinica, id_especialidade, id_usuario)
values ('Guilherme Moreno', '98374', 1, 2, 13);

update consulta set situação = 'Realizada' where id_consulta = 8;
update prontuario set dtnasc_paciente = CAST('10-08-1991' as datetime) where id_prontuario = 7
update prontuario set dtnasc_paciente = '06/03/1970' where id_prontuario = 8;
update prontuario set dtnasc_paciente = '15/01/1983' where id_prontuario = 9;
update prontuario set dtnasc_paciente = '20/12/1967' where id_prontuario = 10
update prontuario set dtnasc_paciente = '10/08/1991' where id_prontuario = 11
update consulta set descrição = 'Paciente encaminhado para outro especialista' where id_consulta = 3