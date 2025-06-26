using app.Enums;
using app.Models;

namespace app.Data
{
    public static class SeedData
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if (!context.Usuarios.Any())
            {
                var senhaDemo = BCrypt.Net.BCrypt.HashPassword("senha123");

                // Criação de usuários de exemplo
                var doadorPf = new UsuarioDoadorPf
                {
                    Id = 1,
                    Nome = "João da Silva",
                    Telefone = "31987654321",
                    Rua = "Rua das Flores",
                    Numero = 123,
                    Complemento = "Apto 1",
                    Bairro = "Bairro A",
                    Cidade = "Belo Horizonte",
                    Estado = Estado.MG,
                    Email = "doadorpf@demo.com",
                    Senha = senhaDemo,
                    Perfil = PerfilDeUsuario.Doador,
                    Cpf = "11122233344",
                    DataNascimento = new DateTime(1985, 5, 10)
                };
                context.Usuarios.Add(doadorPf);

                var doadorPj = new UsuarioDoadorPj
                {
                    Id = 2,
                    Nome = "Mercado Bom Preço",
                    Telefone = "31912345678",
                    Rua = "Avenida Brasil",
                    Numero = 456,
                    Complemento = "Sala 2",
                    Bairro = "Industrial",
                    Cidade = "Contagem",
                    Estado = Estado.MG,
                    Email = "doadorpj@demo.com",
                    Senha = senhaDemo,
                    Perfil = PerfilDeUsuario.Doador,
                    Cnpj = "12345678000195",
                    RazaoSocial = "Mercado Bom Preço LTDA"
                };
                context.Usuarios.Add(doadorPj);

                var ong = new UsuarioOng
                {
                    Id = 3,
                    Nome = "ONG Mãos Amigas",
                    Telefone = "31945678901",
                    Rua = "Rua da Solidariedade",
                    Numero = 789,
                    Complemento = "",
                    Bairro = "Centro",
                    Cidade = "Belo Horizonte",
                    Estado = Estado.MG,
                    Email = "ong@demo.com",
                    Senha = senhaDemo,
                    Perfil = PerfilDeUsuario.ONG,
                    Cnpj = "98765432000176",
                    RazaoSocial = "Associacao Mãos Amigas"
                };
                context.Usuarios.Add(ong);
            }

            if (!context.OfertasDoacao.Where(o => o.Status == Status.Pendente).Any())
            {
                // Criação de ofertas de doação de exemplo
                var oferta1 = new OfertaDoacao
                {
                    Id = 1,
                    IdUsuarioDoador = 1, 
                    DataCadastro = DateTime.Now.AddDays(-3),
                    Status = Status.Pendente,
                    IsCestaCompleta = true,
                    QtdeCestas = 2,
                    TipoTransporte = TipoTransporte.Entrega,
                    OpcoesAgendamento = new List<OpcaoAgendamentoOferta>
                    {
                        new OpcaoAgendamentoOferta { DiaDaSemana = DiaDaSemana.Segunda, HorarioInicio = new TimeSpan(18, 00, 00), HorarioFinal = new TimeSpan(20, 00, 00) },
                        new OpcaoAgendamentoOferta { DiaDaSemana = DiaDaSemana.Quarta, HorarioInicio = new TimeSpan(14, 00, 00), HorarioFinal = new TimeSpan(16, 00, 00) },
                        new OpcaoAgendamentoOferta { DiaDaSemana = DiaDaSemana.Sexta, HorarioInicio = new TimeSpan(10, 00, 00), HorarioFinal = new TimeSpan(12, 00, 00) }
                    }
                };
                context.OfertasDoacao.Add(oferta1);

                var oferta2 = new OfertaDoacao
                {
                    Id = 2,
                    IdUsuarioDoador = 1, 
                    DataCadastro = DateTime.Now.AddDays(-1),
                    Status = Status.Pendente,
                    IsCestaCompleta = false,
                    QtdeCestas = 0,
                    ItensOferta = new List<ItemOferta>
                    {
                        new ItemOferta { Alimento = AlimentoNaoPerecivel.ArrozBranco, QtdeItem = 10, MedidaItem = MedidaItem.kg },
                        new ItemOferta { Alimento = AlimentoNaoPerecivel.FeijaoCarioca, QtdeItem = 5, MedidaItem = MedidaItem.kg },
                        new ItemOferta { Alimento = AlimentoNaoPerecivel.Macarrao, QtdeItem = 2, MedidaItem = MedidaItem.kg },
                        new ItemOferta { Alimento = AlimentoNaoPerecivel.LeiteEmPo, QtdeItem = 3, MedidaItem = MedidaItem.kg },
                        new ItemOferta { Alimento = AlimentoNaoPerecivel.AcucarBranco, QtdeItem = 5, MedidaItem = MedidaItem.kg }
                    },
                    TipoTransporte = TipoTransporte.Entrega,
                    OpcoesAgendamento = new List<OpcaoAgendamentoOferta>
                    {
                        new OpcaoAgendamentoOferta { DiaDaSemana = DiaDaSemana.Segunda, HorarioInicio = new TimeSpan(18, 00, 00), HorarioFinal = new TimeSpan(20, 00, 00) },
                        new OpcaoAgendamentoOferta { DiaDaSemana = DiaDaSemana.Quarta, HorarioInicio = new TimeSpan(14, 00, 00), HorarioFinal = new TimeSpan(16, 00, 00) },
                        new OpcaoAgendamentoOferta { DiaDaSemana = DiaDaSemana.Sexta, HorarioInicio = new TimeSpan(10, 00, 00), HorarioFinal = new TimeSpan(12, 00, 00) }
                    }
                };
                context.OfertasDoacao.Add(oferta2);

                var oferta3 = new OfertaDoacao
                {
                    Id = 3,
                    IdUsuarioDoador = 2, 
                    DataCadastro = DateTime.Now.AddDays(-2),
                    Status = Status.Pendente,
                    IsCestaCompleta = true,
                    QtdeCestas = 10,
                    TipoTransporte = TipoTransporte.Retirada,
                    OpcoesAgendamento = new List<OpcaoAgendamentoOferta>
                    {
                        new OpcaoAgendamentoOferta { DiaDaSemana = DiaDaSemana.Segunda, HorarioInicio = new TimeSpan(18, 00, 00), HorarioFinal = new TimeSpan(20, 00, 00) },
                        new OpcaoAgendamentoOferta { DiaDaSemana = DiaDaSemana.Quarta, HorarioInicio = new TimeSpan(14, 00, 00), HorarioFinal = new TimeSpan(16, 00, 00) },
                        new OpcaoAgendamentoOferta { DiaDaSemana = DiaDaSemana.Sexta, HorarioInicio = new TimeSpan(10, 00, 00), HorarioFinal = new TimeSpan(12, 00, 00) }
                    }
                };
                context.OfertasDoacao.Add(oferta3);

                var oferta4 = new OfertaDoacao
                {
                    Id = 4,
                    IdUsuarioDoador = 2, 
                    DataCadastro = DateTime.Now.AddDays(-5),
                    Status = Status.Pendente,
                    IsCestaCompleta = false,
                    QtdeCestas = 0,
                    ItensOferta = new List<ItemOferta>
                    {
                        new ItemOferta { Alimento = AlimentoNaoPerecivel.ArrozBranco, QtdeItem = 10, MedidaItem = MedidaItem.kg },
                        new ItemOferta { Alimento = AlimentoNaoPerecivel.FeijaoCarioca, QtdeItem = 5, MedidaItem = MedidaItem.kg },
                        new ItemOferta { Alimento = AlimentoNaoPerecivel.Macarrao, QtdeItem = 2, MedidaItem = MedidaItem.kg },
                        new ItemOferta { Alimento = AlimentoNaoPerecivel.LeiteEmPo, QtdeItem = 3, MedidaItem = MedidaItem.kg },
                        new ItemOferta { Alimento = AlimentoNaoPerecivel.AcucarBranco, QtdeItem = 5, MedidaItem = MedidaItem.kg }
                    },
                    TipoTransporte = TipoTransporte.Retirada,
                    OpcoesAgendamento = new List<OpcaoAgendamentoOferta>
                    {
                        new OpcaoAgendamentoOferta { DiaDaSemana = DiaDaSemana.Segunda, HorarioInicio = new TimeSpan(18, 00, 00), HorarioFinal = new TimeSpan(20, 00, 00) },
                        new OpcaoAgendamentoOferta { DiaDaSemana = DiaDaSemana.Quarta, HorarioInicio = new TimeSpan(14, 00, 00), HorarioFinal = new TimeSpan(16, 00, 00) },
                        new OpcaoAgendamentoOferta { DiaDaSemana = DiaDaSemana.Sexta, HorarioInicio = new TimeSpan(10, 00, 00), HorarioFinal = new TimeSpan(12, 00, 00) }
                    }
                };
            }


            if (!context.SolicitacoesDoacao.Where(s => s.Status == Status.Pendente).Any())
            {
                // Criação de solicitações de doação de exemplo
                var solicitacao1 = new SolicitacaoDoacao
                {
                    Id = 1,
                    IdUsuarioOng = 3, 
                    DataSolicitacao = DateTime.Now.AddDays(-2),
                    Status = Status.Pendente,
                    IsCestaCompleta = true,
                    QtdeCestas = 10,
                    TipoTransporte = TipoTransporte.Entrega,
                    AgendamentosSolicitacao = new List<OpcaoAgendamentoSolicit>
                    {
                        new OpcaoAgendamentoSolicit { DiaSemana = "Terca", HorarioInicio = new TimeSpan(9, 00, 00), HorarioFim = new TimeSpan(11, 00, 00) },
                        new OpcaoAgendamentoSolicit { DiaSemana = "Quinta", HorarioInicio = new TimeSpan(13, 00, 00), HorarioFim = new TimeSpan(15, 00, 00) }
                    }
                };
                context.SolicitacoesDoacao.Add(solicitacao1);

                var solicitacao2 = new SolicitacaoDoacao
                {
                    Id = 2,
                    IdUsuarioOng = 3,
                    DataSolicitacao = DateTime.Now,
                    Status = Status.Pendente,
                    IsCestaCompleta = false,
                    QtdeCestas = 0,
                    ItensSolicitacao = new List<ItemSolicitacao>
                    {
                        new ItemSolicitacao { Alimento = AlimentoNaoPerecivel.ArrozBranco, QtdeItem = 10, MedidaItem = MedidaItem.kg },
                        new ItemSolicitacao { Alimento = AlimentoNaoPerecivel.FeijaoCarioca, QtdeItem = 5, MedidaItem = MedidaItem.kg },
                        new ItemSolicitacao { Alimento = AlimentoNaoPerecivel.Macarrao, QtdeItem = 3, MedidaItem = MedidaItem.kg },
                        new ItemSolicitacao { Alimento = AlimentoNaoPerecivel.LeiteEmPo, QtdeItem = 10, MedidaItem = MedidaItem.kg },
                        new ItemSolicitacao { Alimento = AlimentoNaoPerecivel.AcucarBranco, QtdeItem = 10, MedidaItem = MedidaItem.kg }
                    },
                    TipoTransporte = TipoTransporte.Entrega,
                    AgendamentosSolicitacao = new List<OpcaoAgendamentoSolicit>
                    {
                        new OpcaoAgendamentoSolicit { DiaSemana = "Terca", HorarioInicio = new TimeSpan(9, 00, 00), HorarioFim = new TimeSpan(11, 00, 00) },
                        new OpcaoAgendamentoSolicit { DiaSemana = "Quinta", HorarioInicio = new TimeSpan(13, 00, 00), HorarioFim = new TimeSpan(15, 00, 00) }
                    }
                };
                context.SolicitacoesDoacao.Add(solicitacao2);
            }

            context.SaveChanges();
        }
    }
}
