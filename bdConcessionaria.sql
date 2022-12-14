create database bdConcessionaria;
use bdConcessionaria;

-- drop database bdConcessionaria;

-- Criando as tabelas do banco de dados

create table tbLogin(
codUsu int primary key auto_increment,
usuario varchar(50) unique,
senha varchar(20),
tipo int
);

create table tbCliente(
codCli int primary key auto_increment,
nomeCli varchar(100),
emailCli varchar(100)
);

create table tbCategoria(
codCat int primary key auto_increment,
nomeCat varchar(100)
);

create table tbProduto(
codProd int primary key auto_increment,
nomeProd varchar(100),
imagemProd varchar(500),
quantidadeProd int,
valorProd varchar(20),
descricacaoProd varchar(300),
codCat int,
foreign key (codCat) references tbCategoria(codCat)
);


-- --------------------------------------------------------------------------------
-- -----------------------    A T E N Ç Ã O ! ! !    ------------------------------
-- ---------  INSERÇÕES E ALTERAÇÕES PARA MELHORIAS E TESTES DE BACK-END  ---------
-- --------------------------------------------------------------------------------

alter table tbProduto rename column descricacaoProd to descricaoProd;

drop view vw_veiculos;

drop view if exists vw_veiculos;
create view vw_veiculos as
select	t1.codProd,
		t1.nomeProd,
        t1.imagemProd,
        t1.quantidadeProd,
        t1.valorProd,
        t1.descricaoProd,
        t1.codCat,
        t2.categoria
from tbProduto t1
inner join tbCategoria t2 on t1.codCat = t2.codCat;

insert into tbCategoria (codCat, nomeCat) values (default, 'Veículo Híbrido');
insert into tbCategoria (codCat, nomeCat) values (default, 'Veículo Elétrico');
insert into tbCategoria (codCat, nomeCat) values (default, 'Motocicleta Híbrida');
insert into tbCategoria (codCat, nomeCat) values (default, 'Motocicleta Elétrica');

insert into tbProduto (codProd, nomeProd, imagemProd, quantidadeProd, valorProd, descricaoProd, codCat) values (default, 'Toyota Corolla Altis 2.0', '123456789.jpg', 3, '120000', '2022/2022 Completo', 1); 
insert into tbProduto (codProd, nomeProd, imagemProd, quantidadeProd, valorProd, descricaoProd, codCat) values (default, 'Tesla Model S Plaid', '123456789.jpg', 1, '420000', '2021/2020 Completo', 2); 
insert into tbProduto (codProd, nomeProd, imagemProd, quantidadeProd, valorProd, descricaoProd, codCat) values (default, 'Kawazaki Ninja', '123456789.jpg', 4, '110000', '2022/2022 Completo', 3); 
insert into tbProduto (codProd, nomeProd, imagemProd, quantidadeProd, valorProd, descricaoProd, codCat) values (default, 'Voltz', '123456789.jpg', 6, '180000', '2022/2022 Completo', 4); 

select * from vw_veiculos where codCat <= 2;
select * from vw_veiculos where codCat >= 3;

insert into tbLogin values (default, 'admin', '123456', 2);

select * from tbLogin;
select * from tbCategoria;
select * from tbProduto;
select * from tbCliente;

alter table tbCategoria rename column nomeCat to categoria;

alter table tbLogin add nomeCli varchar(100);
alter table tbLogin add emailCli varchar(100);

alter table tbLogin modify column usuario varchar(50);

delete from tbLogin where codUsu > 0;

insert into tbLogin values (default, 'adm', '123456', 2, 'administrador', 'adm@adm.com');

create table tbCarrinho(
codCarrinho int primary key auto_increment,
codUsu int references tbLogin(codUsu),
codProduto int references tbProduto(codProd)
);

create table tbVenda(
codVenda int primary key auto_increment,
codCli int references tbCliente(codCli),
datavenda varchar(10),
valorFinal varchar(50)
);

alter table tbCarrinho rename column codProduto to codProd;
alter table tbVenda rename column codProduto to codProd;

alter table tbVenda rename column codCli to codUsu;

delete from tbProduto where codProd > 0; 

create table itemVenda(
codItemVenda int primary key auto_increment,
codVenda int references tbVenda(codVenda),
codProd  int references tbProduto(codProd),
qtdeVendas int,
valorParcial varchar(50)
);

select * from itemVenda;
select * from tbVenda;

delete from tbLogin where tipo = 1;